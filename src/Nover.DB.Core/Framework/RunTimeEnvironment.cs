using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;


namespace Nover.DB.Core.Framework
{
    /// <summary>
    /// 当前应用程序的运行时环境
    /// </summary>
    public static class RunTimeEnvironment
    {
        /// <summary>
        /// 当前运行的程序是不是ASP.NET程序
        /// </summary>

        /// <summary>
        /// 忽略的文件
        /// </summary>
        private static readonly List<string> s_ignoreFiles = new List<string>() { "apidsp", "hasp", "ht", "libSkiaSharp.dll" };


        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns></returns>
        private static Assembly[] GetLoadAssemblies()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var folderPath = Path.GetDirectoryName(location);
            var assemblies = LoadAssemblies(folderPath, SearchOption.TopDirectoryOnly);

            return assemblies.Distinct().ToArray();
        }

        /// <summary>
        /// 获取Assembly集，根据路径扫描路径下所有dll和exe
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        private static List<Assembly> LoadAssemblies(string folderPath, SearchOption searchOption)
        {
            return GetAssemblyFiles(folderPath, searchOption)
                .Select(LoadFromAssemblyPath).Where(item => item != null)
                .ToList();
        }

        /// <summary>
        /// 获取目录下dll、exe结尾的文件列表,同时做简单过滤
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        private static IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(folderPath, "*.*", searchOption)
                            .Where(t =>
                            {
                                if (t.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                                    return true;
                                //dll过滤
                                if (t.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                                {
                                    var fileName = Path.GetFileName(t);
                                    var isIgnore = s_ignoreFiles.Any(name => fileName.StartsWith(name, StringComparison.OrdinalIgnoreCase));
                                    //不在忽略列表中，或者忽略列表为空 就需要
                                    if (!isIgnore)
                                        return true;
                                }
                                //非dll非exe的不要
                                return false;
                            });
        }

        /// <summary>
        /// 根据单个路径加载并返回Assembly
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        private static Assembly LoadFromAssemblyPath(string assemblyPath)
        {
            try
            {
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            }
            catch (BadImageFormatException)
            {
                //会使用一些非托管的程序集，加载会抛出异常。
                return null;
            }
        }

        /// <summary>
        /// 获取当前程序加载的所有程序集
        /// </summary>
        /// <param name="ignoreSystemAssembly">是否忽略以System开头和动态程序集，通常用于反射时不搜索它们。</param>
        /// <returns></returns>
        public static Assembly[] GetLoadAssemblies(bool ignoreSystemAssembly = false)
        {
            Assembly[] assemblies = GetLoadAssemblies();
            if (ignoreSystemAssembly == false)
                return assemblies;

            // 过滤一些反射中几乎用不到的程序集
            List<Assembly> list = new List<Assembly>(128);

            foreach (Assembly assembly in assemblies)
            {
                if (assembly.IsDynamic)    // 动态程序通常是不需要参考的
                    continue;

                // 过滤以【System】开头的程序集，加快速度
                if (assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase))
                    continue;

                list.Add(assembly);
            }
            return list.ToArray();
        }
    }
}
