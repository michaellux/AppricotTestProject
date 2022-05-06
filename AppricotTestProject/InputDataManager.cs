using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal static class InputDataManager
    {
        public static bool AnalyzeForCorrectnessTargetPath(string? targetPath)
        {
            bool targetPathIsCorrect = !string.IsNullOrEmpty(targetPath);
            return targetPathIsCorrect;
        }
    }
}
