using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 文档栏目文字内容信息表：文字内容，所属栏目，排序顺序，样式
    /// </summary>
    class ColConText
    {
        /// <summary>
        /// 栏目内容文字信息表
        /// </summary>
        public string ConTextId { get; set; }
        public string DocColId { get; set; }
        public string ConText { get; set; }
        public string ConTextStyle { get; set; }
        public int ConTextOrder { get; set; }

    }
}
