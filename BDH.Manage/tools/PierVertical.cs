using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥墩(索塔)垂直度检测项目表
    /// </summary>
    class PierVertical
    {
        //桥墩垂直检测ID
        public string PierVerticalId { get; set; }
        ////材质检测参数单据ID
        public string MaterialComId { get; set; }
        //墩台编号
        public string PierId { get; set; }
        //纵桥向垂直度检测小里程向检测位置上水平距离
        public float PierZsTDistance { get; set; }
        //纵桥向垂直度检测小里程向检测位置上高差
        public float PierZsTDip { get; set; }
        //纵桥向垂直度检测小里程向检测位置下水平距离
        public float PierZsBDistance { get; set; }
        //纵桥向垂直度检测小里程向检测位置下高差
        public float PierZsBDip { get; set; }
        ////纵桥向垂直度检测大里程向检测位置上水平距离
        public float PierZbTDistance { get; set; }
        //纵桥向垂直度检测大里程向检测位置上高差
        public float PierZbTDip { get; set; }
        //纵桥向垂直度检测大里程向检测位置下水平距离
        public float PierZbBDistance { get; set; }
        //纵桥向垂直度检测大里程向检测位置下高差
        public float PierZbBDip { get; set; }
        //横桥向垂直度检测左侧测点位置上水平距离
        public float PierHlTDistance { get; set; }
        //横桥向垂直度检测左侧测点位置上高差
        public float PierHlTDip { get; set; }
        ////横桥向垂直度检测左侧测点位置下水平距离
        public float PierHlBDistance { get; set; }
        //横桥向垂直度检测左侧测点位置下高差
        public float PierHlBDip { get; set; }
        //横桥向垂直度检测右侧测点位置上水平距离
        public float PierHrTDistance { get; set; }
        //横桥向垂直度检测右侧测点位置上高差
        public float PierHrTDip { get; set; }
        //横桥向垂直度检测右侧测点位置下水平距离
        public float PierHrBDistance { get; set; }
        //横桥向垂直度检测右侧测点位置下高差
        public float PierHrBDip { get; set; }
        ////备注
        public string PierRemark { get; set; }
        //检测位置名称（索塔、墩台）
        public string DetectUnitName { get; set; }
       

    }
}
