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
    public class TableCashFlowsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.CashFlows.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashFlow cashFlow = await db.CashFlows.FindAsync(id);
            if (cashFlow == null)
            {
                return HttpNotFound();
            }
            return View(cashFlow);
        }

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type")] CashFlow cashFlow)
        {
            if (ModelState.IsValid)
            {
                CashFlow existsCF = await db.CashFlows.FirstOrDefaultAsync(t => t.Type.ToLower().Equals(cashFlow.Type));
                if (existsCF != null)
                    ModelState.AddModelError("", $"An attempt to duplicate the CashFlow item \"{cashFlow.Type}\"!");

                if (ModelState.IsValid)
                {
                    db.CashFlows.Add(cashFlow);
                    try
                    {
                        int stop = await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
                }
            }

            return View(cashFlow);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            CashFlow cashFlow = await db.CashFlows.FindAsync(id);
            if (cashFlow == null)
                return HttpNotFound();
            return View(cashFlow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type")] CashFlow cashFlow)
        {
            if (ModelState.IsValid)
            {
                CashFlow rez = await db.CashFlows.FirstOrDefaultAsync(t => 
                    t.Type.Equals(cashFlow.Type, StringComparison.OrdinalIgnoreCase) && 
                    t.Id != cashFlow.Id);
                if (rez != null)
                    ModelState.AddModelError("", $"An attempt to duplicate the CashFlow item \"{cashFlow.Type}\"!");

                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(cashFlow).State = EntityState.Modified;
                        int stop = await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
                }
            }
            return View(cashFlow);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashFlow cashFlow = await db.CashFlows.FindAsync(id);
            if (cashFlow == null)
            {
                return HttpNotFound();
            }
            return View(cashFlow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CashFlow cashFlow = await db.CashFlows.FindAsync(id);
            db.CashFlows.Remove(cashFlow);
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
