using System;
using BDH.Config;

namespace BDH.Log
{
    public static class LoggerFactory
    {
        private static ILogger TextInstance;
        private static ILogger DatabaseInstance;

        /// <summary>
        /// 获取数据库日志记录器
        /// </summary>
        /// <returns></returns>
        public static ILogger GetLogger()
        {
            if (DatabaseInstance == null)
            {
                SysSecurityConfig conf = new SysSecurityConfig();
                DatabaseInstance = new DatabaseLogger(conf.LogTableName);
            }

            return DatabaseInstance;
        }

        /// <summary>
        /// 获取文本文件日志记录器
        /// </summary>
        /// <returns></returns>
        public static ILogger GetTextLogger()
        {
            if (TextInstance == null)
            {
                SysSecurityConfig conf = new SysSecurityConfig();
                string log_path = AppDomain.CurrentDomain.BaseDirectory +
                    conf.LogTextFilePath + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                TextInstance = new TextLogger(log_path);
            }

            return TextInstance;
        }
    }
}
