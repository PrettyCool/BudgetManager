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
using Microsoft.AspNet.Identity.Owin;

namespace BudgetManager_V1.Controllers
{
    [Authorize]
    public class TableApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()//a current user only!
        {
            ApplicationUser curUser = GetCurrentUser();
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();

            if(curUser != null)
            {
                foreach (ApplicationUser item in await db.Users.Include(t => t.Person).ToListAsync())
                {
                    if (item.UserName.Equals(curUser.UserName))
                    {
                        applicationUsers.Add(item);
                        break;
                    }
                }
            }
            return View(applicationUsers);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var user = db.Users.Find(id);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
                return HttpNotFound();

            user.Person = await db.People.FirstOrDefaultAsync(p => p.Id.Equals(user.PersonId));

            return View(user);
        }

        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.People, "Id", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PersonId,Email,Password,ConfirmPassword")] RegisterViewModel registerVm)
        {
            Person person = db.People
                .FirstOrDefault(t => 
                    t.FirstName.ToLower().Equals(registerVm.FirstName.ToLower()) &&
                    t.LastName.ToLower().Equals(registerVm.LastName.ToLower()) && 
                    t.Patronymic.ToLower().Equals(registerVm.Patronymic.ToLower()));
            if(person == null)
            {
                person = new Person()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = registerVm.FirstName,
                    LastName = registerVm.LastName,
                    Patronymic = registerVm.Patronymic,
                    Birthday = registerVm.Birthday,
                    ApplicationUsers = new HashSet<ApplicationUser>()
                };
                db.Entry<Person>(person).State = EntityState.Added;
                db.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

                ApplicationUser applicationUser = new ApplicationUser()
                {
                     Email = registerVm.Email,
                     UserName = registerVm.Email,
                     PersonId=person.Id
                };

                Microsoft.AspNet.Identity.IdentityResult result = await userManager.CreateAsync(applicationUser, registerVm.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(applicationUser, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index");
            }

            ViewBag.PersonId = new SelectList(db.People, "Id", "FirstName");
            return View();
        }
        
        public async Task<ActionResult> Edit(string id)
        {
            return RedirectToAction("ChangePassword", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PersonId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PersonId = new SelectList(db.People, "Id", "FirstName", applicationUser.PersonId);
            return View(applicationUser);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser =  db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
