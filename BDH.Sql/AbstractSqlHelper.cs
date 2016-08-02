using System;
using System.Collections.Generic;
using System.Data;

namespace BDH.Sql
{
    /// <summary>
    /// 数据库基础操作抽象类，定义和封装通用的操作功能
    /// </summary>
    public abstract class AbstractSqlHelper : ISqlHelper
    {
        protected string m_ConnectionString;

        public string ConnectionString { get { return this.m_ConnectionString; } }

        public abstract void Open ();
        public abstract void Close ();

        public abstract int ExecuteNonQuery ( string sql );
        public abstract object ExecuteObject ( string sql );
        public abstract DataTable ExecuteDataTable ( string sql );

        public int ExecuteInt32 ( string sql )
        {
            object obj = this.ExecuteObject( sql );

            if ( obj == null )
                throw new Exception( "执行的可能不是一个能准确返回数值的查询" );

            return Convert.ToInt32( obj );
        }

        public string ExecuteString ( string sql )
        {
            object obj = this.ExecuteObject( sql );

            if ( obj == null )
                return null;

            return Convert.ToString( obj );
        }

        public List<string> ExecuteStringList ( string sql )
        {
            var dt = this.ExecuteDataTable( sql );
            List<string> rlt = new List<string>();

            foreach ( DataRow row in dt.Rows )
            {
                if ( row[0] != null ) rlt.Add( row[0].ToString() );
                else rlt.Add( null );
            }

            return rlt;
        }
    }
}
