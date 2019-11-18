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

        public HammingRepairTools(byte[,] checkcodesMatrix)
        {
            HammingCodesMatrix = checkcodesMatrix;
            ExtendedMatrix = GetExtendedMatrix(HammingCodesMatrix);
        }

        private byte[,] GetExtendedMatrix(byte[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var colums = matrix.GetLength(1);
            var extended = new byte[rows+1,colums+1];
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < colums; col++)
                {
                    extended[row+1, col+1] = matrix[row, col];
                }
            }

            for (var i = 1; i < extended.GetLength(0); i++)
            {
                extended[i, 0] = 0;
            }

            for (var i = 0; i < extended.GetLength(1); i++)
            {
                extended[0, i] = 1;
            }
            return extended;
        }

        private byte[] SyndromeOf(byte[] u, byte[,] extendedMatrix)
        {
            var rows = extendedMatrix.GetLength(0);
            var columns = extendedMatrix.GetLength(1);
            var codeconstr = new byte[rows];
            for (var row = 0; row< rows; row++)
            {
                var xor = u.Select((t, bit) => t * extendedMatrix[row, bit]).Sum();
                codeconstr[row] = (byte)(xor % 2);
            }
            
            return codeconstr;
        }

        public List<byte[]> GetSyndromeList(IList<byte[]> constructions)
        {
            var result = new List<byte[]>();
            foreach (var construction in constructions)
            {
                result.Add(SyndromeOf(construction,ExtendedMatrix));
            }

            return result;
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
