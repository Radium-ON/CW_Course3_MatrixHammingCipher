using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMatrixModule
{
    public class MatrixCreator
    {
        private string[] SplitIntoBlocks(string input, int key)
        {
            for (var i = 0; i < input.Length % key; i++)
            {
                if (i < 1)
                {
                    input += '1';
                }
                else
                {
                    input += '0';
                }
            }

            var stringSplit = input.Select((c, index) => new { c, index })
                .GroupBy(x => x.index / key)
                .Select(group => group.Select(item => item.c))
                .Select(chars => new string(chars.ToArray()));
            return stringSplit.ToArray();
        }

        public static T[,] TransposeMatrix<T>(T[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            T[,] Tmatrix = new T[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Tmatrix[j, i] = matrix[i, j];
                }
            }

            return Tmatrix;
        }
    }
}
