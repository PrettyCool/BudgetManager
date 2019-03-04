using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CurrencyInfo;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using CurrencyExchangeRate;

namespace BudgetManager_V1.Models
{
    internal class BudgetInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected  override void Seed(ApplicationDbContext db)
        {
            List<Currency> currencyList = new List<Currency>();
            List<CurrencyCourse> currencyCourseList = new List<CurrencyCourse>();
            List<IncomeExpenseGroup> incomeExpenseGroupList = new List<IncomeExpenseGroup>();
            List<IncomeExpenseItem> incomeExpenseItemList = new List<IncomeExpenseItem>();
            List<PlanFact> planFactList = new List<PlanFact>();
            //at least 4 persons
            try
            {
                #region add people
                List<Person> peopleList = new List<Person>()
                {
                    new Person() {Id= Guid.NewGuid().ToString(), FirstName = "Evhen", LastName = "Honcharov", Patronymic = "Yuriyovich", Birthday = new DateTime(1986, 8, 10) },
                    new Person() {Id= Guid.NewGuid().ToString(), FirstName = "Anna", LastName = "Raylyan", Patronymic = "Sergiivna", Birthday = new DateTime(1993, 4, 9) },
                    new Person() {Id= Guid.NewGuid().ToString(), FirstName = "Olga", LastName = "Honcharova", Patronymic = "Yuriivna", Birthday = new DateTime(1984, 12, 12) },
                    new Person() {Id= Guid.NewGuid().ToString(), FirstName = "Vadik", LastName = "Raylyan", Patronymic = "Sergiyovich", Birthday = new DateTime(1991, 7, 5) }
                };
                db.People.AddRange(peopleList);
                db.SaveChanges();
                #endregion

                #region add users
                CreateUsers(peopleList);
                #endregion

                #region add currencies
                try
                {
                    //getting countries with numeric codes 
                    List<CountryData.Country> countriesList = CountryData.Country.List.ToList();

                    //т.к. НБУ даёт лимит на количество запросов, приходится сохранять в файл, чтобы при каждой инициализации не запрашивать одни и те же данные
                    string rootDir = System.IO.Directory
                        .GetParent(AppDomain.CurrentDomain.BaseDirectory)
                        .Parent.FullName;
                    string xmlFile = "serializedRegionInfo.xml";
                    string fullPath = rootDir + "\\" + xmlFile;
                    CurrencyGetter currencyGetter = new CurrencyGetter();
                    //currencyGetter.SerializeXmlInto(xmlPath);
                    List<RegionData> allCountries = currencyGetter.DeserializeXmlFrom(fullPath);
                    foreach (RegionData region in allCountries)
                    {
                        string numericCountryCode = "";
                        //numericCountryCode = countriesList.FirstOrDefault(c => c.Name.ToLower().Contains);
                        numericCountryCode = countriesList.FirstOrDefault(c => (
                        region.EnglishName.ToLower().Equals(c.Name.ToLower()) ||//important to set this condition first in order to choose USD for United states or Ecuador	USD
                        region.EnglishName.ToLower().Contains(c.Name.ToLower()) ||
                        c.Name.ToLower().Contains(region.EnglishName.ToLower())
                        ))?.NumericCode;

                        if (numericCountryCode == null)
                            numericCountryCode = "";
                        currencyList.Add(new Currency(region, numericCountryCode));
                    }
                    currencyList = currencyList.OrderBy(t => t.ISOCurrencySymbol).ToList();
                    db.Currencies.AddRange(currencyList);
                    db.SaveChanges();
                }
                catch (Exception e) { }
                //db.SaveChanges();
                #endregion

                #region currency cources
                ////Currency rates from http://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json, e.g.:
                ///[{"r030":36,"txt":"Австралійський долар","rate":20.765574,"cc":"AUD","exchangedate":"21.03.2017"},
                /// {"r030":124,"txt":"Канадський долар","rate":20.133597,"cc":"CAD","exchangedate":"21.03.2017"}]
                NbuCurrencyExchangeRate nbuRates = new NbuCurrencyExchangeRate();
                List<NBU_data> rates = CurrencyExchangeRateXmlSerializer.LoadFromXml();
                HandleRates(currencyList, currencyCourseList, rates);
                db.CurrencyCourses.AddRange(currencyCourseList);
                #endregion

                #region cash flow
                //CacheFlow
                List<CashFlow> cashFlowList = new List<CashFlow>
                {
                    new CashFlow(){ Type = "Income"},
                    new CashFlow(){ Type = "Expense"}
                };
                db.CashFlows.AddRange(cashFlowList);
                #endregion

                #region income/expense group
                //expenses
                CashFlow cfTemp = cashFlowList.FirstOrDefault(c => c.Type.ToLower().Equals("expense"));
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Food", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Home", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Personal expenses", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Clothes", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Healthcare", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Beauty", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Holidays and birthdays", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Transport", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Leasure and recreation", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Unexpected expenses", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Taxes", CashFlow = cfTemp });
                //income
                cfTemp = cashFlowList.FirstOrDefault(c => c.Type.ToLower().Equals("income"));
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Salary", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Gifts", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Sales", CashFlow = cfTemp });
                incomeExpenseGroupList.Add(new IncomeExpenseGroup() { Name = "Other incomes", CashFlow = cfTemp });
                db.IncomeExpenseGroups.AddRange(incomeExpenseGroupList);
                //db.SaveChanges();
                #endregion

                #region income/expence items
                //expences
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Vegetables",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("food"))
                });
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Meat",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("food"))
                });
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "underwear",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("clothes"))
                });
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Income tax",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("taxes"))
                });
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Public transport",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("transport"))
                });
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Car service",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("transport"))
                });

                //income
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Salary",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("salary"))
                });
                incomeExpenseItemList.Add(new IncomeExpenseItem()
                {
                    Name = "Awards",
                    IncomeExpenseGroup = incomeExpenseGroupList.FirstOrDefault(t => t.Name.ToLower().Equals("food"))
                });

                db.IncomeExpenseItems.AddRange(incomeExpenseItemList);
                #endregion

                #region plan/fact/budget
                planFactList.Add(new PlanFact() { Name = "Plan" });
                planFactList.Add(new PlanFact() { Name = "Fact" });
                planFactList.Add(new PlanFact() { Name = "Budget" });

                db.PlanFacts.AddRange(planFactList);
                #endregion

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                //Controllers.HomeController homeController = new Controllers.HomeController();
                //homeController.ViewBag.InitDbEntityValidationException = e;
                //homeController.ActionInvoker.InvokeAction(homeController.ControllerContext, "Index");
                //throw;
            }
            catch (Exception e) { }

            base.Seed(db);
        }

        private static void HandleRates(List<Currency> currencyList, List<CurrencyCourse> currencyCourseList, List<NBU_data> rates)
        {
            int i = 0;
            foreach (NBU_data nbuRate in rates)
            {
                i++;
                try
                {
                    Currency currentCurrency = currencyList.FirstOrDefault(c =>
                           c.NumericCountryCode.Length != 0 &&
                           c.NumericCountryCode.Trim().Equals(nbuRate.r030.ToString()));

                    if (currentCurrency == null)
                        continue;

                    //getting rate date 
                    string[] dayMonthYear = nbuRate.exchangedate.Split('.');
                    DateTime rateDate = new DateTime(
                        int.Parse(dayMonthYear[2]),
                        int.Parse(dayMonthYear[1]),
                        int.Parse(dayMonthYear[0]));

                    if (currentCurrency != null)
                        currencyCourseList.Add(new CurrencyCourse()
                        {
                            Course = nbuRate.rate,
                            Date = rateDate,
                            CurrencyId = currentCurrency.Id
                        });
                }
                catch (Exception e) { }
            }
        }

        private async Task CreateUsers(List<Person> peopleList)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            string email, password;

            //user
            email = "admin@gmail.com";
            password = "Admin_12345";
            ApplicationUser user = new ApplicationUser()
            {
                Email = email,
                UserName = email,
                PersonId = peopleList[0].Id,
                PhoneNumber = "+380968486814"
            };
            Microsoft.AspNet.Identity.IdentityResult result = await userManager.CreateAsync(user, password);
            await SignInUserNow(signInManager, user, result);            
            //all the below doesn't work: 
            ////admin
            //result = null;
            //email = "admin@gmail.com";
            //password = "Admin_12345";
            //user = new ApplicationUser() { Email = email, UserName = email, PersonId = peopleList[1].Id };
            //result = await userManager.CreateAsync(user, password);
            ////await SignInUserNow(signInManager, user, result);

            ////moderator
            //result = null;
            //email = "moderator@gmail.com";
            //password = "Moderator_12345";
            //user = new ApplicationUser() { Email = email, UserName = email, PersonId = peopleList[2].Id };
            //result = await userManager.CreateAsync(user, password);
            ////await SignInUserNow(signInManager, user, result);

            ////guest
            //result = null;
            //email = "guest@gmail.com";
            //password = "Guest_12345";
            //user = new ApplicationUser() { Email = email, UserName = email, PersonId = peopleList[3].Id };
            //result = await userManager.CreateAsync(user, password);
            ////await SignInUserNow(signInManager, user, result);

            ////test
            //result = null;
            //email = "test@gmail.com";
            //password = "Test_12345";
            //user = new ApplicationUser() { Email = email, UserName = email, PersonId = peopleList[3].Id };
            //result = await userManager.CreateAsync(user, password);
            ////await SignInUserNow(signInManager, user, result);
        }

        private static async Task SignInUserNow(ApplicationSignInManager signInManager, ApplicationUser user, Microsoft.AspNet.Identity.IdentityResult result)
        {
            if (result.Succeeded)
                await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

       
    }
}

