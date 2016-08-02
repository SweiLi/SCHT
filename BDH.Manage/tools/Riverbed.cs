using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 河床下切检测项目
    /// </summary>
    class RiverBed
    {
        //河床下切检测项目ID
        public string RiverBedItemId { get; set; }
        ////材质检测参数单据ID
        public string MaterialComId { get; set; }
        //桥跨或墩号
        public string RiverBedSpanPierCode { get; set; }
        //测点位置
        public string RiverBedDetectUnit { get; set; }
        //缺河床距参考点位置深度（m）
        public float RiverBedRpDepth { get; set; }
        //位置方向(上游侧、下游侧)
        public string RiverBedDorect { get; set; }
        //备注
        public string RiverBedRemark { get; set; }
      
    }
}
