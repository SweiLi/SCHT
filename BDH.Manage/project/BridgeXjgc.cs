using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁修建工程记录
    /// </summary>
    class BridgeXjgc
    {
        public string BriGcjlId { get; set; }
        public string BriId { get; set; }
        public DateTime BriGcjlBegindate { get; set; }
        public DateTime BriGcjlSuccdate { get; set; }
        public string BriGcjlBuildtype { get; set; }

        public string BriGcjlBuildreason { get; set; }
        public string BriGcjlProjectscope { get; set; }
        public string BriGcjlMoney { get; set; }
        public string BriGcjlPocketbook { get; set; }

        public string BriGcjlQualityevalution { get; set; }
        public string BriGcjlBuildunit { get; set; }

        public string BriGcjlDesignunit { get; set; }
        public string BriGcjlConstructunit { get; set; }
        public string BriGcjlSupervationunit { get; set; }
        public string BriGcjlNote { get; set; }


    }
}
