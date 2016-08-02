using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 水准测量
    /// </summary>
    class LevellingProcess
    {

        //水准测量编号id
        public string LevelProId { get; set; }
        ////材质检测参数单据ID
        public string MaterialComId { get; set; }
        //桩号/位置
        public string LevelProUnit { get; set; }
        //水准尺后视读数
        public float LevelProBack { get; set; }
        ////水准尺前视读数
        public float LevelProBefore { get; set; }
        //高差（+）
        public float LevelProHigh { get; set; }

        //高差（-）
        public float LevelProLow { get; set; }
        ////改正数
        public float LevelProRecity { get; set; }
        //改正后高差
        public float LevelProRecityHigh { get; set; }
        //高层
        public float LevelProTop { get; set; }
        ////距离
        public float LevelProDistance { get; set; }
        //备注
        public string LevelProRecityRemark { get; set; }
    }
}
