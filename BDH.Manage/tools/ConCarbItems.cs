using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 混凝土碳化深度检测项目数据表
    /// </summary>
    class ConCarbItems
    {
        public string ConCarbItemId { get; set; }
        public string MaterialComId { get; set; }
        public string ConCarbUnitName { get; set; }
        public string ConCarbCkptId { get; set; }
        //检测点1碳化深度
        public float ConCarbCkpt1 { get; set; }

        public float ConCarbCkpt2 { get; set; }
        public float ConCarbCkpt3 { get; set; }
        public string ConCarbRemark { get; set; }

    }
}
