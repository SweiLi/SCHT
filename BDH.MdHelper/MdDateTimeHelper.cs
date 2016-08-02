using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.MdHelper
{
    class MdDateTimeHelper
    {
        /// <summary>
        /// 哪天 
        /// </summary>
        /// <param name="days"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetTheDay(int? days, DateTime dateTime)
        {
            int num = days ?? 0;
            return dateTime.AddDays((double)num).ToShortDateString();
        }
        /// <summary>
        /// 周日 
        /// </summary>
        /// <param name="weeks"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetSunday(int? weeks, DateTime dateTime)
        {
            int num = weeks ?? 0;
            return dateTime.AddDays(Convert.ToDouble((int)(-(int)Convert.ToInt16(dateTime.DayOfWeek))) + (double)(7 * num)).ToShortDateString();
        }
        /// <summary>
        /// 周六 
        /// </summary>
        /// <param name="weeks"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetSaturday(int? weeks, DateTime dateTime)
        {
            int num = weeks ?? 0;
            return dateTime.AddDays(Convert.ToDouble((int)(6 - Convert.ToInt16(dateTime.DayOfWeek))) + (double)(7 * num)).ToShortDateString();
        }
        /// <summary>
        /// 月第一天 
        /// </summary>
        /// <param name="months"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetFirstDayOfMonth(int? months, DateTime dateTime)
        {
            int months2 = months ?? 0;
            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(months2).ToShortDateString();
        }
        /// <summary>
        /// 月最后一天 
        /// </summary>
        /// <param name="months"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetLastDayOfMonth(int? months, DateTime dateTime)
        {
            int months2 = months ?? 0;
            return DateTime.Parse(dateTime.ToString("yyyy-MM-01")).AddMonths(months2).AddDays(-1.0).ToShortDateString();
        }
        /// <summary>
        /// 年度第一天 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetFirstDayOfYear(DateTime dateTime)
        {
            int value = Convert.ToInt32(dateTime.Year);
            return DateTime.Parse(dateTime.ToString("yyyy-01-01")).AddYears(value).ToShortDateString();
        }
        /// <summary>
        /// 年度最后一天 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetLastDayOfYear(DateTime dateTime)
        {
            int value = Convert.ToInt32(dateTime.Year);
            return DateTime.Parse(dateTime.ToString("yyyy-01-01")).AddYears(value).AddDays(-1.0).ToShortDateString();
        }
        /// <summary>
        /// 季度第一天 
        /// </summary>
        /// <param name="quarters"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetFirstDayOfQuarter(int? quarters, DateTime dateTime)
        {
            int num = quarters ?? 0;
            return dateTime.AddMonths(num * 3 - (dateTime.Month - 1) % 3).ToString("yyyy-MM-01");
        }
        /// <summary>
        /// 季度最后一天 
        /// </summary>
        /// <param name="quarters"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetLastDayOfQuarter(int? quarters, DateTime dateTime)
        {
            int num = quarters ?? 0;
            return DateTime.Parse(dateTime.AddMonths(num * 3 - (dateTime.Month - 1) % 3).ToString("yyyy-MM-01")).AddDays(-1.0).ToShortDateString();
        }
        /// <summary>
        /// 中文星期 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetDayOfWeekCN(DateTime dateTime)
        {
            string[] array = new string[]
            {
                "星期日",
                "星期一",
                "星期二",
                "星期三",
                "星期四",
                "星期五",
                "星期六"
            };
            return array[(int)Convert.ToInt16(dateTime.DayOfWeek)];
        }
        /// <summary>
        /// 获取星期数字形式,周一开始 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public int GetDayOfWeekNum(DateTime dateTime)
        {
            return (int)((Convert.ToInt16(dateTime.DayOfWeek) == 0) ? 7 : Convert.ToInt16(dateTime.DayOfWeek));
        }
        /// <summary>
        /// 取指定日期是一年中的第几周 
        /// </summary>
        /// <param name="dtime"></param>
        /// <returns></returns>
        public static int GetWeekofyear(DateTime dtime)
        {
            int num = 0;
            DayOfWeek dayOfWeek = DateTime.Parse(dtime.Year.ToString() + "-1-1").DayOfWeek;
            for (int i = (int)(dayOfWeek + 1); i <= dtime.DayOfYear; i += 7)
            {
                num++;
            }
            return num;
        }
        /// <summary>
        /// 计算时间的差值,返回字符串形式
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        private static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            }
            catch (Exception e)
            {
                System.Console.WriteLine("在计算时间间隔函数DateDiff中发生了错误！");
            }
            return dateDiff;
        }

        /// <summary>
        /// 计算多少天前的日期
        /// </summary>
        /// <param name="Sdt"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        private  static DateTime GetDateDiff(String Sdt,int days)
        {
            DateTime Dt = Convert.ToDateTime(Sdt);
            Dt = Dt.AddDays(days);
            return Dt;
        }

    }
}
