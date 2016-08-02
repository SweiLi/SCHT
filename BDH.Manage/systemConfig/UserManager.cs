using System;
using System.Collections.Generic;
using System.Data;
using BDH.Sql;

namespace BDH.Manage
{
    public class UserManager
    {
        public static int GetCount()
        {
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            string sql = "select count(*) from mdtbl_user";

            int val = sql_helper.ExecuteInt32(sql);

            return val;
        }

        public static List<User> GetUserCollection()
        {
            var depart_list = DepartmentManager.GetDepartmentCollection();
            var role_list = RoleManager.GetRoleCollection();

            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            string sql = "select * from mdtbl_user";
            var dt = sql_helper.ExecuteDataTable(sql);

            List<User> user_list = new List<User>();
            foreach (DataRow row in dt.Rows)
            {
                User item = new User();
                //item.UserId = row[0].ToString();
                //item.Department = depart_list.Find((p) => p.ID.Equals(row[1].ToString()));
                //item.Role = role_list.Find((p) => p.ID.Equals(row[2].ToString()));
                //item.Name = row[3].ToString();
                //item.LoginName = row[7].ToString();
                //item.State = (UserState)Convert.ToInt32(row[9]);

                user_list.Add(item);
            }

            return user_list;
        }

        public static bool Save(User user)
        {
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();

            return false;
            //string sql = string.Format("insert into mdtbl_user (u_id, d_id, role_id, u_name, u_login_name, " +
            //    "u_password, u_status) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
            //    user.ID, user.Department.ID, user.Role.ID, user.Name, user.LoginName, user.Password, (int)user.State);

            //try
            //{
            //    sql_helper.ExecuteNonQuery(sql);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
    }
}
