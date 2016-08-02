using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 混凝土电阻率单测试数据项目表
    /// </summary>
    class ConstrResistItem
    {
        public string ConResistItemId { get; set; }
        public string MaterialComId { get; set; }
        public string ResistZone { get; set; }
        public string ResistValue { get; set; }
        public string ResistTemp { get; set; }

        public string ResistRemark { get; set; }

    }
}
