using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManager_V1.Models.VM
{
    public enum CurrencySortWay
    {
        NumericCountryCode, NumericCountryCodeDesc,
        EnglishName, EnglishNameDesc,
        ISOCurrencySymbol, ISOCurrencySymbolDesc,
        CurrencyNativeName, CurrencyNativeNameDesc,
        CurrencyEnglishName, CurrencyEnglishNameDesc,
        ThreeLetterWindowsRegionName, ThreeLetterWindowsRegionNameDesc,
        ThreeLetterISORegionName, ThreeLetterISORegionNameDesc,
        TwoLetterISORegionName, TwoLetterISORegionNameDesc,
        DisplayName, DisplayNameDesc,
        Name, NameDesc,
        CurrencySymbol, CurrencySymbolDesc
    }
}