using Microsoft.VisualStudio.TestTools.UnitTesting;
using PudelkoLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
namespace PudelkoTests
{
    [TestClass]
    public class UnitTestsUnitOfMeasure
    {
        [TestMethod]
        [DataRow(UnitOfMeasure.meter, 3)]
        [DataRow(UnitOfMeasure.centimeter, 1)]
        [DataRow(UnitOfMeasure.milimeter, 0)]
        public void Precisions(UnitOfMeasure u, int expectedPrecision)
        {
            Assert.AreEqual(UnitOfMeasureHelper.GetPrecisionForUnit(u), expectedPrecision);
        }

        [TestMethod]
        [DataRow(1.234, UnitOfMeasure.meter, UnitOfMeasure.milimeter, 1234.0)]
        [DataRow(1.234, UnitOfMeasure.meter, UnitOfMeasure.centimeter, 123.4)]
        [DataRow(1.234, UnitOfMeasure.meter, UnitOfMeasure.meter, 1.234)]
        [DataRow(123.263, UnitOfMeasure.centimeter, UnitOfMeasure.meter, 1.232)]
        [DataRow(4216, UnitOfMeasure.milimeter, UnitOfMeasure.meter, 4.216)]
        [DataRow(523, UnitOfMeasure.milimeter, UnitOfMeasure.centimeter, 52.3)]
        public void Converts(double val, UnitOfMeasure from, UnitOfMeasure to, double expected)
        {
            Assert.AreEqual(expected, UnitOfMeasureHelper.Convert(val, from, to));
        }

        [TestMethod]
        [DataRow(132.23, UnitOfMeasure.centimeter, "132.2")]
        [DataRow(5132.53, UnitOfMeasure.milimeter, "5132")]
        [DataRow(2.909, UnitOfMeasure.meter, "2.909")]
        public void DimensionsToString(double val, UnitOfMeasure u, string expected)
        {
            Assert.AreEqual(expected, UnitOfMeasureHelper.DimensionToString(val, u));
        }
    }
}
