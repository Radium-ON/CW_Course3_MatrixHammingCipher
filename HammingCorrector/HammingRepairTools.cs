using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HammingCorrector.ViewModels;

namespace HammingCorrector
{
    public class HammingRepairTools
    {
        public byte[,] HammingCodesMatrix { get; private set; }//исходная проверочная матрица
        public byte[,] ExtendedMatrix { get; private set; }//расширенная матрица Хэмминга

        private byte[,] GetExtendedMatrix(byte[,] matrix)
        {
            return new byte[,] { };
        }

        private byte[] SyndromeOf(byte[] u)
        {
            return new byte[] { };
        }

        public List<byte[]> GetSyndromeList(IList<byte[]> constructions)
        {
            return null;
        }

        private bool CheckSyndromeForError(byte[] syndrome)
        {
            return false;
        }

        private void RepairBitsInConstruction(byte[] syndrome)
        {

        }
    }
}
