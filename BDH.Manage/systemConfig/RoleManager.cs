using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BDH.Sql;

namespace BDH.Manage
{
    public class RoleManager
    {
        public static List<Role> GetRoleCollection()
        {
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();

            string sql = "select * from mdtbl_role";
            DataTable dt = sql_helper.ExecuteDataTable(sql);

            List<Role> role_list = new List<Role>();
            foreach (DataRow row in dt.Rows)
            {
                Role item = new Role() {
                    RoleId = row[0].ToString(),
                    RoleName = row[1].ToString(),
                    RoleDescription = row[2].ToString() };
                role_list.Add(item);
            }

            return role_list;
        }
    }
}
