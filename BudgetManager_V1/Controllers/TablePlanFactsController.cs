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
    public class TablePlanFactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.PlanFacts.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanFact planFact = await db.PlanFacts.FindAsync(id);
            if (planFact == null)
            {
                return HttpNotFound();
            }
            return View(planFact);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] PlanFact planFact)
        {
            if (ModelState.IsValid)
            {
                db.PlanFacts.Add(planFact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(planFact);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanFact planFact = await db.PlanFacts.FindAsync(id);
            if (planFact == null)
            {
                return HttpNotFound();
            }
            return View(planFact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] PlanFact planFact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planFact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(planFact);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanFact planFact = await db.PlanFacts.FindAsync(id);
            if (planFact == null)
            {
                return HttpNotFound();
            }
            return View(planFact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PlanFact planFact = await db.PlanFacts.FindAsync(id);
            db.PlanFacts.Remove(planFact);
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
