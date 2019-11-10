using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GilbertMourEncoding.Models
{
    public class CodingStepsTable
    {
        private readonly string textEntry;
        private readonly Dictionary<char, int> oneCharCountDictionary = new Dictionary<char, int>();

        public struct TableRecord
        {
            public TableRecord(int textLength, char sign, int signCount)
            {
                this.textLength = textLength;
                Sign = sign;
                SignCount = signCount;
            }

            private readonly int textLength;

            public char Sign { get; set; }
            public int SignCount { get; set; }
            public double Probability => Math.Round((double) SignCount / textLength, 2);
        }

        public List<TableRecord> ListStatsOneChar { get; set; }

        public CodingStepsTable(string textEntry)
        {
            this.textEntry = textEntry;
            FillSignsCountDictionary(oneCharCountDictionary);
            ListStatsOneChar = CreateSignStats(oneCharCountDictionary);
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
        /// Создание статистики вероятностей одного символа
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private List<TableRecord> CreateSignStats(Dictionary<char, int> dictionary)
        {
            var list = new List<TableRecord>();
            foreach (var sign in dictionary)
            {
                list.Add(new TableRecord(textEntry.Length, sign.Key, sign.Value));
            }

            return list;
        }
    }
}