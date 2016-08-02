using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁总体技术状况评定描述表
    /// </summary>
    class BriWholeAsse
    {
        //id
        public string WholeAsseId { get; set; }
        //标度
        public int WholeScale { get; set; }
        //描述
        public string WholeDescribe { get; set; }
        //建议采取相关措施
        public string WholeMeasure { get; set; }

    }
}
