using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMathQGen;

namespace ExexcelGCSEMathsHigher.LengthAreaVolume
{
    public class CircleAreaCircum : IGenerator
    {
        #region IGenerator
        public string GetAuthor()
        {
            return "PixelZerg";
        }
        public string GetDescription()
        {
            return "Finding the area and circumference of circles";
        }
        public string GetName()
        {
            return "Area and Circumference of Circles";
        }
        public Topic GetTopic()
        {
            return Topic.Geometry;
        }
        public void OnSelect()
        {
        }
        #endregion

        public MQData Generate(string args)
        {
            double r = (double)(Utils.random.Next(2, 15)) + (double)(Utils.random.Next(0,10))/10;
            MQData ret = new MQData("<svg width=\"150\" height=\"150\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\">\r\n\t\t\t\t\t\t<circle cx=\"75\" cy=\"75\" r=\"72\" stroke=\"black\" stroke-width=\"2\" style=\"fill:red; fill-opacity:0.0;\"></circle>\r\n\t\t\t\t\t\t<circle cx=\"75\" cy=\"75\" r=\"2\" stroke=\"black\" stroke-width=\"2\" style=\"fill:red; fill-opacity:1.0;\"></circle>\r\n\t\t\t\t\t\t<line x1=\"75\" y1=\"75\" x2=\"142\" y2=\"50\" style=\"stroke:black;stroke-width:2\"></line>\r\n\t\t\t\t\t\t<text x=\"70\" y=\"90\" fill=\"black\" style=\"stroke: #000000;  stroke-width:0.5; font-size: 1em;\">o</text>\r\n\t\t\t\t\t\t<text x=\"90\" y=\"60\" fill=\"black\" transform=\"rotate(-17 100,60)\" style=\"stroke: #000000;  stroke-width:1.0;  font-size: 1em;\">"+r+"cm</text>\r\n\t\t\t\t\t</svg>",
                "Find the radius and circumference of this circle, where o is the centre.", 2);
            List<MQStepData> steps = new List<MQStepData>();
            steps.Add(new MQStepData(@"Area=\pi\times"+r+"^2", "Area = radius squared, multiplied by pi"));
            steps.Add(new MQStepData(@"Area=" + Math.Round(Math.PI * Math.Pow(r, 2), 3, MidpointRounding.AwayFromZero) + @"\tiny( to\;3dp)", "Evaluate to get area"));
            steps.Add(new MQStepData(@"Circumference=2\pi\times" + r, "Circumference= 2 times pi, multiplied by radius"));
            steps.Add(new MQStepData(@"Circumference=" + Math.Round(Math.PI * 2 * r, 3, MidpointRounding.AwayFromZero) + @"\tiny( to\;3dp)", "Evaluate to get circumference"));
            ret.steps = steps;
            return ret;
        }
    }
}
