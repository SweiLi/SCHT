using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 钢筋保护层厚度
    /// </summary>
    class Rebar
    {
        //钢筋保护层厚度检测ID
        public string RebarPlyId { get; set; }
        ////材质检测参数单据ID
        public string MaterialPlyId { get; set; }
        //构件部位
        public string RebarPlyUinit { get; set; }
        //组件名称
        public string RebarPlyUinitName { get; set; }
        //钢筋类型
        public string RebarPlyType { get; set; }
        //设计保护层厚度值
        public float RebarPlyDesign { get; set; }
        //实际保护层厚度值
        public float RebarPlyFact { get; set; }
        //备注
        public string RebarPlyRemark { get; set; }
    }
}
