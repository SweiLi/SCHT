using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁下部结构表
    /// </summary>
    class BridgeXbjg
    {
        public string BriXbjgId { get; set; }
        public string BriJgjsId { get; set; }
        //材料
        public string BriXbjgMaterial { get; set; }
        //墩台
        public string BriXbjgDt { get; set; }
        //形式
        public string BriXbjgType { get; set; }
        //基础形式
        public string BriXbjgBasistype { get; set; }


    }
}
