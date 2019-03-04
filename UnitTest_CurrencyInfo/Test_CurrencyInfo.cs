using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyInfo;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UnitTest_CurrencyInfo
{
    [TestClass]
    public class Test_CurrencyInfo
    {
        [TestMethod]
        public void Test_USD()
        {
            string expectedCur = "USD";
            List<RegionInfo> regions = CurrencyGetter.GetCountriesByIso3166();
            RegionInfo regInf = regions.Where(t => t.ISOCurrencySymbol.Equals(expectedCur)).FirstOrDefault();

            string actual = $"{regInf.ISOCurrencySymbol} - {regInf.CurrencyEnglishName}";
            string expected = expectedCur + " - US Dollar";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EUR()
        {
            string expectedCur = "EUR";
            List<RegionInfo> regions = CurrencyGetter.GetCountriesByIso3166();
            RegionInfo regInf = regions.Where(t => t.ISOCurrencySymbol.Equals(expectedCur)).FirstOrDefault();

            string actual = $"{regInf.ISOCurrencySymbol} - {regInf.CurrencyEnglishName}";
            string expected = expectedCur + " - Euro";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_CheckDistinctCurList()
        {            
            List<RegionInfo> regions = CurrencyGetter.GetDistinctCurrencyList();
            int expected = regions.Count;

            int actual = regions.Select(t => t.CurrencyEnglishName).Distinct().Count();

            Assert.AreEqual(expected, actual);
        }
    }
}
