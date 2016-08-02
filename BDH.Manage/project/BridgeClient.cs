using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁客户信息表
    /// </summary>
    public class BridgeClient
    {
        public string ClientId { get; set; }
        public string PrjId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPostcode { get; set; }
        public string ClientRemark { get; set; }
        public string ClientQq { get; set; }
        public string ClientTel { get; set; }
        public string ClientEmail { get; set; }

    }
}
