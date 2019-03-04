using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyExchangeRate;
using System.Linq;

namespace UnitTest_CurrencyExchangeRates
{
    /// <summary>
    /// archive https://index.minfin.com.ua/exchange/archive/nbu/
    /// </summary>
    [TestClass]
    public class Test_CurrencyExchangeRates
    {
        [TestMethod]
        public void Test1_EUR_2008_10_5()
        {
            NbuCurrencyExchangeRate cur = new NbuCurrencyExchangeRate();
            cur.GetCurrentRates(2008, 10, 5);

            string expected = "EUR 05.10.2008 6.770344";
            NBU_data temp = cur.rootNB.Parsed.Where(t => t.cc.Equals("EUR")).FirstOrDefault();
            string actual = $"{temp.cc} {temp.exchangedate} {temp.rate.ToString().Replace(',', '.')}";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test2_USD_2016_10_05()
        {
            NbuCurrencyExchangeRate cur = new NbuCurrencyExchangeRate();
            cur.GetCurrentRates(2016, 10, 5);

            string expected = "USD 05.10.2016 25.864415";
            NBU_data temp = cur.rootNB.Parsed.Where(t => t.cc == "USD" && t.exchangedate.Equals("05.10.2016")).FirstOrDefault();
            string actual = $"{temp.cc} {temp.exchangedate} {temp.rate.ToString().Replace(',', '.')}";

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        //public void Test3_USD_2018_10_08()
        //{
        //    NbuCurrencyExchangeRate cur = new NbuCurrencyExchangeRate();
        //    cur.GetCurrentRates(2018, 8, 10);
        //    NBU_data temp = cur.rootNB.Parsed.Where(t => t.cc.Equals("USD")).FirstOrDefault();

        //    string actual = $"{temp.cc} {temp.exchangedate} {temp.rate.ToString().Replace(',', '.')}";
        //    string expected = "USD 10.08.1986 27.111393";

        //    Assert.AreEqual(expected, actual);
        //}
    }
}

//NbuCurrencyExchangeRate cur = new NbuCurrencyExchangeRate();
//cur.GetCurrentRates();
//cur.ShowLastLoaded();
//cur.GetCurrentRates(2008, 10, 5);
//cur.ShowLastLoaded();