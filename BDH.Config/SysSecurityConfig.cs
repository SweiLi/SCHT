using System;
using System.IO;
using System.Xml.Linq;

namespace BDH.Config
{
    /// <summary>
    /// 系统安全配置类，负责对配置文档加解密，查询和保存系统相关设置
    /// </summary>
    public class SysSecurityConfig
    {
        private string CONFIG_FILE = AppDomain.CurrentDomain.BaseDirectory + "sys_security.xml";

        private XElement m_Root;
        public string SqlServerConnectionString { get; set; }
        public string SqliteConnectionString { get; set; }
        public string LogTableName { get; set; }
        public string LogTextFilePath { get; set; }
        public string DefaultDatabase { get; set; }

        public SysSecurityConfig ()
        {
            FileEncryptHelper encrypt = new FileEncryptHelper();
            if ( File.Exists( CONFIG_FILE ) )
            {
                //byte[] buff = encrypt.DecryptFile( CONFIG_FILE );
                // this.LoadConfig( buff );
                this.m_Root = XElement.Load(CONFIG_FILE);
                this.LoadConfig(null);
            }
        }

        private void LoadConfig ( byte[] buff )
        {
            //MemoryStream ms = new MemoryStream( buff );
            //this.m_Root = XElement.Load( ms );

            var first_eles = this.m_Root.Elements();
            foreach ( XElement first_ele in first_eles )
            {
                var second_eles = first_ele.Elements();
                foreach ( XElement second_ele in second_eles )
                {
                    if (second_ele.Name.LocalName.Equals("sqlserver"))
                        this.SqlServerConnectionString = second_ele.Value;
                    else if (second_ele.Name.LocalName.Equals("sqlite"))
                        this.SqliteConnectionString = second_ele.Value;
                    else if (second_ele.Name.LocalName.Equals("table"))
                        this.LogTableName = second_ele.Value;
                    else if (second_ele.Name.LocalName.Equals("text"))
                        this.LogTextFilePath = second_ele.Value;
                    else if (second_ele.Name.LocalName.Equals("default"))
                        this.DefaultDatabase = second_ele.Value;
                }
            }
        }

        public void Save ()
        {
            var first_eles = this.m_Root.Elements();
            foreach ( XElement first_ele in first_eles )
            {
                var second_eles = first_ele.Elements();
                foreach ( XElement second_ele in second_eles )
                {
                    if (second_ele.Name.LocalName.Equals("sqlserver"))
                        second_ele.Value = this.SqlServerConnectionString;
                    else if (second_ele.Name.LocalName.Equals("sqlite"))
                        second_ele.Value = this.SqliteConnectionString;
                    else if (second_ele.Name.LocalName.Equals("table"))
                        second_ele.Value = this.LogTableName;
                    else if (second_ele.Name.LocalName.Equals("text"))
                        second_ele.Value = this.LogTextFilePath;
                    else if (second_ele.Name.LocalName.Equals("default"))
                        second_ele.Value = this.DefaultDatabase;
                }
            }

            MemoryStream ms = new MemoryStream();
            this.m_Root.Save( ms, SaveOptions.DisableFormatting );
            FileEncryptHelper encrypt = new FileEncryptHelper();
            encrypt.EncryptFile( ms.ToArray(), CONFIG_FILE );
            ms.Close();
        }
    }
}
