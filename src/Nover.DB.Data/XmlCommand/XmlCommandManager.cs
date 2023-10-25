using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Nover.DB.Core;
using Nover.DB.Core.Xml;
using Nover.DB.Data.Context;

namespace Nover.DB.Data
{
    /// <summary>
    /// 用于维护配置文件中数据库访问命令的管理类
    /// </summary>
    public static class XmlCommandManager
    {
        private static Exception s_ExceptionOnLoad = null;

        private static Dictionary<string, XmlCommandItem> s_dict = null;
        private static Dictionary<DatabaseKind, Dictionary<string, XmlCommandItem>> s_dictXml = new Dictionary<DatabaseKind, Dictionary<string, XmlCommandItem>>();

        /// <summary>
        /// 从指定的Xml字符串加载xml命令。默认加载SqlServer
        /// </summary>
        /// <param name="xml">xml字符串。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadXml(string xml) => LoadXml(DatabaseKind.SqlServer, xml);

        /// <summary>
        /// 从指定的Xml字符串加载xml命令。
        /// </summary>
        /// <param name="dbType">数据库类型。固定值：MySql，SqlServer</param>
        /// <param name="xml">xml字符串。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadXml(string dbType, string xml)
        {
            switch (dbType)
            {

                case "MySQL":
                    LoadXml(DatabaseKind.MySql, xml);
                    break;
                case "SQLServer":
                    LoadXml(DatabaseKind.SqlServer, xml);
                    break;
            }

        }

        /// <summary>
        /// 从指定的Xml字符串加载xml命令。
        /// </summary>
        /// <param name="dbType">数据库类型枚举</param>
        /// <param name="xml">xml字符串。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadXml(DatabaseKind dbType, string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentNullException("xml");
            }

            List<XmlCommandItem> list = XmlExtensions.FromXml<List<XmlCommandItem>>(xml);

