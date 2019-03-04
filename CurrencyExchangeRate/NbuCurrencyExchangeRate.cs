using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeRate
{
    /// <summary>
    /// source : http://www.shapovalenko.pro/?p=60
    /// </summary>
    public class NbuCurrencyExchangeRate
    {
        private static string connectionString = @"http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
        WebClient nbuExchangeRates = new WebClient { Encoding = Encoding.UTF8 };
        public RootNBUdata rootNB = new RootNBUdata();

        /// <summary>
        /// retrieves all the exchange rated of the current day
        /// </summary>
        public void GetCurrentRates()
        {
            rootNB.Parsed = JsonConvert.DeserializeObject<NBU_data[]>(nbuExchangeRates.DownloadString(connectionString));
        }

        /// <summary>
        /// Если необходимо получить курсы валют за прошлую дату, то к строке запроса добавляем параметр «date=YYYYMMDD».
        /// Например, для получения курсов на 22 марта 2017 года, необходимо сформировать такую строку:
        /// http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json&date=20170322
        /// API НБУ позволяет также загрузить курс только для одной валюты — параметр valcode = CCC, где CCC — международный код валюты(USD — доллар США, EUR — евро и т.д.).
        /// </summary>
        /// <param name="year">full year - 4 digits</param>
        /// <param name="month">two digits, e.g. 12 or 06</param>
        /// <param name="day">two digits, e.g. 31 or 06</param>
        public void GetCurrentRates(int year, int month, int day)
        {
            string sMonth = String.Format($"{month,0:D2}");
            string sDay = String.Format($"{day,0:D2}");

            string toAddToConnectionString = $"&date={year}{sMonth}{sDay}";
            rootNB.Parsed = JsonConvert.DeserializeObject<NBU_data[]>(nbuExchangeRates.DownloadString(connectionString + toAddToConnectionString));
        }

        /// <summary>
        /// загрузка конкретной валюты в текущий день
        /// API НБУ позволяет также загрузить курс только для одной валюты — параметр valcode=CCC, 
        /// где CCC — международный код валюты (USD — доллар США, EUR — евро и т.д.).
        /// </summary>
        internal void GetCurrentRate(string valCodeThreeLetter)//works bad
        {
            string toAddToConnectionString = connectionString + $"&valcode={valCodeThreeLetter}";
            rootNB.Parsed = JsonConvert.DeserializeObject<NBU_data[]>(nbuExchangeRates.DownloadString(toAddToConnectionString));
        }

        /// <summary>
        /// загрузка конкретной валюты в конкретный день
        /// API НБУ позволяет также загрузить курс только для одной валюты — параметр valcode=CCC, 
        /// где CCC — международный код валюты (USD — доллар США, EUR — евро и т.д.).
        /// </summary>
        internal void GetCurrentRate(int year, int month, int day, string currencyLetters)//works bad
        {
            string toAddToConnectionString = $"&date={year}{month}{day}";
            rootNB.Parsed = JsonConvert.DeserializeObject<NBU_data[]>(nbuExchangeRates.DownloadString(connectionString + toAddToConnectionString));
            rootNB.Parsed = rootNB.Parsed.Where(t => (t.cc.Equals(currencyLetters))).ToArray();
        }

        public void ShowLastLoaded()
        {
            foreach (NBU_data i in rootNB.Parsed)
            {
                Console.WriteLine(
                    $"{i.cc,-5} " +
                    $"{i.exchangedate,-10} " +
                    $"{i.rate,-10} " +
                    $"{i.txt,-40} " +
                    $"{i.r030,-5}");
            }
        }       
    }
}
