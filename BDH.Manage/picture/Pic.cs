using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 图片表
    /// </summary>
    class Pic
    {
        //图片ID
        public string PicId { get; set; }
        ////外观每日检测单据ID
        public string DailyAppId { get; set; }
        //子桥ID
        public string BridgeChildrenId { get; set; }
        //单据病害ID
        public string RecipDiseaseId { get; set; }
        //材质检测参数单据ID
        public string MaterialComId { get; set; }
        //混凝土强度检测ID
        public string ConStrengId { get; set; }
        //图集ID
        public string PicsId { get; set; }
        //图片名称
        public string PicName { get; set; }
        //上传人
        public string PicMan { get; set; }
        //图片标题
        public string PicTitle { get; set; }
        //图片描述
        public string PicDescribe { get; set; }
        //序号
        public string PicSerial { get; set; }
        //上传时间
        public DateTime PicTime { get; set; }
    }
}
