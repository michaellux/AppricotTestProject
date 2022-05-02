using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppricotTestProject
{
    internal enum OutputTypes
    {
        ToConsole,
        ToFile
    }

    internal enum OutputPlaces
    {
        CurrentCallFolder,
        FileByDefaultInCurrentCallFolder
    }

    internal enum OutputViews
    {
        InBytes,
        ForHuman
    }
}
