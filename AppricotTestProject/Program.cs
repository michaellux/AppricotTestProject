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
            if (InputDataManager.AnalyzeForCorrectnessTargetPath(inputtedPathFromCommandLine))
            {
                FileSystemCrawler.Walk(new DirectoryInfo($@"{inputtedPathFromCommandLine}"));
            }
            else
            {
                FileSystemCrawler.Walk(new DirectoryInfo($@"{DefaultPaths.DefaultFolderPathForCrawl}"));
            }
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Parser Fail");
        }
    }
}

