using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 构件各检测数据扣分表
    /// </summary>
    class DetectionDeduct
    {
        //构件各检测数据ID
        public string DetectionId { get; set; }
        ////病害最高标度
        public int DetectionMaxScale { get; set; }
        //病害指标标度
        public int DetectionCurScale { get; set; }
        //病害对应扣分值
        public float DetectionDeductValue { get; set; }
    }
}
