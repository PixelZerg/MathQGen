using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMathQGen;

namespace ExexcelGCSEMathsHigher.IntegersPowersRoots
{
    public class HCFLCM : IGenerator
    {
        #region IGenerator
        public string GetAuthor()
        {
            return "PixelZerg";
        }
        public string GetDescription()
        {
            return "Finding the highest common factors and lowest common multiples of two numbers";
        }
        public string GetName()
        {
            return "HCF & LCM";
        }
        public Topic GetTopic()
        {
            return Topic.Numerics;
        }
        public void OnSelect()
        {
        }
        #endregion
        public MQData Generate(string args)
        {
            int t1 = 2;
            while (Utils.IsPrime(t1))
            {
                t1 = Utils.random.Next(10, 200);
            }
            int t2 = 2;
            while (Utils.IsPrime(t2))
            {
                t2 = Utils.random.Next(10, 100);
            }
            MQData ret = new MQData(t1 + "," + t2, "Find the HCM and LCM:", 2);

            //Now the steps
            List<MQStepData> steps = new List<MQStepData>();
            #region LCM
            Dictionary<int, int> multiply = new Dictionary<int, int>(); //num, count
            #region step 1
            string cmfacs1 = t1+"=";
            List<int> factors1 = Utils.GetPrimeFactors(t1);
            for (int i = 0; i < factors1.Count; i++)
            {
                cmfacs1 += factors1[i].ToString();
                if (i < factors1.Count - 1)
                {
                    cmfacs1 += @"\times";
                }

                #region mult
                if (multiply.ContainsKey(factors1[i]))
                {
                    int dif = factors1.Where(x => x == factors1[i]).Count() - multiply[factors1[i]];
                    if (dif > 0)
                    {
                        multiply[factors1[i]] += dif;
                    }
                }
                else
                {
                    multiply.Add(factors1[i], factors1.Where(x => x == factors1[i]).Count());
                }
                #endregion
            }
            string cmfacs2 = t2 + "=";
            List<int> factors2 = Utils.GetPrimeFactors(t2);
            for (int i = 0; i < factors2.Count; i++)
            {
                cmfacs2 += factors2[i].ToString();
                if (i < factors2.Count - 1)
                {
                    cmfacs2 += @"\times";
                }

                #region mult
                if (multiply.ContainsKey(factors2[i]))
                {
                    int dif = factors2.Where(x => x == factors2[i]).Count() - multiply[factors2[i]];
                    if (dif > 0)
                    {
                        multiply[factors2[i]] += dif;
                    }
                }
                else
                {
                    multiply.Add(factors2[i], factors2.Where(x => x == factors2[i]).Count());
                }
                #endregion
            }
            steps.Add(new MQStepData(@"\begin{array}{l}" + cmfacs1 + @"\\" + cmfacs2 + @"\end{array}","Find all of the prime factors for each number."));
            #endregion

            string step2 = "LCM=";
            int lcm = 1;
            int no = 0;
            foreach (var pair in multiply)
            {
                step2 += pair.Key + @"^{" + pair.Value + "}";
                if (no < multiply.Count - 1)
                {
                    step2 += @"\times";
                }
                lcm *= (int)Math.Pow(pair.Key,pair.Value);
                no++;
            }
            steps.Add(new MQStepData(step2, "Raise every unique prime factor by the most times it occurs in the prime factorisation of each number. Then multiply all of that together to get the LCM!"));
            steps.Add(new MQStepData("LCM=" + lcm, "Evaluate to get the LCM"));
            #endregion
            #region HCF
            steps.Add(new MQStepData(@"\begin{array}{l}" + cmfacs1 + @"\\" + cmfacs2 + @"\end{array}", "Now for HCF, start by finding all of the prime factors for each number again."));
            Dictionary<int, int> multiply2 = new Dictionary<int, int>();//num, pow (count)
            foreach (var f in factors1)
            {
                if (!multiply2.ContainsKey(f))
                {
                    int cinf1 = factors1.Where(x => x == f).Count();
                    int cinf2 = factors2.Where(x => x == f).Count();
                    multiply2.Add(f, Math.Min(cinf1, cinf2));
                }
            }

            foreach (var f in factors2)
            {
                int cinf1 = factors1.Where(x => x == f).Count();
                int cinf2 = factors2.Where(x => x == f).Count();
                if (!multiply2.ContainsKey(f))
                {
                    multiply2.Add(f, Math.Min(cinf1, cinf2));
                }
                //else
                //{
                //    multiply2[f]=
                //}
            }

            int hcf = 1;
            string step3 = "HCF=";
            no = 0;
            foreach (var pair in multiply2)
            {
                step3 += pair.Key + @"^{" + pair.Value + "}";
                if (no < multiply2.Count - 1)
                {
                    step3 += @"\times";
                }
                hcf *= (int)Math.Pow(pair.Key, pair.Value);
                no++;
            }
            steps.Add(new MQStepData(step3, "Raise each unique prime factor by the number of times that the prime factor occurs in the prime factorisation with the least occurances of the prime factor."));
            steps.Add(new MQStepData("HCF=" + hcf, "Evaluate to get HCF"));
            #endregion
            ret.steps = steps;
            return ret;

        }
    }
}
