using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nover.DB.WebView2
{
    public class MessageResult
    {
        public MessageResult(string identify, object data, int progress = 0, bool isCompleted = false)
        {
            this.Identify = identify;
            this.Data = data;
            this.Progress = progress;
            this.IsCompleted = isCompleted;
        }

        /// <summary>
        /// 标识，用于页面判断
        /// </summary>
        public string Identify { get; private set; }

        /// <summary>
        /// 进度
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// 是否执行完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// 报告进度
        /// </summary>
        /// <returns></returns>
        public static MessageResult ReportProgress(string identify, object data, int progress = 0)
        {
            return new MessageResult(identify, data, progress);
        }

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="identify"></param>
        /// <returns></returns>
        public static MessageResult Completed(string identify, object data = null) => new MessageResult(identify, data, isCompleted: true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageResult<T>
    {
        public MessageResult(string identify, T data, int progress = 0, bool isCompleted = false)
        {
            this.Identify = identify;
            this.Data = data;
            this.Progress = progress;
            this.IsCompleted = isCompleted;
        }

        /// <summary>
        /// 标识，用于页面判断
        /// </summary>
        public string Identify { get; private set; }

        /// <summary>
        /// 进度
        /// </summary>
        public int Progress { get; private set; }

        /// <summary>
        /// 是否执行完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// 报告进度
        /// </summary>
        /// <returns></returns>
        public static MessageResult<T> ReportProgress(string identify, T data, int progress = 0) => new MessageResult<T>(identify, data, progress);

        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="identify"></param>
        /// <returns></returns>
        public static MessageResult<T> Completed(string identify, T data = default(T)) => new MessageResult<T>(identify, data, isCompleted: true);
    }

}
