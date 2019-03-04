using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyInfo
{
    //https://justinchronicles.wordpress.com/2012/03/15/how-to-get-list-of-countries-defined-in-iso-3166-1-programatically-by-c-shar/
    /// <summary>
    /// Gets the list of countries based on ISO 3166-1
    /// </summary>
    /// <returns>Returns the list of countries based on ISO 3166-1</returns>
    public  class CurrencyGetter
    {
        public static List<RegionInfo> GetCountriesByIso3166()
        {
            List<RegionInfo> countries = new List<RegionInfo>();
            foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo country = new RegionInfo(culture.LCID);
                if (countries.Where(p => p.Name == country.Name).Count() == 0)
                    countries.Add(country);
            }
            return countries.OrderBy(p => p.EnglishName).ToList();
        }

        public void SerializeXmlInto(string pathToSerialize)
        {
            using (FileStream fs = new FileStream(pathToSerialize, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<RegionData>));
                List<RegionInfo> temp = GetCountriesByIso3166();
                List<RegionData> listData = new List<RegionData>();
                foreach (var i in temp)
                {
                    RegionData regionData = new RegionData()
                    {
                        CurrencyEnglishName = i.CurrencyEnglishName,
                        CurrencyNativeName = i.CurrencyNativeName,
                        CurrencySymbol = i.CurrencySymbol,
                        DisplayName = i.DisplayName,
                        EnglishName = i.EnglishName,
                        GeoId = i.GeoId,
                        IsMetric = i.IsMetric,
                        ISOCurrencySymbol = i.ISOCurrencySymbol,
                        Name = i.Name,
                        NativeName = i.NativeName,
                        ThreeLetterISORegionName = i.ThreeLetterISORegionName,
                        ThreeLetterWindowsRegionName = i.ThreeLetterWindowsRegionName,
                        TwoLetterISORegionName = i.TwoLetterISORegionName
                    };
                    listData.Add(regionData);
                }

                serializer.Serialize(fs, listData);
            }
        }
        public List<RegionData> DeserializeXmlFrom(string pathDeserialize)
        {
            using (FileStream fs = new FileStream(pathDeserialize, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<RegionData>));
                return serializer.Deserialize(fs) as List<RegionData>;
            }
        }

        public static void CultureDataShowAndSaveToFile()
        {
            string filePath = "../../CultureInfo.txt";
            File.Create(filePath).Close();

            List<RegionInfo> regionInfos = CurrencyGetter.GetCountriesByIso3166();

            // string maxLenCurName = "";
            foreach (var item in regionInfos)
            {
                string data =
                    $"{item.ISOCurrencySymbol,-5} : " +
                    $"{item.CurrencyEnglishName,-50} : " +
                    $"{item.ThreeLetterISORegionName,-4} : " +
                    $"{item.TwoLetterISORegionName,3} : " +
                    $"{item.EnglishName,-35} :" +
                    $"{item.DisplayName}";
                //Console.WriteLine(data);

                //if (maxLenCurName.Length < item.CurrencyEnglishName.Length)
                //    maxLenCurName = item.CurrencyEnglishName;

                AddData(filePath, data);
            }
            // Console.WriteLine($"currency with max length {maxLenCurName.Length} is {maxLenCurName}");
        }

        private static void AddData(string filePath, string data)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(data);
            }
        }

        public static List<RegionInfo> GetDistinctCurrencyList()
        {
            List<RegionInfo> currencyList = new List<RegionInfo>();
            List<RegionInfo> regionInfo = CurrencyInfo.CurrencyGetter.GetCountriesByIso3166();
            SortedDictionary<string, RegionInfo> distinctCurrencyVal = new SortedDictionary<string, RegionInfo>();
            foreach (RegionInfo item in regionInfo)
                if (!(distinctCurrencyVal.ContainsKey(item.ISOCurrencySymbol)))
                    distinctCurrencyVal.Add(item.ISOCurrencySymbol, item);

            foreach (RegionInfo i in distinctCurrencyVal.Values)
                currencyList.Add(i);

            return currencyList;
        }
    }
}