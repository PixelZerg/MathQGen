using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibMathQGen
{
    public static class PublicResources
    {
        public static Random random = new Random();
        private static DirectoryInfo _pluginDir = new DirectoryInfo("Plugins");
        public static DirectoryInfo pluginDir
        {
            get
            {
                if (!_pluginDir.Exists) { Console.WriteLine("Created plugin directory!"); _pluginDir.Create(); }
                return _pluginDir;
            }
        }
    }

    public enum Topic
    {
        Algebra,
        Geometry,
        Calculus,
        Numerics,
        Graphs,
        Others,
    }
}
