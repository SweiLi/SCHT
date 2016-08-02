using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 病害统计结论表
    /// </summary>
    class DiseStatistics
    {

        //病害统计结论ID
        public string DiseStasId { get; set; }
        ////项目编号
        public string ProjectId { get; set; }
        //桥梁编号
        public string BridgeId { get; set; }
        //子桥编号
        public string SonBridgeId { get; set; }
        ////项目名称
        public string ProjectName { get; set; }
        //桥梁名称
        public string BridgeName { get; set; }

        //检测桥梁名称
        public string BriChildName { get; set; }
        ////部件名称
        public string StruName { get; set; }
        //构件名称
        public string CompName { get; set; }
        //病害类型
        public string DiseName { get; set; }
        ////病害标度
        public int DiseScale { get; set; }
        //病害总结
        public string DiseResult { get; set; }
        ////总结人
        public string ResultMan { get; set; }
        //总结时间
        public DateTime ResultTime { get; set; }
    }
}
