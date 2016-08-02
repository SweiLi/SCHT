using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.Config
{
    /// <summary>
    /// 数据库表字段与业务模型属性之间的关联对象
    /// </summary>
    public class FieldMatchItem
    {
        internal FieldMatchItem() { }
        public string FieldName { get; internal set; }
        public string FieldType { get; internal set; }
        public string PropertyName { get; internal set; }
        public string PropertyType { get; internal set; }
    }
}
