using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal class FileSystemPresentator
    {
        public static void PrintFileSystemItems(OutputTypes outputType)
        {
            if (outputType == OutputTypes.ToConsole)
            {
                foreach (var fileSystemInfoItem in FileSystemCollector.fileSystemCollectorItems)
                {
                    Console.WriteLine($"{fileSystemInfoItem.LevelInHierarchy} {fileSystemInfoItem.Name} ({fileSystemInfoItem.Size} bytes)");
                }
            }
            else if (outputType == OutputTypes.ToFile)
            {

            }
            else
            {
                throw new Exception("Unknown output type.");
            }
        }
    }
}
