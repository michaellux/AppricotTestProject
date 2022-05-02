using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal class Options
    {
        private readonly string defaultFileForOutput = $"{Directory.GetCurrentDirectory()}/sizes-{DateTime.Now.ToString("yyyy-MM-dd")}";

        private readonly OutputTypes outputType;
        private readonly string path;
        private readonly string output;
        private readonly OutputViews outputView;

        public Options(bool quite, string path, string output, bool human)
        {
            this.outputType = quite ? OutputTypes.ToFile : OutputTypes.ToConsole;
            this.path = string.IsNullOrEmpty(path) ? Directory.GetCurrentDirectory() : path;
            this.output = string.IsNullOrEmpty(path) ? defaultFileForOutput : output;
            this.outputView = human ? OutputViews.ForHuman : OutputViews.InBytes;
        }

        [Option('q')]
        public bool Quite { get; set; }

        [Option('p', Default = OutputPlaces.CurrentCallFolder)]
        public string? Path { get; set; }

        [Option('o', Default = OutputPlaces.FileByDefaultInCurrentCallFolder)]
        public string? Output { get; set; }

        [Option('h')]
        public bool Humanread { get; set; }
    }
}
