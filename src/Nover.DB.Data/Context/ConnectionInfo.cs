using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nover.DB.Data
{
	/// <summary>
	/// 数据库连接的描述信息
	/// </summary>
	public sealed class ConnectionInfo
	{
		internal ConnectionInfo(string connectionString, string providerName)
		{
			if( string.IsNullOrEmpty(connectionString))
				throw new ArgumentNullException("connectionString");

			this.ConnectionString = connectionString;

			this.ProviderName = string.IsNullOrEmpty(providerName) 
									? "Microsoft.Data.SqlClient"    // 默认连接到SQLSERVER
                                    : providerName;
		}

		/// <summary>
		/// 数据库连接字符串
		/// </summary
		public string ConnectionString { get; internal set; }
		/// <summary>
		/// 数据库提供者类型名称
		/// </summary>
		public string ProviderName { get; internal set; }
			
	
	}
}
