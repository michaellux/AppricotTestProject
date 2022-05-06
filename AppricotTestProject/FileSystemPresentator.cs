using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal static class FileSystemPresentator
    {
        public static void PrintFileSystemItems(CommandLineOptions options)
        {
                if (options.Quite)
                {
                    WriteToFileAsync(options.Output, options.Humanread);
                }
                else
                {
                    foreach (var fileSystemInfoItem in FileSystemCollector.fileSystemCollectorItems)
                    {
                        string stringForOutput = ConstructStringFileSystemInfoItem(fileSystemInfoItem, options.Humanread);
                        Console.WriteLine(stringForOutput);
                    }
                }
        }

        public static string ConstructStringFileSystemInfoItem(FileSystemCollector.FileSystemCollectorItem fileSystemCollectorItem, bool isHumanRead)
        {
            StringBuilder prefixLevelInHierarchy = new StringBuilder();
            string fileSystemCollectorItemName = "";
            string fileSystemCollectorItemSize = "";
            string finalString = "";

            if (fileSystemCollectorItem.LevelInHierarchy == 0)
            {
                prefixLevelInHierarchy.Insert(0, new string('-', 1));
                fileSystemCollectorItemName = $"<{fileSystemCollectorItem.Name}>";
            }
            else
            {
                prefixLevelInHierarchy.Insert(0, new string('-', (fileSystemCollectorItem.LevelInHierarchy) * 2));
                fileSystemCollectorItemName = $"{fileSystemCollectorItem.Name}";
            }

            if (isHumanRead)
            {
                fileSystemCollectorItemSize = $@"({ fileSystemCollectorItem.Size })"; ;
               
            }
            else
            {
                fileSystemCollectorItemSize = $@"({ fileSystemCollectorItem.Size} bytes)";
            }

            finalString = @$"{prefixLevelInHierarchy} {fileSystemCollectorItemName} ({fileSystemCollectorItem.Size} bytes)";

            return finalString;
        }

        public static async Task WriteToFileAsync(string outputFilePath, bool isHumanRead)
        {
            string finalOutput = string.IsNullOrEmpty(outputFilePath) ? DefaultPaths.DefaultFilePathForOutput : outputFilePath;

            using (StreamWriter writer = new StreamWriter(finalOutput, false))
            {
                foreach (var fileSystemInfoItem in FileSystemCollector.fileSystemCollectorItems)
                {
                    string stringForOutput = ConstructStringFileSystemInfoItem(fileSystemInfoItem, isHumanRead);

                    await writer.WriteLineAsync(stringForOutput);
                }
            }
        }
    }
}
