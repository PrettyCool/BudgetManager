using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BudgetManager_V1.Models.VM;
using BudgetManager_V1.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using BudgetManager_V1.Extentions;
using Microsoft.AspNet.Identity;

namespace BudgetManager_V1.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private DateTime currentReportPeriod { get; set; }
        public ReportController() => currentReportPeriod = DateTime.Now;

        public ActionResult Index() => View();

        [HttpGet]
        public ActionResult IncomeExpenseForMonth()
        {
            ViewBag.Sum = 0.0;
            return View(GetDefaultParamIncExpByMonth());
        }

        [HttpPost]
        public async Task<ActionResult> IncomeExpenseForMonth([Bind(Include = "CurrentSelection,ReportType,SelectedForPeriod")]IncomeExpenseByMonthVm reportParams)
        {
            if (reportParams == null)
                reportParams = GetDefaultParamIncExpByMonth();

            if (ModelState.IsValid)
            {
                //change the report period according to request (next or previous
                switch (reportParams.SelectedForPeriod)
                {
                    case SelectedForPeriod.Previous: currentReportPeriod = reportParams.CurrentSelection.AddMonths(-1); break;
                    case SelectedForPeriod.Next: currentReportPeriod = reportParams.CurrentSelection.AddMonths(1); break;
                    default: currentReportPeriod = reportParams.CurrentSelection; break;
                }

                reportParams.CurrentSelection = this.currentReportPeriod;

                ApplicationUser curUser = db.Users.Find(User.Identity.GetUserId());

                List<Ledger> rez = null;
                //getting all the ledger data for report period:
                var temp = db.Ledgers
                    .Where(t => 
                        t.Date.Year == reportParams.CurrentSelection.Year && 
                        t.Date.Month == reportParams.CurrentSelection.Month &&
                        t.Account.ApplicationUserId.Equals(curUser.Id))
                    .Include(t => t.Account)
                    .Include(t => t.Account.Currency)
                    .Include(t => t.IncomeExpenseItem)
                    .Include(t => t.IncomeExpenseItem.IncomeExpenseGroup)
                    .Include(t => t.IncomeExpenseItem.IncomeExpenseGroup.CashFlow)
                    .Include(t => t.PlanFact);

                switch (reportParams.ReportType)
                {
                    case ReportType.Plan:
                        rez = await temp.Where(t => t.PlanFact.Name.ToLower().Equals("plan")).ToListAsync();
                        break;
                    case ReportType.Fact:
                        rez = await temp.Where(t => t.PlanFact.Name.ToLower().Equals("fact")).ToListAsync();
                        break;
                    case ReportType.Budget:
                        rez = await temp.Where(t => t.PlanFact.Name.ToLower().Equals("budget")).ToListAsync();
                        break;
                    default:
                        rez = new List<Ledger>();
                        break;
                }

                reportParams.LedgerData = rez;
                ViewBag.Sum = await GetSumInUAH(rez, reportParams);
            }
            if (reportParams.LedgerData == null)
                reportParams.LedgerData = new List<Ledger>();

            return View(reportParams);
        }

        private async Task<double> GetSumInUAH(List<Ledger> rez, IncomeExpenseByMonthVm param)
        {
            double sum = 0;
            var rateList = await db.CurrencyCourses
                .Include(t => t.Currency)
                .Where(t => t.Date.Year == param.CurrentSelection.Year && t.Date.Month == param.CurrentSelection.Month)
                .ToListAsync();
            var curList = await db.Currencies.ToListAsync();

            foreach (Ledger item in rez)
            {
                if (item.Account.Currency.ISOCurrencySymbol.Equals("uah", StringComparison.OrdinalIgnoreCase))
                {
                    sum += item.Sum;
                }
                else
                {
                    //getting currency exchange rate for curent operation
                    Currency curCurrency = item.Account.Currency;
                    var curRate = rateList.FirstOrDefault(t =>
                        t.Currency.Id == curCurrency.Id &&
                        t.Date.Year == item.Date.Year &&
                        t.Date.Month == item.Date.Month &&
                        t.Date.Day == item.Date.Day);
                    if(curRate == null)
                        this.ModelState.AddModelError("", $"Sum {item.Sum}. There is no rate for the day {item.Date.ToShortDateString()} for currency {item.Account.Currency.ISOCurrencySymbol} ({item.Account.Currency.CurrencyEnglishName})");
                    else
                        sum += item.Sum * curRate.Course;
                }
            }
            return sum;
        }

        [HttpGet]
        public async Task<ActionResult> ProductsByCategories()
        {
            IEnumerable<IGrouping<string, IncomeExpenseItem>> res = await db.IncomeExpenseItems
                .Include(t => t.IncomeExpenseGroup)
                .GroupBy(t => t.IncomeExpenseGroup.Name)
                .ToListAsync();

            return View(res);
        }


        [HttpGet]//fact expences
        public ActionResult TotalsByCategories()
        {
            return View(new TotalByCategoriesVm());
        }
        //fact expences in UAH        
        [HttpPost]
        public ActionResult TotalsByCategories([Bind(Include = "From,To")]TotalByCategoriesVm dataSet)
        {
            CorrectTimeOfDates(dataSet);
            ApplicationUser curUser = db.Users.FirstOrDefault(t => t.UserName.Equals(User.Identity.Name));

            //operations of the current user for selected period:
            List<IGrouping<string, Ledger>> operations = GetFactLedgerExpenses(dataSet, curUser);

            List<CurrencyCourse> currencyCourses = db.CurrencyCourses
                .Where(t => t.Date >= dataSet.From && t.Date <= dataSet.To)
                .Include(t => t.Currency)
                .ToList();

            Dictionary<string, double> rez = new Dictionary<string, double>();
            CalculateFactExpensesByGroups(operations, currencyCourses, rez);
            dataSet.CategorySum = rez;

            return View(dataSet);
        }


        private static IncomeExpenseByMonthVm GetDefaultParamIncExpByMonth()
        {
            return new IncomeExpenseByMonthVm()
            {
                CurrentSelection = DateTime.Now,
                LedgerData = new List<Ledger>(),
                ReportType = ReportType.Fact,
                SelectedForPeriod = SelectedForPeriod.Current
            };
        }
        private List<IGrouping<string, Ledger>> GetFactLedgerExpenses(TotalByCategoriesVm dataSet, ApplicationUser curUser)
        {
            return db.Ledgers
                                .Where(t =>
                                    t.Account.ApplicationUserId.Equals(curUser.Id) &&
                                    t.Date >= dataSet.From &&
                                    t.Date <= dataSet.To &&
                                    t.PlanFact.Name.ToLower().Equals("fact") &&
                                    t.IncomeExpenseItem.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense"))
                                .Include(t => t.Account)
                                .Include(t => t.Account.Currency)
                                .Include(t => t.IncomeExpenseItem)
                                .Include(t => t.IncomeExpenseItem.IncomeExpenseGroup)
                                .Include(t => t.IncomeExpenseItem.IncomeExpenseGroup.CashFlow)
                                .Include(t => t.PlanFact)
                                .GroupBy(t => t.IncomeExpenseItem.IncomeExpenseGroup.Name)//grouping by expense group name
                                .ToList();
        }
        private void CalculateFactExpensesByGroups(List<IGrouping<string, Ledger>> operations, List<CurrencyCourse> currencyCourses, Dictionary<string, double> rez)
        {
            foreach (IGrouping<string, Ledger> item in operations)
            {
                double sum = 0;
                foreach (Ledger i in item)
                {
                    if (i.Account.Currency.ISOCurrencySymbol.ToLower().Equals("uah"))
                    {
                        sum += i.Sum;
                    }
                    else
                    {
                        //find the current course to recalculate at the current rate (rates are equal if the Rate Date and a Currency ID are equal)
                        CurrencyCourse cur = currencyCourses
                            .FirstOrDefault(t => AreDatesEqual(t.Date, i.Date) && t.CurrencyId.Equals(i.Account.CurrencyId));
                        if (cur == null)
                        {
                            ModelState.AddModelError("", $"there in no currency exchange rate for date {i.Date.ToLongDateString()} for currency {i.Account.Currency.EnglishName} - {i.Account.Currency.ISOCurrencySymbol}");
                        }
                        else
                        {
                            sum += i.Sum * cur.Course;
                        }
                    }
                }
                rez.Add(item.Key, sum);
            }
        }
        private static void CorrectTimeOfDates(TotalByCategoriesVm dataSet)
        {
            //sero out time values to compare date only
            dataSet.From = new DateTime(dataSet.From.Year, dataSet.From.Month, dataSet.From.Day);
            //set the last second of a day
            dataSet.To = new DateTime(dataSet.To.Year, dataSet.To.Month, dataSet.To.Day, 23, 59, 59, DateTimeKind.Local);
        }
        private static bool AreDatesEqual(DateTime d1, DateTime d2) => d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day;
    }
}



