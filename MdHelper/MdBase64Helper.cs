using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDH.MdHelper
{
    class MdBase64Helper
    {
        protected static MdBase64Helper SB64 = new MdBase64Helper();

        protected string MCodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZbacdefghijklmnopqrstu_wxyz0123456789*-";

        protected string MPad = "v";

        protected Dictionary<int, char> MT1 = new Dictionary<int, char>();

        protected Dictionary<char, int> MT2 = new Dictionary<char, int>();
        /// <summary>
        /// 设置并验证密码表合法性 
        /// </summary>
        public string CodeTable
        {
            get
            {
                return this.MCodeTable;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表不能为null");
                }
                if (value.Length < 64)
                {
                    throw new Exception("密码表长度必须至少为64");
                }
                this.ValidateRepeat(value);
                this.ValidateEqualPad(value, this.MPad);
                this.MCodeTable = value;
                this.InitDict();
            }
        }
        /// <summary>
        /// 设置并验证补码合法性 
        /// </summary>
        public string Pad
        {
            get
            {
                return this.MPad;
            }
            set
            {
                if (value == null)
                {
                    throw new Exception("密码表的补码不能为null");
                }
                if (value.Length != 1)
                {
                    throw new Exception("密码表的补码长度必须为1");
                }
                this.ValidateEqualPad(this.MCodeTable, value);
                this.MPad = value;
                this.InitDict();
            }
        }

        public MdBase64Helper()
        {
            this.InitDict();
        }
        /// <summary>
        /// 获取具有标准的Base64密码表的加密类 
        /// </summary>
        /// <returns></returns>
        public static MdBase64Helper GetStandardBase64()
        {
            return new MdBase64Helper
            {
                Pad = "=",
                CodeTable = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
            };
        }
        /// <summary>
        /// 使用默认的密码表（双向哈西字典）加密字符串 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return MdBase64Helper.SB64.Encode(input);
        }
        /// <summary>
        /// 使用默认的密码表（双向哈西字典）解密字符串 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            return MdBase64Helper.SB64.Decode(input);
        }

        protected string Encode(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            StringBuilder stringBuilder = new StringBuilder();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            int num = bytes.Length % 3;
            int num2 = 3 - num;
            if (num != 0)
            {
                Array.Resize<byte>(ref bytes, bytes.Length + num2);
            }
            int num3 = (int)Math.Ceiling((double)bytes.Length * 1.0 / 3.0);
            for (int i = 0; i < num3; i++)
            {
                stringBuilder.Append(this.EncodeUnit(new byte[]
                {
                    bytes[i * 3],
                    bytes[i * 3 + 1],
                    bytes[i * 3 + 2]
                }));
            }
            if (num != 0)
            {
                stringBuilder.Remove(stringBuilder.Length - num2, num2);
                for (int j = 0; j < num2; j++)
                {
                    stringBuilder.Append(this.MPad);
                }
            }
            return stringBuilder.ToString();
        }

        protected string EncodeUnit(params byte[] unit)
        {
            int[] array = new int[]
            {
                (unit[0] & 252) >> 2,
                ((int)(unit[0] & 3) << 4) + ((unit[1] & 240) >> 4),
                ((int)(unit[1] & 15) << 2) + ((unit[2] & 192) >> 6),
                (int)(unit[2] & 63)
            };
            StringBuilder stringBuilder = new StringBuilder();
            int[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                int code = array2[i];
                stringBuilder.Append(this.GetEC(code));
            }
            return stringBuilder.ToString();
        }

        protected char GetEC(int code)
        {
            return this.MT1[code];
        }

        protected string Decode(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return "";
            }
            List<byte> list = new List<byte>();
            char[] array = source.ToCharArray();
            int num = array.Length % 4;
            if (num != 0)
            {
                Array.Resize<char>(ref array, array.Length - num);
            }
            int num2 = source.IndexOf(this.MPad);
            if (num2 != -1)
            {
                num2 = source.Length - num2;
            }
            int num3 = array.Length / 4;
            for (int i = 0; i < num3; i++)
            {
                this.DecodeUnit(list, new char[]
                {
                    array[i * 4],
                    array[i * 4 + 1],
                    array[i * 4 + 2],
                    array[i * 4 + 3]
                });
            }
            for (int j = 0; j < num2; j++)
            {
                list.RemoveAt(list.Count - 1);
            }
            return Encoding.UTF8.GetString(list.ToArray());
        }

        protected void DecodeUnit(List<byte> byteArr, params char[] chArray)
        {
            int[] array = new int[3];
            byte[] array2 = new byte[chArray.Length];
            for (int i = 0; i < chArray.Length; i++)
            {
                array2[i] = this.FindChar(chArray[i]);
            }
            array[0] = ((int)array2[0] << 2) + ((array2[1] & 48) >> 4);
            array[1] = ((int)(array2[1] & 15) << 4) + ((array2[2] & 60) >> 2);
            array[2] = ((int)(array2[2] & 3) << 6) + (int)array2[3];
            byteArr.AddRange(from t in array
                             select (byte)t);
        }

        protected byte FindChar(char ch)
        {
            int num = this.MT2[ch];
            return (byte)num;
        }

        protected void InitDict()
        {
            this.MT1.Clear();
            this.MT2.Clear();
            this.MT2.Add(this.MPad[0], -1);
            for (int i = 0; i < this.MCodeTable.Length; i++)
            {
                this.MT1.Add(i, this.MCodeTable[i]);
                this.MT2.Add(this.MCodeTable[i], i);
            }
        }

        protected void ValidateRepeat(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.LastIndexOf(input[i]) > i)
                {
                    throw new Exception("密码表中含有重复字符：" + input[i]);
                }
            }
        }

        protected void ValidateEqualPad(string input, string pad)
        {
            if (input.IndexOf(pad) > -1)
            {
                throw new Exception("密码表中包含了补码字符：" + pad);
            }
        }

        protected void Test()
        {
            this.InitDict();
            string text = this.Encode("false");
            string b = this.Decode(text);
            Console.WriteLine(text);
            Console.WriteLine("abc ABC 你好！◎＃￥％……!@#$%^" == b);
        }
    }
}
