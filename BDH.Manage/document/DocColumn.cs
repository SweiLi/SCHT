using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 文档栏目信息
    /// </summary>
    class DocColumn
    {
        public string DocColId { get; set; }
        public string DocTempId { get; set; }
        public string DocColName { get; set; }
        public string DocColLevel { get; set; }
        public int DocColOrder { get; set; }
        public string DocColParentid { get; set; }

    }
}
