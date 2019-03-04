using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetManager_V1.Models.VM
{
    public class CurrencyCourcesViewModels
    {
        public IEnumerable<CurrencyCourse> CurrencyCourses { get; set; }
        public List<Currency> FilterCurrencies { get; set; }
        public DateTime FilterDate { get; set; }
    }
}