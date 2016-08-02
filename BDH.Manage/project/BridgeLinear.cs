using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁线性
    /// </summary>
    class BridgeLinear
    {
        public string BriLineId { get; set; }
        public string MaterialComId { get; set; }
        public string BriLineName { get; set; }
        //桥面检测平距
        public float BriLineWidth { get; set; }
        //桥面检测高差
        public float BriLineHeight { get; set; }

        public string BriLineRemark { get; set; }

    }
}
