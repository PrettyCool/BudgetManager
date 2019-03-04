using System;
//using ISO3166;
using System.Runtime.Serialization;

namespace CountryData
{
    [Serializable]
    public class Country
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string TwoLetterCode { get; set; }
        [DataMember] public string ThreeLetterCode { get; set; }
        [DataMember] public string NumericCode { get; set; }

        public Country()
        {

        }
        private Country(string name, string twoLetterCode, string threeLetterCode, string numericCode)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            NumericCode = numericCode;
        }

        public static readonly Country[] List = new[]
        {
            //source: https://www.nationsonline.org/oneworld/country_code_list.htm
            new Country("Afghanistan", "AF", "AFG", "4"),
            new Country("Aland Islands", "AX", "ALA", "248"),
            new Country("Albania", "AL", "ALB", "8"),
            new Country("Algeria", "DZ", "DZA", "12"),
            new Country("American Samoa", "AS", "ASM", "16"),
            new Country("Andorra", "AD", "AND", "20"),
            new Country("Angola", "AO", "AGO", "24"),
            new Country("Anguilla", "AI", "AIA", "660"),
            new Country("Antarctica", "AQ", "ATA", "10"),
            new Country("Antigua and Barbuda", "AG", "ATG", "28"),
            new Country("Argentina", "AR", "ARG", "32"),
            new Country("Armenia", "AM", "ARM", "51"),
            new Country("Aruba", "AW", "ABW", "533"),
            new Country("Australia", "AU", "AUS", "36"),
            new Country("Austria", "AT", "AUT", "40"),
            new Country("Azerbaijan", "AZ", "AZE", "31"),
            new Country("Bahamas", "BS", "BHS", "44"),
            new Country("Bahrain", "BH", "BHR", "48"),
            new Country("Bangladesh", "BD", "BGD", "50"),
            new Country("Barbados", "BB", "BRB", "52"),
            new Country("Belarus", "BY", "BLR", "112"),
            new Country("Belgium", "BE", "BEL", "56"),
            new Country("Belize", "BZ", "BLZ", "84"),
            new Country("Benin", "BJ", "BEN", "204"),
            new Country("Bermuda", "BM", "BMU", "60"),
            new Country("Bhutan", "BT", "BTN", "64"),
            new Country("Bolivia", "BO", "BOL", "68"),
            new Country("Bosnia and Herzegovina", "BA", "BIH", "70"),
            new Country("Botswana", "BW", "BWA", "72"),
            new Country("Bouvet Island", "BV", "BVT", "74"),
            new Country("Brazil", "BR", "BRA", "76"),
            new Country("British Virgin Islands", "VG", "VGB", "92"),
            new Country("British Indian Ocean Territory", "IO", "IOT", "86"),
            new Country("Brunei Darussalam", "BN", "BRN", "96"),
            new Country("Bulgaria", "BG", "BGR", "100"),
            new Country("Burkina Faso", "BF", "BFA", "854"),
            new Country("Burundi", "BI", "BDI", "108"),
            new Country("Cambodia", "KH", "KHM", "116"),
            new Country("Cameroon", "CM", "CMR", "120"),
            new Country("Canada", "CA", "CAN", "124"),
            new Country("Cape Verde", "CV", "CPV", "132"),
            new Country("Cayman Islands", "KY", "CYM", "136"),
            new Country("Central African Republic", "CF", "CAF", "140"),
            new Country("Chad", "TD", "TCD", "148"),
            new Country("Chile", "CL", "CHL", "152"),
            new Country("China", "CN", "CHN", "156"),
            new Country("Hong Kong, SAR China", "HK", "HKG", "344"),
            new Country("Macao, SAR China", "MO", "MAC", "446"),
            new Country("Christmas Island", "CX", "CXR", "162"),
            new Country("Cocos (Keeling) Islands", "CC", "CCK", "166"),
            new Country("Colombia", "CO", "COL", "170"),
            new Country("Comoros", "KM", "COM", "174"),
            new Country("Congo (Brazzaville)", "CG", "COG", "178"),
            new Country("Congo, (Kinshasa)", "CD", "COD", "180"),
            new Country("Cook Islands", "CK", "COK", "184"),
            new Country("Costa Rica", "CR", "CRI", "188"),
            new Country("Côte d'Ivoire", "CI", "CIV", "384"),
            new Country("Croatia", "HR", "HRV", "191"),
            new Country("Cuba", "CU", "CUB", "192"),
            new Country("Cyprus", "CY", "CYP", "196"),
            new Country("Czech Republic", "CZ", "CZE", "203"),
            new Country("Denmark", "DK", "DNK", "208"),
            new Country("Djibouti", "DJ", "DJI", "262"),
            new Country("Dominica", "DM", "DMA", "212"),
            new Country("Dominican Republic", "DO", "DOM", "214"),
            new Country("Ecuador", "EC", "ECU", "218"),
            new Country("Egypt", "EG", "EGY", "818"),
            new Country("El Salvador", "SV", "SLV", "222"),
            new Country("Equatorial Guinea", "GQ", "GNQ", "226"),
            new Country("Eritrea", "ER", "ERI", "232"),
            new Country("Estonia", "EE", "EST", "233"),
            new Country("Ethiopia", "ET", "ETH", "231"),
            new Country("Falkland Islands (Malvinas)", "FK", "FLK", "238"),
            new Country("Faroe Islands", "FO", "FRO", "234"),
            new Country("Fiji", "FJ", "FJI", "242"),
            new Country("Finland", "FI", "FIN", "246"),
            new Country("France", "FR", "FRA", "250"),
            new Country("French Guiana", "GF", "GUF", "254"),
            new Country("French Polynesia", "PF", "PYF", "258"),
            new Country("French Southern Territories", "TF", "ATF", "260"),
            new Country("Gabon", "GA", "GAB", "266"),
            new Country("Gambia", "GM", "GMB", "270"),
            new Country("Georgia", "GE", "GEO", "268"),
            new Country("Germany", "DE", "DEU", "276"),
            new Country("Ghana", "GH", "GHA", "288"),
            new Country("Gibraltar", "GI", "GIB", "292"),
            new Country("Greece", "GR", "GRC", "300"),
            new Country("Greenland", "GL", "GRL", "304"),
            new Country("Grenada", "GD", "GRD", "308"),
            new Country("Guadeloupe", "GP", "GLP", "312"),
            new Country("Guam", "GU", "GUM", "316"),
            new Country("Guatemala", "GT", "GTM", "320"),
            new Country("Guernsey", "GG", "GGY", "831"),
            new Country("Guinea", "GN", "GIN", "324"),
            new Country("Guinea-Bissau", "GW", "GNB", "624"),
            new Country("Guyana", "GY", "GUY", "328"),
            new Country("Haiti", "HT", "HTI", "332"),
            new Country("Heard and Mcdonald Islands", "HM", "HMD", "334"),
            new Country("Holy See (Vatican City State)", "VA", "VAT", "336"),
            new Country("Honduras", "HN", "HND", "340"),
            new Country("Hungary", "HU", "HUN", "348"),
            new Country("Iceland", "IS", "ISL", "352"),
            new Country("India", "IN", "IND", "356"),
            new Country("Indonesia", "ID", "IDN", "360"),
            new Country("Iran, Islamic Republic of", "IR", "IRN", "364"),
            new Country("Iraq", "IQ", "IRQ", "368"),
            new Country("Ireland", "IE", "IRL", "372"),
            new Country("Isle of Man", "IM", "IMN", "833"),
            new Country("Israel", "IL", "ISR", "376"),
            new Country("Italy", "IT", "ITA", "380"),
            new Country("Jamaica", "JM", "JAM", "388"),
            new Country("Japan", "JP", "JPN", "392"),
            new Country("Jersey", "JE", "JEY", "832"),
            new Country("Jordan", "JO", "JOR", "400"),
            new Country("Kazakhstan", "KZ", "KAZ", "398"),
            new Country("Kenya", "KE", "KEN", "404"),
            new Country("Kiribati", "KI", "KIR", "296"),
            new Country("Korea (North)", "KP", "PRK", "408"),
            new Country("Korea (South)", "KR", "KOR", "410"),
            new Country("Kuwait", "KW", "KWT", "414"),
            new Country("Kyrgyzstan", "KG", "KGZ", "417"),
            new Country("Lao PDR", "LA", "LAO", "418"),
            new Country("Latvia", "LV", "LVA", "428"),
            new Country("Lebanon", "LB", "LBN", "422"),
            new Country("Lesotho", "LS", "LSO", "426"),
            new Country("Liberia", "LR", "LBR", "430"),
            new Country("Libya", "LY", "LBY", "434"),
            new Country("Liechtenstein", "LI", "LIE", "438"),
            new Country("Lithuania", "LT", "LTU", "440"),
            new Country("Luxembourg", "LU", "LUX", "442"),
            new Country("Macedonia, Republic of", "MK", "MKD", "807"),
            new Country("Madagascar", "MG", "MDG", "450"),
            new Country("Malawi", "MW", "MWI", "454"),
            new Country("Malaysia", "MY", "MYS", "458"),
            new Country("Maldives", "MV", "MDV", "462"),
            new Country("Mali", "ML", "MLI", "466"),
            new Country("Malta", "MT", "MLT", "470"),
            new Country("Marshall Islands", "MH", "MHL", "584"),
            new Country("Martinique", "MQ", "MTQ", "474"),
            new Country("Mauritania", "MR", "MRT", "478"),
            new Country("Mauritius", "MU", "MUS", "480"),
            new Country("Mayotte", "YT", "MYT", "175"),
            new Country("Mexico", "MX", "MEX", "484"),
            new Country("Micronesia, Federated States of", "FM", "FSM", "583"),
            new Country("Moldova", "MD", "MDA", "498"),
            new Country("Monaco", "MC", "MCO", "492"),
            new Country("Mongolia", "MN", "MNG", "496"),
            new Country("Montenegro", "ME", "MNE", "499"),
            new Country("Montserrat", "MS", "MSR", "500"),
            new Country("Morocco", "MA", "MAR", "504"),
            new Country("Mozambique", "MZ", "MOZ", "508"),
            new Country("Myanmar", "MM", "MMR", "104"),
            new Country("Namibia", "NA", "NAM", "516"),
            new Country("Nauru", "NR", "NRU", "520"),
            new Country("Nepal", "NP", "NPL", "524"),
            new Country("Netherlands", "NL", "NLD", "528"),
            new Country("Netherlands Antilles", "AN", "ANT", "530"),
            new Country("New Caledonia", "NC", "NCL", "540"),
            new Country("New Zealand", "NZ", "NZL", "554"),
            new Country("Nicaragua", "NI", "NIC", "558"),
            new Country("Niger", "NE", "NER", "562"),
            new Country("Nigeria", "NG", "NGA", "566"),
            new Country("Niue", "NU", "NIU", "570"),
            new Country("Norfolk Island", "NF", "NFK", "574"),
            new Country("Northern Mariana Islands", "MP", "MNP", "580"),
            new Country("Norway", "NO", "NOR", "578"),
            new Country("Oman", "OM", "OMN", "512"),
            new Country("Pakistan", "PK", "PAK", "586"),
            new Country("Palau", "PW", "PLW", "585"),
            new Country("Palestinian Territory", "PS", "PSE", "275"),
            new Country("Panama", "PA", "PAN", "591"),
            new Country("Papua New Guinea", "PG", "PNG", "598"),
            new Country("Paraguay", "PY", "PRY", "600"),
            new Country("Peru", "PE", "PER", "604"),
            new Country("Philippines", "PH", "PHL", "608"),
            new Country("Pitcairn", "PN", "PCN", "612"),
            new Country("Poland", "PL", "POL", "616"),
            new Country("Portugal", "PT", "PRT", "620"),
            new Country("Puerto Rico", "PR", "PRI", "630"),
            new Country("Qatar", "QA", "QAT", "634"),
            new Country("Réunion", "RE", "REU", "638"),
            new Country("Romania", "RO", "ROU", "642"),
            new Country("Russian Federation", "RU", "RUS", "643"),
            new Country("Rwanda", "RW", "RWA", "646"),
            new Country("Saint-Barthélemy", "BL", "BLM", "652"),
            new Country("Saint Helena", "SH", "SHN", "654"),
            new Country("Saint Kitts and Nevis", "KN", "KNA", "659"),
            new Country("Saint Lucia", "LC", "LCA", "662"),
            new Country("Saint-Martin (French part)", "MF", "MAF", "663"),
            new Country("Saint Pierre and Miquelon", "PM", "SPM", "666"),
            new Country("Saint Vincent and Grenadines", "VC", "VCT", "670"),
            new Country("Samoa", "WS", "WSM", "882"),
            new Country("San Marino", "SM", "SMR", "674"),
            new Country("Sao Tome and Principe", "ST", "STP", "678"),
            new Country("Saudi Arabia", "SA", "SAU", "682"),
            new Country("Senegal", "SN", "SEN", "686"),
            new Country("Serbia", "RS", "SRB", "688"),
            new Country("Seychelles", "SC", "SYC", "690"),
            new Country("Sierra Leone", "SL", "SLE", "694"),
            new Country("Singapore", "SG", "SGP", "702"),
            new Country("Slovakia", "SK", "SVK", "703"),
            new Country("Slovenia", "SI", "SVN", "705"),
            new Country("Solomon Islands", "SB", "SLB", "090"),
            new Country("Somalia", "SO", "SOM", "706"),
            new Country("South Africa", "ZA", "ZAF", "710"),
            new Country("South Georgia and the South Sandwich Islands", "GS", "SGS", "239"),
            new Country("South Sudan", "SS", "SSD", "728"),
            new Country("Spain", "ES", "ESP", "724"),
            new Country("Sri Lanka", "LK", "LKA", "144"),
            new Country("Sudan", "SD", "SDN", "736"),
            new Country("Suriname", "SR", "SUR", "740"),
            new Country("Svalbard and Jan Mayen Islands", "SJ", "SJM", "744"),
            new Country("Swaziland", "SZ", "SWZ", "748"),
            new Country("Sweden", "SE", "SWE", "752"),
            new Country("Switzerland", "CH", "CHE", "756"),
            new Country("Syrian Arab Republic (Syria)", "SY", "SYR", "760"),
            new Country("Taiwan, Republic of China", "TW", "TWN", "158"),
            new Country("Tajikistan", "TJ", "TJK", "762"),
            new Country("Tanzania, United Republic of", "TZ", "TZA", "834"),
            new Country("Thailand", "TH", "THA", "764"),
            new Country("Timor-Leste", "TL", "TLS", "626"),
            new Country("Togo", "TG", "TGO", "768"),
            new Country("Tokelau", "TK", "TKL", "772"),
            new Country("Tonga", "TO", "TON", "776"),
            new Country("Trinidad and Tobago", "TT", "TTO", "780"),
            new Country("Tunisia", "TN", "TUN", "788"),
            new Country("Turkey", "TR", "TUR", "792"),
            new Country("Turkmenistan", "TM", "TKM", "795"),
            new Country("Turks and Caicos Islands", "TC", "TCA", "796"),
            new Country("Tuvalu", "TV", "TUV", "798"),
            new Country("Uganda", "UG", "UGA", "800"),
            new Country("Ukraine", "UA", "UKR", "804"),
            new Country("United Arab Emirates", "AE", "ARE", "784"),
            new Country("United Kingdom", "GB", "GBR", "826"),
            new Country("United States of America", "US", "USA", "840"),
            new Country("US Minor Outlying Islands", "UM", "UMI", "581"),
            new Country("Uruguay", "UY", "URY", "858"),
            new Country("Uzbekistan", "UZ", "UZB", "860"),
            new Country("Vanuatu", "VU", "VUT", "548"),
            new Country("Venezuela (Bolivarian Republic)", "VE", "VEN", "862"),
            new Country("Vietnam", "VN", "VNM", "704"),
            new Country("Virgin Islands, US", "VI", "VIR", "850"),
            new Country("Wallis and Futuna Islands", "WF", "WLF", "876"),
            new Country("Western Sahara", "EH", "ESH", "732"),
            new Country("Yemen", "YE", "YEM", "887"),
            new Country("Zambia", "ZM", "ZMB", "894"),
            new Country("Zimbabwe", "ZW", "ZWE", "716")
        };
    }
}



