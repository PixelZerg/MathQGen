using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibMathQGen
{
    public static class Utils
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

        public static bool IsPrime(int number)
        {
            double boundary = Math.Floor(Math.Sqrt(number));

            if (number == 1) return false;
            if (number == 2) return true;

            for (int i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
        public static List<int> GetFactors(int number)
        {
            List<int> factors = new List<int>();
            int max = (int)Math.Sqrt(number);  //round down
            for (int factor = 1; factor <= max; ++factor)
            { //test from 1 to the square root, or the int below it, inclusive.
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != number / factor)
                    { // Don't add the square root twice!  Thanks Jon
                        factors.Add(number / factor);
                    }
                }
            }
            return factors;
        }
        public static List<int> GetPrimeFactors(int number)
        {
            var primes = new List<int>();
            //primes.Add(number);
            for (int div = 2; div <= number; div++)
            {
                while (number % div == 0)
                {
                    primes.Add(div);
                    number = number / div;
                }
            }
            return primes;
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
