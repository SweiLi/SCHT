using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 材质检测参数单据通用表
    /// </summary>
    class MaterialCom
    {
        //材质检测参数单据ID
        public string MaterialComId { get; set; }
        ////子桥ID
        public string BridgeChildrenId { get; set; }
        //检测名称
        public string MaterialComTestName { get; set; }
        //试验依据
        public string MaterialComBase { get; set; }
        //工程部位/用途
        public string MaterialComName { get; set; }
        //委托/任务编号
        public string MaterialComMark { get; set; }
        ////样品名称
        public string MaterialComSampleName { get; set; }
        //检测湿度
        public string MaterialComCheckHum { get; set; }
        //检测温度
        public string MaterialComCheckTem { get; set; }
        //主要设备及编号
        public string MaterialComMainDev { get; set; }
        //样品编号
        public string MaterialComSampleId { get; set; }
        ////样品描述
        public string MateriaComSampleDes { get; set; }
        //试验日期
        public DateTime MaterialComCheckDay { get; set; }
        //检测人
        public string MaterialComTestMan { get; set; }
        //审核人
        public string MaterialComCheckMan { get; set; }
    }
}
