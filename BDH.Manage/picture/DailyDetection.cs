using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁日外观检测单
    /// </summary>
    class DailyDetection
    {
        //外观每日检测单据ID
        public string DailyAppId { get; set; }
        //子桥ID
        public string BridgeChildrenId { get; set; }
        //工程部位/用途
        public string DailyProPlace { get; set; }
        //委托/任务编号
        public string DailyTaskId { get; set; }
        //试验依据
        public string DailyExpBase { get; set; }
        //样品编号
        public string DailySampleId { get; set; }
        //样品名称
        public string DailySampleName { get; set; }
        //样品描述
        public string DailySampleDes { get; set; }
        //检测温度
        public string DailyTemperature { get; set; }
        //检测湿度
        public string DailyHumidness { get; set; }
        //实验日期
        public DateTime DailyExpDay { get; set; }
        //主要设备及编号
        public string DailyMainDevice { get; set; }
        //检测人
        public string DailyExpMan { get; set; }
        //复核人
        public string DailyAuditor { get; set; }
        //备注信息
        public string DailyRemark { get; set; }
    }
}