//public ActionResult TotalsByCategories([Bind(Include = "From,To")]TotalByCategoriesVm dataSet)
//{
//    if (ModelState.IsValid)
//    {
//        var curUser = db.Users.FirstOrDefault(t => t.UserName.Equals(User.Identity.Name));
//        var rez = db.Ledgers
//            .Where(t => 
//                    t.Account.ApplicationUserId.Equals(curUser.Id) &&
//                    t.Date <= dataSet.To && 
//                    t.PlanFact.Name.ToLower().Equals("fact") && 
//                    t.IncomeExpenseItem.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense"))
//            .Include(t=>t.Account)
//            .Include(t=>t.Account.Currency)
//            .Include(t=>t.IncomeExpenseItem)
//            .Include(t=>t.IncomeExpenseItem.IncomeExpenseGroup)
//            .Include(t=>t.IncomeExpenseItem.IncomeExpenseGroup.CashFlow)
//            .Include(t=>t.PlanFact)
//            .ToList();

//        var cources = db.CurrencyCourses.Include(d => d.Currency).ToList();

//        foreach (var item in rez)
//        {
//            var temp = cources.FirstOrDefault(t => 
//                AreDatesEqual(t.Date , item.Date) && 
//                t.Currency.NumericCountryCode.Equals(item.Account.Currency.NumericCountryCode));
//            if (temp == null)
//            {
//                ModelState.AddModelError("", $"Enter the currency rate for {item.Account.Currency.ISOCurrencySymbol} - {item.Account.Currency.CurrencyEnglishName} for date {item.Date}");
//            }
//        }

