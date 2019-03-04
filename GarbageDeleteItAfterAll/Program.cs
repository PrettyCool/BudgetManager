using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExchangeRate;

namespace GarbageDeleteItAfterAll
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = DateTime.Now;

            //try
            //{
            //    List<NBU_data> list = CurrencyExchangeRateXmlSerializer.LoadFromNbu(from, to);
            //    CurrencyExchangeRateXmlSerializer.AppendNewRatesOnly(list);
            //}
            //catch (Exception e) { Console.WriteLine(e.Message); }


            //CurrencyExchangeRateXmlSerializer.Save(list);
            //var t = CurrencyExchangeRateXmlSerializer.LoadFromXml();

        }
    }
}
