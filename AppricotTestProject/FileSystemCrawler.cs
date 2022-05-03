using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal class FileSystemCrawler
    {
        public static class FileSystemCrawlerState
        {
            public static int currentLevelInHierarchy = 0;
            public static int currentLevelSubFolderInHierarchy = 0;
        }

        //based on: https://csharp.webdelphi.ru/algoritm-obxoda-dereva-katalogov-v-c-s-ispolzovaniem-rekursii/
        public static IEnumerable<FileSystemCollector.FileSystemCollectorItem> Walk(DirectoryInfo targetDirectoryInfo, int levelInHierarchy)
        {
            FileInfo[]? files = null;
            DirectoryInfo[]? subDirs = null;

            yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.currentLevelInHierarchy, targetDirectoryInfo);
            if (levelInHierarchy == 0)
            {
                FileSystemCrawlerState.currentLevelInHierarchy++;
            }
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

            if (levelInHierarchy != 0) { FileSystemCrawlerState.currentLevelInHierarchy++; }
            foreach (var file in files)
            {
                yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.currentLevelInHierarchy, file);
            }
            //WalkBySubDirectories
            subDirs = targetDirectoryInfo.GetDirectories();
            foreach (var dirInfo in subDirs)
            {
                FileSystemCrawlerState.currentLevelSubFolderInHierarchy = FileSystemCrawlerState.currentLevelInHierarchy;
                
                foreach (var fileSystemCollectorItem in Walk(dirInfo, FileSystemCrawlerState.currentLevelInHierarchy))
                {
                    yield return fileSystemCollectorItem;
                }
                FileSystemCrawlerState.currentLevelInHierarchy++;
                FileSystemCrawlerState.currentLevelInHierarchy = FileSystemCrawlerState.currentLevelSubFolderInHierarchy;
            }
            //yield return ReturnCurrentDirectoryWithFiles(targetDirectoryInfo, files);
            //yield return WalkBySubDirectories(targetDirectoryInfo);
        }
    }
}
