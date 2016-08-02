using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 处理图片表
    /// </summary>
    class DealPic
    {
        //处理图片ID
        public string DealPicId { get; set; }
        //单据病害ID
        public string RecipDiseaseId { get; set; }
        //处理图片标题
        public string DealPicName { get; set; }
        //处理图片描述
        public string DealPicDiscribe { get; set; }
        //处理图片样式
        public int DealPicStyle { get; set; }
        //图片类型
        public string DealPicType { get; set; }
        //图片URL
        public string DealPicUrl { get; set; }
        //处理图片的数量
        public int DealPicNum { get; set; }
    }
}
