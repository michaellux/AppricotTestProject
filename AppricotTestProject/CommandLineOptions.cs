using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal class CommandLineOptions
    {
        private readonly OutputTypes outputType;
        private readonly OutputViews outputView;

        public CommandLineOptions(bool quite, string path, string output, bool human)
        {
            this.outputType = quite ? OutputTypes.ToFile : OutputTypes.ToConsole;           
            this.outputView = human ? OutputViews.ForHuman : OutputViews.InBytes;
        }

        public CommandLineOptions() {}

        [Option(shortName: 'q', longName: "quite")]
        public bool Quite { get; set; }

        [Option(shortName: 'p', longName: "path")]
        public string? Path { get; set; }

        [Option(shortName:'o', longName: "output")]
        public string? Output { get; set; }

        [Option(shortName:'h', longName: "humanread")]
        public bool Humanread { get; set; }
    }
}
