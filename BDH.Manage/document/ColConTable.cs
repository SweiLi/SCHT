using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 文档栏目表格信息表：表头内容，所属栏目，所对应表单名称，排序顺序，样式，对应表单文件URL。
    /// </summary>
    class ColConTable
    {
        public string ConTableId { get; set; }
        public string DocColId { get; set; }
        public string ConTableTitle { get; set; }
        public string ConTableFilename { get; set; }
        public string ConTableStyle { get; set; }
        public string ConTableUrl { get; set; }
        public int ConTableOrder { get; set; }

    }
}
