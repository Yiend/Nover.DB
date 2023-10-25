using Nover.DB.Core.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Volo.Abp;

namespace Nover.DB.Core
{
    /// <summary>
    /// 基于文件的懒加载对象
    /// 实现:
    /// 1.从文件延时加载对象
    /// 2.对象缓存 
    /// 3.基于文件修改时间的缓存依赖
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FileLazyLoader<T> where T : class
    {
        private readonly object _lockobject = new object();
        private readonly string _filename;
        private readonly Func<string, T> _loadFunc;
        private readonly Func<T> _getDefault;
        private DateTime _cacheTime = DateTime.MinValue;
        private T _instance = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="loadFunc">对象加载函数</param>
        /// <param name="getDefault">当文件不存在时，使用这个方法创建默认对象</param>
        public FileLazyLoader(string filename, Func<string, T> loadFunc, Func<T> getDefault = null)
        {
            Check.NotNull(filename, nameof(filename));
            Check.NotNull(loadFunc, nameof(loadFunc));

            _filename = filename;
            _loadFunc = loadFunc;
            _getDefault = getDefault ?? (() => default);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        public void DeleteFile()
        {
            if (File.Exists(_filename))
            {
                File.Delete(_filename);
                RefreshInstance(true);
            }
        }
        /// <summary>
        /// 刷新实例
        /// 一般情况,不需要手工调用这个方法
        /// </summary>
        /// <param name="force">强制刷新</param>
        public void RefreshInstance(bool force)
        {
            if (_instance is null || force || OutTime())
            {
                lock (_lockobject)
                {
                    if (_instance is null || force || OutTime())
                    {
                        Load();
                    }
                }
            }
        }

        // 判断缓存是否过期
        // 依据是文件修改时间新于缓存时间
        private bool OutTime()
        {
            return File.GetLastWriteTime(_filename) > _cacheTime;
        }

        // 从文件加载对象
        private void Load()
        {
            if (File.Exists(_filename))
                _instance = RetryHelper.Get<T>(() => _loadFunc(_filename));
            else
                _instance = _getDefault();

            _cacheTime = DateTime.Now;
        }

        /// <summary>
        /// 存储文件内容
        /// </summary>
        /// <param name="saveFunc"></param>
        public void Save(Action<T, string> saveFunc)
        {
            RetryHelper.Execute(() => saveFunc(_instance, _filename));
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            RetryHelper.Execute(() => XmlHelper.XmlSerializeToFile(_instance, _filename));
        }


        /// <summary>
        /// 读取对象最新实例
        /// </summary>
        public T Instance
        {
            get
            {
                RefreshInstance(false);
                return _instance;
            }
        }

        /// <summary>
        /// 设置最新实例
        /// </summary>
        /// <param name="instance"></param>
        public void SetInstance(T instance)
        {
            _instance = instance;
        }
    }
}