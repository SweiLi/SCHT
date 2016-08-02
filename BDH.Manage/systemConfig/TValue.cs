using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁技术状况评定t值表
    /// </summary>
    class TValue
    {
        //桥梁技术状况评定t值ID
        public string TValueId { get; set; }
        //构件数量
        public int CompnonetAmount { get; set; }
        //t值
        public float Tvalue { get; set; }
        
    }
}
