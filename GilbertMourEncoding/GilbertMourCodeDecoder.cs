using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GilbertMourEncoding
{
    public class GilbertMourCodeDecoder
    {
        private readonly Dictionary<string, char> _charWithCodeDictionary;

        public GilbertMourCodeDecoder(List<GilbertMourCodeAlgorithm.CodeEntry> codeEntries)
        {
            _charWithCodeDictionary = GetCharWithCodesDictionary(codeEntries);
        }


        public string DecodeHammingCodes(List<byte[]> repairedCodes)
        {
            var maxcodelength = _charWithCodeDictionary.Keys.Max(s => s.Length) + 1;
            var trimmedCode = GetCharSequenceFromListBytes(repairedCodes);

            var sblock = new StringBuilder();

            var sresult = new StringBuilder();

            var leng = 0;
            while (leng < trimmedCode.Length)
            {
                sblock.Append(trimmedCode[leng]);
                if (sblock.Length < maxcodelength)
                {
                    if (_charWithCodeDictionary.ContainsKey(sblock.ToString()))
                    {
                        sresult.Append(_charWithCodeDictionary[sblock.ToString()]);
                        sblock.Clear();
                    }
                }
                else
                {
                    sresult.Append('*');
                    sblock.Clear();
                }
                leng++;
            }

            return sresult.ToString();
        }

        private Dictionary<string, char> GetCharWithCodesDictionary(
            List<GilbertMourCodeAlgorithm.CodeEntry> codeEntries)
        {
            var dict = new Dictionary<string, char>();
            foreach (var codeEntry in codeEntries)
            {
                dict.Add(codeEntry.SigmaLimit, codeEntry.Xi);
            }

            return dict;
        }

        private string GetCharSequenceFromListBytes(List<byte[]> codes)
        {
            var sb = new StringBuilder();
            foreach (var code in codes)
            {
                sb.Append(string.Join("", code.Skip(1).Take(4)));
            }

            var notrimmed = sb.ToString();
            var trimmed=string.Empty;
            if (notrimmed.EndsWith("0"))
            {
                trimmed = notrimmed.TrimEnd('0');
                trimmed.Remove(trimmed.Length - 1);
                return trimmed;
            }

            return notrimmed;
        }
    }
}
