using System.Collections.Generic;
using System.Data;

namespace BDH.Sql
{
    /// <summary>
    /// 数据库基础操作辅助接口
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// 执行非查询语句的操作
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery ( string sql );

        /// <summary>
        /// 执行查询语句，结果保存为DataTable对象
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>查询结果</returns>
        DataTable ExecuteDataTable ( string sql );

        /// <summary>
        /// 执行仅返回单一数据的查询
        /// </summary>
        /// <param name="sql">要执行的SQL语句，该语句仅返回单一值</param>
        /// <returns>查询结果</returns>
        object ExecuteObject ( string sql );

        /// <summary>
        /// 执行仅返回Int32类型的数据查询
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>查询结果</returns>
        int ExecuteInt32 ( string sql );

        /// <summary>
        /// 执行仅返回一个String类型的数据查询
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>查询结果</returns>
        string ExecuteString ( string sql );

        /// <summary>
        /// 执行一个查询语句，返回String类型的数据列表
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>查询结果</returns>
        List<string> ExecuteStringList ( string sql );
    }
}
