using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁部件表
    /// </summary>
    class BriCompAsse
    {
        //部件ID
        public string BridCompId { get; set; }
        //部件标度
        public int BridCompScale { get; set; }
        //部件描述
        public string BridCompDescribe { get; set; }
        //是否主要部件
        public int IsMain { get; set; }


    }
}
