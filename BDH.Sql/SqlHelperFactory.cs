using System;
using BDH.Config;

namespace BDH.Sql
{
    /// <summary>
    /// 数据库操作辅助类工厂，用于创建所需的数据库辅助类
    /// </summary>
    public class SqlHelperFactory
    {
        private static SqlServerHelper SqlSvrHelperInstance;
        private static SqliteHelper SqliteHelperInstance;

        public static ISqlHelper GetSqlHelper ( string connectionString, DataBaseType dbType )
        {
            ISqlHelper sql_helper = null;

            if ( dbType == DataBaseType.SQLServer )
                sql_helper = new SqlServerHelper( connectionString );
            else if ( dbType == DataBaseType.SQLite )
                sql_helper = new SqliteHelper( connectionString );

            return sql_helper;
        }

        public static ISqlHelper GetSingleSqlHelper ( DataBaseType dbType )
        {
            SysSecurityConfig conf = new SysSecurityConfig();

            if ( dbType == DataBaseType.SQLServer )
            {
                if ( SqlSvrHelperInstance == null )
                    SqlSvrHelperInstance = new SqlServerHelper(conf.SqlServerConnectionString );

                return SqlSvrHelperInstance;
            }
            else if ( dbType == DataBaseType.SQLite )
            {
                if ( SqliteHelperInstance == null )
                    SqliteHelperInstance = new SqliteHelper(conf.SqliteConnectionString );

                return SqliteHelperInstance;
            }

            return null;
        }

        public static ISqlHelper GetDefaultSqlHelper()
        {
            SysSecurityConfig conf = new SysSecurityConfig();
            if (conf.DefaultDatabase.Equals("SQLITE", StringComparison.OrdinalIgnoreCase))
            {
                if (SqliteHelperInstance == null)
                    SqliteHelperInstance = new SqliteHelper(conf.SqliteConnectionString);

                return SqliteHelperInstance;
            }
            else if (conf.DefaultDatabase.Equals("SQLSERVER", StringComparison.OrdinalIgnoreCase))
            {
                if (SqlSvrHelperInstance == null)
                    SqlSvrHelperInstance = new SqlServerHelper(conf.SqlServerConnectionString);

                return SqlSvrHelperInstance;
            }

            return null;
        }
    }
}
