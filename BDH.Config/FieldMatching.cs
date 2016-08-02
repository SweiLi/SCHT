using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BDH.Config
{
    /// <summary>
    /// 数据库表与业务模型之间的关联对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FieldMatching<T>
    {
        private static readonly string CONFIG_FILE = 
            AppDomain.CurrentDomain.BaseDirectory + "fieldmatch.xml";
        private static XElement root = null;

        public string TableName { get; private set; }
        public string ObjectName { get; private set; }
        public List<FieldMatchItem> Items { get; private set; }

        public static FieldMatching<T> Create()
        {
            if (root == null) root = XElement.Load(CONFIG_FILE);

            Type type = typeof(T);
            //var tt = root.Elements("match");
            var query_fm = from item_fm in root.Elements("match")
                        where item_fm.Attribute("objname").Value.Equals(type.Name)
                        select item_fm;
            foreach (var fm in query_fm)
            {
                FieldMatching<T> fm_inst = new FieldMatching<T>();
                fm_inst.Items = new List<FieldMatchItem>();

                fm_inst.TableName = fm.Attribute("tblname").Value;
                fm_inst.ObjectName = fm.Attribute("objname").Value;

                var query_item = from item in fm.Elements() select item;
                foreach (var item in query_item)
                {
                    FieldMatchItem fmi = new FieldMatchItem();
                    fmi.FieldName = item.Attribute("field").Value;
                    fmi.FieldType = item.Attribute("fieldtype").Value;
                    fmi.PropertyName = item.Attribute("property").Value;
                    fmi.PropertyType = item.Attribute("propertytype").Value;
                    fm_inst.Items.Add(fmi);
                }

                return fm_inst;
            }

            return null;
        }

        public FieldMatchItem GetItemByField(string field_name)
        {
            foreach (var item in this.Items)
            {
                if (item.FieldName.Equals(field_name)) return item;
            }

            return null;
        }

        public FieldMatchItem GetItemByProperty(string property_name)
        {
            foreach (var item in this.Items)
            {
                if (item.PropertyName.Equals(property_name)) return item;
            }

            return null;
        }
    }
}
