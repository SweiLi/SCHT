using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    ///桥梁结构权重信息
    /// </summary>
    class BriStructureWeight
    {
        //结构权重ID
        public string StructureWeightId { get; set; }
        //结构名称(上部、下部、桥面系)
        public int StructureName { get; set; }
        //结构权重
        public float StructureWeight { get; set; }

    }
}
