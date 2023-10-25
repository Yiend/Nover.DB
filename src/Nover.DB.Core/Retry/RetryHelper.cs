using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nover.DB.Core
{
    /// <summary>
    /// 重试辅助方法
    /// </summary>
    public static class RetryHelper
    {
        /// <summary>
        /// 重试N次执行操作
        /// 如果重试N次后仍然失败，抛出最后一次异常
        /// </summary>
        /// <param name="action"></param>
        /// <param name="retryTimes">重试次数</param>
        /// <param name="waitMs">重试等待毫秒数</param>
        public static void Execute(Action action, int retryTimes = 5, int waitMs = 200)
        {
            var i = 0;
            while (true)
            {
                try
                {
                    action();

                    // 执行成功，不需要再重试，直接返回
                    return;
                }
                catch
                { // 吃掉每一次尝试的异常，记录下来。
                  // 如果重试次数到达后，还是有异常，则再次抛出

                    // 发生异常，等待后，执行重试
                    if (waitMs > 0)
                        Thread.Sleep(waitMs);

                    // 重试结束，还是没有正确返回，抛出异常
                    if (i == retryTimes)
                        throw;
                    i++;
                }
            }

        }

        /// <summary>
        /// 重试N次执行操作，获取返回值
        /// 如果重试N次后仍然失败，抛出最后一次异常
        /// </summary>
        /// <param name="func"></param>
        /// <param name="retryTimes">重试次数</param>
        /// <param name="waitMs">重试等待毫秒数</param>
        /// <returns></returns>
        public static T Get<T>(Func<T> func, int retryTimes = 5, int waitMs = 100)
        {
            var i = 0;
            while (true)
            {
                try
                {
                    // 执行成功后，直接返回
                    return func();
                }
                catch
                {
                    // 吃掉每一次尝试的异常，记录下来
                    // 如果重试次数到达后，还是有异常，则再次抛出

                    // 发生异常，等待后，执行重试
                    if (waitMs > 0)
                        Thread.Sleep(waitMs);

                    // 重试结束，还是没有正确返回，抛出异常
                    if (i == retryTimes)
                        throw;

                    i++;
                }
            }

        }

        /// <summary>
        /// 重试N次执行操作，获取返回值
        /// 如果重试N次后仍然失败，返回默认值
        /// </summary>
        /// <param name="func"></param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="retryTimes">重试次数</param>
        /// <param name="waitMs">重试等待秒数</param>
        /// <returns></returns>
        public static T Get<T>(Func<T> func, T defaultValue, int retryTimes = 5, int waitMs = 100)
        {
            var i = 0;
            while (true)
            {
                try
                {
                    // 执行成功后，直接返回
                    return func();
                }
                catch
                { // 吃掉每一次尝试的异常，记录下来
                  // 如果重试次数到达后，还是有异常，则再次抛出

                    // 发生异常，等待后，执行重试
                    if (waitMs > 0)
                        Thread.Sleep(waitMs);

                    // 重试结束，还是没有正确返回，抛出异常
                    if (i == retryTimes)
                        throw;

                    i++;
                }
            }

        }
    }
}
