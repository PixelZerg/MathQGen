using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMathQGen
{
    public class MQStepData
    {
        public string function = "";
        public string Description = "";
        public bool IsLatex
        {
            get
            {
                return !(function.Contains("<") && function.Contains("</"));
            }
        }

        public MQStepData(string func, string desc)
        {
            function = func;
            Description = desc;
        }
    }
}
