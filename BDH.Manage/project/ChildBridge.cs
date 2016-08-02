using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 子桥信息表
    /// </summary>
    public class ChildBridge
    {
        //子桥梁ID
        public string Id { get; set; }
        //桥梁ID
        public string ParentId { get; set; }
        //桥梁类别ID
        public string TypeId { get; set; }
        //子桥名称
        public string Name { get; set; }
        //子桥描述
        public string Description { get; set; }
        //子桥桩号
        public string PileId { get; set; }
    }
}
