using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetManager_V1.Models;
using BudgetManager_V1.Models.VM;

namespace BudgetManager_V1.Controllers
{
    [Authorize]
    public class TableCurrenciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index(CurrencySortWay curSort = CurrencySortWay.ISOCurrencySymbol)
        {
            List<Currency> curList = await db.Currencies.ToListAsync();
            ReverseFilters(curSort);
            curList = SortCurrencies(curList, curSort);
            return View(curList);
        }


        #region additional
        private void ReverseFilters(CurrencySortWay curSort)
        {
            ViewData["NumericCountryCode"] = curSort == CurrencySortWay.NumericCountryCode ? CurrencySortWay.NumericCountryCodeDesc : CurrencySortWay.NumericCountryCode;
            ViewData["EnglishName"] = curSort == CurrencySortWay.EnglishName ? CurrencySortWay.EnglishNameDesc : CurrencySortWay.EnglishName;
            ViewData["ISOCurrencySymbol"] = curSort == CurrencySortWay.ISOCurrencySymbol ? CurrencySortWay.ISOCurrencySymbolDesc : CurrencySortWay.ISOCurrencySymbol;
            ViewData["CurrencyNativeName"] = curSort == CurrencySortWay.CurrencyNativeName ? CurrencySortWay.CurrencyNativeNameDesc : CurrencySortWay.CurrencyNativeName;
            ViewData["CurrencyEnglishName"] = curSort == CurrencySortWay.CurrencyEnglishName ? CurrencySortWay.CurrencyEnglishNameDesc : CurrencySortWay.CurrencyEnglishName;
            ViewData["ThreeLetterWindowsRegionName"] = curSort == CurrencySortWay.ThreeLetterWindowsRegionName ? CurrencySortWay.ThreeLetterWindowsRegionNameDesc : CurrencySortWay.ThreeLetterWindowsRegionName;
            ViewData["ThreeLetterISORegionName"] = curSort == CurrencySortWay.ThreeLetterISORegionName ? CurrencySortWay.ThreeLetterISORegionNameDesc : CurrencySortWay.ThreeLetterISORegionName;
            ViewData["TwoLetterISORegionName"] = curSort == CurrencySortWay.TwoLetterISORegionName ? CurrencySortWay.TwoLetterISORegionNameDesc : CurrencySortWay.TwoLetterISORegionName;
            ViewData["DisplayName"] = curSort == CurrencySortWay.DisplayName ? CurrencySortWay.DisplayNameDesc : CurrencySortWay.DisplayName;
            ViewData["Name"] = curSort == CurrencySortWay.Name ? CurrencySortWay.NameDesc : CurrencySortWay.Name;
            ViewData["CurrencySymbol"] = curSort == CurrencySortWay.CurrencySymbol ? CurrencySortWay.CurrencySymbolDesc : CurrencySortWay.CurrencySymbol;
        }
        private List<Currency> SortCurrencies(List<Currency> curList, CurrencySortWay currencySortWay)
        {
            switch (currencySortWay)
            {
                //converting to list is obligatory to run the sorting!
                case CurrencySortWay.NumericCountryCode: return curList.OrderBy(c => c.NumericCountryCode).ToList();
                case CurrencySortWay.NumericCountryCodeDesc: return curList.OrderByDescending(c => c.NumericCountryCode).ToList();

                case CurrencySortWay.EnglishName: return curList.OrderBy(c => c.EnglishName).ToList();
                case CurrencySortWay.EnglishNameDesc: return curList.OrderByDescending(c => c.EnglishName).ToList();


                case CurrencySortWay.ISOCurrencySymbolDesc: return curList.OrderByDescending(c => c.ISOCurrencySymbol).ToList();

                case CurrencySortWay.CurrencyNativeName: return curList.OrderBy(c => c.CurrencyNativeName).ToList();
                case CurrencySortWay.CurrencyNativeNameDesc: return curList.OrderByDescending(c => c.CurrencyNativeName).ToList();

                case CurrencySortWay.CurrencyEnglishName: return curList.OrderBy(c => c.CurrencyEnglishName).ToList();
                case CurrencySortWay.CurrencyEnglishNameDesc: return curList.OrderByDescending(c => c.CurrencyEnglishName).ToList();

                case CurrencySortWay.ThreeLetterWindowsRegionName: return curList.OrderBy(c => c.ThreeLetterWindowsRegionName).ToList();
                case CurrencySortWay.ThreeLetterWindowsRegionNameDesc: return curList.OrderByDescending(c => c.ThreeLetterWindowsRegionName).ToList();

                case CurrencySortWay.ThreeLetterISORegionName: return curList.OrderBy(c => c.ThreeLetterISORegionName).ToList();
                case CurrencySortWay.ThreeLetterISORegionNameDesc: return curList.OrderByDescending(c => c.ThreeLetterISORegionName).ToList();

                case CurrencySortWay.TwoLetterISORegionName: return curList.OrderBy(c => c.TwoLetterISORegionName).ToList();
                case CurrencySortWay.TwoLetterISORegionNameDesc: return curList.OrderByDescending(c => c.TwoLetterISORegionName).ToList();

                case CurrencySortWay.DisplayName: return curList.OrderBy(c => c.DisplayName).ToList();
                case CurrencySortWay.DisplayNameDesc: return curList.OrderByDescending(c => c.DisplayName).ToList();

                case CurrencySortWay.Name: return curList.OrderBy(c => c.Name).ToList();
                case CurrencySortWay.NameDesc: return curList.OrderByDescending(c => c.Name).ToList();

                case CurrencySortWay.CurrencySymbol: return curList.OrderBy(c => c.CurrencySymbol).ToList();
                case CurrencySortWay.CurrencySymbolDesc: return curList.OrderByDescending(c => c.CurrencySymbol).ToList();

                default:
                case CurrencySortWay.ISOCurrencySymbol: return curList.OrderBy(c => c.ISOCurrencySymbol).ToList();
            }
        }
        #endregion

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EnglishName,ISOCurrencySymbol,CurrencyNativeName,CurrencyEnglishName,GeoId,IsMetric,ThreeLetterWindowsRegionName,ThreeLetterISORegionName,TwoLetterISORegionName,NativeName,DisplayName,Name,CurrencySymbol,NumericCountryCode")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                var temp = await db.Currencies.FirstOrDefaultAsync(t => t.NumericCountryCode.Equals(currency.NumericCountryCode));
                if(temp != null)
                    ModelState.AddModelError("", $"The currency with Numeric country code {temp.NumericCountryCode} already exists");

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Currencies.Add(currency);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
                }
            }

            return View(currency);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
                return HttpNotFound();
            return View(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EnglishName,ISOCurrencySymbol,CurrencyNativeName,CurrencyEnglishName,GeoId,IsMetric,ThreeLetterWindowsRegionName,ThreeLetterISORegionName,TwoLetterISORegionName,NativeName,DisplayName,Name,CurrencySymbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(currency).State = EntityState.Modified;
                    int stop = await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(currency);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = await db.Currencies.FindAsync(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Currency currency = await db.Currencies.FindAsync(id);
            db.Currencies.Remove(currency);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
