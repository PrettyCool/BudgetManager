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
    public class TableIncomeExpenseGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index(IncomeExpenseSort sortWay = IncomeExpenseSort.Name)
        {
            var rez = db.IncomeExpenseGroups.Include(i => i.CashFlow);

            ViewData["Name"] = sortWay == IncomeExpenseSort.Name ? IncomeExpenseSort.NameDesc : IncomeExpenseSort.Name;
            ViewData["CashFlow"] = sortWay == IncomeExpenseSort.CashFlowType ? IncomeExpenseSort.CashFlowTypeDesc : IncomeExpenseSort.CashFlowType;
            
            return View(await SortIncExp(sortWay, rez));
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenseGroup incomeExpenseGroup = await db.IncomeExpenseGroups.FindAsync(id);
            if (incomeExpenseGroup == null)
            {
                return HttpNotFound();
            }
            return View(incomeExpenseGroup);
        }


        public ActionResult Create()
        {
            ViewBag.CashFlowId = new SelectList(db.CashFlows, "Id", "Type");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,CashFlowId")] IncomeExpenseGroup incomeExpenseGroup)
        {
            if (ModelState.IsValid)
            {
                var rez = await db.IncomeExpenseGroups.FirstOrDefaultAsync(t => t.Name.Equals(incomeExpenseGroup.Name, StringComparison.OrdinalIgnoreCase));
                if(rez != null)
                    ModelState.AddModelError("",$"An attempt to duplicate the income/expense group by name: {incomeExpenseGroup.Name}");

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.IncomeExpenseGroups.Add(incomeExpenseGroup);
                        int stop = await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
                }
            }

            ViewBag.CashFlowId = new SelectList(db.CashFlows, "Id", "Type", incomeExpenseGroup.CashFlowId);
            return View(incomeExpenseGroup);
        }


        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            IncomeExpenseGroup incomeExpenseGroup = await db.IncomeExpenseGroups.FindAsync(id);
            if (incomeExpenseGroup == null)
                return HttpNotFound();
            ViewBag.CashFlowId = new SelectList(db.CashFlows, "Id", "Type", incomeExpenseGroup.CashFlowId);
            return View(incomeExpenseGroup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CashFlowId")] IncomeExpenseGroup incomeExpenseGroup)
        {
            if (ModelState.IsValid)
            {
                var rez = await db.IncomeExpenseGroups
                    .FirstOrDefaultAsync(t =>
                        t.Name.Equals(incomeExpenseGroup.Name, StringComparison.OrdinalIgnoreCase) &&
                        t.Id != incomeExpenseGroup.Id);
                if (rez != null)
                    ModelState.AddModelError("", $"The income/expense group by name \"{incomeExpenseGroup.Name}\" already exists!");

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(incomeExpenseGroup).State = EntityState.Modified;
                        int stop = await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
                }
            }
            ViewBag.CashFlowId = new SelectList(db.CashFlows, "Id", "Type", incomeExpenseGroup.CashFlowId);
            return View(incomeExpenseGroup);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomeExpenseGroup incomeExpenseGroup = await db.IncomeExpenseGroups.FindAsync(id);
            if (incomeExpenseGroup == null)
            {
                return HttpNotFound();
            }
            return View(incomeExpenseGroup);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IncomeExpenseGroup incomeExpenseGroup = await db.IncomeExpenseGroups.FindAsync(id);
            db.IncomeExpenseGroups.Remove(incomeExpenseGroup);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }        


        private static async Task<List<IncomeExpenseGroup>> SortIncExp(IncomeExpenseSort sortWay, IQueryable<IncomeExpenseGroup> rez)
        {
            switch (sortWay)
            {
                case IncomeExpenseSort.NameDesc: return await rez.OrderByDescending(t => t.Name).ThenByDescending(t => t.CashFlow.Type).ToListAsync();
                case IncomeExpenseSort.CashFlowType: return await rez.OrderBy(t => t.CashFlow.Type).ThenBy(t => t.Name).ToListAsync();
                case IncomeExpenseSort.CashFlowTypeDesc: return await rez.OrderByDescending(t => t.CashFlow.Type).ThenByDescending(t => t.Name).ToListAsync();
                default:
                case IncomeExpenseSort.Name: return await rez.OrderBy(t => t.Name).ThenBy(t => t.CashFlow.Type).ToListAsync();
            }
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
