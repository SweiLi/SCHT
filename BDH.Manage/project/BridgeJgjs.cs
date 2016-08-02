using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁结构数据
    /// </summary>
    class BridgeJgjs
    {
        public string BriJgjsId { get; set; }
        public string BriId { get; set; }
        public float BriJgjsLength { get; set; }
        public float BriJgjsDeckwidth { get; set; }
        public float BriJgjsRoadwaywidth { get; set; }

        public float BriJgjsDeckheight { get; set; }
        public float BriJgjsDownheight { get; set; }
        public float BriJgjsUpheight { get; set; }
        public float BriJgjsRampwidth { get; set; }

        public float BriJgjsRampallwidth { get; set; }
        public string BriJgjsRamplinear { get; set; }
        public string BriJgjsExpansiontype { get; set; }
        public string BriJgjsBearingtype { get; set; }
        public string BriJgjsEarthquakeacceleration { get; set; }

        public string BriJgjsConicalslope { get; set; }

        public string BriJgjsKeepslope { get; set; }
        public string BriJgjsRegulatingstructure { get; set; }
        public string BriJgjsCsw { get; set; }
        public string BriJgjsDesignsw { get; set; }
        public string BriJgjsHissw { get; set; }

    }
}
