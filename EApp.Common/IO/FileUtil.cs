using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EApp.Common.IO
{
    public sealed class FileUtil
    {
        //file seperator in windows
        public static readonly string SEPERATOR = @"\";

        /// <summary>
        /// buffer for reading file
        /// </summary>
        public static readonly int BUFFER_SIZE = 1024;

        //1KB = 1024 b
        public static readonly double K_SIZE = 1024.0;
        //1M =1024 KB
        public static readonly double M_SIZE = K_SIZE * K_SIZE;

        private FileUtil() { }

        #region Folder creation/Copy/Move/Delete/Properties

        /// <summary>
        /// add the last "\" of the dir
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AddonDirectory(string path)
        {
            //add the end "\"
            if (!path.EndsWith(FileUtil.SEPERATOR))
            {
                path += FileUtil.SEPERATOR;
            }

            return path;
        }

        public static IEnumerable<string> GetAllSubDirectoryNames(string path) 
        {
            return GetAllSubDirectoryNames(path, "*", true);
        }

        public static IEnumerable<string> GetAllSubDirectoryNames(string path, string searchPattern, bool toSearchChildren)
        {
            try
            {
                if (toSearchChildren)
                {
                    return Directory.GetDirectories(path, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(path, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public static bool ExistsDirectory(string path)
        {
            return Directory.Exists(path);
        }

        public static void CreateDirectory(string path) 
        {
            if (!ExistsDirectory(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CopyDirectory(string sourceDirPath, string targetDirPath)
        {
            DirectoryInfo sourceDirInfo = new DirectoryInfo(sourceDirPath);

            IEnumerable<DirectoryInfo> allDirInfoList = sourceDirInfo.EnumerateDirectories("*", SearchOption.AllDirectories);

            if (allDirInfoList == null)
            {
                return;
            }
        }

        public static void MoveDirectory(string sourceDirPath, string targetDirPath)
        {
            if (ExistsDirectory(sourceDirPath))
            {
                Directory.Move(sourceDirPath, targetDirPath);
            }
        }

        ///<summary> 
        /// Clear all sub-directories and all files in the specified directory but the specified directory keep and is not be deleted. 
        /// </summary> 
        public static void ClearDirectory(string path)
        {
            if (ExistsDirectory(path))
            {
                //delete all files 
                IEnumerable<string> fileNames = GetAllFileNames(path);

                foreach (string fileName in fileNames)
                {
                    DeleteFile(fileName);
                }

                //delete all sub directories
                IEnumerable<string> directoryNames = GetAllSubDirectoryNames(path);
                foreach (string directoryName in directoryNames)
                {
                    DeleteDirectory(directoryName, false);
                }
            }
        }

        public static void DeleteDirectory(string path, bool recursive)
        {
            if (ExistsDirectory(path))
            {
                Directory.Delete(path, recursive);
            }
        }

        #endregion

        #region File Creation/Copy/Move/Delete/Properties

        public static string GetFileDirectoryPath(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.DirectoryName;
        }

        public static string GetFileName(string fileName) 
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Name;
        }

        public static string GetFileExtensionName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Extension;
        }

        public static string GetFileNameWithoutExtension(string fileName)
        {
            string fullFileName = GetFileName(fileName);
            string extensionName = GetFileExtensionName(fileName);

            int position = fullFileName.LastIndexOf(extensionName, StringComparison.InvariantCultureIgnoreCase);

            return fullFileName.Substring(0, position - 1);
        }

        public static IEnumerable<string> GetAllFileNames(string directoryPath) 
        {
            return GetAllFileNames(directoryPath, "*", true);
        }

        public static IEnumerable<string> GetAllFileNames(string directoryPath, string searchPattern, bool toSearchChildren)
        {
            if (!ExistsDirectory(directoryPath))
            {
                return null;
            }

            try
            {
                if (toSearchChildren)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public static bool ExistsFile(string name)
        {
            return File.Exists(name);
        }

        public static void CreateFile(string path)
        {
            if (ExistsFile(path))
            {
                return;
            }

            FileStream fileStream = null;

            try
            {
                FileInfo fileInfo = new FileInfo(path);
                fileStream = fileInfo.Create();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public static void CreateFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static void CreateFile(string path, byte[] content)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                fileStream.Write(content, 0, content.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public static void CreateFile(string path, string content, Encoding encoding)
        {
            FileStream fileStream = null;
            StreamWriter writer = null;

            try
            {
                FileInfo fileInfo = new FileInfo(path);
                fileStream = fileInfo.Create();

                writer = new StreamWriter(fileStream, encoding);
                writer.Write(content);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }

                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public static void CopyFile(string sourceFilePath, string targetFilePath)
        {
            CopyFile(sourceFilePath, targetFilePath, true);
        }

        public static void CopyFile(string sourceFilePath, string targetFilePath, bool overwrite)
        {
            if (!ExistsFile(sourceFilePath))
            {
                return;
            }

            FileInfo fileInfo = new FileInfo(sourceFilePath);
            fileInfo.CopyTo(targetFilePath, overwrite);
        }

        public static void MoveFile(string sourceFilePath, string tartgetDirPath)
        {
            string sourceFileName = GetFileName(sourceFilePath);

            if (ExistsDirectory(tartgetDirPath))
            {
                //if there is already a existing file then delete it
                string targetFilePath = AddonDirectory(tartgetDirPath) + sourceFileName;

                if (ExistsFile(targetFilePath))
                {
                    DeleteFile(targetFilePath);
                }

                //Move file to the specified directory
                File.Move(sourceFilePath, targetFilePath);
            }
        }

        public static void RenameFile(string filePath, string newName)
        {
            if (!ExistsFile(filePath))
            {
                return;
            }

            string newFilePath = Path.Combine(GetFileDirectoryPath(filePath), newName + "." + GetFileExtensionName(filePath));

            File.Move(filePath, newFilePath);
        }

        public static void ClearFile(string path)
        {
            File.Delete(path);

            CreateFile(path);
        }

        public static void DeleteFile(string path)
        {
            if (ExistsFile(path))
            {
                File.Delete(path);
            }
        }

        #endregion

        #region Write or append content to file

        /// <summary> 
        /// writes the specified string to the file, and then closes
        //  the file. If the target file already exists, it is overwritten.
        /// </summary>        
        public static void WriteText(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public static void WriteText(string path, string content, Encoding encoding)
        {
            File.WriteAllText(path, content, encoding);
        }

        /// <summary> 
        /// Opens a file, appends the specified string to the file, and then closes the
        //  file. If the file does not exist, this method creates a file, writes the
        //  specified string to the file, then closes the file.
        /// </summary> 
        public static void AppendText(string path, string content)
        {
            File.AppendAllText(path, content);
        }

        public static void AppendText(string path, string content, Encoding encoding)
        {
            File.AppendAllText(path, content, encoding);
        }

        public static void WriteBytesToFile(string path, byte[] content)
        {
            if (content == null)
            {
                return;
            }

            FileStream fileStream = null;
            MemoryStream memStream = new MemoryStream(content);

            try
            {
                byte[] buffer = new byte[BUFFER_SIZE * 4];

                fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

                memStream.Seek(0, SeekOrigin.Begin);

                int position = memStream.Read(buffer, 0, BUFFER_SIZE);

                while (position > 0)
                {
                    fileStream.Write(buffer, 0, position);

                    position = memStream.Read(buffer, 0, BUFFER_SIZE);
                }

                fileStream.Flush();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (memStream != null)
                {
                    memStream.Close();
                }

                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        #endregion

        #region Read file

        /// <summary> 
        /// Get line count of content in a text file
        /// </summary>        
        public static int GetLineCount(string filePath)
        { 
            string[] rows = File.ReadAllLines(filePath);
            return rows.Length;
        }

        /// <summary> 
        /// Get the length of the specified Byte
        /// </summary>        
        public static long GetFileSize(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return fileInfo.Length;
        }
        /// <summary> 
        /// Get the length of the specified (KB)  
        /// </summary>       
        public static double GetFileSizeByKB(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return Convert.ToDouble(Convert.ToDouble(fileInfo.Length) / K_SIZE);
        }
        /// <summary> 
        /// Get the length of the specified (MB)  
        /// </summary>    
        public static double GetFileSizeByMB(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            return Convert.ToDouble(Convert.ToDouble(fileInfo.Length) / M_SIZE);
        }

        public static byte[] ReadStreamToBytes(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }

            MemoryStream memStream = null;

            try
            {
                byte[] buffer = new byte[BUFFER_SIZE * 4];

                memStream = new MemoryStream();

                int position = stream.Read(buffer, 0, BUFFER_SIZE);

                while (position > 0)
                {
                    memStream.Write(buffer, 0, position);

                    position = stream.Read(buffer, 0, BUFFER_SIZE);
                }

                memStream.Flush();

                return memStream.ToArray();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (memStream != null)
                {
                    memStream.Close();
                }

                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static byte[] ReadFileToBytes(string path)
        {
            if (!ExistsFile(path))
            {
                return null;
            }

            FileStream fileStream = null;
            MemoryStream memStream = null;

            try
            {
                byte[] buffer = new byte[BUFFER_SIZE * 4];

                memStream = new MemoryStream();

                fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

                int position = fileStream.Read(buffer, 0, BUFFER_SIZE);

                while (position > 0)
                {
                    memStream.Write(buffer, 0, position);

                    position = fileStream.Read(buffer, 0, BUFFER_SIZE);
                }

                memStream.Flush();

                return memStream.ToArray();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (memStream != null)
                {
                    memStream.Close();
                }

                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public static void ReadFileToFile(string sourceFilePath, string targetFilePath)
        {
            if (!ExistsFile(sourceFilePath))
            {
                return;
            }

            FileStream sourceFileStream = null;
            FileStream targetFileStream = null;

            try
            {
                byte[] buffer = new byte[BUFFER_SIZE * 4];

                sourceFileStream = new FileStream(sourceFilePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);

                targetFileStream = new FileStream(targetFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);

                sourceFileStream.Seek(0, SeekOrigin.Begin);

                int position = sourceFileStream.Read(buffer, 0, BUFFER_SIZE);

                while (position > 0)
                {
                    targetFileStream.Write(buffer, 0, position);

                    position = sourceFileStream.Read(buffer, 0, BUFFER_SIZE);
                }

                targetFileStream.Flush();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (targetFileStream != null)
                {
                    targetFileStream.Close();
                }

                if (targetFileStream != null)
                {
                    targetFileStream.Close();
                }
            }
        }

        public static string ReadFileToText(string path)
        {
            return ReadFileToText(path, Encoding.Default);
        }

        public static string ReadFileToText(string path, Encoding encoding)
        {
            if (!ExistsFile(path))
            {
                return null;
            }

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(path, encoding);
                return reader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        #endregion
    }
}
