using System;
using System.Runtime.Serialization;

namespace CurrencyInfo
{
    [Serializable]
    public class RegionData
    {
        [DataMember]
        public string CurrencyNativeName { get; set; }
        [DataMember]
        public string CurrencyEnglishName { get; set; }
        [DataMember]
        public int GeoId { get; set; }
        [DataMember]
        public bool IsMetric { get; set; }
        [DataMember]
        public string ThreeLetterWindowsRegionName { get; set; }
        [DataMember]
        public string ThreeLetterISORegionName { get; set; }
        [DataMember]
        public string TwoLetterISORegionName { get; set; }
        [DataMember]
        public string NativeName { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string EnglishName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string CurrencySymbol { get; set; }
        [DataMember]
        public string ISOCurrencySymbol { get; set; }

        public RegionData()
        {

        }
    }
}
