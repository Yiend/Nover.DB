using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Nover.DB.Data.BulkCopy
{
    /// <summary>
    /// 基于微软数据库的api
    /// </summary>
    internal class MicrosoftBulkCopy : IDbBulkCopy
    {
        private SqlBulkCopy _bulkCopy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MicrosoftBulkCopy(DbConnection conn, BulkCopyOptions options, DbTransaction tran)
        {

            SqlConnection sqlConnection = conn as SqlConnection;

            if (sqlConnection == null)
            {
                throw new InvalidOperationException("只支持在SqlServer环境下使用SqlBulkCopy。");
            }

            SqlBulkCopyOptions sqlBulkCopyOptions = (SqlBulkCopyOptions)options;

            SqlTransaction sqlTransaction = tran as SqlTransaction;

            _bulkCopy = new SqlBulkCopy(sqlConnection, sqlBulkCopyOptions, sqlTransaction);
        }

        /// <summary>
        /// 设置目标表
        /// </summary>
        /// <param name="tableName"></param>
        public void SetDestinationTableName(string tableName)
        {
            _bulkCopy.DestinationTableName = tableName;
        }

        public string GetDestinationTableName()
        {
            return _bulkCopy.DestinationTableName;
        }

        /// <summary>
        /// 添加列映射
        /// </summary>
        /// <param name="sourceColumn"></param>
        /// <param name="destinationColumn"></param>
        public void AddColumnMapping(string sourceColumn, string destinationColumn)
        {
            _bulkCopy.ColumnMappings.Add(sourceColumn, destinationColumn);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="table"></param>
        public void WriteToServer(DataTable table)
        {
            _bulkCopy.WriteToServer(table);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="rows"></param>
        public void WriteToServer(DataRow[] rows)
        {
            _bulkCopy.WriteToServer(rows);
        }

        /// <summary>
        /// 资源回收
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)_bulkCopy)?.Dispose();
        }
    }
}