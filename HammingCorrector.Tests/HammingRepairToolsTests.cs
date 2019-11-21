using Microsoft.VisualStudio.TestTools.UnitTesting;
using HammingCorrector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;

namespace HammingCorrector.Tests
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
    [PexClass(typeof(HammingRepairTools))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public class HammingRepairToolsTests
    {
        [TestMethod]
        public void GetSyndromeListTest()
        {
            //arrange
            var H = new byte[,]
            {
                {1, 1, 1, 0, 1, 0, 0},
                {1, 0, 1, 1, 0, 1, 0},
                {0, 1, 1, 1, 0, 0, 1}
            };
            var expectedSynList = new List<byte[]>
            {
                new byte[] {1, 1, 0, 1}
            };
            //act
            var tools = new HammingRepairTools(H);
            var actualSynList = tools.GetSyndromeList(new List<byte[]> { new byte[] { 1, 0, 1, 0, 1, 0, 1, 1 } });
            //assert
            Assert.That.AreEqual(expectedSynList, actualSynList,
                (m1, m2) =>
                {
                    if (m1.Where((t, i) => t.Length != m2[i].Length).Any())
                        return false;

                    for (var j = 0; j < m2.Count; j++)
                    {
                        for (var k = 0; k < m2[0].Length; k++)
                        {
                            if (m1[j][k] != m2[j][k])
                                return false;
                        }
                    }
                    return true;
                });
        }

        [TestMethod]
        public void HammingRepairToolsTest()
        {
            //arrange
            var H = new byte[,]
            {
                {1, 1, 1, 0, 1, 0, 0},
                {1, 0, 1, 1, 0, 1, 0},
                {0, 1, 1, 1, 0, 0, 1}
            };
            var extendedH = new byte[,]
            {
                {1, 1, 1, 1, 1, 1, 1, 1},
                {0, 1, 1, 1, 0, 1, 0, 0},
                {0, 1, 0, 1, 1, 0, 1, 0},
                {0, 0, 1, 1, 1, 0, 0, 1}
            };
            //act
            var tools = new HammingRepairTools(H);
            var actualExtended = tools.ExtendedMatrix;
            //assert
            CollectionAssert.AreEqual(extendedH, actualExtended);
        }

        [TestMethod]
        public void GetRepairedConstructionsTest()
        {
            //arrange
            var H = new byte[,]
            {
                {1, 1, 1, 0, 1, 0, 0},
                {1, 0, 1, 1, 0, 1, 0},
                {0, 1, 1, 1, 0, 0, 1}
            };
            var constructionlist = new List<byte[]>
            {
                new byte[] { 1, 0, 1, 0, 1, 0, 1, 1 },
                new byte[] { 1, 0, 1, 1, 1, 0, 1, 1 },
                new byte[] { 1, 1, 1, 0, 1, 0, 1, 1 },
            };

            var expectedRepairConstructions = new List<byte[]>
            {
                new byte[] {1, 0, 0, 0, 1, 0, 1, 1},
                new byte[] {1, 0, 1, 1, 1, 0, 1, 1},
                new byte[] {1, 1, 1, 0, 1, 0, 1, 1},
            };
            //act
            var tools = new HammingRepairTools(H);
            var syndromes = tools.GetSyndromeList(constructionlist);
            var actualRepairConstructions = tools.GetRepairedConstructions(constructionlist, syndromes);
            //assert
            Assert.That.AreEqual(expectedRepairConstructions, actualRepairConstructions,
                (m1, m2) =>
                {
                    if (m1.Where((t, i) => t.Length != m2[i].Length).Any())
                        return false;

                    for (var j = 0; j < m2.Count; j++)
                    {
                        for (var k = 0; k < m2[0].Length; k++)
                        {
                            if (m1[j][k] != m2[j][k])
                                return false;
                        }
                    }
                    return true;
                });
        }
    }
}