//public static readonly Country[] List = new[]
//       {
//            new Country("Afghanistan", "AF", "AFG", "004"),
//            new Country("Åland Islands", "AX", "ALA", "248"),
//            new Country("Albania", "AL", "ALB", "008"),
//            new Country("Algeria", "DZ", "DZA", "012"),
//            new Country("American Samoa", "AS", "ASM", "016"),
//            new Country("Andorra", "AD", "AND", "020"),
//            new Country("Angola", "AO", "AGO", "024"),
//            new Country("Anguilla", "AI", "AIA", "660"),
//            new Country("Antarctica", "AQ", "ATA", "010"),
//            new Country("Antigua and Barbuda", "AG", "ATG", "028"),
//            new Country("Argentina", "AR", "ARG", "032"),
//            new Country("Armenia", "AM", "ARM", "051"),
//            new Country("Aruba", "AW", "ABW", "533"),
//            new Country("Australia", "AU", "AUS", "036"),
//            new Country("Austria", "AT", "AUT", "040"),
//            new Country("Azerbaijan", "AZ", "AZE", "031"),
//            new Country("Bahamas", "BS", "BHS", "044"),
//            new Country("Bahrain", "BH", "BHR", "048"),
//            new Country("Bangladesh", "BD", "BGD", "050"),
//            new Country("Barbados", "BB", "BRB", "052"),
//            new Country("Belarus", "BY", "BLR", "112"),
//            new Country("Belgium", "BE", "BEL", "056"),
//            new Country("Belize", "BZ", "BLZ", "084"),
//            new Country("Benin", "BJ", "BEN", "204"),
//            new Country("Bermuda", "BM", "BMU", "060"),
//            new Country("Bhutan", "BT", "BTN", "064"),
//            new Country("Bolivia, Plurinational State of", "BO", "BOL", "068"),
//            new Country("Bonaire, Sint Eustatius and Saba", "BQ", "BES", "535"),
//            new Country("Bosnia and Herzegovina", "BA", "BIH", "070"),
//            new Country("Botswana", "BW", "BWA", "072"),
//            new Country("Bouvet Island", "BV", "BVT", "074"),
//            new Country("Brazil", "BR", "BRA", "076"),
//            new Country("British Indian Ocean Territory", "IO", "IOT", "086"),
//            new Country("Brunei Darussalam", "BN", "BRN", "096"),
//            new Country("Bulgaria", "BG", "BGR", "100"),
//            new Country("Burkina Faso", "BF", "BFA", "854"),
//            new Country("Burundi", "BI", "BDI", "108"),
//            new Country("Cambodia", "KH", "KHM", "116"),
//            new Country("Cameroon", "CM", "CMR", "120"),
//            new Country("Canada", "CA", "CAN", "124"),
//            new Country("Cabo Verde", "CV", "CPV", "132"),
//            new Country("Cayman Islands", "KY", "CYM", "136"),
//            new Country("Central African Republic", "CF", "CAF", "140"),
//            new Country("Chad", "TD", "TCD", "148"),
//            new Country("Chile", "CL", "CHL", "152"),
//            new Country("China", "CN", "CHN", "156"),
//            new Country("Christmas Island", "CX", "CXR", "162"),
//            new Country("Cocos (Keeling) Islands", "CC", "CCK", "166"),
//            new Country("Colombia", "CO", "COL", "170"),
//            new Country("Comoros", "KM", "COM", "174"),
//            new Country("Congo", "CG", "COG", "178"),
//            new Country("Congo, the Democratic Republic of the", "CD", "COD", "180"),
//            new Country("Cook Islands", "CK", "COK", "184"),
//            new Country("Costa Rica", "CR", "CRI", "188"),
//            new Country("Côte d'Ivoire", "CI", "CIV", "384"),
//            new Country("Croatia", "HR", "HRV", "191"),
//            new Country("Cuba", "CU", "CUB", "192"),
//            new Country("Curaçao", "CW", "CUW", "531"),
//            new Country("Cyprus", "CY", "CYP", "196"),
//            new Country("Czechia", "CZ", "CZE", "203"),
//            new Country("Denmark", "DK", "DNK", "208"),
//            new Country("Djibouti", "DJ", "DJI", "262"),
//            new Country("Dominica", "DM", "DMA", "212"),
//            new Country("Dominican Republic", "DO", "DOM", "214"),
//            new Country("Ecuador", "EC", "ECU", "218"),
//            new Country("Egypt", "EG", "EGY", "818"),
//            new Country("El Salvador", "SV", "SLV", "222"),
//            new Country("Equatorial Guinea", "GQ", "GNQ", "226"),
//            new Country("Eritrea", "ER", "ERI", "232"),
//            new Country("Estonia", "EE", "EST", "233"),
//            new Country("Ethiopia", "ET", "ETH", "231"),
//            new Country("Falkland Islands (Malvinas)", "FK", "FLK", "238"),
//            new Country("Faroe Islands", "FO", "FRO", "234"),
//            new Country("Fiji", "FJ", "FJI", "242"),
//            new Country("Finland", "FI", "FIN", "246"),
//            new Country("France", "FR", "FRA", "250"),
//            new Country("French Guiana", "GF", "GUF", "254"),
//            new Country("French Polynesia", "PF", "PYF", "258"),
//            new Country("French Southern Territories", "TF", "ATF", "260"),
//            new Country("Gabon", "GA", "GAB", "266"),
//            new Country("Gambia", "GM", "GMB", "270"),
//            new Country("Georgia", "GE", "GEO", "268"),
//            new Country("Germany", "DE", "DEU", "276"),
//            new Country("Ghana", "GH", "GHA", "288"),
//            new Country("Gibraltar", "GI", "GIB", "292"),
//            new Country("Greece", "GR", "GRC", "300"),
//            new Country("Greenland", "GL", "GRL", "304"),
//            new Country("Grenada", "GD", "GRD", "308"),
//            new Country("Guadeloupe", "GP", "GLP", "312"),
//            new Country("Guam", "GU", "GUM", "316"),
//            new Country("Guatemala", "GT", "GTM", "320"),
//            new Country("Guernsey", "GG", "GGY", "831"),
//            new Country("Guinea", "GN", "GIN", "324"),
//            new Country("Guinea-Bissau", "GW", "GNB", "624"),
//            new Country("Guyana", "GY", "GUY", "328"),
//            new Country("Haiti", "HT", "HTI", "332"),
//            new Country("Heard Island and McDonald Islands", "HM", "HMD", "334"),
//            new Country("Holy See (Vatican City State)", "VA", "VAT", "336"),
//            new Country("Honduras", "HN", "HND", "340"),
//            new Country("Hong Kong", "HK", "HKG", "344"),
//            new Country("Hungary", "HU", "HUN", "348"),
//            new Country("Iceland", "IS", "ISL", "352"),
//            new Country("India", "IN", "IND", "356"),
//            new Country("Indonesia", "ID", "IDN", "360"),
//            new Country("Iran, Islamic Republic of", "IR", "IRN", "364"),
//            new Country("Iraq", "IQ", "IRQ", "368"),
//            new Country("Ireland", "IE", "IRL", "372"),
//            new Country("Isle of Man", "IM", "IMN", "833"),
//            new Country("Israel", "IL", "ISR", "376"),
//            new Country("Italy", "IT", "ITA", "380"),
//            new Country("Jamaica", "JM", "JAM", "388"),
//            new Country("Japan", "JP", "JPN", "392"),
//            new Country("Jersey", "JE", "JEY", "832"),
//            new Country("Jordan", "JO", "JOR", "400"),
//            new Country("Kazakhstan", "KZ", "KAZ", "398"),
//            new Country("Kenya", "KE", "KEN", "404"),
//            new Country("Kiribati", "KI", "KIR", "296"),
//            new Country("Korea, Democratic People's Republic of", "KP", "PRK", "408"),
//            new Country("Korea, Republic of", "KR", "KOR", "410"),
//            new Country("Kuwait", "KW", "KWT", "414"),
//            new Country("Kyrgyzstan", "KG", "KGZ", "417"),
//            new Country("Lao People's Democratic Republic", "LA", "LAO", "418"),
//            new Country("Latvia", "LV", "LVA", "428"),
//            new Country("Lebanon", "LB", "LBN", "422"),
//            new Country("Lesotho", "LS", "LSO", "426"),
//            new Country("Liberia", "LR", "LBR", "430"),
//            new Country("Libya", "LY", "LBY", "434"),
//            new Country("Liechtenstein", "LI", "LIE", "438"),
//            new Country("Lithuania", "LT", "LTU", "440"),
//            new Country("Luxembourg", "LU", "LUX", "442"),
//            new Country("Macao", "MO", "MAC", "446"),
//            new Country("Macedonia, the former Yugoslav Republic of", "MK", "MKD", "807"),
//            new Country("Madagascar", "MG", "MDG", "450"),
//            new Country("Malawi", "MW", "MWI", "454"),
//            new Country("Malaysia", "MY", "MYS", "458"),
//            new Country("Maldives", "MV", "MDV", "462"),
//            new Country("Mali", "ML", "MLI", "466"),
//            new Country("Malta", "MT", "MLT", "470"),
//            new Country("Marshall Islands", "MH", "MHL", "584"),
//            new Country("Martinique", "MQ", "MTQ", "474"),
//            new Country("Mauritania", "MR", "MRT", "478"),
//            new Country("Mauritius", "MU", "MUS", "480"),
//            new Country("Mayotte", "YT", "MYT", "175"),
//            new Country("Mexico", "MX", "MEX", "484"),
//            new Country("Micronesia, Federated States of", "FM", "FSM", "583"),
//            new Country("Moldova, Republic of", "MD", "MDA", "498"),
//            new Country("Monaco", "MC", "MCO", "492"),
//            new Country("Mongolia", "MN", "MNG", "496"),
//            new Country("Montenegro", "ME", "MNE", "499"),
//            new Country("Montserrat", "MS", "MSR", "500"),
//            new Country("Morocco", "MA", "MAR", "504"),
//            new Country("Mozambique", "MZ", "MOZ", "508"),
//            new Country("Myanmar", "MM", "MMR", "104"),
//            new Country("Namibia", "NA", "NAM", "516"),
//            new Country("Nauru", "NR", "NRU", "520"),
//            new Country("Nepal", "NP", "NPL", "524"),
//            new Country("Netherlands", "NL", "NLD", "528"),
//            new Country("New Caledonia", "NC", "NCL", "540"),
//            new Country("New Zealand", "NZ", "NZL", "554"),
//            new Country("Nicaragua", "NI", "NIC", "558"),
//            new Country("Niger", "NE", "NER", "562"),
//            new Country("Nigeria", "NG", "NGA", "566"),
//            new Country("Niue", "NU", "NIU", "570"),
//            new Country("Norfolk Island", "NF", "NFK", "574"),
//            new Country("Northern Mariana Islands", "MP", "MNP", "580"),
//            new Country("Norway", "NO", "NOR", "578"),
//            new Country("Oman", "OM", "OMN", "512"),
//            new Country("Pakistan", "PK", "PAK", "586"),
//            new Country("Palau", "PW", "PLW", "585"),
//            new Country("Palestine, State of", "PS", "PSE", "275"),
//            new Country("Panama", "PA", "PAN", "591"),
//            new Country("Papua New Guinea", "PG", "PNG", "598"),
//            new Country("Paraguay", "PY", "PRY", "600"),
//            new Country("Peru", "PE", "PER", "604"),
//            new Country("Philippines", "PH", "PHL", "608"),
//            new Country("Pitcairn", "PN", "PCN", "612"),
//            new Country("Poland", "PL", "POL", "616"),
//            new Country("Portugal", "PT", "PRT", "620"),
//            new Country("Puerto Rico", "PR", "PRI", "630"),
//            new Country("Qatar", "QA", "QAT", "634"),
//            new Country("Réunion", "RE", "REU", "638"),
//            new Country("Romania", "RO", "ROU", "642"),
//            new Country("Russian Federation", "RU", "RUS", "643"),
//            new Country("Rwanda", "RW", "RWA", "646"),
//            new Country("Saint Barthélemy", "BL", "BLM", "652"),
//            new Country("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", "654"),
//            new Country("Saint Kitts and Nevis", "KN", "KNA", "659"),
//            new Country("Saint Lucia", "LC", "LCA", "662"),
//            new Country("Saint Martin (French part)", "MF", "MAF", "663"),
//            new Country("Saint Pierre and Miquelon", "PM", "SPM", "666"),
//            new Country("Saint Vincent and the Grenadines", "VC", "VCT", "670"),
//            new Country("Samoa", "WS", "WSM", "882"),
//            new Country("San Marino", "SM", "SMR", "674"),
//            new Country("Sao Tome and Principe", "ST", "STP", "678"),
//            new Country("Saudi Arabia", "SA", "SAU", "682"),
//            new Country("Senegal", "SN", "SEN", "686"),
//            new Country("Serbia", "RS", "SRB", "688"),
//            new Country("Seychelles", "SC", "SYC", "690"),
//            new Country("Sierra Leone", "SL", "SLE", "694"),
//            new Country("Singapore", "SG", "SGP", "702"),
//            new Country("Sint Maarten (Dutch part)", "SX", "SXM", "534"),
//            new Country("Slovakia", "SK", "SVK", "703"),
//            new Country("Slovenia", "SI", "SVN", "705"),
//            new Country("Solomon Islands", "SB", "SLB", "090"),
//            new Country("Somalia", "SO", "SOM", "706"),
//            new Country("South Africa", "ZA", "ZAF", "710"),
//            new Country("South Georgia and the South Sandwich Islands", "GS", "SGS", "239"),
//            new Country("South Sudan", "SS", "SSD", "728"),
//            new Country("Spain", "ES", "ESP", "724"),
//            new Country("Sri Lanka", "LK", "LKA", "144"),
//            new Country("Sudan", "SD", "SDN", "729"),
//            new Country("Suriname", "SR", "SUR", "740"),
//            new Country("Svalbard and Jan Mayen", "SJ", "SJM", "744"),
//            new Country("Swaziland", "SZ", "SWZ", "748"),
//            new Country("Sweden", "SE", "SWE", "752"),
//            new Country("Switzerland", "CH", "CHE", "756"),
//            new Country("Syrian Arab Republic", "SY", "SYR", "760"),
//            new Country("Taiwan, Province of China", "TW", "TWN", "158"),
//            new Country("Tajikistan", "TJ", "TJK", "762"),
//            new Country("Tanzania, United Republic of", "TZ", "TZA", "834"),
//            new Country("Thailand", "TH", "THA", "764"),
//            new Country("Timor-Leste", "TL", "TLS", "626"),
//            new Country("Togo", "TG", "TGO", "768"),
//            new Country("Tokelau", "TK", "TKL", "772"),
//            new Country("Tonga", "TO", "TON", "776"),
//            new Country("Trinidad and Tobago", "TT", "TTO", "780"),
//            new Country("Tunisia", "TN", "TUN", "788"),
//            new Country("Turkey", "TR", "TUR", "792"),
//            new Country("Turkmenistan", "TM", "TKM", "795"),
//            new Country("Turks and Caicos Islands", "TC", "TCA", "796"),
//            new Country("Tuvalu", "TV", "TUV", "798"),
//            new Country("Uganda", "UG", "UGA", "800"),
//            new Country("Ukraine", "UA", "UKR", "804"),
//            new Country("United Arab Emirates", "AE", "ARE", "784"),
//            new Country("United Kingdom", "GB", "GBR", "826"),
//            new Country("United States", "US", "USA", "840"),
//            new Country("United States Minor Outlying Islands", "UM", "UMI", "581"),
//            new Country("Uruguay", "UY", "URY", "858"),
//            new Country("Uzbekistan", "UZ", "UZB", "860"),
//            new Country("Vanuatu", "VU", "VUT", "548"),
//            new Country("Venezuela, Bolivarian Republic of", "VE", "VEN", "862"),
//            new Country("Viet Nam", "VN", "VNM", "704"),
//            new Country("Virgin Islands, British", "VG", "VGB", "092"),
//            new Country("Virgin Islands, U.S.", "VI", "VIR", "850"),
//            new Country("Wallis and Futuna", "WF", "WLF", "876"),
//            new Country("Western Sahara", "EH", "ESH", "732"),
//            new Country("Yemen", "YE", "YEM", "887"),
//            new Country("Zambia", "ZM", "ZMB", "894"),
//            new Country("Zimbabwe", "ZW", "ZWE", "716"),
//        };


