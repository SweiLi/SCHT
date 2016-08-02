using System;
using BDH.Sql;

namespace BDH.Log
{
    /// <summary>
    /// 数据库日志类，将日志消息写入指定的数据库
    /// </summary>
    public class DatabaseLogger : AbstractLogger
    {
        private ISqlHelper m_SqlHelper = null;
        private string m_LogTableName;
        public DatabaseLogger(string tbl_name)
        {
            this.m_LogTableName = tbl_name;
            this.m_SqlHelper = SqlHelperFactory.GetSingleSqlHelper(DataBaseType.SQLite);
        }

        protected override bool WriteLog ( string userid, LogType log_type, string message, string title )
        {
            string sql = string.Format("insert into {0} values ('{1}', {2}, '{3}', '{4}')",
                userid, this.m_LogTableName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 
                (int)log_type, title, message);

            try
            {
                int val = this.m_SqlHelper.ExecuteNonQuery(sql);
                return val == 1;
            }
            catch (Exception ex)
            {
                ILogger logger = LoggerFactory.GetTextLogger();
                logger.WriteError(userid, ex.Message, title);

                return false;
            }
        }
    }
}
