using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    static class DefaultPaths
    {
        public static string DefaultFolderPathForCrawl => Directory.GetCurrentDirectory();
        public static string DefaultFilePathForOutput => @$"{Directory.GetCurrentDirectory()}\sizes-{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
    }
}
