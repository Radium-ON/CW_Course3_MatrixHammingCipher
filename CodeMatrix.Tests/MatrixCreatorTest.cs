// <copyright file="MatrixCreatorTest.cs">Copyright ©  2017</copyright>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeMatrix;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMatrix.Tests
{
    public static class MSTestExtension
    {
        public delegate bool CompareFunc<in T>(T obj1, T obj2);

        private class LambdaComparer<T> : IComparer
        {
            private readonly CompareFunc<T> _compareFunc;

            public LambdaComparer(CompareFunc<T> compareFunc)
            {
                _compareFunc = compareFunc;
            }

            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }

                if (!(x is T t1) || !(y is T t2))
                {
                    return -1;
                }

                return _compareFunc(t1, t2) ? 0 : 1;
            }
        }
        public static void AreEqual<T>(this Assert assert, T expected, T actual, CompareFunc<T> compareFunc)
        {
            var comparer = new LambdaComparer<T>(compareFunc);

            CollectionAssert.AreEqual(
                new[] { expected },
                new[] { actual }, comparer,
                $"\nExpected: <{expected}>.\nActual: <{actual}>.");
        }
    }

    /// <summary>Этот класс содержит параметризованные модульные тесты для MatrixCreator</summary>
    [PexClass(typeof(MatrixCreator))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class MatrixCreatorTest
    {
        private class JaggedArrayComparer : Comparer<byte[][]>
        {
            public override int Compare(byte[][] x, byte[][] y)
            {
                var comp = 0;
                if (x?.LongLength!=y?.LongLength)
                {
                    return 0;
                }

                for (var j = 0; j < x.Length; j ++)
                {
                    for (var k = 0; k < x[j].Length; k ++)
                    {
                        comp=x[j][k].CompareTo(y[j][k]);
                        if (comp==0)
                        {
                            return comp;
                        }

                    }
                }
                return comp;
            }
        }

        [TestMethod]
        public void MatrixCreatorTest_GetHammingMatrix()
        {
            //arrange
            var expectedG = new byte[,]
            {
                {1, 0, 0, 0, 1, 1, 0},
                {0, 1, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 1, 1},
                {0, 0, 0, 1, 0, 1, 1}
            };
            var expectedH = new byte[,]
            {
                {1, 1, 1, 0, 1, 0, 0},
                {1, 0, 1, 1, 0, 1, 0},
                {0, 1, 1, 1, 0, 0, 1}
            };
            //act
            var m = new MatrixCreator();
            var actualG = m.GeneratingMatrix;
            var actualH = m.HammingCodesMatrix;
            //assert
            CollectionAssert.AreEqual(expectedG, actualG);
            CollectionAssert.AreEqual(expectedH, actualH);
        }

        [TestMethod]
        public void MatrixCreatorTest_ConstructorGenerateMatrix()
        {
            //arrange
            var expectedG = new byte[,]
            {
                {1, 0, 0, 0, 1, 1, 0},
                {0, 1, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 1, 1},
                {0, 0, 0, 1, 0, 1, 1}
            };

            //act
            var m = new MatrixCreator(expectedG);
            var actualG = m.GeneratingMatrix;

            //assert
            CollectionAssert.AreEqual(expectedG, actualG);
        }

        [TestMethod]
        public void GetCodeConstructionsFromCipherTest()
        {
            //arrange
            var cipher = "0010010010111011111111";
            var defaultG = new byte[,]
            {
                {1, 0, 0, 0, 1, 1, 0},
                {0, 1, 0, 0, 1, 0, 1},
                {0, 0, 1, 0, 1, 1, 1},
                {0, 0, 0, 1, 0, 1, 1}
            };
            var expectedCodes = new byte[][]
            {
                new byte[]{0, 0, 0, 1, 0, 1, 1, 1},
                new byte[]{1, 0, 1, 0, 0, 1, 0, 1},
                new byte[]{0, 1, 0, 1, 1, 0, 1, 0},
                new byte[]{0, 1, 0, 1, 1, 0, 1, 0},
                new byte[]{1, 1, 1, 1, 1, 1, 1, 1},
                new byte[]{0, 1, 1, 1, 0, 1, 0, 0}
            };
            //act
            var m = new MatrixCreator(defaultG);
            var actualCodes = m.GetCodeConstructionsFromCipher(cipher);
            //assert
            Assert.That.AreEqual(expectedCodes, actualCodes,(
                (m1, m2) =>
                {
                    if (m1.Where((t, i) => t.Length!=m2[i].Length).Any())
                        return false;

                    for (var j = 0; j < m2.Length; j ++)
                    {
                        for (var k = 0; k < m2[0].Length; k ++)
                        {
                            if (m1[j][k] != m2[j][k])
                                return false;
                        }
                    }
                    return true;
                }));
        }
    }
}
