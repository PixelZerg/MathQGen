using LibMathQGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQGen.DefaultGens
{
    class Test1 : IGenerator
    {
        public MQData Generate(string args)
        {
            string s = "";
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                s += r.Next(1, 100);
                    if (r.NextDouble() > 0.5)
                    {
                        s += "+";
                    }
                    if (r.NextDouble() <= 0.5)
                    {
                        s += "-";
                    }

            }
            s += r.Next(1, 100);
            return new MQData(s,"Become mind-numbed:", null);
        }

        public string GetAuthor()
        {
            return "PixelZerg";
        }

        public string GetDescription()
        {
            return "woahh";
        }

        public string GetName()
        {
            return this.GetType().Name;
        }

        public Topic GetTopic()
        {
            return Topic.Others;
        }

        public void OnSelect()
        {
        }
    }
}