//        Dictionary<string, double> keyValuePairs = new Dictionary<string, double>();
//        if (ModelState.IsValid)
//        {
//            //received a target group of fact expenses:
//            IEnumerable<IGrouping<string, Ledger>> ledg = rez.GroupBy(t => t.IncomeExpenseItem.IncomeExpenseGroup.Name);
//            //it is by default! change after the adding a setting object into UserTable!!!
//            Currency userBalanceCurrency = db.Currencies.FirstOrDefault(t => t.ISOCurrencySymbol.ToLower().Equals("uah"));
//            dataSet.UserBalanceCurrency = userBalanceCurrency;

//            if (dataSet.CategorySum == null)
//                dataSet.CategorySum = new Dictionary<string, double>();

//            if (ledg != null)
//            {
//                foreach (var item in ledg)
//                {
//                    //calculating the sum by certain category:
//                    double sum = 0;
//                    foreach (Ledger i in item)
//                    {
//                        //if operation currency equals user balance currency
//                        if (i.Account.Currency.CurrencyEnglishName.Equals(userBalanceCurrency.CurrencyEnglishName))
//                        {
//                            sum += i.Sum;
//                        }
//                        else
//                        {
//                            sum += i.Sum * cources.FirstOrDefault(t => 
//                            AreDatesEqual(t.Date, i.Date) && 
//                            t.Currency.ISOCurrencySymbol.Equals(i.Account.Currency.ISOCurrencySymbol)).Course;
//                        }
//                    }
//                    dataSet.CategorySum.Add(item.Key, sum);
//                }
//            }
//        }
//    }
//    return View(dataSet);
//}


//public async Task<ActionResult> IncomeExpenseForMonth(DateTime CurrentSelection,
//           SelectedForPeriod selectedForPeriod = SelectedForPeriod.Current,
//           ReportType reportType = ReportType.Fact)
//{
//    //if a period was selected, save it
//    if (!(CurrentSelection == null || CurrentSelection == new DateTime(1, 1, 1, 0, 0, 0, 0)))
//        currentReportPeriod = CurrentSelection;

//    //change the report period according to request (next or previous
//    switch (selectedForPeriod)
//    {
//        case SelectedForPeriod.Previous: currentReportPeriod = currentReportPeriod.AddMonths(-1); break;
//        case SelectedForPeriod.Next: currentReportPeriod = currentReportPeriod.AddMonths(1); break;
//        default: break;
//    }

//    List<Ledger> rez = null;
//    using (ApplicationDbContext db = new ApplicationDbContext())
//    {
//        //getting all the ledger data for report period:
//        var temp = db.Ledgers
//            .Where(t => t.Date.Year == CurrentSelection.Year && t.Date.Month == CurrentSelection.Month)
//            .Include(t => t.Account)
//            .Include(t => t.IncomeExpenseItem)
//            .Include(t => t.PlanFact);

//        switch (reportType)
//        {
//            case ReportType.Plan:
//                rez = await temp.Where(t => t.PlanFact.Name.ToLower().Equals("plan")).ToListAsync();
//                break;
//            case ReportType.Fact:
//                rez = await temp.Where(t => t.PlanFact.Name.ToLower().Equals("fact")).ToListAsync();
//                break;
//            case ReportType.Budget:
//                rez = await temp.Where(t => t.PlanFact.Name.ToLower().Equals("budget")).ToListAsync();
//                break;
//            default:
//                rez = new List<Ledger>();
//                break;
//        }
//    }

//    IncomeExpenseByMonthVm iebm = new IncomeExpenseByMonthVm()
//    {
//        CurrentSelection = CurrentSelection,
//        SelectedForPeriod = selectedForPeriod,
//        ReportType = reportType,
//        LedgerData = rez
//    };
//    return View(iebm);
//}