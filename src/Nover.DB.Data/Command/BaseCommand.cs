﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nover.DB.Data
{
    /// <summary>
    /// 执行数据库操作的基类
    /// </summary>
    public abstract class BaseCommand
    {
        /// <summary>
        /// DbContext实例
        /// </summary>
        protected DbContext _context;
        internal DbContext Context => _context;

        /// <summary>
        /// 用于SQL语句的参数占位符，命令名称的计算
        /// </summary>
        internal ParaNameBuilder ParaNameBuilder { get; private set; }

        /// <summary>
        /// DbCommand实例
        /// </summary>
        protected DbCommand _command;
        /// <summary>
        /// 执行存储过程使用的DbCommand对象
        /// </summary>
        public virtual DbCommand Command => _command;

        internal BaseCommand(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
            _command = context.Connection.CreateCommand();
            ParaNameBuilder = ParaNameBuilderFactory.Factory.Instance.GetBuilder(_command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DbParameter[] CloneParameters()
        {
            DbParameter[] parameters2 = new DbParameter[_command.Parameters.Count];

            int i = 0;
            foreach (DbParameter src in _command.Parameters)
            {
                DbParameter newParam = _command.CreateParameter();
                newParam.ParameterName = src.ParameterName;
                newParam.DbType = src.DbType;
                newParam.Size = src.Size;
                newParam.Value = src.Value;
                newParam.Direction = src.Direction;
                parameters2[i++] = newParam;
            }
            return parameters2;
        }


        #region Execute 方法


        /// <summary>
        /// 开始执行数据库操作前要处理的额外操作
        /// </summary>
        protected virtual void BeforeExecute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="DbExceuteException"></exception>
        private T Execute<T>(Func<DbCommand, T> func)
        {
            // 打开数据库连接
            _context.OpenConnection();


            // 设置命令的连接以及事务对象
            DbCommand command = this.Command;
            command.Connection = _context.Connection;

            if (_context.Transaction != null)
                command.Transaction = _context.Transaction;

            // 触发执行 前 事件
            _context.EventManager.FireBeforeExecute(this);

            this.BeforeExecute();

            try
            {
                // 执行数据库操作
                T result = func(command);

                // 触发执行 后 事件
                _context.EventManager.FireAfterExecute(this);

                return result;
            }
            catch (System.Exception ex)
            {
                // 触发 异常 事件
                _context.EventManager.FireOnException(this, ex);

                // 重新抛出一个特定的异常，方便异常日志中记录command信息。
                throw new DbExceuteException(ex, command);
            }
            finally
            {
                // 让命令与连接，事务断开，避免这些资源外泄。
                command.Connection = null;
                command.Transaction = null;

                if (_context.AutoDisposable)
                    _context.Dispose();
            }
        }




        /// <summary>
        /// 执行命令，并返回影响函数
        /// </summary>
        /// <returns>影响行数</returns>
        public int ExecuteNonQuery()
        {
            return Execute(cmd => cmd.ExecuteNonQuery());
        }


        /// <summary>
        /// 执行查询，以DataTable形式返回结果
        /// </summary>
        /// <returns>数据集</returns>
        public DataTable ToDataTable()
        {
            return Execute(cmd =>
            {
                DataTable table = new DataTable("_tb");
                DbDataAdapter da = _context.Factory.CreateDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(table);

                return table;
            });
        }


        /// <summary>
        /// 执行查询，以DataSet形式返回结果
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet ToDataSet()
        {
            return Execute(cmd =>
            {
                DataSet ds = new DataSet();

                DbDataAdapter adapter = _context.Factory.CreateDataAdapter();
                adapter.SelectCommand = cmd;

                adapter.Fill(ds);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = "_tb" + i.ToString();
                }
                return ds;
            });
        }

        /// <summary>
        /// 执行命令，返回DbDataReader对象实例
        /// </summary>
        /// <returns>DbDataReader实例</returns>
        public DbDataReader ExecuteReader()
        {
            return Execute(cmd => cmd.ExecuteReader());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private T ToScalar<T>(object obj)
        {
            if (obj == null || DBNull.Value.Equals(obj))
                return default(T);

            if (obj is T)
                return (T)obj;

            Type targetType = typeof(T);

            //单测走不到
            //if( targetType == typeof(object) ) 
            //	return (T)obj;

            return (T)Convert.ChangeType(obj, targetType);
        }



        /// <summary>
        /// 执行命令，返回第一行第一列的值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <returns>结果集的第一行,第一列</returns>
        public T ExecuteScalar<T>()
        {
            return Execute(cmd => ToScalar<T>(cmd.ExecuteScalar()));
        }

        /// <summary>
        /// 执行命令，并返回第一列的值列表
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <returns>结果集的第一列集合</returns>
        public List<T> ToScalarList<T>()
        {
            return Execute(cmd =>
            {
                List<T> list = new List<T>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(ToScalar<T>(reader[0]));
                    }
                    return list;
                }
            });
        }



        /// <summary>
        /// 执行命令，将结果集转换为实体列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>实体集合</returns>
        public List<T> ToList<T>() where T : class, new()
        {
            return Execute(cmd =>
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    IDataLoader<T> loader = DataLoaderFactory.GetLoader<T>();
                    return loader.ToList(reader);
                }
            });
        }

        /// <summary>
        /// 执行命令，将结果集的第一行转换为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>实体</returns>
        public T ToSingle<T>() where T : class, new()
        {
            return Execute(cmd =>
            {
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    IDataLoader<T> loader = DataLoaderFactory.GetLoader<T>();
                    return loader.ToSingle(reader);
                }
            });
        }

        #endregion

    }
}
