using System;
using System.Data;
using System.Data.SQLite;

namespace BDH.Sql
{
    /// <summary>
    /// SQLITE数据库基础操作类
    /// </summary>
    public class SqliteHelper : AbstractSqlHelper, IDisposable
    {
        private SQLiteConnection m_Connection = null;
        public SqliteHelper ( string connectionString )
        {
            this.m_ConnectionString = connectionString;
        }

        private bool disposed = false;
        public void Dispose ()
        {
            this.Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose ( bool disposing )
        {
            if ( !disposed )
            {
                if ( disposing )
                {
                    this.Close();
                    this.disposed = true;
                }
            }
        }

        public override void Open ()
        {
            if ( m_Connection == null ) m_Connection = new SQLiteConnection( this.m_ConnectionString );

            if ( m_Connection.State != ConnectionState.Open ) m_Connection.Open();
        }

        public override void Close ()
        {
            m_Connection.Close();
        }

        public override int ExecuteNonQuery ( string sql )
        {
            this.Open();

            SQLiteCommand cm = new SQLiteCommand( sql, this.m_Connection );
            int count = cm.ExecuteNonQuery();

            return count;
        }

        public override DataTable ExecuteDataTable ( string sql )
        {
            this.Open();

            DataTable dt = new DataTable();
            using ( SQLiteDataAdapter ada = new SQLiteDataAdapter( sql, this.m_Connection ) )
            {
                ada.Fill( dt );
            }

            return dt;
        }

        public override object ExecuteObject ( string sql )
        {
            this.Open();

            SQLiteCommand cm = new SQLiteCommand( sql, this.m_Connection );
            object obj = cm.ExecuteScalar();

            if ( obj == DBNull.Value ) return null;

            return obj;
        }
    }
}
