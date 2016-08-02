using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁技术状况评定
    /// </summary>
    public class BridgeJszk
    {
        public string BriJszkId { get; set; }
        public string BriId { get; set; }
        public DateTime BriJszkCheckdate { get; set; }
        public string BriJszkDqorts { get; set; }
        public string BriJszkAlllevel { get; set; }

        public string BriJszkAbutmentlevel { get; set; }
        public string BriJszkPierlevel { get; set; }

        public string BriJszkSubgragelevel { get; set; }
        public string BriJszkUpconlevel { get; set; }
        public string BriJszkBearlevel { get; set; }
        public string BriJszkByxx { get; set; }
        public string BriJszkCzdc { get; set; }

        public DateTime BriJszkNextdate { get; set; }

    }
}
