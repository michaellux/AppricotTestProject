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
            private static int currentLevelInHierarchy;
            internal static int CurrentLevelInHierarchy { get => currentLevelInHierarchy; set => currentLevelInHierarchy = value; }
        }

        //based on: https://csharp.webdelphi.ru/algoritm-obxoda-dereva-katalogov-v-c-s-ispolzovaniem-rekursii/
        public static IEnumerable<FileSystemCollector.FileSystemCollectorItem> Walk(DirectoryInfo targetDirectoryInfo, int levelInHierarchy)
        {
            FileInfo[]? files = null;
            DirectoryInfo[]? subDirs = null;

            yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.CurrentLevelInHierarchy, targetDirectoryInfo);

            FileSystemCrawlerState.CurrentLevelInHierarchy++;

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
                yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.CurrentLevelInHierarchy, file);
            }

            subDirs = targetDirectoryInfo.GetDirectories();
           
            foreach (var dirInfo in subDirs)
            {
                foreach (var fileSystemCollectorItem in Walk(dirInfo, FileSystemCrawlerState.CurrentLevelInHierarchy))
                {
                    yield return fileSystemCollectorItem;
                }
            }
            FileSystemCrawlerState.CurrentLevelInHierarchy--;
        }
    }
}
