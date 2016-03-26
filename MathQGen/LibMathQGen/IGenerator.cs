using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMathQGen
{
    public interface IGenerator
    {
        MQData Generate(string args);
        void OnSelect();

        Topic GetTopic();
        string GetDescription();
        string GetName();
        string GetAuthor();
    }
}
