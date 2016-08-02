using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDH.Sql;

namespace BDH.Manage
{
    public class BriComDisInfoManager
    {
        public static void Read()
        {
            ISqlHelper sql_helper = SqlHelperFactory.GetDefaultSqlHelper();
            string sql = "select count (*) from mdtbl_bri_com_dis_info";
            int val = sql_helper.ExecuteInt32(sql);
            Console.WriteLine("count=" + val.ToString());
        }
    }
}
