using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 病害检测定量描述信息表
    /// </summary>
    class QuantifyInfo
    {
        //病害定量描述ID
        public string QuantifyInfoId { get; set; }
        ////单据病害ID
        public string RecipDiseaseId { get; set; }
        //定量描述关键字
        public string QuantifyKey { get; set; }
        //单位
        public string QuantifyUnit { get; set; }
        //定量结果
        public float QuantifyValue { get; set; }
        //定性描述
        public string QuantifyDescribe { get; set; }
    }
}
