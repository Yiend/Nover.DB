using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nover.DB.Core
{
    /// <summary>
    /// 提供对配置文件解析的功能。
    /// </summary>
    public abstract class BaseResourceConfigLoader : IConfigLoader
    {
        /// <summary>
        /// 是否能处理指定的配置文件
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract bool CanLoad(ConfigFileLoadArgs args);

        /// <summary>
        /// 读取某个配置文件
        /// </summary>
        /// <param name="args"></param>
        public abstract void LoadFile(ConfigFileLoadArgs args);

        /// <summary>
        /// 结束所有的配置读取操作，用于更新缓存变量。
        /// </summary>
        public abstract void EndLoad();

        /// <summary>
        /// 根据数据库加载不同的XmlCommand
        /// todo MySql 后续需要根据应用数据库加载
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool XmlCommandCanLoad(string fileName)
        {
            var canLoad = fileName.EndsWith(".XmlCommand.config", StringComparison.OrdinalIgnoreCase);
            //var dbConnectionInfo = Core.Pipeline.Data.ConnectionManager.GetPubPlatformDbConnectionInfo();
            //if (DatabaseProviderName.MySql == dbConnectionInfo.ProviderName)
            //{
            //    return fileName.IndexOf("MYSQL",StringComparison.OrdinalIgnoreCase) > -1 && canLoad;
            //}
            //else if (DatabaseProviderName.MSSql == dbConnectionInfo.ProviderName) 
            //{
            //    return fileName.IndexOf("SQLSERVER", StringComparison.OrdinalIgnoreCase) > -1 && canLoad;
            //}
            return canLoad;
        }
    }
}