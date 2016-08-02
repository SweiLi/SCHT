using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 病害标度等级表
    /// </summary>
    class DisScale
    {
        //编号
        public string ScaleId { get; set; }
        ////部件病害评定ID
        public string BcdiId { get; set; }
        //关键词
        public string ScaleKeys { get; set; }
        //取值最小值
        public float ScaleMin { get; set; }
        //取值最大值
        public float ScaleMax { get; set; }
        //单位
        public string ScaleUnit { get; set; }
        //连接符
        public int ConCon { get; set; }
    }
}
