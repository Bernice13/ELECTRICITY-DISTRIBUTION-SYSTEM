using System;
using System.IO;

namespace PortalLibrary.Constant
{
    public class FileConstant
    {
        public readonly static string DBFOLDER = Path.Combine(Environment.GetFolderPath
                    (Environment.SpecialFolder.Desktop), "Electricity Digital System");
    }
}