using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 图集信息表
    /// </summary>
    class Pics
    {
        //图集ID
        public string PicsId { get; set; }
        ////图集信_图集ID
        public string MdtPicsId { get; set; }
        //图集名称
        public string PicsName { get; set; }
        //图集标题
        public string PicsTitle { get; set; }
        //图集描述
        public string PicsDescribe { get; set; }
        //图集URL
        public string PicsUrl { get; set; }
        //图集新建日期
        public DateTime PicsTime { get; set; }
        //图集新建人
        public string PicsMan { get; set; }
    }
}
