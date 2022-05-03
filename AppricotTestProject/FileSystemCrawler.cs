using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal class FileSystemCrawler
    {
		//based on: https://csharp.webdelphi.ru/algoritm-obxoda-dereva-katalogov-v-c-s-ispolzovaniem-rekursii/
		public static void Walk(DirectoryInfo targetDirectory)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = GetAllFilesInCurrentFolder(targetDirectory, files);

            if (files != null)
            {
                PrintFileNamesToConsole(files);
                WalkBySubDirectories(targetDirectory);
            }
        }

        private static void PrintFileNamesToConsole(FileInfo[] files)
        {
            foreach (FileInfo fi in files)
            {
                Console.WriteLine(fi.FullName);
            }
        }

        private static FileInfo[] GetAllFilesInCurrentFolder(DirectoryInfo root, FileInfo[] files)
        {
            try
            {
                files = root.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            return files;
        }

        private static void WalkBySubDirectories(DirectoryInfo root)
        {
            DirectoryInfo[] subDirs = root.GetDirectories();
            foreach (DirectoryInfo dirInfo in subDirs)
            {
                Walk(dirInfo);
            }
        }
    }
}
