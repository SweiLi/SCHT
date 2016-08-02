using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using BDH.Config;

namespace BDH.MdHelper
{
    public class MdDataTableHelper<T> where T: new()
    {
        public static IList<T> ConvertToModel(DataTable dt)
        {
            FieldMatching<T> fm = FieldMatching<T>.Create();

            IList<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            PropertyInfo[] propertys = type.GetProperties();//t.GetType().GetProperties();// 获得此模型的公共属性
            
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    var fm_item = fm.GetItemByProperty(pi.Name);

                    if (fm_item != null && dt.Columns.Contains(fm_item.FieldName))
                    {
                        if (pi.CanWrite)
                        {
                            object val = dr[fm_item.FieldName];
                            //if (fm_item.FieldName.Equals("pri_sign_date"))
                            //    Console.WriteLine(val.ToString());
                            if (val != null && val != DBNull.Value) pi.SetValue(t, val, null);
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
