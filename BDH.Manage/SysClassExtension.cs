using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    public static class SysClassExtension
    {
        public static string ToSqlString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToFlagString(this DateTime time)
        {
            return time.ToString("yyMMddHHmmss");
        }
    }
}
