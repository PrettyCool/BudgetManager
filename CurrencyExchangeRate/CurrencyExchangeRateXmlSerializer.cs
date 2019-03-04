using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace CurrencyExchangeRate
{
    public class CurrencyExchangeRateXmlSerializer
    {
        private static readonly string rootDir;
        private static readonly string filePath;
        private static NbuCurrencyExchangeRate nbuRates;
        private static NBU_data[] rates;

        static CurrencyExchangeRateXmlSerializer()
        {
            rootDir = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            filePath = rootDir + "\\" + "serializedCurrencyRates.xml";
            nbuRates = new NbuCurrencyExchangeRate();
            rates = null;
        }

        /// <summary>
        /// Gets a list of NBU rates for a specified period.
        /// Set the period From -> TO to download data from NBU site 
        /// </summary>
        public static List<NBU_data> LoadFromNbu(DateTime from, DateTime to)
        {
            List<NBU_data> toSerialize = new List<NBU_data>();            
            while (from <= to)
            {
                nbuRates.rootNB.Parsed = null;
                nbuRates.GetCurrentRates(from.Year, from.Month, from.Day);//an array of rates for a day
                foreach (NBU_data i in nbuRates.rootNB.Parsed)
                    toSerialize.Add(i);
                from = from.AddDays(1);
            }
            return toSerialize;
        }
        /// <summary>
        /// Creates a new file/overwrites existing file. And saves a list og NBU rates in XML format
        /// 
        /// </summary>
        /// <param name="toSerialize"></param>
        public static void Save(List<NBU_data> toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<NBU_data>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, toSerialize);
            }
        }

        public static void AppendNewRatesOnly(List<NBU_data> toSerialize)
        {
            EnsureFileCreation();
            XmlDocument loadedXml = LoadData();//loading already saved rates from xml file
            XmlElement root = loadedXml.DocumentElement;
            XmlNodeList loadedRates = root.SelectNodes(nameof(NBU_data));

            foreach (NBU_data rate in toSerialize)//loaded from NBU
            {
                bool alreadyExists = false;
                foreach (XmlNode i in loadedRates)//already saved in a file
                {
                    try
                    {
                        //[{ "r030":36,"txt":"Австралійський долар","rate":20.763487,"cc":"AUD","exchangedate":"22.03.2017" }]
                        if (i.SelectSingleNode("r030").InnerText.Equals(rate.r030.ToString()) &&
                            DateTime.Parse(i.SelectSingleNode("exchangedate").InnerText) == DateTime.Parse(rate.exchangedate))
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
                if (!alreadyExists)
                {
                    XmlNode newNode = loadedXml.CreateElement(nameof(NBU_data));

                    XmlNode r030 = loadedXml.CreateElement(nameof(rate.r030));
                    r030.InnerText = rate.r030.ToString();

                    XmlNode cc= loadedXml.CreateElement(nameof(rate.cc));
                    cc.InnerText = rate.cc;

                    XmlNode exchangedate = loadedXml.CreateElement(nameof(rate.exchangedate));
                    exchangedate.InnerText = rate.exchangedate;

                    XmlNode cource = loadedXml.CreateElement(nameof(rate.rate));
                    cource.InnerText = rate.rate.ToString().Replace(',','.');

                    XmlNode txt = loadedXml.CreateElement(nameof(rate.txt));
                    txt.InnerText = rate.txt;

                    newNode.AppendChild(r030);
                    newNode.AppendChild(cc);
                    newNode.AppendChild(exchangedate);
                    newNode.AppendChild(cource);
                    newNode.AppendChild(txt);

                    root.AppendChild(newNode);
                }
            }

            loadedXml.Save(filePath);
        }

        public static List<NBU_data> LoadFromXml()
        {
            List<NBU_data> list = new List<NBU_data>();

            XmlDocument document = LoadData();
            XmlElement root = document.DocumentElement;
            XmlNodeList allRates = root.SelectNodes(nameof(NBU_data));

            foreach (XmlNode rate in allRates)
            {
                //try
                //{
                //    var cc = rate.SelectSingleNode("cc").InnerText;
                //    var exchangedate = rate.SelectSingleNode("exchangedate").InnerText;
                //    var r030 = int.Parse(rate.SelectSingleNode("r030").InnerText);
                //    var cource = double.Parse(rate.SelectSingleNode("rate").InnerText.Replace('.',','));
                //    var txt = rate.SelectSingleNode("txt").InnerText;
                //}
                //catch (Exception e)                {                }

                list.Add(
                    new NBU_data()
                    {
                        cc = rate.SelectSingleNode("cc").InnerText,
                        exchangedate = rate.SelectSingleNode("exchangedate").InnerText,
                        r030 = int.Parse(rate.SelectSingleNode("r030").InnerText),
                        rate = double.Parse(rate.SelectSingleNode("rate").InnerText.Replace('.',',')),
                        txt = rate.SelectSingleNode("txt").InnerText
                    });
            }

            return list;
        }

        private static XmlDocument LoadData()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);
            return xDoc;
        }

        private static void EnsureFileCreation() => File.Open(filePath, FileMode.OpenOrCreate).Close();
    }
}
