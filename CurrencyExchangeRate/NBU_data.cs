using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;//nuget package Newtonsoft.Json is required

namespace CurrencyExchangeRate
{
    [Serializable]
    [DataContract]
    public class NBU_data
    {
        public NBU_data()        {        }

        [DataMember]
        [JsonProperty(PropertyName = "r030")]
        public int r030; // международный код валюты

        [DataMember]
        [JsonProperty(PropertyName = "txt")]
        public string txt; // наименование валюты

        [DataMember]
        [JsonProperty(PropertyName = "rate")]
        public double rate; // курс валюты по отношению к гривне

        [DataMember]
        [JsonProperty(PropertyName = "cc")]
        public string cc; // международный код валюты

        [DataMember]
        [JsonProperty(PropertyName = "exchangedate")]
        public string exchangedate; // дата курса
    }
}
