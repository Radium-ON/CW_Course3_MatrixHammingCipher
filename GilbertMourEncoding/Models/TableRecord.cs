using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GilbertMourEncoding.Models
{
    public class TableRecord
    {
        private readonly string textEntry;
        private readonly Dictionary<char, int> oneCharCountDictionary = new Dictionary<char, int>();
        private readonly Dictionary<string, int> twoCharCountDictionary = new Dictionary<string, int>();

        public struct CharStats
        {
            public CharStats(int textLength, char sign, int signCount)
            {
                this.textLength = textLength;
                Sign = sign;
                //TwoSign = "";
                SignCount = signCount;
            }

            //public CharStats(int textLength, string twoSign, int signCount)
            //{
            //    this.textLength = textLength;
            //    Sign = char.MinValue;
            //    TwoSign = twoSign;
            //    SignCount = signCount;
            //}
            private readonly int textLength;

            //public string TwoSign { get; set; }
            public char Sign { get; set; }
            public int SignCount { get; set; }
            public double Probability => Math.Round((double) SignCount / textLength, 2);
        }

        public List<CharStats> ListStatsOneChar { get; set; }
        //public List<CharStats> ListStatsTwoChar { get; set; }

        public TableRecord(string textEntry)
        {
            this.textEntry = textEntry;
            FillSignsCountDictionary(oneCharCountDictionary);
            //FillSignsCountDictionary(twoCharCountDictionary);
            ListStatsOneChar = CreateSignStats(oneCharCountDictionary);
            //ListStatsTwoChar = CreateSignStats(twoCharCountDictionary);
        }

        /// <summary>
        /// Расчёт встречаемости одного символа
        /// </summary>
        /// <param name="dictionary"></param>
        private void FillSignsCountDictionary(IDictionary<char, int> dictionary)
        {
            foreach (var sign in textEntry)
            {
                if (!dictionary.ContainsKey(sign))
                {
                    dictionary.Add(sign, 1);
                }
                else dictionary[sign] += 1;
            }
        }

        /// <summary>
        /// Расчёт встречаемости пар символов
        /// </summary>
        /// <param name="dictionary"></param>
        private void FillSignsCountDictionary(IDictionary<string, int> dictionary)
        {
            for (var i = 0; i < textEntry.Length - 1; i++)
            {
                var charPair = string.Concat(textEntry[i], textEntry[i + 1]);
                if (!dictionary.ContainsKey(charPair))
                {
                    dictionary.Add(charPair, 1);
                }
                else dictionary[charPair] += 1;
            }
        }

        /// <summary>
        /// Создание статистики вероятностей одного символа
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private List<CharStats> CreateSignStats(Dictionary<char, int> dictionary)
        {
            var list = new List<CharStats>();
            foreach (var sign in dictionary)
            {
                list.Add(new CharStats(textEntry.Length, sign.Key, sign.Value));
            }

            return list;
        }


        /// <summary>
        /// Безусловная энтропия для входной последовательности
        /// </summary>
        /// <returns></returns>
        public double AbsoluteEntropyCalculation()
        {
            var calcEntropy = 0.0;
            foreach (var stat in ListStatsOneChar)
            {
                calcEntropy += stat.Probability * Math.Log(stat.Probability, 2);
            }

            return -calcEntropy;
        }


        /// <summary>
        /// Расчет количества информации для сообщения из n не равновероятных символов
        /// </summary>
        /// <returns></returns>
        public double InformationQuantityCalculation()
        {
            var calcInfo = 0.0;
            foreach (var stat in ListStatsOneChar)
            {
                calcInfo += stat.Probability * Math.Log(stat.Probability, 2);
            }

            return -textEntry.Length * calcInfo;
        }
    }
}