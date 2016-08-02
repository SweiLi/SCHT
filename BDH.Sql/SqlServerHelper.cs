using System;
using System.Data;
using System.Data.SqlClient;

namespace BDH.Sql
{
    /// <summary>
    /// SQL SERVER数据库基础操作类
    /// </summary>
    public class SqlServerHelper : AbstractSqlHelper, IDisposable
    {
        private SqlConnection m_Connection;

        public SqlServerHelper ( string connectionString )
        {
            this.m_ConnectionString = connectionString;
            this.m_Connection = new SqlConnection(this.m_ConnectionString);
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

        public override void Open()
        {
            if (this.m_Connection.State != ConnectionState.Open)
                this.m_Connection.Open();
        }

        public override void Close()
        {
            if (this.m_Connection != null)
                this.m_Connection.Close();
        }

        public override int ExecuteNonQuery(string sql)
        {
            this.Open();

            SqlCommand cm = new SqlCommand(sql, this.m_Connection);
            int count = cm.ExecuteNonQuery();

            return count;
        }

        public override DataTable ExecuteDataTable(string sql)
        {
            this.Open();

            SqlDataAdapter ada = new SqlDataAdapter(sql, this.m_Connection);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            return dt;
        }

        public override object ExecuteObject(string sql)
        {
            this.Open();

            SqlCommand cm = new SqlCommand(sql, this.m_Connection);
            object obj = cm.ExecuteScalar();

            if (obj == DBNull.Value) return null;

            return obj;
        }
    }
}
