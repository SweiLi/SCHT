using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 定性定量描述扩展表
    /// </summary>
    class QuantifyExt
    {
        //定量ID
        public string QuantifyExtId { get; set; }
        ////单据病害ID
        public string RecipDiseaseId { get; set; }
        //关键字
        public string QuantifyExtAddKey { get; set; }
        //关键值
        public string QuantifyExtAddValue { get; set; }
    }
}
