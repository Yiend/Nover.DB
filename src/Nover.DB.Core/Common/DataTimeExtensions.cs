using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nover.DB.Core
{
    /// <summary>
    /// 包含DataTime类型相关的扩展方法
    /// </summary>
    public static class DataTimeExtensions
    {
        /// <summary>
        /// 返回包含日期时间格式的字符串（"yyyy-MM-dd HH:mm:ss"）
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 返回仅仅包含日期格式的字符串（"yyyy-MM-dd"）
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 清除时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ClearTime(this DateTime dateTime)
        {
            return dateTime.Subtract(new TimeSpan(0, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond));
        }

        /// <summary>
        /// 检查给定的值是否为周末
        /// </summary>
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// 检查给定的值是否为工作日
        /// </summary>
        public static bool IsWeekday(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }
    }
}
