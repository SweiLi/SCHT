using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace BDH.Config
{
    /// <summary>
    /// 对配置文件进行加解密
    /// </summary>
    class FileEncryptHelper
    {
        private string iv = "13579qwert";
        private string key = "sayhello";
        private byte[] desKey;
        private byte[] desIV;
        public FileEncryptHelper ()
        {
            desKey = Encoding.ASCII.GetBytes( key );
            desIV = Encoding.ASCII.GetBytes( iv );
        }

        /// <summary>
        /// 对字节数组进行加密
        /// </summary>
        /// <param name="data">源字节数组</param>
        /// <param name="desKey">KEY</param>
        /// <param name="desIV">IV</param>
        /// <returns>加密后的流</returns>
        private MemoryStream EncryptData ( byte[] data, byte[] desKey, byte[] desIV )
        {
            DES des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream encStream = new CryptoStream( ms, des.CreateEncryptor( desKey, desIV ), CryptoStreamMode.Write );
            encStream.Write( data, 0, data.Length );
            encStream.Close();

            return ms;
        }

        /// <summary>
        /// 对已加密的字节数组进行解密
        /// </summary>
        /// <param name="deData">已加密的字节数组</param>
        /// <param name="desKey">KEY</param>
        /// <param name="desIV">IV</param>
        /// <returns>已解密的字节数组</returns>
        private byte[] DecryptData ( byte[] deData, byte[] desKey, byte[] desIV )
        {
            DES des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream( deData );
            CryptoStream decStream = new CryptoStream( ms, des.CreateDecryptor( desKey, desIV ), CryptoStreamMode.Read );
            byte[] buff = new byte[1024 * 5];
            int ret = decStream.Read( buff, 0, buff.Length );
            decStream.Close();
            ms.Close();

            byte[] bs = new byte[ret];
            for ( int i = 0; i < ret; i++ )
                bs[i] = buff[i];

            return bs;
        }

        /// <summary>
        /// 对数据进行加密，并保存到目标文件
        /// </summary>
        /// <param name="sourceData">源数据数组</param>
        /// <param name="targetFile">目标文件</param>
        public void EncryptFile ( byte[] sourceData, string targetFile )
        {
            MemoryStream ms = this.EncryptData( sourceData, this.desKey, this.desIV );
            byte[] enBuff = ms.ToArray();
            ms.Close();

            using ( FileStream fsw = new FileStream( targetFile, FileMode.Create, FileAccess.Write ) )
            {
                fsw.Write( enBuff, 0, enBuff.Length );
                fsw.Close();
            }

        }

        /// <summary>
        /// 对源文件进行加密，保存到目标文件
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="targetFile">目标文件</param>
        public void EncryptFile ( string sourceFile, string targetFile )
        {
            using ( FileStream fs = new FileStream( sourceFile, FileMode.Open, FileAccess.Read ) )
            {
                byte[] buff = new byte[fs.Length];
                fs.Read( buff, 0, buff.Length );
                fs.Close();

                // Encrypt data
                MemoryStream ms = this.EncryptData( buff, this.desKey, this.desIV );
                byte[] enBuff = ms.ToArray();
                ms.Close();

                using ( FileStream fsw = new FileStream( targetFile, FileMode.Create, FileAccess.Write ) )
                {
                    fsw.Write( enBuff, 0, enBuff.Length );
                    fsw.Close();
                }
            }
        }

        /// <summary>
        /// 对已加密文件进行解密，返回数组
        /// </summary>
        /// <param name="encryptFile">已加密的源文件</param>
        /// <returns>字节数组</returns>
        public byte[] DecryptFile ( string encryptFile )
        {
            using ( FileStream fs = new FileStream( encryptFile, FileMode.Open, FileAccess.Read ) )
            {
                byte[] buff = new byte[fs.Length];
                fs.Read( buff, 0, buff.Length );
                fs.Close();

                byte[] deBuff = this.DecryptData( buff, this.desKey, this.desIV );

                return deBuff;
            }
        }

        /// <summary>
        /// 对已加密文件进行解密，并保存到目标文件
        /// </summary>
        /// <param name="encryptFile">已加密的源文件</param>
        /// <param name="targetFile">保存的目标文件</param>
        public void DecryptFile ( string encryptFile, string targetFile )
        {
            byte[] data = this.DecryptFile( encryptFile );

            using ( FileStream fsw = new FileStream( targetFile, FileMode.Create, FileAccess.Write ) )
            {
                fsw.Write( data, 0, data.Length );
                fsw.Close();
            }
        }
    }
}
