using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 氯离子
    /// </summary>
    class Chloridion
    {
        public string ChloItemId { get; set; }
        public string MaterialComId { get; set; }
        public string ChloExperId { get; set; }
        public float ChloVolume { get; set; }
        public float ChloBuretteData { get; set; }

        public float ChloPotential { get; set; }
        public float ChloPd { get; set; }
        public float ChloPreVolume { get; set; }
        public string ChloMicroCom1 { get; set; }
        public string ChloMicroCom2 { get; set; }
        public float ChloLastVolume { get; set; }
        public float ChloConcentration { get; set; }
        public string ChloRemark { get; set; }


    }
}
