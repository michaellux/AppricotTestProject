using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal static class FileSystemInfoDefiner
    {
        

        public static void UpdateFileSystemInfo()
        {
            foreach (var fileSystemInfoItem in FileSystemCollector.fileSystemCollectorItems)
            {
                if (IsDirectory(fileSystemInfoItem.FileSystemType))
                {
                    DefineDirectorySize((DirectoryInfo)fileSystemInfoItem.FileSystemType, fileSystemInfoItem);
                }
            }
        }

        //https://codengineering.ru/q/better-way-to-check-if-a-path-is-a-file-or-a-directory-25057
        public static bool IsDirectory(FileSystemInfo? fileSystemInfo)
        {
            if (fileSystemInfo == null)
            {
                return false;
            }

            if ((int)fileSystemInfo.Attributes != -1)
            {
                return fileSystemInfo.Attributes.HasFlag(FileAttributes.Directory);
            }

            return fileSystemInfo is DirectoryInfo;
        }

        //source: https://github.com/appsoftwareltd/folder-size/blob/master/src/FolderSize/Program.cs
        public static BigInteger DefineDirectorySize(DirectoryInfo targetDirectoryInfo, FileSystemCollector.FileSystemCollectorItem fileSystemCollectorItem)
        {
            BigInteger directorySizeBytes = 0;

            try
            {
                // Add file sizes for current directory

                FileInfo[] fileInfos = targetDirectoryInfo.GetFiles();

                foreach (FileInfo fileInfo in fileInfos)
                {
                    directorySizeBytes += fileInfo.Length;
                }
                fileSystemCollectorItem.Size += directorySizeBytes;

                // Recursively add subdirectory sizes

                DirectoryInfo[] subDirectories = targetDirectoryInfo.GetDirectories();

                foreach (DirectoryInfo di in subDirectories)
                {
                    directorySizeBytes += DefineDirectorySize(di, fileSystemCollectorItem);
                }
                fileSystemCollectorItem.Size = directorySizeBytes;
            }
            catch (UnauthorizedAccessException uaex)
            {
                new Exception ("Unable to calculate size - Unauthorised");
            }
            catch (Exception ex)
            {
                new Exception ("Unable to calculate size - Error");
            }

            return directorySizeBytes;
        }
    }
}
