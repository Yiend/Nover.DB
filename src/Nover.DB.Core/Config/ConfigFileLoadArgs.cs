using System.Reflection;

namespace Nover.DB.Core
{
    /// <summary>
    /// 表示加载配置文件的参数信息。
    /// </summary>
    public sealed class ConfigFileLoadArgs
	{
		/// <summary>
		/// 表示配置文件所属的嵌入程序集，如果是外部文件则为 null。
		/// </summary>
		public Assembly Assembly { get; internal set; }

		/// <summary>
		/// 配置文件的名称.
		/// 如果是嵌入资源，则包含完整的命名空间，如果是外部文件就是全路径的文件名。
		/// </summary>
		public string FileName { get; internal set; }

		/// <summary>
		/// 配置文件的内容。
		/// </summary>
		public string FileContent { get; internal set; }

        /// <summary>
        /// 数据库类型: 固定值 MySQL,SQLServer
        /// </summary>
        public string DbType { get; internal set; }

	}
}
