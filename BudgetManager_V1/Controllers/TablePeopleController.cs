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
    public class TablePeopleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //selects one person - the host of the current logged in accound
        public ActionResult Index()
        {
            ApplicationUser curUser = GetCurrentUser();
            List<Person> personList = new List<Person>();

            if (curUser != null)
            {
                List<Person> allPeople = db.People.Include(t => t.ApplicationUsers).ToList();
                
                foreach (Person item in allPeople)
                {
                    foreach (ApplicationUser i in item.ApplicationUsers)
                    {
                        if (i.Id.Equals(curUser.Id))
                        {
                            personList.Add(item);
                            return View(personList);
                        }
                    }
                }
            }
            return View(personList);
        }


        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,Patronymic,Birthday")] Person person)
        {
            if (ModelState.IsValid)
            {
                bool exists = await IsPersonAlreadyCreated(person);
                if (!exists)
                {
                    person.Id = Guid.NewGuid().ToString();
                    db.People.Add(person);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(person);
        }

        /// <summary>
        /// if a person already created - add a ModelState error
        /// </summary>
        /// <param name="person"></param>
        /// <returns>in accordance to method name</returns>
        private async Task<bool> IsPersonAlreadyCreated(Person person)
        {
            Person temp = await db.People.FirstOrDefaultAsync(t => 
                t.FirstName.Equals(person.FirstName, StringComparison.OrdinalIgnoreCase) &&
                t.LastName.Equals(person.LastName, StringComparison.OrdinalIgnoreCase) &&
                t.Patronymic.Equals(person.Patronymic, StringComparison.OrdinalIgnoreCase));

            if (temp != null)
            {
                ModelState.AddModelError("", $"The user {person.ToString()} is already exists");
                return true;
            }
            return false;
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Patronymic,Birthday")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Person person = await db.People.FindAsync(id);
            db.People.Remove(person);
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
