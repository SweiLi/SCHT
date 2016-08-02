using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 栏目模板信息
    /// </summary>
    class ColTemplate
    {
        public string ColTempId { get; set; }
        public string DocColId { get; set; }
        public string ColTempDesc { get; set; }
        public string ColTempName { get; set; }
        public string ColTempUrl { get; set; }
        public string ColTempFilename { get; set; }
        public string ColTempMan { get; set; }
        public DateTime ColTempTime { get; set; }

    }
}
