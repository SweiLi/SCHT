using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BDH.Sql;
using BDH.MdHelper;

namespace BDH.Manage
{
    public class ProjectManager
    {
        public string GetNewProjectId()
        {
            string sql = "select count(*) from mdtbl_project";
            int count = SqlHelperFactory.GetDefaultSqlHelper().ExecuteInt32(sql);

            return string.Format("qljc{0:D4}", count);
        }

        public int AddProject(Project p)
        {
            String sql = string.Format("INSERT INTO mdtbl_project(prj_id,  prj_name, begintime, endtime, prj_describe, prj_contract, prj_man, prj_status) VALUES" +
                "('{0}','{1}','{2}','{3}','{4}', '{5}', '{6}', {7})", 
                p.Id, p.Name, p.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"), p.EndTime.ToString("yyyy-MM-dd HH:mm:ss"), p.Description, p.ContractNumber, p.Creator, p.Status);
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return sql_helper.ExecuteNonQuery(sql);
        }

        public static List<Project> GetProjectsByCon()
        {
            String sqlCon = "SELECT prj_id,client_id,prj_name,begintime,endtime,prj_describe,prj_contract," +
                "prj_man,prj_status,check_man,check_date,prj_principal,prj_sign_date,prj_address FROM mdtbl_project";
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            return (MdDataTableHelper<Project>.ConvertToModel(sql_helper.ExecuteDataTable(sqlCon))).ToList<Project>();
        }

        public static List<Project> GetProjects(string name, DateTime? date, string contract, int state_num)
        {
            string sql_filter = "";
            if (!string.IsNullOrWhiteSpace(name))
                sql_filter += string.Format("prj_name like '%{0}%' and ", name);

            if (date.HasValue)
                sql_filter += string.Format("begintime='{0}' and ", date.Value.ToString("yyyy-MM-dd 00:00:00"));

            if (!string.IsNullOrWhiteSpace(contract))
                sql_filter += string.Format("prj_contract like '%{0}%' and ", contract);

            if (state_num > -1)
                sql_filter += string.Format("prj_status='{0}' and ", state_num);

            string sql = "select * from mdtbl_project";

            if (!string.IsNullOrWhiteSpace(sql_filter))
            {
                sql_filter = sql_filter.Substring(0, sql_filter.Length - 5);
                sql += " where " + sql_filter;
            }

            var dt = SqlHelperFactory.GetDefaultSqlHelper().ExecuteDataTable(sql);

            return MdDataTableHelper<Project>.ConvertToModel(dt).ToList<Project>();
        }
    }
}
