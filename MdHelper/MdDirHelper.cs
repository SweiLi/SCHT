using System;
using System.IO;

namespace BDH.MdHelper
{
    public class MdDirHelper
    {
        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法. 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static string[] GetDirs(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }
        /// <summary>
        /// 创建文件夹 
        /// </summary>
        /// <param name="destDirectory"></param>
        public static void CreateDir(string destDirectory)
        {
            if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
        }
        /// <summary>
        /// 复制文件夹 
        /// </summary>
        /// <param name="strFromDirectory"></param>
        /// <param name="strToDirectory"></param>
        /// <returns></returns>
        public static bool CopyDir(string strFromDirectory, string strToDirectory)
        {
            Directory.CreateDirectory(strToDirectory);
            if (!Directory.Exists(strFromDirectory))
            {
                return false;
            }
            string[] directories = Directory.GetDirectories(strFromDirectory);
            if (directories.Length > 0)
            {
                string[] array = directories;
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    MdDirHelper.CopyDir(text, strToDirectory + text.Substring(text.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(strFromDirectory);
            if (files.Length > 0)
            {
                string[] array2 = files;
                for (int j = 0; j < array2.Length; j++)
                {
                    string text2 = array2[j];
                    File.Copy(text2, strToDirectory + text2.Substring(text2.LastIndexOf("\\")));
                }
            }
            return true;
        }
        /// <summary>
        /// 删除文件夹 
        /// </summary>
        /// <param name="dirFullPath"></param>
        /// <returns></returns>
        public static bool DeleteDir(string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 得到当前文件夹中所有文件列表string[] 
        /// </summary>
        /// <param name="dirFullPath"></param>
        /// <returns></returns>
        public static string[] GetDirFiles(string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                return Directory.GetFiles(dirFullPath, "*.*", SearchOption.TopDirectoryOnly);
            }
            return null;
        }
        /// <summary>
        /// 得到当前文件夹及下级文件夹中所有文件列表string[] 
        /// </summary>
        /// <param name="dirFullPath"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static string[] GetDirFiles(string dirFullPath, SearchOption so)
        {
            if (Directory.Exists(dirFullPath))
            {
                return Directory.GetFiles(dirFullPath, "*.*", so);
            }
            return null;
        }
        /// <summary>
        /// 得到当前文件夹及下级文件夹中所有文件列表string
        /// </summary>
        /// <param name="dirFullPath"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static string[] GetDirFiles(string dirFullPath, string searchPattern)
        {
            if (Directory.Exists(dirFullPath))
            {
                return Directory.GetFiles(dirFullPath, searchPattern);
            }
            return null;
        }
        /// <summary>
        /// 得到当前文件夹及下级文件夹中指定文件类型［扩展名］文件列表string[] 
        /// </summary>
        /// <param name="dirFullPath"></param>
        /// <param name="searchPattern"></param>
        /// <param name="so"></param>
        /// <returns></returns>
        public static string[] GetDirFiles(string dirFullPath, string searchPattern, SearchOption so)
        {
            if (Directory.Exists(dirFullPath))
            {
                return Directory.GetFiles(dirFullPath, searchPattern, so);
            }
            return null;
        }
        /// <summary>
        /// 确保文件夹被创建 
        /// </summary>
        /// <param name="filePath"></param>
        public static void AssertDirExist(string filePath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
        /// <summary>
        /// 检测指定目录是否存在 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        /// <summary>
        /// 检测指定目录是否为空 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool IsEmptyDirectory(string directoryPath)
        {
            string[] fileNames = MdDirHelper.GetFileNames(directoryPath);
            if (fileNames.Length > 0)
            {
                return false;
            }
            string[] dirs = MdDirHelper.GetDirs(directoryPath);
            return dirs.Length <= 0;
        }
        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法. 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static bool ContainFile(string directoryPath, string searchPattern)
        {
            string[] fileNames = MdDirHelper.GetFileNames(directoryPath, searchPattern, false);
            return fileNames.Length != 0;
        }
        /// <summary>
        /// 检测指定目录中是否存在指定的文件 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="searchPattern"></param>
        /// <param name="isSearchChild"></param>
        /// <returns></returns>
        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] fileNames = MdDirHelper.GetFileNames(directoryPath, searchPattern, true);
            return fileNames.Length != 0;
        }
        /// <summary>
        /// 取当前目录 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
        /// <summary>
        /// 设当前目录 
        /// </summary>
        /// <param name="path"></param>
        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }
        /// <summary>
        /// 取路径中不允许存在的字符 
        /// </summary>
        /// <returns></returns>
        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }
        /// <summary>
        /// 获取驱动器信息
        /// </summary>
        /// <returns></returns>
        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }
        /// <summary>
        /// 获取指定目录中所有文件列表 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static string[] GetFileNames(string directoryPath)
        {
            if (!MdDirHelper.IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }
        /// <summary>
        /// 获取指定目录及子目录中所有文件列表 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="searchPattern"></param>
        /// <param name="isSearchChild"></param>
        /// <returns></returns>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            if (!MdDirHelper.IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            if (isSearchChild)
            {
                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
            }
            return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
        }
    }
}
