using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BDH.MdHelper
{
    public class MdXmlHelper
    {
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string Read(string path, string node, string attribute)
        {
            string result = "";
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlNode xmlNode = xmlDocument.SelectSingleNode(node);
                if (xmlNode != null && xmlNode.Attributes != null)
                {
                    result = (attribute.Equals("") ? xmlNode.InnerText : xmlNode.Attributes[attribute].Value);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlNode xmlNode = xmlDocument.SelectSingleNode(node);
            if (element.Equals(""))
            {
                if (!attribute.Equals(""))
                {
                    XmlElement xmlElement = (XmlElement)xmlNode;
                    if (xmlElement != null)
                    {
                        xmlElement.SetAttribute(attribute, value);
                    }
                }
            }
            else
            {
                XmlElement xmlElement2 = xmlDocument.CreateElement(element);
                if (attribute.Equals(""))
                {
                    xmlElement2.InnerText = value;
                }
                else
                {
                    xmlElement2.SetAttribute(attribute, value);
                }
                if (xmlNode != null)
                {
                    xmlNode.AppendChild(xmlElement2);
                }
            }
            xmlDocument.Save(path);
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        public static void Update(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlNode xmlNode = xmlDocument.SelectSingleNode(node);
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (attribute.Equals(""))
                {
                    if (xmlElement != null)
                    {
                        xmlElement.InnerText = value;
                    }
                }
                else if (xmlElement != null)
                {
                    xmlElement.SetAttribute(attribute, value);
                }
                xmlDocument.Save(path);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(path);
                XmlNode xmlNode = xmlDocument.SelectSingleNode(node);
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (attribute.Equals(""))
                {
                    if (xmlNode != null && xmlNode.ParentNode != null)
                    {
                        xmlNode.ParentNode.RemoveChild(xmlNode);
                    }
                }
                else if (xmlElement != null)
                {
                    xmlElement.RemoveAttribute(attribute);
                }
                xmlDocument.Save(path);
            }
            catch
            {
            }
        }
    }
}
