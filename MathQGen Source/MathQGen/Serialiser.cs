using LibMathQGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MathQGen
{
    public class Serialiser
    {

        // public static Dictionary<IGenerator, long> generators = new Dictionary<IGenerator, long>();
        public static Dictionary<string, List<GeneratorData>> gens = new Dictionary<string, List<GeneratorData>>(); //namespace, list of gens
        public static void Add(IGenerator generator)
        {
            Console.Write(generator + "...");
            List<GeneratorData> val;
            long speed = 0;
            Stopwatch s = new Stopwatch();
            s.Start();
            generator.Generate("");
            s.Stop();
            speed = s.ElapsedTicks;

            if (gens.TryGetValue(generator.GetType().Namespace, out val))
            {
                // yay, value exists!
                val.Add(new GeneratorData(generator, speed));
                gens[generator.GetType().Namespace] = val;
            }
            else
            {
                // darn, lets add the value
                val = new List<GeneratorData>();
                val.Add(new GeneratorData(generator, speed));
                gens.Add(generator.GetType().Namespace, val);
            }
            Console.WriteLine(speed);
        }


        public static void LoadExt()
        {
            Console.WriteLine("Loading external plugins...");
            List<Type> pt = new List<Type>();
            foreach (string file in Directory.GetFiles(Utils.pluginDir.FullName, "*.dll"))
            {
                //Console.WriteLine(file);
                Assembly asm = Assembly.Load(AssemblyName.GetAssemblyName(file));
                if (asm != null)
                {
                    foreach (Type type in asm.GetTypes())
                    {
                        if (type.IsInterface || type.IsAbstract)
                        {
                            continue;
                        }
                        else
                        {
                            if (type.GetInterface(typeof(IGenerator).FullName) != null)
                            {
                                pt.Add(type);
                                Console.WriteLine("Loading " + file + ":");
                            }
                        }
                    }
                }
            }

            foreach (Type t in pt)
            {
                //IGenerator gen = (IGenerator)Activator.CreateInstance(t);
                //Console.Write("\t"+gen + "...");
                //Stopwatch s = new Stopwatch();
                //s.Start();
                //gen.Generate(null);
                //s.Stop();
                //generators.Add(gen, s.ElapsedTicks);
                //Console.WriteLine("[{0}]", s.ElapsedTicks);
                Add((IGenerator)Activator.CreateInstance(t));
            }
            Console.WriteLine("Finished loading external plugins...");

        }

        public static void Load(params IGenerator[] gens)
        {
            //Console.WriteLine("Loading Generators...");
            foreach (var gen in gens)
            {
                //Console.Write(gen + "...");
                //Stopwatch s = new Stopwatch();
                //s.Start();
                //gen.Generate(null);
                //s.Stop();
                //generators.Add(gen, s.ElapsedTicks);
                //Console.WriteLine("[{0}]", s.ElapsedTicks);
                Add(gen);
            }
            LoadExt();
            Sort();
           // Console.WriteLine("Generator loading done!");
        }

        public static void LoadGenerator(IGenerator g)
        {
            //Console.Write(g + "...");
            //Stopwatch s = new Stopwatch();
            //s.Start();
            //g.Generate(null);
            //s.Stop();
            //generators.Add(g, s.ElapsedTicks);
            //Console.WriteLine("[{0}]", s.ElapsedTicks);
            Add(g);
        }

        public static void Sort()
        {
            //the longer the elapsed milliseconds, the more complex it is
            //generators = (from x in generators orderby x.Value ascending select x).ToDictionary(pair => pair.Key, pair => pair.Value);
            for (int i = 0; i < gens.Count; i++)
            {
                List<GeneratorData> sorted = (from x in gens.Values.ToList()[i] orderby x.speed ascending select x).ToList();
                gens[gens.Keys.ToList()[i]] = sorted;
            }
        }
    }
}
