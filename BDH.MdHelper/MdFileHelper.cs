using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BDH.MdHelper
{
    public class MdFileHelper
    {
        /// <summary>
        /// 删除文件 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static bool DeleteFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                if (File.GetAttributes(fileFullPath) == FileAttributes.Normal)
                {
                    File.Delete(fileFullPath);
                }
                else
                {
                    File.SetAttributes(fileFullPath, FileAttributes.Normal);
                    File.Delete(fileFullPath);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据传来的文件全路径，获取文件名称部分默认包括扩展名 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static string GetFileName(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                FileInfo fileInfo = new FileInfo(fileFullPath);
                return fileInfo.Name;
            }
            return null;
        }
        /// <summary>
        /// 根据传来的文件全路径，获取文件名称部分 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <param name="includeExtension"></param>
        /// <returns></returns>
        public static string GetFileName(string fileFullPath, bool includeExtension)
        {
            if (!File.Exists(fileFullPath))
            {
                return null;
            }
            FileInfo fileInfo = new FileInfo(fileFullPath);
            if (includeExtension)
            {
                return fileInfo.Name;
            }
            return fileInfo.Name.Replace(fileInfo.Extension, "");
        }
        /// <summary>
        /// 根据传来的文件全路径，获取新的文件名称全路径,一般用作临时保存用 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static string GetNewFileFullName(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                FileInfo fileInfo = new FileInfo(fileFullPath);
                string str = fileFullPath.Replace(fileInfo.Extension, "");
                for (int i = 0; i < 1000; i++)
                {
                    fileFullPath = str + i.ToString() + fileInfo.Extension;
                    if (!File.Exists(fileFullPath))
                    {
                        break;
                    }
                }
            }
            return fileFullPath;
        }
        /// <summary>
        /// 根据传来的文件全路径，获取文件扩展名不包括“.”，如“doc” 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static string GetFileExtension(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                FileInfo fileInfo = new FileInfo(fileFullPath);
                return fileInfo.Extension;
            }
            return null;
        }
        /// <summary>
        /// 根据传来的文件全路径，外部打开文件，默认用系统注册类型关联软件打开 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static bool OpenFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                Process.Start(fileFullPath);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据传来的文件全路径，得到文件大小，规范文件大小称呼，如1ＧＢ以上，单位用ＧＢ，１ＭＢ以上，单位用ＭＢ，１ＭＢ以下，单位用ＫＢ 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static string GetFileSize(string fileFullPath)
        {
            if (!File.Exists(fileFullPath))
            {
                return null;
            }
            FileInfo fileInfo = new FileInfo(fileFullPath);
            long length = fileInfo.Length;
            if (length > 1073741824L)
            {
                return Convert.ToString(Math.Round(((double)length + 0.0) / 1073741824.0, 2)) + " GB";
            }
            if (length > 1048576L)
            {
                return Convert.ToString(Math.Round(((double)length + 0.0) / 1048576.0, 2)) + " MB";
            }
            return Convert.ToString(Math.Round(((double)length + 0.0) / 1024.0, 2)) + " KB";
        }
        /// <summary>
        /// 文件转换成二进制，返回二进制数组Byte[] 
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static byte[] FileToStreamByte(string fileFullPath)
        {
            byte[] array = null;
            if (File.Exists(fileFullPath))
            {
                FileStream fileStream = new FileStream(fileFullPath, FileMode.Open);
                array = new byte[fileStream.Length];
                fileStream.Read(array, 0, array.Length);
                fileStream.Close();
            }
            return array;
        }
        /// <summary>
        /// 二进制数组Byte[]生成文件  
        /// </summary>
        /// <param name="createFileFullPath"></param>
        /// <param name="streamByte"></param>
        /// <returns></returns>
        public static bool ByteStreamToFile(string createFileFullPath, byte[] streamByte)
        {
            if (!File.Exists(createFileFullPath))
            {
                FileStream fileStream = File.Create(createFileFullPath);
                fileStream.Write(streamByte, 0, streamByte.Length);
                fileStream.Close();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 二进制数组Byte[]生成文件，并验证文件是否存在，存在则先删除 
        /// </summary>
        /// <param name="createFileFullPath"></param>
        /// <param name="streamByte"></param>
        /// <param name="fileExistsDelete"></param>
        /// <returns></returns>
        public static bool ByteStreamToFile(string createFileFullPath, byte[] streamByte, bool fileExistsDelete)
        {
            if (File.Exists(createFileFullPath) && fileExistsDelete && !MdFileHelper.DeleteFile(createFileFullPath))
            {
                return false;
            }
            FileStream fileStream = File.Create(createFileFullPath);
            fileStream.Write(streamByte, 0, streamByte.Length);
            fileStream.Close();
            return true;
        }
        /// <summary>
        /// 读写文件，并进行匹配文字替换 
        /// </summary>
        /// <param name="pathRead"></param>
        /// <param name="pathWrite"></param>
        /// <param name="replaceStrings"></param>
        public static void ReadAndWriteFile(string pathRead, string pathWrite, Dictionary<string, string> replaceStrings)
        {
            StreamReader streamReader = new StreamReader(pathRead);
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            StreamWriter streamWriter = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            string text = streamReader.ReadToEnd();
            if (replaceStrings != null && replaceStrings.Count > 0)
            {
                foreach (KeyValuePair<string, string> current in replaceStrings)
                {
                    text = text.Replace(current.Key, current.Value);
                }
            }
            streamWriter.WriteLine(text);
            streamReader.Close();
            streamWriter.Close();
        }
        /// <summary>
        /// 读取文件 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            StreamReader streamReader = new StreamReader(filePath);
            string result = null;
            if (File.Exists(filePath))
            {
                result = streamReader.ReadToEnd();
            }
            streamReader.Close();
            return result;
        }
        /// <summary>
        /// 写入文件 
        /// </summary>
        /// <param name="pathWrite"></param>
        /// <param name="content"></param>
        public static void WriteFile(string pathWrite, string content)
        {
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            StreamWriter streamWriter = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            streamWriter.WriteLine(content);
            streamWriter.Close();
        }
        /// <summary>
        /// 读取并附加文本 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        public static void ReadAndAppendFile(string filePath, string content)
        {
            File.AppendAllText(filePath, content, Encoding.GetEncoding("utf-8"));
        }
        /// <summary>
        /// 复制文件 
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="dest"></param>
        public static void CopyFile(string sources, string dest)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sources);
            FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
            for (int i = 0; i < fileSystemInfos.Length; i++)
            {
                FileSystemInfo fileSystemInfo = fileSystemInfos[i];
                string text = Path.Combine(dest, fileSystemInfo.Name);
                if (fileSystemInfo is FileInfo)
                {
                    File.Copy(fileSystemInfo.FullName, text, true);
                }
                else
                {
                    Directory.CreateDirectory(text);
                    MdFileHelper.CopyFile(fileSystemInfo.FullName, text);
                }
            }
        }
        /// <summary>
        /// 复制文件 
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="dest"></param>
        public static void MoveFile(string sources, string dest)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sources);
            FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
            for (int i = 0; i < fileSystemInfos.Length; i++)
            {
                FileSystemInfo fileSystemInfo = fileSystemInfos[i];
                string text = Path.Combine(dest, fileSystemInfo.Name);
                if (fileSystemInfo is FileInfo)
                {
                    File.Move(fileSystemInfo.FullName, text);
                }
                else
                {
                    Directory.CreateDirectory(text);
                    MdFileHelper.MoveFile(fileSystemInfo.FullName, text);
                }
            }
        }
        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
