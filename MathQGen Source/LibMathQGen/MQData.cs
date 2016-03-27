using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMathQGen
{
    public class MQData
    {
        public string function = "";
        public string Instruction = "";
        public int? Marks = null;

        private List<MQStepData> _steps = new List<MQStepData>();
        public List<MQStepData> steps
        {
            get { return _steps; }
            set { _steps = value; _answers = true; }
        }
        private bool _answers = false;
        public bool answers
        {
            get { return _answers; }
        }
        public bool IsLatex
        {
            get
            {
                return !(function.Contains("<") && function.Contains("</"));
            }
        }

        /// <param name="func">A LaTeX expression / HTML</param>
        /// <param name="instruction">The "question" e.g: Expand</param>
        /// <param name="marks">Number of marks for this question -- is nullable</param>
        public MQData(string func, string instruction, int? marks)
        {
            function = func;
            Instruction = instruction;
            Marks = marks;
        }
        /// <param name="func">A LaTeX expression / HTML</param>
        /// <param name="instruction">The "question" e.g: Expand</param>
        /// <param name="marks">Number of marks for this question -- is nullable</param>
        /// <param name="stdata">A list containing step data, should you wish to provide step by step answers to the problem</param>
        public MQData(string func, string instruction, int? marks, List<MQStepData> stdata)
        {
            function = func;
            Instruction = instruction;
            Marks = marks;
            steps = stdata;
            _answers = true;
        }
    }
}
