using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 用户图片信息表
    /// </summary>
    class UserPics
    {
        //用户图片ID
        public string UpicId { get; set; }
        //用户ID
        public string UId { get; set; }
        //图片Url
        public string UpicUrl { get; set; }
        //图片标题
        public string UpicTitle { get; set; }
        //图片描述
        public string UpicDescribe { get; set; }

      
    }
}
