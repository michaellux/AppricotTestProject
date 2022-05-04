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
            Console.WriteLine("Parser success");

            OutputFileSystemItems(
                SaveFileSystemItems(
                    GetFileSystemItems(options.Path)
                    )
                , options.outputType);
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

        private static List<FileSystemCollector.FileSystemCollectorItem> SaveFileSystemItems(IEnumerable<FileSystemCollector.FileSystemCollectorItem> fileSystemItems)
        {
            foreach (var fileSystemInfoItem in fileSystemItems)
            {
                FileSystemCollector.fileSystemCollectorItems.Add(fileSystemInfoItem);
            }

            return FileSystemCollector.fileSystemCollectorItems;
        }

        private static void OutputFileSystemItems(List<FileSystemCollector.FileSystemCollectorItem> fileSystemItems, OutputTypes outputType)
        {
            FileSystemPresentator.PrintFileSystemItems(fileSystemItems, outputType);
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Parser Fail");
        }
    }
}

