using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁日外观病害检测单实体类
    /// </summary>
    class DailyOutDisease
    {
        //单据病害ID
        public string RecipDiseId { get; set; }
        ////外观每日检测单据ID
        public string DailyAppId { get; set; }
        //缺陷位置
        public string DisePosition { get; set; }
        //缺陷类型
        public string DiseType { get; set; }
        //缺陷情况描述
        public string DiseDescribe { get; set; }
        //备注
        public string DiseRemark { get; set; }


    }
}
