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
		public static IEnumerable<FileSystemInfo> Walk(DirectoryInfo targetDirectoryInfo)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;
            yield return targetDirectoryInfo;

            //ReturnCurrentDirectoryWithFiles
            try
            {
                files = targetDirectoryInfo.GetFiles("*.*");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files == null) yield break;
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
                yield return file;
            }

            //WalkBySubDirectories
            subDirs = targetDirectoryInfo.GetDirectories();
            foreach (var dirInfo in subDirs)
                foreach (var file in Walk(dirInfo))
                    yield return file;

            //yield return ReturnCurrentDirectoryWithFiles(targetDirectoryInfo, files);
            //yield return WalkBySubDirectories(targetDirectoryInfo);
        }
    }
}
