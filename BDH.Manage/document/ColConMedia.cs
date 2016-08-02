using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 文档栏目媒体信息表，文档栏目媒体信息表：媒体内容，所属栏目，媒体标题，媒体名称，排序顺序，样式，媒体URL，媒体展现图
    /// </summary>
    class ColConMedia
    {

        public string ConMediaId { get; set; }
        public string DocColId { get; set; }
        public string ConMediaTitle { get; set; }
        public string ConMediaFilename { get; set; }
        public string ConMediaDescr { get; set; }
        public string ConMediaUrl { get; set; }
        public int ConMediaOrder { get; set; }
        public string ConMediaShowtype { get; set; }
        public string ConMediaPicUrl { get; set; }


    }
}
