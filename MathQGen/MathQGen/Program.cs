using LibMathQGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQGen
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes().ToList().Where(t => t.Namespace == "MathQGen.DefaultGens").ToList())
            {
                IGenerator gen = (IGenerator)Activator.CreateInstance(t);
                Serialiser.LoadGenerator(gen);
            }
            Serialiser.Load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new UI.Display());
            new UI.Display().ShowDialog();
        }
    }
}
