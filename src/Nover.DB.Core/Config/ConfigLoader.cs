using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nover.DB.Core
{
    /// <summary>
	/// 定义了配置文件读取器的接口。
	/// </summary>
	public interface IConfigLoader
    {
        /// <summary>
        /// 是否能处理指定的配置文件。
        /// </summary>
        /// <param name="args">加载配置文件的参数。</param>
        /// <returns></returns>
        bool CanLoad(ConfigFileLoadArgs args);

        /// <summary>
        /// 读取某个配置文件。
        /// </summary>
        /// <param name="args">加载配置文件的参数。</param>
        void LoadFile(ConfigFileLoadArgs args);

        /// <summary>
        /// 结束所有的配置读取操作，用于更新缓存变量。
        /// </summary>
        void EndLoad();
    }


    /// <summary>
    /// 配置文件加载器，用于加载所有配置（只负责读取，不负责解析内容）
    /// </summary>
    public static class ConfigLoader
    {
        /// <summary>
        /// 加载程序集配置文件
        /// </summary>
        /// <param name="loaderList"></param>
        public static void LoadAssemblyConfigFiles(params IConfigLoader[] loaderList)
        {
            // 如果没有任何的加载实例，就不操作了。
            if (loaderList == null || loaderList.Length == 0)
                return;

            // 从程序集中加载所有配置文件。
            List<ConfigFileLoadArgs> list = LoadConfigFilesFromAssembly();

            LoadCustomizeConfig(list, loaderList);
        }

        /// <summary>
        /// 加载自定义配置文件
        /// </summary>
        /// <param name="loaderList"></param>
        public static void LoadCustomizeConfigFiles(params IConfigLoader[] loaderList)
        {
            // 如果没有任何的加载实例，就不操作了。
            if (loaderList == null || loaderList.Length == 0)
                return;

            // 从项目二开目录中读取配置文件。
            List<ConfigFileLoadArgs> list = LoadConfigFilesFromCustomizeDirectory();
            if (list != null && list.Count > 0)
                LoadCustomizeConfig(list, loaderList);
        }

        private static void LoadCustomizeConfig(List<ConfigFileLoadArgs> list, params IConfigLoader[] loaderList)
        {

            // 将每个配置文件传递给所有加载器
            foreach (ConfigFileLoadArgs args in list)

                foreach (IConfigLoader loader in loaderList)
                {

                    if (loader.CanLoad(args))
                    {
                        // 交给加载器处理（由加载器加载配置文件）
                        loader.LoadFile(args);

                        // 配置文件已处理，就不再继续循环。
                        break;
                    }
                }


            // 通知各加载器，所有操作已完成。
            foreach (IConfigLoader loader in loaderList)
                loader.EndLoad();
        }

        /// <summary>
        /// 从程序集中加载所有配置文件。
        /// </summary>
        /// <returns></returns>
        private static List<ConfigFileLoadArgs> LoadConfigFilesFromAssembly()
        {
            List<ConfigFileLoadArgs> list = new List<ConfigFileLoadArgs>();
            var assemblies = GetAssemblyList<IncludeConfigFileAttribute>();

            var dbType_MySQL = "MySQL";
            foreach (Assembly asm in assemblies)
            {

                // 只针对应用 IncludeConfigFileAttribute 的程序集有效
                if (asm.GetCustomAttributes(typeof(IncludeConfigFileAttribute), false).Length == 0)
                    continue;

                foreach (string name in asm.GetManifestResourceNames())
                    // 只处理 config 扩展名的嵌入资源文件
                    if (name.EndsWith(".config", StringComparison.OrdinalIgnoreCase))
                        list.Add(new ConfigFileLoadArgs
                        {
                            Assembly = asm,
                            FileName = name,
                            FileContent = ReadAssemblyResource(asm, name),
                            DbType = name.IndexOf(string.Format("{0}{1}{0}", ".", dbType_MySQL), StringComparison.OrdinalIgnoreCase) > -1 ? "MySQL" : "SQLServer"
                        });
            }

            return list;
        }


        /// <summary>
        /// 获取带个指定修饰属性的程序集列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<Assembly> GetAssemblyList<T>() where T : Attribute
        {
            List<Assembly> list = new List<Assembly>(128);

            var assemblies = GetLoadAssemblies(true);
            foreach (Assembly assembly in assemblies)
            {

                if (assembly.GetCustomAttributes<T>().Count() == 0)
                    continue;

                list.Add(assembly);
            }

            return list;
        }

        /// <summary>
        /// 获取当前程序加载的所有程序集
        /// </summary>
        /// <param name="ignoreSystemAssembly">是否忽略以System开头和动态程序集，通常用于反射时不搜索它们。</param>
        /// <returns></returns>
        private static Assembly[] GetLoadAssemblies(bool ignoreSystemAssembly = false)
        {
            Assembly[] GetAssemblies()
            {
                var location = Assembly.GetExecutingAssembly().Location;
                var folderPath = Path.GetDirectoryName(location);
                var assemblies = AssemblyHelper.LoadAssemblies(folderPath, SearchOption.TopDirectoryOnly);
                return assemblies.Distinct().Where(AssemblyHelper.NotSystemAssemblies).ToArray();
            }


            Assembly[] assemblies = GetAssemblies();
            if (ignoreSystemAssembly == false)
                return assemblies;

            // 过滤一些反射中几乎用不到的程序集
            List<Assembly> list = new List<Assembly>(128);

            foreach (Assembly assembly in assemblies)
            {
                if (assembly.IsDynamic)    // 动态程序通常是不需要参考的
                    continue;

                // 过滤以【System】开头的程序集，加快速度
                if (assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase) ||
                    assembly.FullName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase))
                    continue;

                list.Add(assembly);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 从项目二开文件夹中读取配置文件。
        /// </summary>
        /// <returns></returns>

        private static List<ConfigFileLoadArgs> LoadConfigFilesFromCustomizeDirectory()
        {
            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XmlCommand");
            if (Directory.Exists(directoryPath) == false)
            {
                return null;
            }
            List<ConfigFileLoadArgs> list = new List<ConfigFileLoadArgs>();

            var option = new EnumerationOptions()
            {
                MatchCasing = MatchCasing.CaseInsensitive,
                RecurseSubdirectories = true
            };
            string[] files = Directory.GetFiles(directoryPath, "*.config", option);
            var dbType_MySQL = "MySQL";
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    var separator = Path.DirectorySeparatorChar;
                    var dbType = file.Replace(AppDomain.CurrentDomain.BaseDirectory, "").IndexOf(string.Format("{0}{1}{0}", separator, dbType_MySQL), StringComparison.OrdinalIgnoreCase) > -1 ? "MySQL" : "SQLServer";
                    list.Add(new ConfigFileLoadArgs
                    {
                        FileName = fileName,
                        FileContent = ReadFile(file),
                        DbType = dbType
                    });
                }
            }
            return list;
        }




        /// <summary>
        /// 根据指定的程序集和资源名称，读取资源的内容
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string ReadAssemblyResource(Assembly asm, string name)
        {
            using (Stream strem = asm.GetManifestResourceStream(name))
            {
                // 默认按UTF-8编码方式读取。
                using (StreamReader reader = new StreamReader(strem, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 根据指定的程序集和资源名称，读取资源的内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string ReadFile(string fileName)
        {
            // 默认按UTF-8编码方式读取。
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
