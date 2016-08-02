using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Manage
{
    /// <summary>
    /// 桥梁材质检测方法表
    /// </summary>
    class Material
    {
        //检测方法id
        public string FunId { get; set; }
        ////检测方法名字
        public string FunName { get; set; }
        //检测方法英文名字
        public string FunEname { get; set; }
        //检测方法函数名字
        public string FunFunName { get; set; }
        //材质类型
        public string MatType { get; set; }
        //描述
        public string FunDescribe { get; set; }
    }
}
