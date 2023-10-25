using System;

namespace Nover.DB.Data
{
    /// <summary>
    /// 初始化接口封装类
    /// </summary>
    public sealed class Initializer
    {
        /// <summary>
        /// Initializer的实例引用
        /// </summary>
        public static readonly Initializer Instance = new Initializer();

        /// <summary>
        /// 默认的实例列表长度
        /// </summary>
        internal int DefaultEntityListLen { get; private set; } = 50;

        /// <summary>
        /// 是否允许创建一次性的DbContext实例，默认值：false
        /// 如果启用，那么可以不必创建ConnectionScope范围块，则直接操作数据库
        /// </summary>
        internal bool IsAutoCreateOneOffDbContext { get; private set; }


        /// <summary>
        /// 是否允许创建一次性的DbContext实例，默认值：false
        /// 如果启用，那么可以不必创建ConnectionScope范围块，则直接操作数据库
        /// </summary>
        /// <returns></returns>
        public Initializer AllowCreateOneOffDbContext()
        {
            IsAutoCreateOneOffDbContext = true;
            return this;
        }

        /// <summary>
        /// 设置默认的实例列表长度
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public Initializer SetDefaultEntityListLen(int len)
        {
            if (len < 1)
                throw new ArgumentOutOfRangeException("len");

            DefaultEntityListLen = len;
            return this;
        }

      

        /// <summary>
        /// 从指定的目录中加载所有 XmlCommand 配置
        /// </summary>
        /// <param name="directoryPath">包含XmlCommand配置文件的目录，如果不指定就表示接受XmlCommand规范的默认目录</param>
        /// <returns></returns>
        public Initializer LoadXmlCommandFromDirectory(string directoryPath = null)
        {
            // 如果不指定目录，就采用XmlCommand规范的默认目录（建议不指定！）
            if (string.IsNullOrEmpty(directoryPath))
            {
                //当前执行程序下的 XmlCommand 目录
                directoryPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "XmlCommand");
            }
            XmlCommandManager.LoadFromDirectory(directoryPath);
            return this;
        }

 
    }
}
