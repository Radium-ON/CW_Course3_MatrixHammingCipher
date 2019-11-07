using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GilbertMourEncoding.Models;

namespace GilbertMourEncoding
{
    public class GilbertMourCodeAlgorithm
    {
        public struct CodeEntry
        {
            public CodeEntry(char xi, double pxi, double q, double sigma, int li, string sigmaBinary, string sigmaLimit)
            {
                Xi = xi;
                Pxi = pxi;
                Q = q;
                Sigma = sigma;
                Li = li;
                SigmaBinary = sigmaBinary;
                SigmaLimit = sigmaLimit;
            }

            public char Xi { get; set; }
            public double Pxi { get; set; }
            public double Q { get; set; }
            public double Sigma { get; set; }
            public int Li { get; set; }
            public string SigmaBinary { get; set; }
            public string SigmaLimit { get; set; }
        }

        public GilbertMourCodeAlgorithm()
        {
            
        }
        public GilbertMourCodeAlgorithm(ICollection<TableRecord.CharStats> stats)
        {
            foreach (var stat in stats)
            {
                _chars.Add(stat.Sign);
                _probabilities.Add(stat.Probability);
            }

            _cumulate = GetCumulateArray(_probabilities).ToList();
            _sigmas = GetSigmasArray(_probabilities, _cumulate).ToList();
            _limits = GetSigmaLengthArray(_probabilities).ToList();
            for (var i = 0; i < stats.Count; i++)
            {
                _codes.Add(ConvertFractToBinaryCode(_sigmas[i], _limits[i]));
                CodeEntries.Add(
                    new CodeEntry(_chars[i], _probabilities[i],
                        _cumulate[i],
                        _sigmas[i],
                        _limits[i], Convert.ToString($"0.{ConvertFractToBinaryCode(_sigmas[i],10)}"),
                        _codes[i]));
            }
        }

        private List<char> _chars = new List<char>();
        private List<double> _probabilities = new List<double>();
        private List<double> _cumulate = new List<double>();
        private List<double> _sigmas = new List<double>();
        private List<int> _limits = new List<int>();
        private List<string> _codes = new List<string>();

        public List<CodeEntry> CodeEntries { get; private set; }=new List<CodeEntry>();

        public string ConvertFractToBinaryCode(double sigma, int limit)
        {
            var sb = new StringBuilder();
            var right = (double)sigma - (int)sigma;
            for (var i = 0; i < limit; i++)
            {
                right = right * 2 - (int)right * 2;
                sb.Append((int)right);
                if (right == 1.0)
                {
                    break;
                }
            }

            return sb.ToString();
        }

        public double[] GetCumulateArray(List<double> probs)
        {
            var qList = new double[probs.Count];
            for (var i = 1; i < probs.Count; i++)
            {
                qList[i] = qList[i - 1] + probs[i - 1];
            }

            return qList;

        }

        public double[] GetSigmasArray(List<double> probs, List<double> qList)
        {
            var sigmas = new double[probs.Count];
            for (var i = 0; i < probs.Count; i++)
            {
                sigmas[i] = qList[i] + probs[i] / 2.0;
            }

            return sigmas;
        }

        public int[] GetSigmaLengthArray(List<double> probs)
        {
            var limits = new int[probs.Count];
            for (var i = 0; i < probs.Count; i++)
            {
                limits[i] = (int)Math.Ceiling(-Math.Log(probs[i] / 2.0, 2));
            }

            return limits;
        }
    }
}
