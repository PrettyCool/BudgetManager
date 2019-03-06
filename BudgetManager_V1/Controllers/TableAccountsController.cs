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
using Microsoft.AspNet.Identity;

namespace BudgetManager_V1.Controllers
{
    [Authorize]
    public class TableAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            ApplicationUser curUser = GetCurrentUser();
            IQueryable<Account> accounts = null;

            if (curUser != null)
            {
                accounts = db.Accounts
                    .Where(t=>t.ApplicationUser.Id.Equals(curUser.Id))
                    .Include(a => a.ApplicationUser)
                    .Include(a => a.Currency);
            }
            var toSend = await accounts?.ToListAsync();
            return View(toSend);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        public ActionResult Create()
        {
            ViewBag.CurrencyId = new SelectList(db.Currencies.OrderBy(t=>t.EnglishName), "Id", "EnglishName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,CurrencyId")] Account account)
        {
            ApplicationUser curUser = await InitAccountWithEntities(account);

            Account existsAlready = await db.Accounts
                .Include(t => t.ApplicationUser).Include(t => t.Currency)
                .FirstOrDefaultAsync(
                    t => t.ApplicationUserId.Equals(curUser.Id) &&
                    t.CurrencyId == account.CurrencyId &&
                    t.Name.Equals(account.Name));

            if (existsAlready != null)
                ModelState.AddModelError("", $"The account {account.Name} ({account?.Currency?.ISOCurrencySymbol}) already exists");

            if (ModelState.IsValid)
            {
                try
                {
                    db.Accounts.Add(account);
                    int stop = await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "EnglishName", account.CurrencyId);

            return View(account);
        }        

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ApplicationUser curUser = GetCurrentUser();
            Account account = await db.Accounts
                .Where(t => t.ApplicationUserId.Equals(curUser.Id))
                .FirstOrDefaultAsync(t => t.Id == id);
            if (account == null)
                return HttpNotFound();

            //ViewBag.ApplicationUserId = db.Users.Find(db.Users.FirstOrDefault(u => u.Email.Equals(User.Identity.Name)).Id).Id;
            ViewBag.CurrencyId = new SelectList(db.Currencies.OrderBy(c => c.EnglishName), "Id", "EnglishName", account.CurrencyId);
            return View(account);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CurrencyId")] Account account)
        {
            ApplicationUser curUser = await InitAccountWithEntities(account);

            Account existsAlready = await db.Accounts
                .Include(t => t.ApplicationUser).Include(t => t.Currency)
                .FirstOrDefaultAsync(
                    t => t.ApplicationUserId.Equals(curUser.Id) &&
                    t.CurrencyId == account.CurrencyId &&
                    t.Name.Equals(account.Name));
            //there is already a user account with the same name and currency
            if(existsAlready != null && existsAlready.Id != account.Id)
                ModelState.AddModelError("", $"An attempt to duplicate the account ({account.Name} - {account.Currency.ISOCurrencySymbol})");

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(account).State = EntityState.Modified;
                    int stop = await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e) { ModelState.AddModelError("", e.Message); }
            }
            ViewBag.CurrencyId = new SelectList(db.Currencies, "Id", "EnglishName", account.CurrencyId);
            return View(account);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Account account = await db.Accounts.FindAsync(id);
            db.Accounts.Remove(account);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        /// <summary>
        /// sets a current User and a requested Currency for an account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private async Task<ApplicationUser> InitAccountWithEntities(Account account)
        {
            ApplicationUser curUser = GetCurrentUser();
            account.ApplicationUserId = curUser.Id;
            account.ApplicationUser = curUser;

            Currency requestedAccountCurrency = await db.Currencies.FirstOrDefaultAsync(t => t.Id == account.CurrencyId);
            account.Currency = requestedAccountCurrency;
            return curUser;
        }

        private ApplicationUser GetCurrentUser() => db.Users.Find(User.Identity.GetUserId());

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
