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
    public class TableCurrencyCoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index(int? numericCountryCode = 9999)//id - filtered currency id 
        {
            var currencies = db.Currencies.OrderBy(t => t.EnglishName).ToList();

            List<CurrencyCourse> cources;
            if (numericCountryCode != null && numericCountryCode != 9999)//filter by selected id (9999 - all curencies)
                cources = await db.CurrencyCourses
                    .Where(c => c.Currency.NumericCountryCode.Equals(numericCountryCode.ToString()))
                    .OrderBy(t => t.Date).ThenBy(t=>t.Currency.EnglishName)
                    .ToListAsync();
            else
                cources = await db.CurrencyCourses.Select(t => t).OrderBy(t=>t.Date).ToListAsync();
            //add
            currencies.Insert(0, new Currency() { NumericCountryCode= "9999", EnglishName= "All", CurrencyEnglishName = "All" });

            CurrencyCourcesViewModels ccv = new CurrencyCourcesViewModels()
            {
                CurrencyCourses = cources,
                FilterCurrencies = currencies//new SelectList(currencies, "Id", "CurrencyEnglishName")
            };

            return View(ccv);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyCourse currencyCourse = await db.CurrencyCourses.FindAsync(id);
            if (currencyCourse == null)
            {
                return HttpNotFound();
            }
            return View(currencyCourse);
        }

        public ActionResult Create()
        {
            ViewBag.CurrencyId = new SelectList(db.Currencies.OrderBy(t=>t.EnglishName), "Id", "EnglishName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,CurrencyId")] CurrencyCourse currencyCourse, string course)
        {
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "EnglishName", currencyCourse.CurrencyId);

            if (currencyCourse == null)
                ModelState.AddModelError("", $"Impossible to create a new currency rate - data was not received!");
            course = course.Replace('.', ',');
            if (!Double.TryParse(course, out double rez))
                ModelState.AddModelError("", $"the course value {course} is not valid");

            if (ModelState.IsValid)
            {
                Currency requestedCurrency = await db.Currencies.FindAsync(currencyCourse.CurrencyId);
                //zero out time of a new rate:
                currencyCourse.Date = new DateTime(currencyCourse.Date.Year, currencyCourse.Date.Month, currencyCourse.Date.Day, 0, 0, 0);

                CurrencyCourse temp = await db.CurrencyCourses.FirstOrDefaultAsync(t =>
                    t.Date == currencyCourse.Date &&
                    t.Currency.EnglishName.Equals(requestedCurrency.EnglishName));
                if (temp != null)
                    ModelState.AddModelError("", $"Can not create this course. There is already this entry in the database. Select an action EDIT in the cource list");

                if (ModelState.IsValid)
                {
                    currencyCourse.Course = rez;
                    db.CurrencyCourses.Add(currencyCourse);
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
            }           

            return View(currencyCourse);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyCourse currencyCourse = await db.CurrencyCourses.FindAsync(id);
            if (currencyCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "EnglishName", currencyCourse.CurrencyId);
            return View(currencyCourse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,CurrencyId")] CurrencyCourse currencyCourse, string course)
        {
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "EnglishName", currencyCourse.CurrencyId);

            if (currencyCourse == null)
                ModelState.AddModelError("", $"Impossible to create a new currency rate - data was not received!");
            course = course.Replace('.', ',');
            if (!Double.TryParse(course, out double rez))
                ModelState.AddModelError("", $"the course value {course} is not valid");

            if (ModelState.IsValid)
            {
                //zero out time of a new rate:
                currencyCourse.Date = new DateTime(currencyCourse.Date.Year, currencyCourse.Date.Month, currencyCourse.Date.Day, 0, 0, 0);

                CurrencyCourse toEdit = await db.CurrencyCourses.FirstOrDefaultAsync(t => 
                    t.Date == currencyCourse.Date &&
                    t.Currency.Id == currencyCourse.CurrencyId);

                if (toEdit != null && toEdit.Id != currencyCourse.Id)
                    ModelState.AddModelError("", $"Cannot create one more entry of the same currency ({toEdit.Currency.EnglishName}) of the same date ({toEdit.Date.ToShortDateString()})");

                if (ModelState.IsValid)
                {
                    currencyCourse.Course = rez;
                    db.Entry(currencyCourse).State = EntityState.Modified;
                    try
                    {
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }            
            }
            return View(currencyCourse);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyCourse currencyCourse = await db.CurrencyCourses.FindAsync(id);
            if (currencyCourse == null)
            {
                return HttpNotFound();
            }
            return View(currencyCourse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CurrencyCourse currencyCourse = await db.CurrencyCourses.FindAsync(id);
            db.CurrencyCourses.Remove(currencyCourse);
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
