using System;
using System.Collections.Concurrent;

namespace Nover.DB.Data
{
    /// <summary>
    /// 管理数据库的连接信息。
    /// 即使不调用，也会自动调用Init()方法一次。
    /// </summary>
    public static class ConnectionManager
    {
        private static ConcurrentDictionary<string, ConnectionInfo> s_dict = new ConcurrentDictionary<string, ConnectionInfo>();


        /// <summary>
        /// 直接注册数据库连接
        /// </summary>
        /// <param name="setting"></param>
        public static void RegisterConnection(string name, ConnectionInfo setting)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            if (string.IsNullOrEmpty(setting.ProviderName))
                throw new ArgumentNullException("setting.ProviderName");

            if (string.IsNullOrEmpty(setting.ConnectionString))
                throw new ArgumentNullException("setting.ConnectionString");


            if (s_dict.TryAdd(name, setting) == false)
                throw new InvalidOperationException("不允许注册同名的数据库连接信息：" + name);
        }

        /// <summary>
        /// 根据名称获取对应的连接信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ConnectionInfo GetConnection(string name)
        {
            if (s_dict.TryGetValue(name, out ConnectionInfo connectionInfo))
            {
                return connectionInfo;
            }
            throw new ArgumentOutOfRangeException("指定的连接名称没有注册：" + name);
        }


    }
}
