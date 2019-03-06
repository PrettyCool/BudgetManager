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
    public class TableLedgersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public  ActionResult Index(LedgerSort sortWay = LedgerSort.Date)
        {
            ApplicationUser curUser = GetCurrentUser();
            List<Ledger> ledgers = null;
            if (curUser != null)
            {
                 ledgers = db.Ledgers
                    .Include(l => l.Account)
                    .Where(t=>t.Account.ApplicationUserId.Equals(curUser.Id))
                    .Include(l => l.IncomeExpenseItem)
                    .Include(l => l.Account.ApplicationUser)
                    .Include(l => l.PlanFact)
                    .ToList();

                if (ledgers == null)
                    return View(new List<Ledger>());
            }

            SaveCurrentSortWay(sortWay);
            var rez = OnSortHandle(sortWay, ledgers);
            return View(rez);
        }
        
        #region additional
        private void SaveCurrentSortWay(LedgerSort sortWay)
        {
            ViewData["DateSort"] = sortWay == LedgerSort.Date ? LedgerSort.DateDesc : LedgerSort.Date;
            ViewData["SumSort"] = sortWay == LedgerSort.Sum ? LedgerSort.SumDesc : LedgerSort.Sum;
            ViewData["CommentSort"] = sortWay == LedgerSort.Comment ? LedgerSort.CommentDesc : LedgerSort.Comment;
            ViewData["AccountSort"] = sortWay == LedgerSort.Account ? LedgerSort.AccountDesc : LedgerSort.Account;
            ViewData["IncomeExpenseItemSort"] = sortWay == LedgerSort.IncomeExpenseItem ? LedgerSort.IncomeExpenseItemDesc : LedgerSort.IncomeExpenseItem;
            ViewData["PlanFactSort"] = sortWay == LedgerSort.PlanFact ? LedgerSort.PlanFactDesc : LedgerSort.PlanFact;
        }
        private List<Ledger> OnSortHandle(LedgerSort sortWay, List<Ledger> data)
        {
            switch (sortWay)
            {                
                case LedgerSort.DateDesc: return  data.OrderByDescending(t => t.Date).ToList();

                case LedgerSort.Sum:    return data.OrderBy(t => t.Sum).ToList();                
                case LedgerSort.SumDesc: return  data.OrderByDescending(t => t.Sum).ToList();

                case LedgerSort.Comment: return  data.OrderBy(t => t.Comment).ToList();
                case LedgerSort.CommentDesc: return  data.OrderByDescending(t => t.Comment).ToList();

                case LedgerSort.Account: return  data.OrderBy(t => t.Account.Name).ToList();
                case LedgerSort.AccountDesc: return  data.OrderByDescending(t => t.Account.Name).ToList();

                case LedgerSort.IncomeExpenseItem: return  data.OrderBy(t => t.IncomeExpenseItem.Name).ToList();
                case LedgerSort.IncomeExpenseItemDesc: return  data.OrderByDescending(t => t.IncomeExpenseItem.Name).ToList();

                case LedgerSort.PlanFact: return  data.OrderBy(t => t.PlanFact.Name).ToList();
                case LedgerSort.PlanFactDesc: return  data.OrderByDescending(t => t.PlanFact.Name).ToList();

                default:
                case LedgerSort.Date: return  data.OrderBy(t => t.Date).ToList();
            }
        }

        #endregion

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ledger ledger = await db.Ledgers.FindAsync(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", "Currency.EnglishName");
            ViewBag.IncomeExpenseItemId = new SelectList(db.IncomeExpenseItems, "Id", "Name");
            ViewBag.PlanFactId = new SelectList(db.PlanFacts, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "Id,Date,Comment,AccountId,IncomeExpenseItemId,PlanFactId")] Ledger ledger, 
            string sum)
        {
            double res = ConvertTextIntoNumber(ref sum);

            if (ModelState.IsValid)
            {
                ledger.Sum = res;
                db.Ledgers.Add(ledger);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", ledger.AccountId);
            ViewBag.IncomeExpenseItemId = new SelectList(db.IncomeExpenseItems, "Id", "Name", ledger.IncomeExpenseItemId);
            ViewBag.PlanFactId = new SelectList(db.PlanFacts, "Id", "Name", ledger.PlanFactId);
            return View(ledger);
        }

        private double ConvertTextIntoNumber(ref string sum)
        {
            sum = sum.Replace('.', ',');
            double res;
            if (!Double.TryParse(sum, out res))
                ModelState.AddModelError("", $"Incorrect value: {sum}");
            return res;
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ledger ledger = await db.Ledgers.FindAsync(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", ledger.AccountId);
            ViewBag.IncomeExpenseItemId = new SelectList(db.IncomeExpenseItems, "Id", "Name", ledger.IncomeExpenseItemId);
            ViewBag.PlanFactId = new SelectList(db.PlanFacts, "Id", "Name", ledger.PlanFactId);
            return View(ledger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Date,Comment,AccountId,IncomeExpenseItemId,PlanFactId")] Ledger ledger,
            string sum)
        {
            double res = ConvertTextIntoNumber(ref sum);

            if (ModelState.IsValid)
            {
                ledger.Sum = res;
                db.Entry(ledger).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "Name", ledger.AccountId);
            ViewBag.IncomeExpenseItemId = new SelectList(db.IncomeExpenseItems, "Id", "Name", ledger.IncomeExpenseItemId);
            ViewBag.PlanFactId = new SelectList(db.PlanFacts, "Id", "Name", ledger.PlanFactId);
            return View(ledger);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ledger ledger = await db.Ledgers.FindAsync(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ledger ledger = await db.Ledgers.FindAsync(id);
            db.Ledgers.Remove(ledger);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private ApplicationUser GetCurrentUser() => db.Users.FirstOrDefault(t => t.UserName.Equals(this.User.Identity.Name));
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
