// <copyright file="MatrixCreatorTest.cs">Copyright ©  2017</copyright>

using System;
using CodeMatrix;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMatrix.Tests
{
    /// <summary>Этот класс содержит параметризованные модульные тесты для MatrixCreator</summary>
    [PexClass(typeof(MatrixCreator))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class MatrixCreatorTest
    {
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
    }
}
