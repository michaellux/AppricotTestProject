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
                if (fileSystemCollectorItem.Size <= 1000)
                {
                    fileSystemCollectorItemSize = $@"({ fileSystemCollectorItem.Size} bytes)";
                }
                else if (fileSystemCollectorItem.Size <= 1e+6)
                {
                    var sizeKB = Math.Round(fileSystemCollectorItem.Size / 1024, 2);
                    fileSystemCollectorItemSize = $@"({sizeKB} kilobytes)";
                }
                else if (fileSystemCollectorItem.Size <= 1e+9)
                {
                    var sizeMB = Math.Round(fileSystemCollectorItem.Size / (1024 * 1024), 2);
                    fileSystemCollectorItemSize = $@"({sizeMB} megabytes)";
                }
                else if (fileSystemCollectorItem.Size <= 1e+12)
                {
                    var sizeGB = Math.Round(fileSystemCollectorItem.Size / (1024 * 1024 * 1024), 2);
                    fileSystemCollectorItemSize = $@"({sizeGB} gigabytes)";
                }
            }
            else
            {
                fileSystemCollectorItemSize = $@"({ fileSystemCollectorItem.Size} bytes)";
            }

            finalString = @$"{prefixLevelInHierarchy} {fileSystemCollectorItemName} {fileSystemCollectorItemSize}";

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
