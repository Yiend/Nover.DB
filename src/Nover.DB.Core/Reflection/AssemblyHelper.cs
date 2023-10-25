using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Nover.DB.Core
{
    /// <summary>
    /// 程序帮助类
    /// </summary>
    internal static class AssemblyHelper
    {
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static List<Assembly> LoadAssemblies(string folderPath, SearchOption searchOption)
        {
            return GetAssemblyFiles(folderPath, searchOption)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .ToList();
        }

        /// <summary>
        /// 获取程序集文件
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetAssemblyFiles(string folderPath, SearchOption searchOption, bool isIncludeExe = false)
        {
            var files = Directory.EnumerateFiles(folderPath, "*.*", searchOption);
            if (isIncludeExe)
            {
                return files.Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));
            }

            return files.Where(s => s.EndsWith(".dll"));
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IReadOnlyList<Type> GetAllTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
        }

        /// <summary>
        /// 不是系统程序集
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        internal static bool NotSystemAssemblies(Assembly assembly)
        {
            // 过滤以【System】开头的程序集，加快速度
            if (assembly.FullName.StartsWith("System", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //系统的程序集忽略
            if (assembly.FullName.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //忽略CSScriptLibrary, 因为CSScriptLibrary解析依赖时会需要Mono.CSharp， 但实际运行时时是不需要Mono.CSharp
            if (assembly.FullName.StartsWith("CSScriptLibrary", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            //单测框架生成的程序集，不加载
            if (assembly.FullName.Contains(".Fakes"))
            {
                return false;
            }
            if (assembly.FullName.StartsWith("NUnit", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (assembly.FullName.StartsWith("Pipelines", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }

    }



}
