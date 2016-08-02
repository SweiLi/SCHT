
namespace BDH.Log
{
    /// <summary>
    /// 日志抽象类
    /// </summary>
    public abstract class AbstractLogger : ILogger
    {
        protected abstract bool WriteLog ( string userid, LogType log_type, string message, string title);

        public bool WriteWarning (string userid, string msg, string title )
        {
            return this.WriteLog( userid, LogType.Warning, msg, title );
        }

        public bool WriteInfo (string userid, string msg, string title )
        {
            return this.WriteLog( userid, LogType.Info, msg, title );
        }

        public bool WriteError (string userid, string msg, string title )
        {
            return this.WriteLog( userid, LogType.Error, msg, title );
        }
    }
}
