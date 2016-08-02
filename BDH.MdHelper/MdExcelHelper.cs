using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace BDH.MdHelper
{
    public class MdExcelHelper
    {
        public enum ExcelType
        {
            Excel2003,
            Excel2007
        }

        public enum IMEXType
        {
            ExportMode,
            ImportMode,
            LinkedMode
        }
        /// <summary>
        /// 返回Excel 连接字符串 [IMEX=1] 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="header"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static string GetExcelConnectstring(string excelPath, bool header, MdExcelHelper.ExcelType eType)
        {
            return MdExcelHelper.GetExcelConnectstring(excelPath, header, eType, MdExcelHelper.IMEXType.ImportMode);
        }
        /// <summary>
        /// 返回Excel 连接字符串
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="header"></param>
        /// <param name="eType"></param>
        /// <param name="imex"></param>
        /// <returns></returns>
        public static string GetExcelConnectstring(string excelPath, bool header, MdExcelHelper.ExcelType eType, MdExcelHelper.IMEXType imex)
        {
            if (!MdFileHelper.IsExistFile(excelPath))
            {
                throw new FileNotFoundException("Excel路径不存在!");
            }
            string text = "NO";
            if (header)
            {
                text = "YES";
            }
            string result;
            if (eType == MdExcelHelper.ExcelType.Excel2003)
            {
                result = string.Concat(new object[]
                {
                    "Provider=Microsoft.Jet.OleDb.4.0; data source=",
                    excelPath,
                    ";Extended Properties='Excel 8.0; HDR=",
                    text,
                    "; IMEX=",
                    imex.GetHashCode(),
                    "'"
                });
            }
            else
            {
                result = string.Concat(new object[]
                {
                    "Provider=Microsoft.ACE.OLEDB.12.0; data source=",
                    excelPath,
                    ";Extended Properties='Excel 12.0 Xml; HDR=",
                    text,
                    "; IMEX=",
                    imex.GetHashCode(),
                    "'"
                });
            }
            return result;
        }
        /// <summary>
        /// 返回Excel工作表名 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(string excelPath, MdExcelHelper.ExcelType eType)
        {
            string excelConnectstring = MdExcelHelper.GetExcelConnectstring(excelPath, true, eType);
            return MdExcelHelper.GetExcelTablesName(excelConnectstring);
        }
        /// <summary>
        /// 返回Excel工作表名 
        /// </summary>
        /// <param name="connectstring"></param>
        /// <returns></returns>
        public static List<string> GetExcelTablesName(string connectstring)
        {
            List<string> excelTablesName;
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
            {
                excelTablesName = MdExcelHelper.GetExcelTablesName(oleDbConnection);
            }
            return excelTablesName;
        }

        public static List<string> GetExcelTablesName(OleDbConnection connection)
        {
            List<string> list = new List<string>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (oleDbSchemaTable != null && oleDbSchemaTable.Rows.Count > 0)
            {
                for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
                {
                    list.Add(MdConvertHelper.ConvertTo<string>(oleDbSchemaTable.Rows[i][2]));
                }
            }
            return list;
        }
        /// <summary>
        /// 返回Excel第一个工作表表名 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static string GetExcelFirstTableName(string excelPath, MdExcelHelper.ExcelType eType)
        {
            string excelConnectstring = MdExcelHelper.GetExcelConnectstring(excelPath, true, eType);
            return MdExcelHelper.GetExcelFirstTableName(excelConnectstring);
        }

        public static string GetExcelFirstTableName(string connectstring)
        {
            string excelFirstTableName;
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
            {
                excelFirstTableName = MdExcelHelper.GetExcelFirstTableName(oleDbConnection);
            }
            return excelFirstTableName;
        }

        public static string GetExcelFirstTableName(OleDbConnection connection)
        {
            string result = string.Empty;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (oleDbSchemaTable != null && oleDbSchemaTable.Rows.Count > 0)
            {
                result = MdConvertHelper.ConvertTo<string>(oleDbSchemaTable.Rows[0][2]);
            }
            return result;
        }
        /// <summary>
        /// 获取Excel文件中指定工作表的列 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="eType"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<string> GetColumnsList(string excelPath, MdExcelHelper.ExcelType eType, string table)
        {
            string excelConnectstring = MdExcelHelper.GetExcelConnectstring(excelPath, true, eType);
            DataTable readerSchema;
            using (OleDbConnection oleDbConnection = new OleDbConnection(excelConnectstring))
            {
                oleDbConnection.Open();
                readerSchema = MdExcelHelper.GetReaderSchema(table, oleDbConnection);
            }
            return (from DataRow dr in readerSchema.Rows
                    let columnName = dr["ColumnName"].ToString()
                    let datatype = ((OleDbType)dr["ProviderType"]).ToString()
                    let netType = dr["DataType"].ToString()
                    select columnName).ToList<string>();
        }
        /// <summary>
        /// 获取数据表中
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static DataTable GetReaderSchema(string tableName, OleDbConnection connection)
        {
            DataTable schemaTable;
            using (IDataReader dataReader = ((IDbCommand)new OleDbCommand
            {
                CommandText = string.Format("select * from [{0}]", tableName),
                Connection = connection
            }).ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
            {
                schemaTable = dataReader.GetSchemaTable();
            }
            return schemaTable;
        }
        /// <summary>
        /// EXCEL导入DataSet 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="table"></param>
        /// <param name="header"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelPath, string table, bool header, MdExcelHelper.ExcelType eType)
        {
            string excelConnectstring = MdExcelHelper.GetExcelConnectstring(excelPath, header, eType);
            return MdExcelHelper.ExcelToDataSet(excelConnectstring, table);
        }
        /// <summary>
        /// 判断是否存在该数据表
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private static bool IsExistExcelTableName(OleDbConnection connection, string table)
        {
            List<string> excelTablesName = MdExcelHelper.GetExcelTablesName(connection);
            return excelTablesName.Any((string tName) => tName.ToLower() == table.ToLower());
        }
        /// <summary>
        /// EXCEL导入DataSet 
        /// </summary>
        /// <param name="connectstring"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string connectstring, string table)
        {
            DataSet result;
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
            {
                DataSet dataSet = new DataSet();
                if (MdExcelHelper.IsExistExcelTableName(oleDbConnection, table))
                {
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + table + "]", oleDbConnection);
                    oleDbDataAdapter.Fill(dataSet, table);
                }
                result = dataSet;
            }
            return result;
        }
        /// <summary>
        /// EXCEL所有工作表导入DataSet 
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="header"></param>
        /// <param name="eType"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelPath, bool header, MdExcelHelper.ExcelType eType)
        {
            string excelConnectstring = MdExcelHelper.GetExcelConnectstring(excelPath, header, eType);
            return MdExcelHelper.ExcelToDataSet(excelConnectstring);
        }
        /// <summary>
        /// EXCEL所有工作表导入DataSet 
        /// </summary>
        /// <param name="connectstring"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string connectstring)
        {
            DataSet result;
            using (OleDbConnection oleDbConnection = new OleDbConnection(connectstring))
            {
                DataSet dataSet = new DataSet();
                List<string> excelTablesName = MdExcelHelper.GetExcelTablesName(oleDbConnection);
                foreach (string current in excelTablesName)
                {
                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT * FROM [" + current + "]", oleDbConnection);
                    oleDbDataAdapter.Fill(dataSet, current);
                }
                result = dataSet;
            }
            return result;
        }
        /// <summary>
        /// 把一个数据集中的数据导出到Excel文件中(XML格式操作) 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fileName"></param>
        public static void DataSetToExcel(DataSet source, string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName);
            int num = 1;
            streamWriter.Write("<xml version>\r\n<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n xmlns:x=\"urn:schemas-    microsoft-com:office:excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\r\n <Styles>\r\n <Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n <Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>\r\n <Protection/>\r\n </Style>\r\n <Style ss:ID=\"BoldColumn\">\r\n <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n <Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat ss:Format=\"@\"/>\r\n </Style>\r\n <Style ss:ID=\"Decimal\">\r\n <NumberFormat ss:Format=\"#,##0.###\"/>\r\n </Style>\r\n <Style ss:ID=\"Integer\">\r\n <NumberFormat ss:Format=\"0\"/>\r\n </Style>\r\n <Style ss:ID=\"DateLiteral\">\r\n <NumberFormat ss:Format=\"yyyy-mm-dd;@\"/>\r\n </Style>\r\n </Styles>\r\n ");
            for (int i = 0; i < source.Tables.Count; i++)
            {
                int num2 = 0;
                DataTable dataTable = source.Tables[i];
                streamWriter.Write("<Worksheet ss:Name=\"Sheet" + num + "\">");
                streamWriter.Write("<Table>");
                streamWriter.Write("<Row>");
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    streamWriter.Write("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                    streamWriter.Write(source.Tables[0].Columns[j].ColumnName);
                    streamWriter.Write("</Data></Cell>");
                }
                streamWriter.Write("</Row>");
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    num2++;
                    if (num2 == 64000)
                    {
                        num2 = 0;
                        num++;
                        streamWriter.Write("</Table>");
                        streamWriter.Write(" </Worksheet>");
                        streamWriter.Write("<Worksheet ss:Name=\"Sheet" + num + "\">");
                        streamWriter.Write("<Table>");
                    }
                    streamWriter.Write("<Row>");
                    int k = 0;
                    while (k < source.Tables[0].Columns.Count)
                    {
                        Type type = dataRow[k].GetType();
                        string key;
                        if ((key = type.ToString()) != null)
                        {
                            Dictionary<String, int> dic = new Dictionary<string, int>(10){
                               { "System.String", 0},{"System.DateTime", 1},{ "System.Boolean",2},{ "System.Int16",3},{"System.Int32",4},{"System.Int64",5},{ "System.Byte",6},{"System.Decimal",7},{"System.Double",8},{"System.DBNull",9}
                            };

                        int num3;
                        if (dic.TryGetValue(key, out num3))
						{
                            switch (num3)
                            {
                                case 0:
                                    {
                                        string text = dataRow[k].ToString();
                                        text = text.Trim();
                                        text = text.Replace("&", "&");
                                        text = text.Replace(">", ">");
                                        text = text.Replace("<", "<");
                                        streamWriter.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                                        streamWriter.Write(text);
                                        streamWriter.Write("</Data></Cell>");
                                        break;
                                    }
                                case 1:
                                    {
                                        DateTime dateTime = (DateTime)dataRow[k];
                                        string value = string.Concat(new object[]
                                        {
                                                    dateTime.Year,
                                                    "-",
                                                    (dateTime.Month < 10) ? ("0" + dateTime.Month) : dateTime.Month.ToString(),
                                                    "-",
                                                    (dateTime.Day < 10) ? ("0" + dateTime.Day) : dateTime.Day.ToString(),
                                                    "T",
                                                    (dateTime.Hour < 10) ? ("0" + dateTime.Hour) : dateTime.Hour.ToString(),
                                                    ":",
                                                    (dateTime.Minute < 10) ? ("0" + dateTime.Minute) : dateTime.Minute.ToString(),
                                                    ":",
                                                    (dateTime.Second < 10) ? ("0" + dateTime.Second) : dateTime.Second.ToString(),
                                                    ".000"
                                        });
                                        streamWriter.Write("<Cell ss:StyleID=\"DateLiteral\"><Data ss:Type=\"DateTime\">");
                                        streamWriter.Write(value);
                                        streamWriter.Write("</Data></Cell>");
                                        break;
                                    }
                                case 2:
                                    streamWriter.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                                    streamWriter.Write(dataRow[k].ToString());
                                    streamWriter.Write("</Data></Cell>");
                                    break;
                                case 3:
                                case 4:
                                case 5:
                                case 6:
                                    streamWriter.Write("<Cell ss:StyleID=\"Integer\"><Data ss:Type=\"Number\">");
                                    streamWriter.Write(dataRow[k].ToString());
                                    streamWriter.Write("</Data></Cell>");
                                    break;
                                case 7:
                                case 8:
                                    streamWriter.Write("<Cell ss:StyleID=\"Decimal\"><Data ss:Type=\"Number\">");
                                    streamWriter.Write(dataRow[k].ToString());
                                    streamWriter.Write("</Data></Cell>");
                                    break;
                                case 9:
                                    streamWriter.Write("<Cell ss:StyleID=\"StringLiteral\"><Data ss:Type=\"String\">");
                                    streamWriter.Write("");
                                    streamWriter.Write("</Data></Cell>");
                                    break;
                                default:
                                    goto IL_4F8;
                            }
                            k++;
                            continue;
                        }
                    }
                    IL_4F8:
						            throw new Exception(type + " not handled.");
                }
                streamWriter.Write("</Row>");
			    }
                streamWriter.Write("</Table>");
				streamWriter.Write(" </Worksheet>");
				num++;
		    }
			streamWriter.Write("</Workbook>");
			streamWriter.Close();
		}
        /// <summary>
        /// 将DataTable导出为Excel(OleDb 方式操作
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fileName"></param>
		public static void DataSetToExcel(DataTable dataTable, string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "xls files (*.xls)|*.xls",
                FileName = fileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                if (File.Exists(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch
                    {
                        MessageBox.Show("该文件正在使用中,关闭文件或重新命名导出文件再试!");
                        return;
                    }
                }
                OleDbConnection oleDbConnection = new OleDbConnection();
                OleDbCommand oleDbCommand = new OleDbCommand();
                try
                {
                    oleDbConnection.ConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + fileName + ";Extended ProPerties=\"Excel 8.0;HDR=Yes;\"";
                    oleDbConnection.Open();
                    oleDbCommand.CommandType = CommandType.Text;
                    oleDbCommand.Connection = oleDbConnection;
                    string text = "CREATE TABLE sheet1 (";
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        if (i < dataTable.Columns.Count - 1)
                        {
                            text = text + "[" + dataTable.Columns[i].Caption + "] TEXT(100) ,";
                        }
                        else
                        {
                            text = text + "[" + dataTable.Columns[i].Caption + "] TEXT(200) )";
                        }
                    }
                    oleDbCommand.CommandText = text;
                    oleDbCommand.ExecuteNonQuery();
                    for (int j = 0; j < dataTable.Rows.Count; j++)
                    {
                        text = "INSERT INTO sheet1 VALUES('";
                        for (int k = 0; k < dataTable.Columns.Count; k++)
                        {
                            if (k < dataTable.Columns.Count - 1)
                            {
                                text = text + dataTable.Rows[j][k] + " ','";
                            }
                            else
                            {
                                text = text + dataTable.Rows[j][k] + " ')";
                            }
                        }
                        oleDbCommand.CommandText = text;
                        oleDbCommand.ExecuteNonQuery();
                    }
                    MessageBox.Show("导出EXCEL成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出EXCEL失败:" + ex.Message);
                }
                finally
                {
                    oleDbCommand.Dispose();
                    oleDbConnection.Close();
                    oleDbConnection.Dispose();
                }
            }
        }
	}
}