            list.ForEach((p) =>
            {
                if (s_dictXml.TryGetValue(dbType, out Dictionary<string, XmlCommandItem> itemDic))
                {
                    if (itemDic.ContainsKey(p.CommandName) == false)
                    {
                        itemDic.AddValue(p.CommandName, p);
                    }
                }
                else
                {
                    var newItem = new Dictionary<string, XmlCommandItem>();
                    newItem.AddValue(p.CommandName, p);
                    s_dictXml.AddValue(dbType, newItem);
                }
            });
        }

        /// <summary>
        /// 从定制的xmlcommand路径中装载xmlcommand，存在则更新，不存在则新增。
        /// </summary>
        /// <param name="xmlContent">xml字符串。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadCustomizeXml(string xmlContent) => LoadCustomizeXml(DatabaseKind.SqlServer, xmlContent);

        /// <summary>
        /// 从定制的xmlcommand路径中装载xmlcommand，存在则更新，不存在则新增。
        /// </summary>
        /// <param name="dbType">数据库类型。固定值：MySql，SqlServer</param>
        /// <param name="xmlContent">xml字符串。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadCustomizeXml(string dbType, string xmlContent)
        {
            switch (dbType)
            {

                case "MySql":
                    LoadCustomizeXml(DatabaseKind.MySql, xmlContent);
                    break;
                case "SqlServer":
                    LoadCustomizeXml(DatabaseKind.SqlServer, xmlContent);
                    break;
            }
        }

        /// <summary>
        /// 从定制的xmlcommand路径中装载xmlcommand，存在则更新，不存在则新增。
        /// </summary>
        /// <param name="dbType">数据库类型枚举</param>
        /// <param name="xmlContent">xml字符串。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadCustomizeXml(DatabaseKind dbType, string xmlContent)
        {
            List<XmlCommandItem> list = XmlExtensions.FromXml<List<XmlCommandItem>>(xmlContent);

            list.ForEach(x =>
            {
                if (s_dictXml.TryGetValue(dbType, out Dictionary<string, XmlCommandItem> item))
                {
                    item.UpdateValue(x.CommandName, x);
                }
                else
                {
                    var newItem = new Dictionary<string, XmlCommandItem>();
                    newItem.AddValue(x.CommandName, x);
                    s_dictXml.AddValue(dbType, newItem);
                }
            });
        }

        /// <summary>
        /// <para>从指定的目录中加载全部的用于数据访问命令。</para>
        /// <para>说明：1. 这个方法只需要在程序初始化调用一次就够了。</para>
        /// <para>       2. 如果是一个ASP.NET程序，CommandManager还会负责监视此目录，如果文件有更新，会自动重新加载。</para>
        /// </summary>
        /// <param name="directoryPath">包含数据访问命令的目录。不加载子目录，仅加载扩展名为 .config 的文件。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadCommnads(string directoryPath)
        {
            if (Directory.Exists(directoryPath) == false)
                throw new DirectoryNotFoundException(string.Format("目录 {0} 不存在。",
                    directoryPath));

            Exception exception = null;
            s_dict = LoadCommandsInternal(directoryPath, out exception);

            if (exception != null)
                s_ExceptionOnLoad = exception;

            if (s_ExceptionOnLoad != null)
                throw s_ExceptionOnLoad;
        }


        /// <summary>
        /// <para>从指定的目录中加载全部的用于数据访问命令。</para>
        /// <para>说明：1. 这个方法只需要在程序初始化调用一次就够了。</para>
        /// <para>       2. 如果是一个ASP.NET程序，CommandManager还会负责监视此目录，如果文件有更新，会自动重新加载。</para>
        /// </summary>
        /// <param name="directoryPath">包含数据访问命令的目录。不加载子目录，仅加载扩展名为 .config 的文件。</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadFromDirectory(string directoryPath)
        {
            if (s_dict != null) // 不要删除这个判断检查，因为后面会监视这个目录。
                throw new InvalidOperationException("不允许重复调用这个方法。");

            if (Directory.Exists(directoryPath) == false)
                throw new DirectoryNotFoundException(string.Format("目录 {0} 不存在。",
                    directoryPath));

            Exception exception = null;
            s_dict = LoadCommandsInternal(directoryPath, out exception);

            if (exception != null)
                s_ExceptionOnLoad = exception;

            if (s_ExceptionOnLoad != null)
                throw s_ExceptionOnLoad;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Dictionary<string, XmlCommandItem> LoadCommandsInternal(string directoryPath, out Exception exception)
        {
            exception = null;
            Dictionary<string, XmlCommandItem> dict = null;

            try
            {
                //获取所有的xmlcommand文件
                string[] files = Directory.GetFiles(directoryPath, "*.config", SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    dict = new Dictionary<string, XmlCommandItem>(1024 * 2);

                    //将每个xmlcommand文件中的sql 存入字典中
                    foreach (string file in files)
                    {
                        List<XmlCommandItem> list = XmlHelper.XmlDeserializeFromFile<List<XmlCommandItem>>(file);
                        foreach (var x in list)
                            dict.AddValue(x.CommandName, x);
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                dict = null;
            }

            return dict;
        }



        /// <summary>
        /// 根据配置文件中的命令名获取对应的命令对象
        /// 通过连接范围自动获取provideName的情况
        /// </summary>
        /// <param name="name">command名称</param>
        /// <returns></returns>
        public static XmlCommandItem GetCommand(string name)
        {
            return GetCommand(DatabaseKind.SqlServer, name);
        }


        /// <summary>
        /// 根据配置文件中的命令名获取对应的命令对象。
        /// </summary>
        /// <param name="name">命令名称，它应该能对应一个XmlCommand。</param>
        /// <param name="providerName"></param>
        /// <returns>如果找到符合名称的XmlCommand，则返回它，否则返回null。</returns>
        public static XmlCommandItem GetCommand(DatabaseKind dbType,string name)
        {
            if (s_ExceptionOnLoad != null)
                throw s_ExceptionOnLoad;

           
            XmlCommandItem command;
            if (s_dictXml != null)
            {
                if (s_dictXml.TryGetValue(dbType, out Dictionary<string, XmlCommandItem> items))
                {
                    if (items.TryGetValue(name, out command))
                    {
                        return command;
                    }
                }
            }

            if (s_dict != null)
            {
                if (s_dict.TryGetValue(name, out command))
                    return command;
            }

            return null;
        }

    }
}
