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
        }

        //based on: https://csharp.webdelphi.ru/algoritm-obxoda-dereva-katalogov-v-c-s-ispolzovaniem-rekursii/
        public static IEnumerable<FileSystemCollector.FileSystemCollectorItem> Walk(DirectoryInfo targetDirectoryInfo, int levelInHierarchy)
        {
            FileInfo[]? files = null;
            DirectoryInfo[]? subDirs = null;

            yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.currentLevelInHierarchy, targetDirectoryInfo);

            Console.WriteLine($"\nНачинаем просматривать папки и файлы вложенные в папку (поэтому увеличиваем уровень): {targetDirectoryInfo.Name} \n");
            FileSystemCrawlerState.currentLevelInHierarchy++;
            Console.WriteLine("\nТекущий уровень: " + FileSystemCrawlerState.currentLevelInHierarchy + "\n");

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

            if (files == null)
            {
                Console.WriteLine("\nФайлов нет.\n");
                yield break;
            }

            foreach (var file in files)
            {
                yield return new FileSystemCollector.FileSystemCollectorItem(FileSystemCrawlerState.currentLevelInHierarchy, file);
            }

            //WalkBySubDirectories
            subDirs = targetDirectoryInfo.GetDirectories();
           
            foreach (var dirInfo in subDirs)
            {
                foreach (var fileSystemCollectorItem in Walk(dirInfo, FileSystemCrawlerState.currentLevelInHierarchy))
                {
                    yield return fileSystemCollectorItem;
                }
            }
            Console.WriteLine("Выходим из папки (поэтому понижаем уровень)");
            FileSystemCrawlerState.currentLevelInHierarchy--;
            Console.WriteLine("\nТекущий уровень: " + FileSystemCrawlerState.currentLevelInHierarchy + "\n");
        }
    }
}
