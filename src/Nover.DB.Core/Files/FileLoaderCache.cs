using Nover.DB.Core.Xml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nover.DB.Core
{
    /// <summary>
    /// 文件加载缓存
    /// </summary>
    public static class FileLoaderCache
    {
        static ConcurrentDictionary<string, object> s_ConfigFileCache = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 获取文件加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileLazyLoader<T> GetFileLoader<T>(string filePath) where T : class, new()
        {
            if (s_ConfigFileCache.TryGetValue(filePath, out var configFile))
            {
                return (FileLazyLoader<T>)configFile;
            }
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            var dir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            configFile = new FileLazyLoader<T>(fullPath, XmlHelper.XmlDeserializeFromFile<T>, () => new T());
            s_ConfigFileCache.TryAdd(filePath, configFile);
            return (FileLazyLoader<T>)configFile;
        }

        /// <summary>
        /// 尝试删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="fileLazyLoader"></param>
        /// <returns></returns>
        public static bool TryRemove<T>(string filePath, out FileLazyLoader<T> fileLazyLoader) where T : class, new()
        {
            if (s_ConfigFileCache.TryGetValue(filePath, out object obj))
            {
                s_ConfigFileCache.TryRemove(filePath, out object removedOjb);
                fileLazyLoader = (FileLazyLoader<T>)obj;
                return true;
            }
            fileLazyLoader = default;
            return false;
        }
    }
}
