using System;
using System.Data;
using System.Data.SqlClient;

namespace Nover.DB.Data.BulkCopy
{


    /// <summary>
    /// 
    /// </summary>
    public interface IDbBulkCopy:IDisposable
    {

        
        /// <summary>
        /// 设置目标表
        /// </summary>
        /// <param name="tableName"></param>
        void SetDestinationTableName(string tableName);
        
        /// <summary>
        /// 获取目标表
        /// </summary>
        string GetDestinationTableName();

        /// <summary>
        /// 添加列映射
        /// </summary>
        /// <param name="sourceColumn"></param>
        /// <param name="destinationColumn"></param>
        void AddColumnMapping(string sourceColumn, string destinationColumn);

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="table"></param>
        void WriteToServer(DataTable table);

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="rows"></param>
        void WriteToServer(DataRow[] rows);
    }
}