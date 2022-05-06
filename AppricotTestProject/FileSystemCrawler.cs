using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal static class FileSystemCrawler
    {
        internal static class FileSystemCrawlerState
        {
            internal static int currentLevelInHierarchy = 0;
        }

        //based on: https://csharp.webdelphi.ru/algoritm-obxoda-dereva-katalogov-v-c-s-ispolzovaniem-rekursii/
        public static IEnumerable<FileSystemCollector.FileSystemCollectorItem> Walk(DirectoryInfo targetDirectoryInfo, int levelInHierarchy)
        {
            FileInfo[]? files = null;
            DirectoryInfo[]? subDirs = null;

            yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.currentLevelInHierarchy, targetDirectoryInfo);

            FileSystemCrawlerState.currentLevelInHierarchy++;

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

            if (files == null)
            {
                yield break;
            }

            foreach (var file in files)
            {
                yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.currentLevelInHierarchy, file);
            }

            subDirs = targetDirectoryInfo.GetDirectories();
           
            foreach (var dirInfo in subDirs)
            {
                foreach (var fileSystemCollectorItem in Walk(dirInfo, FileSystemCrawlerState.currentLevelInHierarchy))
                {
                    yield return fileSystemCollectorItem;
                }
            }
            FileSystemCrawlerState.currentLevelInHierarchy--;
        }
    }
}
