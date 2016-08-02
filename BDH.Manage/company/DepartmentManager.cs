using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using BDH.Sql;

namespace BDH.Manage
{
    public class DepartmentManager
    {
        public static List<Department> GetDepartmentCollection()
        {
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();

            string sql = "select * from mdtbl_department";
            DataTable dt = sql_helper.ExecuteDataTable(sql);

            List<Department> depart_list = new List<Department>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    Department item = new Department()
            //    {
            //        ID = row[0].ToString(),
            //        CompanyID = row[1].ToString(),
            //        Name = row[2].ToString()
            //    };
            //    depart_list.Add(item);
            //}

            return depart_list;
        }
    }
}
