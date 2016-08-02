using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 自振频率检测表
    /// </summary>
    class Frequency
    {
        //自振频率id
        public string FrequencyId { get; set; }
        ////材质检测参数单据ID
        public string MaterialComId { get; set; }
        //测点单元
        public string FrequencyUnit { get; set; }
        //测点位置
        public string FrequencyPosition { get; set; }
        ////测点值
        public float FrequencyValue { get; set; }
        //基准值
        public float FrequencyStandardValue { get; set; }

        //备注
        public string FrequencyRemark { get; set; }
    }
}
