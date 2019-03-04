using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BudgetManager_V1.Models
{

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Accounts = new HashSet<Account>();
        }

        public string PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }


        public virtual ICollection<Account> Accounts { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Person> People { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyCourse> CurrencyCourses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CashFlow> CashFlows { get; set; }
        public DbSet<IncomeExpenseGroup> IncomeExpenseGroups { get; set; }
        public DbSet<IncomeExpenseItem> IncomeExpenseItems { get; set; }
        public DbSet<PlanFact> PlanFacts { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<BudgetManager_V1.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<BudgetManager_V1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}