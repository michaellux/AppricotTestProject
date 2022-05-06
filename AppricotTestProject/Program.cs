using CommandLine;

namespace AppricotTestProject
{
    static internal class Program
    {
        
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private static void Run(CommandLineOptions options)
        {
            SaveFileSystemItems(GetFileSystemItems(options.Path));
            DefineFileSystemItemsProperties();
            OutputFileSystemItems(options);
        }

        private static IEnumerable<FileSystemCollector.FileSystemCollectorItem> GetFileSystemItems(string? pathToTargetFolder) {
            string? inputtedPathFromCommandLine = pathToTargetFolder;
            IEnumerable<FileSystemCollector.FileSystemCollectorItem>? fileSystemInfoCollection = null;
            if (InputDataManager.AnalyzeForCorrectnessTargetPath(inputtedPathFromCommandLine))
            {
                fileSystemInfoCollection = FileSystemCrawler.Walk(new DirectoryInfo($@"{inputtedPathFromCommandLine}"), 0);
            }
            else
            {
                fileSystemInfoCollection = FileSystemCrawler.Walk(new DirectoryInfo($@"{DefaultPaths.DefaultFolderPathForCrawl}"), 0);
            }
            return fileSystemInfoCollection;
        }

        private static void SaveFileSystemItems(IEnumerable<FileSystemCollector.FileSystemCollectorItem> fileSystemInfoItems)
        {
            foreach (var fileSystemInfoItem in fileSystemInfoItems)
            {
                FileSystemCollector.fileSystemCollectorItems.Add(fileSystemInfoItem);
            }
        }

        private static void DefineFileSystemItemsProperties()
        {
            FileSystemInfoDefiner.UpdateFileSystemInfo();
        }

        private static void OutputFileSystemItems(CommandLineOptions options)
        {
            FileSystemPresentator.PrintFileSystemItems(options);
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Parser Fail");
        }
    }
}

