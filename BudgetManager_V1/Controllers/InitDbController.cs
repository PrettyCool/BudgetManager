using BudgetManager_V1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BudgetManager_V1.Controllers
{
    /// <summary>
    /// used for testing
    /// </summary>
    [Authorize]
    public class InitDbController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() => View();

        public async Task< ActionResult> LedgerStartInit()
        {
            ApplicationUser currentUser = GetCurrentUser();
            //if a ledger table is empty for a current user:
            if (currentUser != null && !db.Ledgers.Where(t=>t.Account.ApplicationUserId.Equals(currentUser.Id)).Any())
            {
                int minutesStep = 2;
                int minSum = 1;
                int maxSum = 1000;

                List<Account> accountList = db.Accounts
                    .Where(t=> t.ApplicationUserId.Equals(currentUser.Id))
                    .Include(t => t.Currency)
                    .Include(t => t.ApplicationUser)
                    .ToList();

                List<IncomeExpenseItem> incomeExpenseItemList = db.IncomeExpenseItems
                    .Include(t => t.IncomeExpenseGroup)
                    .Include(t => t.IncomeExpenseGroup.CashFlow)
                    .ToList();
                List<PlanFact> planFactList = db.PlanFacts.ToList();
                List<Ledger> operations = new List<Ledger>();

                Ledger temp = null;
                #region #########1
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList
                        .FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense") && t.Name.ToLower().Equals("underwear")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);

                #endregion
                #region #########2
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("uah")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("uah")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########3
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########4
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("budget")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("budget")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########5
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.
                            Where(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense"))
                            .FirstOrDefault(t => t.Name.ToLower().Equals("meat"))
                            .Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.
                            Where(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income"))
                            .FirstOrDefault(t => t.Name.ToLower().Equals("meat"))
                            .Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion

                #region #########6
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########7
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("uah")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("uah")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########8
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########9
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("budget")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("budget")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########10
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.
                            Where(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense"))
                            .FirstOrDefault(t => t.Name.ToLower().Equals("meat"))
                            .Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(-1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.
                            Where(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income"))
                            .FirstOrDefault(t => t.Name.ToLower().Equals("meat"))
                            .Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion

                #region #########11
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########12
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("uah")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("uah")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########13
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########14
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("budget")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("budget")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.FirstOrDefault(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income")).Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion
                #region #########15
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("fact")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.
                            Where(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("expense"))
                            .FirstOrDefault(t => t.Name.ToLower().Equals("meat"))
                            .Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                try
                {
                    temp = new Ledger()
                    {
                        Sum = GetRandomSumBetween(minSum, maxSum),
                        Date = DateTime.Now.AddMonths(1).AddMinutes(minutesStep++),
                        Comment = "test comment: " + Guid.NewGuid().ToString(),

                        PlanFactId = planFactList.FirstOrDefault(t => t.Name.ToLower().Equals("plan")).Id,
                        AccountId = accountList.FirstOrDefault(t => t.Currency.ISOCurrencySymbol.ToLower().Equals("usd")).Id,
                        IncomeExpenseItemId = incomeExpenseItemList.
                            Where(t => t.IncomeExpenseGroup.CashFlow.Type.ToLower().Equals("income"))
                            .FirstOrDefault(t => t.Name.ToLower().Equals("meat"))
                            .Id
                    };
                }
                catch (Exception e) { var t = e; }
                operations.Add(temp);
                #endregion



                db.Ledgers.AddRange(operations);
                try { int stop = await db.SaveChangesAsync(); }
                catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
            }

            return RedirectToAction("Index", "TableLedgers");
        }
        public async Task<ActionResult> ClearLedger()
        {
            ApplicationUser curUser = GetCurrentUser();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (Ledger item in db.Ledgers.Include(t => t.Account).Where(t => t.Account.ApplicationUserId.Equals(curUser.Id)))
                    db.Entry(item).State = EntityState.Deleted;
                try
                {
                    int stop = await db.SaveChangesAsync();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
            }
            return RedirectToAction("Index", "TableLedgers");
        }

        public async Task<ActionResult> InitAccountList()
        {
            ApplicationUser curUser = GetCurrentUser();
            if (!db.Accounts.Where(t=>t.ApplicationUserId.Equals(curUser.Id)).Any())
            {
                var dollarCur = await db.Currencies.FirstOrDefaultAsync(t => t.NumericCountryCode.Equals(840.ToString()));
                var uahCur = await db.Currencies.FirstOrDefaultAsync(t => t.NumericCountryCode.Equals(804.ToString()));

                List<Account> list = new List<Account>();

                list.Add(new Account() { ApplicationUserId = curUser.Id, Currency = dollarCur, Name = "cash" });
                list.Add(new Account() { ApplicationUserId = curUser.Id, Currency = uahCur, Name = "cash" });
                list.Add(new Account() { ApplicationUserId = curUser.Id, Name = "2600541278965234", Currency = uahCur });
                db.Accounts.AddRange(list);
                try { int stop = await db.SaveChangesAsync(); }
                catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
            }
            return RedirectToAction("Index", "TableAccounts");
        }
        public async Task<ActionResult> ClearAccount()
        {
            ApplicationUser curUser = GetCurrentUser();

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (Account item in db.Accounts.Where(t => t.ApplicationUserId.Equals(curUser.Id)))
                    db.Entry(item).State = EntityState.Deleted;
                int stop = await db.SaveChangesAsync();
            }
            return RedirectToAction("Index", "TableLedgers");
        }


        private ApplicationUser GetCurrentUser() => db.Users.FirstOrDefault(t => t.UserName.Equals(this.User.Identity.Name));
        private static double GetRandomSumBetween(int left, int right)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next(left, right) * 1.0 + rnd.Next(left, right) * 1.0 / 100;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}