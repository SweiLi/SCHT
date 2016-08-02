using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁索力检测
    /// </summary>
    class Cableforce
    {
        public string CableforceItemId { get; set; }
        public string MaterialComId { get; set; }
        //检测位置
        public string CableforceDUnit { get; set; }
        //检测值
        public float CableforceDValue { get; set; }

    }
}
