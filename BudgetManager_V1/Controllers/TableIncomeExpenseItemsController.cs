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

namespace BudgetManager_V1.Controllers
{
    [Authorize]
    public class TableIncomeExpenseItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var incomeExpenseItems = db.IncomeExpenseItems.Include(i => i.IncomeExpenseGroup);
            return View(await incomeExpenseItems.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenseItem incomeExpenseItem = await db.IncomeExpenseItems.FindAsync(id);
            if (incomeExpenseItem == null)
            {
                return HttpNotFound();
            }
            return View(incomeExpenseItem);
        }

        public ActionResult Create()
        {
            ViewBag.IncomeExpenseGroupId = new SelectList(db.IncomeExpenseGroups, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,IncomeExpenseGroupId")] IncomeExpenseItem incomeExpenseItem)
        {
            if (ModelState.IsValid)
            {
                var rez = await db.IncomeExpenseItems.FirstOrDefaultAsync(t => t.Name.Equals(incomeExpenseItem.Name, StringComparison.OrdinalIgnoreCase));
                if (rez != null)
                    ModelState.AddModelError("", $"An attempt to duplicate income/expense item by name \"{incomeExpenseItem.Name}\"!");

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.IncomeExpenseItems.Add(incomeExpenseItem);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
                }
            }

            ViewBag.IncomeExpenseGroupId = new SelectList(db.IncomeExpenseGroups, "Id", "Name", incomeExpenseItem.IncomeExpenseGroupId);
            return View(incomeExpenseItem);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            IncomeExpenseItem incomeExpenseItem = await db.IncomeExpenseItems.FindAsync(id);
            if (incomeExpenseItem == null)
                return HttpNotFound();
            ViewBag.IncomeExpenseGroupId = new SelectList(db.IncomeExpenseGroups, "Id", "Name", incomeExpenseItem.IncomeExpenseGroupId);
            return View(incomeExpenseItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IncomeExpenseGroupId")] IncomeExpenseItem incomeExpenseItem)
        {
            if (ModelState.IsValid)
            {
                var rez = await db.IncomeExpenseItems
                    .FirstOrDefaultAsync(t =>
                            t.Name.Equals(incomeExpenseItem.Name, StringComparison.OrdinalIgnoreCase) &&
                            t.Id != incomeExpenseItem.Id);
                if (rez != null)
                    ModelState.AddModelError("", $"The income/expense item by name \'{incomeExpenseItem.Name}\' already exists!");

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(incomeExpenseItem).State = EntityState.Modified;
                        int stop = await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); } 
                }
            }
            ViewBag.IncomeExpenseGroupId = new SelectList(db.IncomeExpenseGroups, "Id", "Name", incomeExpenseItem.IncomeExpenseGroupId);
            return View(incomeExpenseItem);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenseItem incomeExpenseItem = await db.IncomeExpenseItems.FindAsync(id);
            if (incomeExpenseItem == null)
            {
                return HttpNotFound();
            }
            return View(incomeExpenseItem);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IncomeExpenseItem incomeExpenseItem = await db.IncomeExpenseItems.FindAsync(id);
            db.IncomeExpenseItems.Remove(incomeExpenseItem);
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
