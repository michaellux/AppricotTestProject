using CommandLine;

namespace AppricotTestProject
{
    internal class Program
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
            string? inputtedPathFromCommandLine = options.Path;
            IEnumerable<FileSystemCollector.FileSystemCollectorItem>? fileSystemInfoCollection = null;
            if (InputDataManager.AnalyzeForCorrectnessTargetPath(inputtedPathFromCommandLine))
            {
                fileSystemInfoCollection = FileSystemCrawler.Walk(new DirectoryInfo($@"{inputtedPathFromCommandLine}"), 0);
            }
            else
            {
                fileSystemInfoCollection = FileSystemCrawler.Walk(new DirectoryInfo($@"{DefaultPaths.DefaultFolderPathForCrawl}"), 0);
            }

            foreach (var fileSystemInfoItem in fileSystemInfoCollection)
            {
                FileSystemPresentator.PrintFileSystemInfoItem(fileSystemInfoItem, options.outputType);
            }
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Parser Fail");
        }
    }
}

