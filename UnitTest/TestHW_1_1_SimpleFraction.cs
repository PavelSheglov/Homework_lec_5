using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hw_1_1;

namespace UnitTest
{
    [TestClass]
    public class TestHW_1_1_SimpleFraction
    { 
        [TestMethod]
        public void TestDefaultConstructor()
        {
            var fraction = new SimpleFraction();
            Assert.AreEqual(0, fraction.Numerator);
            Assert.AreEqual(1, fraction.Denominator);
        }

        [TestMethod]
        public void PositiveTestSingleArgumentConstructor()
        {
            var fraction = new SimpleFraction(15);
            Assert.AreEqual(0, fraction.Numerator);
            Assert.AreEqual(1, fraction.Denominator);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void NegativeTestSingleArgumentConstructor()
        {
            var fraction = new SimpleFraction(0);
        }

        [TestMethod]
        public void PositiveTestDoubleArgumentsConstructor()
        {
            var fraction1 = new SimpleFraction(7, 15);
            var fraction2 = new SimpleFraction(5, 15);
            var fraction3 = new SimpleFraction(-45, 25);
            var fraction4 = new SimpleFraction(1, -2);

            Assert.AreEqual(7, fraction1.Numerator);
            Assert.AreEqual(15, fraction1.Denominator);
            Assert.AreEqual(1, fraction2.Numerator);
            Assert.AreEqual(3, fraction2.Denominator);
            Assert.AreEqual(-9, fraction3.Numerator);
            Assert.AreEqual(5, fraction3.Denominator);
            Assert.AreEqual(-1, fraction4.Numerator);
            Assert.AreEqual(2, fraction4.Denominator);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void NegativeTestDoubleArgumentsConstructor()
        {
            var fraction = new SimpleFraction(25, 0);
        }

        [TestMethod]
        public void TestGetSum()
        {
            var fraction1 = new SimpleFraction(7, 15);
            var fraction2 = new SimpleFraction(5, 15);
            var result1 = SimpleFraction.GetSum(fraction1, fraction2);
            var fraction3 = new SimpleFraction(3, 8);
            var fraction4 = new SimpleFraction(4, 5);
            var result2 = SimpleFraction.GetSum(fraction3, fraction4);

            Assert.AreEqual(4, result1.Numerator);
            Assert.AreEqual(5, result1.Denominator);
            Assert.AreEqual(47, result2.Numerator);
            Assert.AreEqual(40, result2.Denominator);
        }

        [TestMethod]
        public void TestGetDifference()
        {
            var fraction1 = new SimpleFraction(7, 15);
            var fraction2 = new SimpleFraction(-5, 15);
            var result1 = SimpleFraction.GetDifference(fraction1, fraction2);
            var fraction3 = new SimpleFraction(3, 8);
            var fraction4 = new SimpleFraction(4, 5);
            var result2 = SimpleFraction.GetDifference(fraction3, fraction4);

            Assert.AreEqual(4, result1.Numerator);
            Assert.AreEqual(5, result1.Denominator);
            Assert.AreEqual(-17, result2.Numerator);
            Assert.AreEqual(40, result2.Denominator);
        }

        [TestMethod]
        public void TestGetComposition()
        {
            var fraction1 = new SimpleFraction(7, 15);
            var fraction2 = new SimpleFraction(5, 49);
            var result1 = SimpleFraction.GetComposition(fraction1, fraction2);
            var fraction3 = new SimpleFraction(3, 11);
            var fraction4 = new SimpleFraction(4, 5);
            var result2 = SimpleFraction.GetComposition(fraction3, fraction4);

            Assert.AreEqual(1, result1.Numerator);
            Assert.AreEqual(21, result1.Denominator);
            Assert.AreEqual(12, result2.Numerator);
            Assert.AreEqual(55, result2.Denominator);
        }
        
        [TestMethod]
        public void PositiveTestGetQuotient()
        {
            var fraction1 = new SimpleFraction(15, 7);
            var fraction2 = new SimpleFraction(5, 49);
            var result1 = SimpleFraction.GetQuotient(fraction1, fraction2);
            var fraction3 = new SimpleFraction(3, 11);
            var fraction4 = new SimpleFraction(4, 5);
            var result2 = SimpleFraction.GetQuotient(fraction3, fraction4);

            Assert.AreEqual(21, result1.Numerator);
            Assert.AreEqual(1, result1.Denominator);
            Assert.AreEqual(15, result2.Numerator);
            Assert.AreEqual(44, result2.Denominator);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void NegativeTestGetQuotient()
        {
            var fraction1 = new SimpleFraction(15, 7);
            var fraction2 = new SimpleFraction(0, 49);
            var result1 = SimpleFraction.GetQuotient(fraction1, fraction2);
        }

        [TestMethod]
        public void TestToCompare()
        {
            var fraction1 = new SimpleFraction(3, 8);
            var fraction2 = new SimpleFraction(5, 9);
            var fraction3 = new SimpleFraction(5, 9);

            Assert.AreEqual(-1, fraction1.CompareTo(fraction2));
            Assert.AreEqual(1, fraction2.CompareTo(fraction1));
            Assert.AreEqual(0, fraction2.CompareTo(fraction3));
        }

        [TestMethod]
        public void TestToString()
        {
            var fraction1 = new SimpleFraction(3, 8);
            
            Assert.AreEqual("3/8", fraction1.ToString());
        }
    }
}