//public static class ISO3166
//{
//    /// <summary>
//    /// Obtain ISO3166-1 Country based on its alpha3 code.
//    /// </summary>
//    /// <param name="alpha3"></param>
//    /// <returns></returns>
//    public static ISO3166Country FromAlpha3(string alpha3)
//    {
//        Collection<ISO3166Country> collection = BuildCollection();
//        return collection.FirstOrDefault(p => p.Alpha3 == alpha3);
//    }

//    #region Build Collection
//    private static Collection<ISO3166Country> BuildCollection()
//    {
//        Collection<ISO3166Country> collection = new Collection<ISO3166Country>();

//        // This collection built from Wikipedia entry on ISO3166-1 on 9th Feb 2016

//        collection.Add(new ISO3166Country("Afghanistan", "AF", "AFG", 4));
//        collection.Add(new ISO3166Country("Åland Islands", "AX", "ALA", 248));
//        collection.Add(new ISO3166Country("Albania", "AL", "ALB", 8));
//        collection.Add(new ISO3166Country("Algeria", "DZ", "DZA", 12));
//        collection.Add(new ISO3166Country("American Samoa", "AS", "ASM", 16));
//        collection.Add(new ISO3166Country("Andorra", "AD", "AND", 20));
//        collection.Add(new ISO3166Country("Angola", "AO", "AGO", 24));
//        collection.Add(new ISO3166Country("Anguilla", "AI", "AIA", 660));
//        collection.Add(new ISO3166Country("Antarctica", "AQ", "ATA", 10));
//        collection.Add(new ISO3166Country("Antigua and Barbuda", "AG", "ATG", 28));
//        collection.Add(new ISO3166Country("Argentina", "AR", "ARG", 32));
//        collection.Add(new ISO3166Country("Armenia", "AM", "ARM", 51));
//        collection.Add(new ISO3166Country("Aruba", "AW", "ABW", 533));
//        collection.Add(new ISO3166Country("Australia", "AU", "AUS", 36));
//        collection.Add(new ISO3166Country("Austria", "AT", "AUT", 40));
//        collection.Add(new ISO3166Country("Azerbaijan", "AZ", "AZE", 31));
//        collection.Add(new ISO3166Country("Bahamas", "BS", "BHS", 44));
//        collection.Add(new ISO3166Country("Bahrain", "BH", "BHR", 48));
//        collection.Add(new ISO3166Country("Bangladesh", "BD", "BGD", 50));
//        collection.Add(new ISO3166Country("Barbados", "BB", "BRB", 52));
//        collection.Add(new ISO3166Country("Belarus", "BY", "BLR", 112));
//        collection.Add(new ISO3166Country("Belgium", "BE", "BEL", 56));
//        collection.Add(new ISO3166Country("Belize", "BZ", "BLZ", 84));
//        collection.Add(new ISO3166Country("Benin", "BJ", "BEN", 204));
//        collection.Add(new ISO3166Country("Bermuda", "BM", "BMU", 60));
//        collection.Add(new ISO3166Country("Bhutan", "BT", "BTN", 64));
//        collection.Add(new ISO3166Country("Bolivia (Plurinational State of)", "BO", "BOL", 68));
//        collection.Add(new ISO3166Country("Bonaire, Sint Eustatius and Saba", "BQ", "BES", 535));
//        collection.Add(new ISO3166Country("Bosnia and Herzegovina", "BA", "BIH", 70));
//        collection.Add(new ISO3166Country("Botswana", "BW", "BWA", 72));
//        collection.Add(new ISO3166Country("Bouvet Island", "BV", "BVT", 74));
//        collection.Add(new ISO3166Country("Brazil", "BR", "BRA", 76));
//        collection.Add(new ISO3166Country("British Indian Ocean Territory", "IO", "IOT", 86));
//        collection.Add(new ISO3166Country("Brunei Darussalam", "BN", "BRN", 96));
//        collection.Add(new ISO3166Country("Bulgaria", "BG", "BGR", 100));
//        collection.Add(new ISO3166Country("Burkina Faso", "BF", "BFA", 854));
//        collection.Add(new ISO3166Country("Burundi", "BI", "BDI", 108));
//        collection.Add(new ISO3166Country("Cabo Verde", "CV", "CPV", 132));
//        collection.Add(new ISO3166Country("Cambodia", "KH", "KHM", 116));
//        collection.Add(new ISO3166Country("Cameroon", "CM", "CMR", 120));
//        collection.Add(new ISO3166Country("Canada", "CA", "CAN", 124));
//        collection.Add(new ISO3166Country("Cayman Islands", "KY", "CYM", 136));
//        collection.Add(new ISO3166Country("Central African Republic", "CF", "CAF", 140));
//        collection.Add(new ISO3166Country("Chad", "TD", "TCD", 148));
//        collection.Add(new ISO3166Country("Chile", "CL", "CHL", 152));
//        collection.Add(new ISO3166Country("China", "CN", "CHN", 156));
//        collection.Add(new ISO3166Country("Christmas Island", "CX", "CXR", 162));
//        collection.Add(new ISO3166Country("Cocos (Keeling) Islands", "CC", "CCK", 166));
//        collection.Add(new ISO3166Country("Colombia", "CO", "COL", 170));
//        collection.Add(new ISO3166Country("Comoros", "KM", "COM", 174));
//        collection.Add(new ISO3166Country("Congo", "CG", "COG", 178));
//        collection.Add(new ISO3166Country("Congo (Democratic Republic of the)", "CD", "COD", 180));
//        collection.Add(new ISO3166Country("Cook Islands", "CK", "COK", 184));
//        collection.Add(new ISO3166Country("Costa Rica", "CR", "CRI", 188));
//        collection.Add(new ISO3166Country("Côte d'Ivoire", "CI", "CIV", 384));
//        collection.Add(new ISO3166Country("Croatia", "HR", "HRV", 191));
//        collection.Add(new ISO3166Country("Cuba", "CU", "CUB", 192));
//        collection.Add(new ISO3166Country("Curaçao", "CW", "CUW", 531));
//        collection.Add(new ISO3166Country("Cyprus", "CY", "CYP", 196));
//        collection.Add(new ISO3166Country("Czech Republic", "CZ", "CZE", 203));
//        collection.Add(new ISO3166Country("Denmark", "DK", "DNK", 208));
//        collection.Add(new ISO3166Country("Djibouti", "DJ", "DJI", 262));
//        collection.Add(new ISO3166Country("Dominica", "DM", "DMA", 212));
//        collection.Add(new ISO3166Country("Dominican Republic", "DO", "DOM", 214));
//        collection.Add(new ISO3166Country("Ecuador", "EC", "ECU", 218));
//        collection.Add(new ISO3166Country("Egypt", "EG", "EGY", 818));
//        collection.Add(new ISO3166Country("El Salvador", "SV", "SLV", 222));
//        collection.Add(new ISO3166Country("Equatorial Guinea", "GQ", "GNQ", 226));
//        collection.Add(new ISO3166Country("Eritrea", "ER", "ERI", 232));
//        collection.Add(new ISO3166Country("Estonia", "EE", "EST", 233));
//        collection.Add(new ISO3166Country("Ethiopia", "ET", "ETH", 231));
//        collection.Add(new ISO3166Country("Falkland Islands (Malvinas)", "FK", "FLK", 238));
//        collection.Add(new ISO3166Country("Faroe Islands", "FO", "FRO", 234));
//        collection.Add(new ISO3166Country("Fiji", "FJ", "FJI", 242));
//        collection.Add(new ISO3166Country("Finland", "FI", "FIN", 246));
//        collection.Add(new ISO3166Country("France", "FR", "FRA", 250));
//        collection.Add(new ISO3166Country("French Guiana", "GF", "GUF", 254));
//        collection.Add(new ISO3166Country("French Polynesia", "PF", "PYF", 258));
//        collection.Add(new ISO3166Country("French Southern Territories", "TF", "ATF", 260));
//        collection.Add(new ISO3166Country("Gabon", "GA", "GAB", 266));
//        collection.Add(new ISO3166Country("Gambia", "GM", "GMB", 270));
//        collection.Add(new ISO3166Country("Georgia", "GE", "GEO", 268));
//        collection.Add(new ISO3166Country("Germany", "DE", "DEU", 276));
//        collection.Add(new ISO3166Country("Ghana", "GH", "GHA", 288));
//        collection.Add(new ISO3166Country("Gibraltar", "GI", "GIB", 292));
//        collection.Add(new ISO3166Country("Greece", "GR", "GRC", 300));
//        collection.Add(new ISO3166Country("Greenland", "GL", "GRL", 304));
//        collection.Add(new ISO3166Country("Grenada", "GD", "GRD", 308));
//        collection.Add(new ISO3166Country("Guadeloupe", "GP", "GLP", 312));
//        collection.Add(new ISO3166Country("Guam", "GU", "GUM", 316));
//        collection.Add(new ISO3166Country("Guatemala", "GT", "GTM", 320));
//        collection.Add(new ISO3166Country("Guernsey", "GG", "GGY", 831));
//        collection.Add(new ISO3166Country("Guinea", "GN", "GIN", 324));
//        collection.Add(new ISO3166Country("Guinea-Bissau", "GW", "GNB", 624));
//        collection.Add(new ISO3166Country("Guyana", "GY", "GUY", 328));
//        collection.Add(new ISO3166Country("Haiti", "HT", "HTI", 332));
//        collection.Add(new ISO3166Country("Heard Island and McDonald Islands", "HM", "HMD", 334));
//        collection.Add(new ISO3166Country("Holy See", "VA", "VAT", 336));
//        collection.Add(new ISO3166Country("Honduras", "HN", "HND", 340));
//        collection.Add(new ISO3166Country("Hong Kong", "HK", "HKG", 344));
//        collection.Add(new ISO3166Country("Hungary", "HU", "HUN", 348));
//        collection.Add(new ISO3166Country("Iceland", "IS", "ISL", 352));
//        collection.Add(new ISO3166Country("India", "IN", "IND", 356));
//        collection.Add(new ISO3166Country("Indonesia", "ID", "IDN", 360));
//        collection.Add(new ISO3166Country("Iran (Islamic Republic of)", "IR", "IRN", 364));
//        collection.Add(new ISO3166Country("Iraq", "IQ", "IRQ", 368));
//        collection.Add(new ISO3166Country("Ireland", "IE", "IRL", 372));
//        collection.Add(new ISO3166Country("Isle of Man", "IM", "IMN", 833));
//        collection.Add(new ISO3166Country("Israel", "IL", "ISR", 376));
//        collection.Add(new ISO3166Country("Italy", "IT", "ITA", 380));
//        collection.Add(new ISO3166Country("Jamaica", "JM", "JAM", 388));
//        collection.Add(new ISO3166Country("Japan", "JP", "JPN", 392));
//        collection.Add(new ISO3166Country("Jersey", "JE", "JEY", 832));
//        collection.Add(new ISO3166Country("Jordan", "JO", "JOR", 400));
//        collection.Add(new ISO3166Country("Kazakhstan", "KZ", "KAZ", 398));
//        collection.Add(new ISO3166Country("Kenya", "KE", "KEN", 404));
//        collection.Add(new ISO3166Country("Kiribati", "KI", "KIR", 296));
//        collection.Add(new ISO3166Country("Korea (Democratic People's Republic of)", "KP", "PRK", 408));
//        collection.Add(new ISO3166Country("Korea (Republic of)", "KR", "KOR", 410));
//        collection.Add(new ISO3166Country("Kuwait", "KW", "KWT", 414));
//        collection.Add(new ISO3166Country("Kyrgyzstan", "KG", "KGZ", 417));
//        collection.Add(new ISO3166Country("Lao People's Democratic Republic", "LA", "LAO", 418));
//        collection.Add(new ISO3166Country("Latvia", "LV", "LVA", 428));
//        collection.Add(new ISO3166Country("Lebanon", "LB", "LBN", 422));
//        collection.Add(new ISO3166Country("Lesotho", "LS", "LSO", 426));
//        collection.Add(new ISO3166Country("Liberia", "LR", "LBR", 430));
//        collection.Add(new ISO3166Country("Libya", "LY", "LBY", 434));
//        collection.Add(new ISO3166Country("Liechtenstein", "LI", "LIE", 438));
//        collection.Add(new ISO3166Country("Lithuania", "LT", "LTU", 440));
//        collection.Add(new ISO3166Country("Luxembourg", "LU", "LUX", 442));
//        collection.Add(new ISO3166Country("Macao", "MO", "MAC", 446));
//        collection.Add(new ISO3166Country("Macedonia (the former Yugoslav Republic of)", "MK", "MKD", 807));
//        collection.Add(new ISO3166Country("Madagascar", "MG", "MDG", 450));
//        collection.Add(new ISO3166Country("Malawi", "MW", "MWI", 454));
//        collection.Add(new ISO3166Country("Malaysia", "MY", "MYS", 458));
//        collection.Add(new ISO3166Country("Maldives", "MV", "MDV", 462));
//        collection.Add(new ISO3166Country("Mali", "ML", "MLI", 466));
//        collection.Add(new ISO3166Country("Malta", "MT", "MLT", 470));
//        collection.Add(new ISO3166Country("Marshall Islands", "MH", "MHL", 584));
//        collection.Add(new ISO3166Country("Martinique", "MQ", "MTQ", 474));
//        collection.Add(new ISO3166Country("Mauritania", "MR", "MRT", 478));
//        collection.Add(new ISO3166Country("Mauritius", "MU", "MUS", 480));
//        collection.Add(new ISO3166Country("Mayotte", "YT", "MYT", 175));
//        collection.Add(new ISO3166Country("Mexico", "MX", "MEX", 484));
//        collection.Add(new ISO3166Country("Micronesia (Federated States of)", "FM", "FSM", 583));
//        collection.Add(new ISO3166Country("Moldova (Republic of)", "MD", "MDA", 498));
//        collection.Add(new ISO3166Country("Monaco", "MC", "MCO", 492));
//        collection.Add(new ISO3166Country("Mongolia", "MN", "MNG", 496));
//        collection.Add(new ISO3166Country("Montenegro", "ME", "MNE", 499));
//        collection.Add(new ISO3166Country("Montserrat", "MS", "MSR", 500));
//        collection.Add(new ISO3166Country("Morocco", "MA", "MAR", 504));
//        collection.Add(new ISO3166Country("Mozambique", "MZ", "MOZ", 508));
//        collection.Add(new ISO3166Country("Myanmar", "MM", "MMR", 104));
//        collection.Add(new ISO3166Country("Namibia", "NA", "NAM", 516));
//        collection.Add(new ISO3166Country("Nauru", "NR", "NRU", 520));
//        collection.Add(new ISO3166Country("Nepal", "NP", "NPL", 524));
//        collection.Add(new ISO3166Country("Netherlands", "NL", "NLD", 528));
//        collection.Add(new ISO3166Country("New Caledonia", "NC", "NCL", 540));
//        collection.Add(new ISO3166Country("New Zealand", "NZ", "NZL", 554));
//        collection.Add(new ISO3166Country("Nicaragua", "NI", "NIC", 558));
//        collection.Add(new ISO3166Country("Niger", "NE", "NER", 562));
//        collection.Add(new ISO3166Country("Nigeria", "NG", "NGA", 566));
//        collection.Add(new ISO3166Country("Niue", "NU", "NIU", 570));
//        collection.Add(new ISO3166Country("Norfolk Island", "NF", "NFK", 574));
//        collection.Add(new ISO3166Country("Northern Mariana Islands", "MP", "MNP", 580));
//        collection.Add(new ISO3166Country("Norway", "NO", "NOR", 578));
//        collection.Add(new ISO3166Country("Oman", "OM", "OMN", 512));
//        collection.Add(new ISO3166Country("Pakistan", "PK", "PAK", 586));
//        collection.Add(new ISO3166Country("Palau", "PW", "PLW", 585));
//        collection.Add(new ISO3166Country("Palestine, State of", "PS", "PSE", 275));
//        collection.Add(new ISO3166Country("Panama", "PA", "PAN", 591));
//        collection.Add(new ISO3166Country("Papua New Guinea", "PG", "PNG", 598));
//        collection.Add(new ISO3166Country("Paraguay", "PY", "PRY", 600));
//        collection.Add(new ISO3166Country("Peru", "PE", "PER", 604));
//        collection.Add(new ISO3166Country("Philippines", "PH", "PHL", 608));
//        collection.Add(new ISO3166Country("Pitcairn", "PN", "PCN", 612));
//        collection.Add(new ISO3166Country("Poland", "PL", "POL", 616));
//        collection.Add(new ISO3166Country("Portugal", "PT", "PRT", 620));
//        collection.Add(new ISO3166Country("Puerto Rico", "PR", "PRI", 630));
//        collection.Add(new ISO3166Country("Qatar", "QA", "QAT", 634));
//        collection.Add(new ISO3166Country("Réunion", "RE", "REU", 638));
//        collection.Add(new ISO3166Country("Romania", "RO", "ROU", 642));
//        collection.Add(new ISO3166Country("Russian Federation", "RU", "RUS", 643));
//        collection.Add(new ISO3166Country("Rwanda", "RW", "RWA", 646));
//        collection.Add(new ISO3166Country("Saint Barthélemy", "BL", "BLM", 652));
//        collection.Add(new ISO3166Country("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", 654));
//        collection.Add(new ISO3166Country("Saint Kitts and Nevis", "KN", "KNA", 659));
//        collection.Add(new ISO3166Country("Saint Lucia", "LC", "LCA", 662));
//        collection.Add(new ISO3166Country("Saint Martin (French part)", "MF", "MAF", 663));
//        collection.Add(new ISO3166Country("Saint Pierre and Miquelon", "PM", "SPM", 666));
//        collection.Add(new ISO3166Country("Saint Vincent and the Grenadines", "VC", "VCT", 670));
//        collection.Add(new ISO3166Country("Samoa", "WS", "WSM", 882));
//        collection.Add(new ISO3166Country("San Marino", "SM", "SMR", 674));
//        collection.Add(new ISO3166Country("Sao Tome and Principe", "ST", "STP", 678));
//        collection.Add(new ISO3166Country("Saudi Arabia", "SA", "SAU", 682));
//        collection.Add(new ISO3166Country("Senegal", "SN", "SEN", 686));
//        collection.Add(new ISO3166Country("Serbia", "RS", "SRB", 688));
//        collection.Add(new ISO3166Country("Seychelles", "SC", "SYC", 690));
//        collection.Add(new ISO3166Country("Sierra Leone", "SL", "SLE", 694));
//        collection.Add(new ISO3166Country("Singapore", "SG", "SGP", 702));
//        collection.Add(new ISO3166Country("Sint Maarten (Dutch part)", "SX", "SXM", 534));
//        collection.Add(new ISO3166Country("Slovakia", "SK", "SVK", 703));
//        collection.Add(new ISO3166Country("Slovenia", "SI", "SVN", 705));
//        collection.Add(new ISO3166Country("Solomon Islands", "SB", "SLB", 90));
//        collection.Add(new ISO3166Country("Somalia", "SO", "SOM", 706));
//        collection.Add(new ISO3166Country("South Africa", "ZA", "ZAF", 710));
//        collection.Add(new ISO3166Country("South Georgia and the South Sandwich Islands", "GS", "SGS", 239));
//        collection.Add(new ISO3166Country("South Sudan", "SS", "SSD", 728));
//        collection.Add(new ISO3166Country("Spain", "ES", "ESP", 724));
//        collection.Add(new ISO3166Country("Sri Lanka", "LK", "LKA", 144));
//        collection.Add(new ISO3166Country("Sudan", "SD", "SDN", 729));
//        collection.Add(new ISO3166Country("Suriname", "SR", "SUR", 740));
//        collection.Add(new ISO3166Country("Svalbard and Jan Mayen", "SJ", "SJM", 744));
//        collection.Add(new ISO3166Country("Swaziland", "SZ", "SWZ", 748));
//        collection.Add(new ISO3166Country("Sweden", "SE", "SWE", 752));
//        collection.Add(new ISO3166Country("Switzerland", "CH", "CHE", 756));
//        collection.Add(new ISO3166Country("Syrian Arab Republic", "SY", "SYR", 760));
//        collection.Add(new ISO3166Country("Taiwan, Province of China[a]", "TW", "TWN", 158));
//        collection.Add(new ISO3166Country("Tajikistan", "TJ", "TJK", 762));
//        collection.Add(new ISO3166Country("Tanzania, United Republic of", "TZ", "TZA", 834));
//        collection.Add(new ISO3166Country("Thailand", "TH", "THA", 764));
//        collection.Add(new ISO3166Country("Timor-Leste", "TL", "TLS", 626));
//        collection.Add(new ISO3166Country("Togo", "TG", "TGO", 768));
//        collection.Add(new ISO3166Country("Tokelau", "TK", "TKL", 772));
//        collection.Add(new ISO3166Country("Tonga", "TO", "TON", 776));
//        collection.Add(new ISO3166Country("Trinidad and Tobago", "TT", "TTO", 780));
//        collection.Add(new ISO3166Country("Tunisia", "TN", "TUN", 788));
//        collection.Add(new ISO3166Country("Turkey", "TR", "TUR", 792));
//        collection.Add(new ISO3166Country("Turkmenistan", "TM", "TKM", 795));
//        collection.Add(new ISO3166Country("Turks and Caicos Islands", "TC", "TCA", 796));
//        collection.Add(new ISO3166Country("Tuvalu", "TV", "TUV", 798));
//        collection.Add(new ISO3166Country("Uganda", "UG", "UGA", 800));
//        collection.Add(new ISO3166Country("Ukraine", "UA", "UKR", 804));
//        collection.Add(new ISO3166Country("United Arab Emirates", "AE", "ARE", 784));
//        collection.Add(new ISO3166Country("United Kingdom of Great Britain and Northern Ireland", "GB", "GBR", 826));
//        collection.Add(new ISO3166Country("United States of America", "US", "USA", 840));
//        collection.Add(new ISO3166Country("United States Minor Outlying Islands", "UM", "UMI", 581));
//        collection.Add(new ISO3166Country("Uruguay", "UY", "URY", 858));
//        collection.Add(new ISO3166Country("Uzbekistan", "UZ", "UZB", 860));
//        collection.Add(new ISO3166Country("Vanuatu", "VU", "VUT", 548));
//        collection.Add(new ISO3166Country("Venezuela (Bolivarian Republic of)", "VE", "VEN", 862));
//        collection.Add(new ISO3166Country("Viet Nam", "VN", "VNM", 704));
//        collection.Add(new ISO3166Country("Virgin Islands (British)", "VG", "VGB", 92));
//        collection.Add(new ISO3166Country("Virgin Islands (U.S.)", "VI", "VIR", 850));
//        collection.Add(new ISO3166Country("Wallis and Futuna", "WF", "WLF", 876));
//        collection.Add(new ISO3166Country("Western Sahara", "EH", "ESH", 732));
//        collection.Add(new ISO3166Country("Yemen", "YE", "YEM", 887));
//        collection.Add(new ISO3166Country("Zambia", "ZM", "ZMB", 894));
//        collection.Add(new ISO3166Country("Zimbabwe", "ZW", "ZWE", 716));

//        return collection;
//    }
//    #endregion
//}

///// <summary>
///// Representation of an ISO3166-1 Country
///// </summary>
//public class ISO3166Country
//{
//    public ISO3166Country(string name, string alpha2, string alpha3, int numericCode)
//    {
//        this.Name = name;
//        this.Alpha2 = alpha2;
//        this.Alpha3 = alpha3;
//        this.NumericCode = numericCode;
//    }

//    public string Name { get; private set; }

//    public string Alpha2 { get; private set; }

//    public string Alpha3 { get; private set; }

//    public int NumericCode { get; private set; }
//}