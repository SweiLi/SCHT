using System;
using System.Text;
using System.IO;

namespace BDH.Log
{
    /// <summary>
    /// 文本日志类，将日志消息写入指定的文本文件
    /// </summary>
    public class TextLogger : AbstractLogger, IDisposable
    {
        private StreamWriter m_Writer;

        private bool disposed = false;

        public TextLogger ( string log_path )
        {
            this.m_Writer = new StreamWriter( log_path, true, Encoding.Default );
        }

        public void Dispose ()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose ( bool disposing )
        {
            if ( !disposed )
            {
                if ( disposing )
                {
                    this.m_Writer.Close();
                    this.disposed = true;
                }
            }
        }

        protected override bool WriteLog ( string userid, LogType log_type, string message, string title )
        {
            try
            {
                var log_str = string.Format( "{0}\t{1}\t{2}\t{3}\t{4}", 
                    userid, DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ),
                    log_type.ToString(), title, message );
                this.m_Writer.WriteLine( log_str );
                this.m_Writer.Flush();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
