using CommandLine;

namespace AppricotTestProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser(with => with.EnableDashDash = true);
            var result = parser.ParseArguments<Options>(args);
            Console.WriteLine("Hello World.");
        }
    }
}

