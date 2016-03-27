using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQGen
{
    public class GeneratorData
    {
        public LibMathQGen.IGenerator generator = null;
        public long speed = 0;
        public GeneratorData(LibMathQGen.IGenerator _generator,long _speed)
        {
            generator = _generator;
            speed = _speed;
        }
        public GeneratorData(LibMathQGen.IGenerator _generator)
        {
            generator = _generator;
        }

    }
}
