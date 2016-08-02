using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 混凝土强度检测表
    /// </summary>
    class ConstrengTest
    {
        public string ConStrengId { get; set; }
        public string BriChildrenId { get; set; }
        //试验依据
        public string MatComBase { get; set; }
        //工程部位/用途
        public string MatComName { get; set; }
        //委托/任务编号
        public string MatComMark { get; set; }
        //样品名称
        public string MatComSampleName { get; set; }
        //检测湿度
        public string MatComCheckHum { get; set; }
        //检测温度
        public string MatComCheckTem { get; set; }
        //主要设备及编号
        public string MatComMainDev { get; set; }
        //检测方法
        public string MatComTestName { get; set; }
        //样品编号
        public string MatComSampleId { get; set; }
        //样品描述
        public string MatComSampleDes { get; set; }
        //试验日期
        public DateTime MatComCheckDay { get; set; }
        //设计强度等级
        public string ConResistivityLevel { get; set; }
        //齿龄
        public string ConResistivityYear { get; set; }
        //泵送混凝土
        public string ConResistivityPump { get; set; }
        //审核人
        public string MatComTestMan { get; set; }
        //检测人
        public string MatComCheckMan { get; set; }


    }
}
