using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁构件表
    /// </summary>
    class BridgeMember
    {
        public string MemberId { get; set; }
        public string BridgeChildrenId { get; set; }
        public string CompomentId { get; set; }
        //构件编码
        public string MemberCode { get; set; }
        //构件备注
        public string MemberRemark { get; set; }


    }
}
