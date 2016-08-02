
namespace BDH.Log
{
    /// <summary>
    /// 日志操作接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 写入告警日志消息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="title">日志标题</param>
        /// <returns>是否写入成功</returns>
        bool WriteWarning(string userid, string msg, string title);

        /// <summary>
        /// 写入普通日志消息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="title">日志标题</param>
        /// <returns>是否写入成功</returns>
        bool WriteInfo (string userid, string msg, string title );

        /// <summary>
        /// 写入错误日志消息
        /// </summary>
        /// <param name="msg">日志消息</param>
        /// <param name="title">日志标题</param>
        /// <returns>是否写入成功</returns>
        bool WriteError (string userid, string msg, string title );
    }
}
