using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 系统权限表
    /// </summary>
    class Permission
    {
        //权限id
        public string PerId { get; set; }
        ////权限描述
        public string PerDescribe { get; set; }
        //权限url
        public string PerUrl { get; set; }
        //权限缩写
        public string PerAbbre { get; set; }
        //权限中文名称
        public string PerName { get; set; }
    }
}
