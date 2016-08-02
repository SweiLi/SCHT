using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace BDH.MdHelper
{
    public class MdConvertHelper
    {
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string ConvertBase(string value, int from, int to)
        {
            if (!MdConvertHelper.IsBaseNumber(from))
            {
                throw new ArgumentException("参数from只能是2,8,10,16四个值。");
            }
            if (!MdConvertHelper.IsBaseNumber(to))
            {
                throw new ArgumentException("参数to只能是2,8,10,16四个值。");
            }
            int value2 = Convert.ToInt32(value, from);
            string text = Convert.ToString(value2, to);
            if (to == 2)
            {
                switch (text.Length)
                {
                    case 3:
                        text = "00000" + text;
                        break;
                    case 4:
                        text = "0000" + text;
                        break;
                    case 5:
                        text = "000" + text;
                        break;
                    case 6:
                        text = "00" + text;
                        break;
                    case 7:
                        text = "0" + text;
                        break;
                }
            }
            return text;
        }
        /// <summary>
        /// 判断几进制
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <returns></returns>
        private static bool IsBaseNumber(int baseNumber)
        {
            return baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16;
        }
        /// <summary>
        /// 将string转换成byte[] 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }
        /// <summary>
        /// 使用指定字符集将string转换成byte[] 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        /// <summary>
        /// 将byte[]转换成string 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        /// <summary>
        /// 使用指定字符集将byte[]转换成string 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static int BytesToInt32(byte[] data)
        {
            if (data.Length < 4)
            {
                return 0;
            }
            int result = 0;
            if (data.Length >= 4)
            {
                byte[] array = new byte[4];
                Buffer.BlockCopy(data, 0, array, 0, 4);
                result = BitConverter.ToInt32(array, 0);
            }
            return result;
        }
        /// <summary>
        /// 将数据转换为整型 转换失败返回默认值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int ToInt32<T>(T data, int defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            int result;
            try
            {
                result = Convert.ToInt32(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static int ToInt32(string data, int defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            int result;
            if (int.TryParse(data, out result))
            {
                return result;
            }
            return defValue;
        }

        public static int ToInt32(object data, int defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            int result;
            try
            {
                result = Convert.ToInt32(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }
        /// <summary>
        /// 将数据转换为布尔类型 转换失败返回默认值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static bool ToBoolean<T>(T data, bool defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            bool result;
            try
            {
                result = Convert.ToBoolean(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static bool ToBoolean(string data, bool defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            bool result;
            if (bool.TryParse(data, out result))
            {
                return result;
            }
            return defValue;
        }

        public static bool ToBoolean(object data, bool defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            bool result;
            try
            {
                result = Convert.ToBoolean(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }
        /// <summary>
        /// 将数据转换为单精度浮点型 转换失败返回默认值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static float ToFloat<T>(T data, float defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            float result;
            try
            {
                result = Convert.ToSingle(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static float ToFloat(object data, float defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            float result;
            try
            {
                result = Convert.ToSingle(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static float ToFloat(string data, float defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            float result;
            if (float.TryParse(data, out result))
            {
                return result;
            }
            return defValue;
        }
        /// <summary>
        /// 将数据转换为双精度浮点型 转换失败返回默认值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static double ToDouble<T>(T data, double defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            double result;
            try
            {
                result = Convert.ToDouble(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static double ToDouble<T>(T data, int decimals, double defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            double result;
            try
            {
                result = Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static double ToDouble(object data, double defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            double result;
            try
            {
                result = Convert.ToDouble(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static double ToDouble(string data, double defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            double result;
            if (double.TryParse(data, out result))
            {
                return result;
            }
            return defValue;
        }

        public static double ToDouble(object data, int decimals, double defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            double result;
            try
            {
                result = Math.Round(Convert.ToDouble(data), decimals);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static double ToDouble(string data, int decimals, double defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            double value;
            if (double.TryParse(data, out value))
            {
                return Math.Round(value, decimals);
            }
            return defValue;
        }
        /// <summary>
        /// 将数据转换为指定类型 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object ConvertTo(object data, Type targetType)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return null;
            }
            Type type = data.GetType();
            if (targetType == type)
            {
                return data;
            }
            if ((targetType != typeof(Guid) && targetType != typeof(Guid?)) || type != typeof(string))
            {
                if (targetType.IsEnum)
                {
                    try
                    {
                        object result = Enum.Parse(targetType, data.ToString(), true);
                        return result;
                    }
                    catch
                    {
                        object result = Enum.ToObject(targetType, data);
                        return result;
                    }
                }
                if (targetType.IsGenericType)
                {
                    targetType = targetType.GetGenericArguments()[0];
                }
                return Convert.ChangeType(data, targetType);
            }
            if (string.IsNullOrEmpty(data.ToString()))
            {
                return null;
            }
            return new Guid(data.ToString());
        }

        public static T ConvertTo<T>(object data)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return default(T);
            }
            object obj = MdConvertHelper.ConvertTo(data, typeof(T));
            if (obj == null)
            {
                return default(T);
            }
            return (T)((object)obj);
        }

        public static decimal ToDecimal<T>(T data, decimal defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            decimal result;
            try
            {
                result = Convert.ToDecimal(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static decimal ToDecimal(object data, decimal defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            decimal result;
            try
            {
                result = Convert.ToDecimal(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static decimal ToDecimal(string data, decimal defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            decimal result;
            if (decimal.TryParse(data, out result))
            {
                return result;
            }
            return defValue;
        }
        /// <summary>
        /// 将数据转换为DateTime 转换失败返回 默认值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime<T>(T data, DateTime defValue) where T : class
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            DateTime result;
            try
            {
                result = Convert.ToDateTime(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static DateTime ToDateTime(object data, DateTime defValue)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }
            DateTime result;
            try
            {
                result = Convert.ToDateTime(data);
            }
            catch
            {
                result = defValue;
            }
            return result;
        }

        public static DateTime ToDateTime(string data, DateTime defValue)
        {
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }
            DateTime result;
            if (DateTime.TryParse(data, out result))
            {
                return result;
            }
            return defValue;
        }
        /// <summary>
        /// 转半角的函数(DBC case) 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertToSBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == ' ')
                {
                    array[i] = '\u3000';
                }
                else if (array[i] < '\u007f')
                {
                    array[i] += 'ﻠ';
                }
            }
            return new string(array);
        }

        public static string ConvertToDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '\u3000')
                {
                    array[i] = ' ';
                }
                else if (array[i] > '＀' && array[i] < '｟')
                {
                    array[i] -= 'ﻠ';
                }
            }
            return new string(array);
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<TResult> DataTableToList<TResult>(DataTable dt) where TResult : class, new()
        {
            //创建一个属性的列表
            var prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            var t = typeof(TResult);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            var oblist = new List<TResult>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                var ob = new TResult();
                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }
    }
}
