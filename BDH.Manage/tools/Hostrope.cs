using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 主揽数据录入数据表
    /// </summary>
    class HostRope
    {
        //主缆数据表id
        public string HostRopeId { get; set; }
        ////材质检测参数单据ID
        public string MaterialComId { get; set; }
        //测点编号
        public string HostRopeCode { get; set; }
        //平距（m）
        public float HostRopeWidth { get; set; }
        ////高差（m）
        public float HostRopeHeight { get; set; }
        //位置
        public string HostRopeUnit { get; set; }

        //备注
        public string HostRopeRemark { get; set; }
    }
}
