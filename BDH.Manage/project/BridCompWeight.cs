using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁组件权重信息
    /// </summary>
    class BridCompWeight
    {

        public string CompWeightId { get; set; }
        //桥梁类型
        public string BridgerType { get; set; }
        //桥梁结构
        public string BridgeStructure { get; set; }
        //类别i
        public int TypeI { get; set; }
        //评价部件
        public string AssesComp { get; set; }
        //组件权重
        public float CompWeight { get; set; }


    }
}
