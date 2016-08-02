using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁组件病害信息表
    /// </summary>
    class BriComDisInfo
    {
        public string BcdiId { get; set; }
        public string BcdiBridgetype { get; set; }
        public string BcdiStruc { get; set; }
        public string BcdiComponentName { get; set; }
        public string BcdiDisease { get; set; }

        public string BcdiScale { get; set; }
        public string BcdiQualitative { get; set; }
        public string BcdiQuantify { get; set; }
        public string BcdiIsIdentify { get; set; }

    }
}
