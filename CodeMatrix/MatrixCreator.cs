using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeMatrix
{
    public class MatrixCreator
    {
        public byte[,] HammingCodesMatrix { get; set; }
        public byte[,] GeneratingMatrix { get; set; }

        public MatrixCreator(byte[,] generatingMatrix = null)
        {
            //порождающая матрица по умолчанию (вариант задания)
            if (generatingMatrix == null)
            {
                var g = new byte[,]
                {
                    {1, 0, 0, 0, 1, 1, 0},
                    {0, 1, 0, 0, 1, 0, 1},
                    {0, 0, 1, 0, 1, 1, 1},
                    {0, 0, 0, 1, 0, 1, 1}
                };
                GeneratingMatrix = g;
            }
            else
            {
                GeneratingMatrix = generatingMatrix;
            }
            HammingCodesMatrix = GetHammingCodeMatrix(GeneratingMatrix, 3);
        }

        public static T[,] TransposeMatrix<T>(T[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            T[,] Tmatrix = new T[columns, rows];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Tmatrix[j, i] = matrix[i, j];
                }
            }

            return Tmatrix;
        }

        public byte[][] GetCodeConstructionsFromCipher(string cipher)
        {
            var blocks = SplitIntoBlocks(cipher, 4);
            var blockLength = blocks[0].Length;
            var codematrix = new byte[blocks.Length][];

            for (var i = 0; i < blocks.Length; i++)
            {
                codematrix[i]=new byte[8];
            }
            
            //get block, parse it to byte array and * matrix
            for (var block = 0; block < blocks.Length; block++)
            {
                var bitblock = new byte[blockLength];
                for (var bit = 0; bit < blockLength; bit++)
                {
                    var b = blocks[block];
                    bitblock[bit] = (byte)char.GetNumericValue(b[bit]);
                }

                var vector = MultiplyVectorByGMatrix(bitblock, GeneratingMatrix);

                for (var i = 0; i < vector.Length; i++)
                {
                    codematrix[block][i] = vector[i];
                }
            }

            return codematrix;
        }

        private byte[] MultiplyVectorByGMatrix(byte[] bitblock, byte[,] gmatrix)
        {
            var gColumns = gmatrix.GetLength(1);
            var codeconstr = new byte[gColumns + 1];
            for (var column = 0; column < gColumns; column++)//columns G (7)
            {
                var xor = bitblock.Select((t, bit) => t * gmatrix[bit, column]).Sum();
                codeconstr[column + 1] = (byte)(xor % 2);
            }
            //xor check bits
            codeconstr[0] = (byte)(codeconstr[5] ^ codeconstr[6] ^ codeconstr[7]);
            return codeconstr;
        }

        private byte[,] GetHammingCodeMatrix(byte[,] gmatrix, int checkbits)
        {
            var rows = gmatrix.GetLength(0);
            var columns = gmatrix.GetLength(1);
            var checkmatrix = new byte[4, checkbits];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 4; j < columns; j++)
                {
                    checkmatrix[i, j - 4] = gmatrix[i, j];
                }
            }

            var transposeMatrix = TransposeMatrix(checkmatrix);
            var transrows = transposeMatrix.GetLength(0);
            var hammingMatrix = new byte[transrows, columns];
            for (var i = 0; i < transrows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (j < columns - 3)
                    {
                        hammingMatrix[i, j] = transposeMatrix[i, j];
                    }
                    else if (i == j - 4)
                    {
                        hammingMatrix[i, j] = 1;
                    }
                    else
                    {
                        hammingMatrix[i, j] = 0;
                    }
                }
            }

            return hammingMatrix;
        }

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
    }
}
