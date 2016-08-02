using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    class Structure
    {
        //结构位置编号id
        public string StruId { get; set; }
        ////材质检测参数单ID
        public string MaterialComId { get; set; }
        //测点编号
        public string StruNumber { get; set; }
        //检测部位
        public string StruUnit { get; set; }
        //检测值
        public float StruValue { get; set; }
        //备注
        public string StruRemark { get; set; }

       
    }
}
