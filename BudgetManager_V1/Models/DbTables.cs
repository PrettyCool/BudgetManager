using CurrencyInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BudgetManager_V1.Models
{
    public class Account
    {
        public Account()
        {
            Ledgers = new HashSet<Ledger>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        [Required(ErrorMessage = "Account name is required")]
        public string Name { get; set; }

        public string ApplicationUserId { get; set; }
        public int CurrencyId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }

        public virtual ICollection<Ledger> Ledgers { get; set; }       
    }

    public class CashFlow
    {
        public CashFlow()
        {
            IncomeExpenseTypes = new HashSet<IncomeExpenseGroup>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Cash flow type is required")]
        [StringLength(30)]
        [Index(IsUnique = true)]
        public string Type { get; set; }

        public virtual ICollection<IncomeExpenseGroup> IncomeExpenseTypes { get; set; }

        //public override string ToString() => Type;
    }

    public class Currency
    {
        public Currency()
        {
            CurrencyCourses = new HashSet<CurrencyCourse>();
            Accounts = new HashSet<Account>();
        }
        public Currency(RegionData data, string numericCountryCode)
        {
            this.EnglishName = data.EnglishName;
            this.ISOCurrencySymbol = data.ISOCurrencySymbol;
            this.CurrencyNativeName = data.CurrencyNativeName;
            this.CurrencyEnglishName = data.CurrencyEnglishName;
            this.GeoId = data.GeoId;
            this.IsMetric = data.IsMetric;
            this.ThreeLetterWindowsRegionName = data.ThreeLetterWindowsRegionName;
            this.ThreeLetterISORegionName = data.ThreeLetterISORegionName;
            this.TwoLetterISORegionName = data.TwoLetterISORegionName;
            this.NativeName = data.NativeName;
            this.DisplayName = data.DisplayName;
            this.Name = data.Name;
            this.CurrencySymbol = data.CurrencySymbol;
            this.NumericCountryCode = numericCountryCode;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Currency english name is required!")]
        [Index(IsUnique = true)]
        public string EnglishName { get; set; }

        [Required(ErrorMessage = "ISO currency symbol is required! The length must be equal to 3")]
        [MaxLength(3), MinLength(3)]
        public string ISOCurrencySymbol { get; set; }

        public string CurrencyNativeName { get; set; }
        public string CurrencyEnglishName { get; set; }
        public int GeoId { get; set; }
        public bool IsMetric { get; set; }
        public string ThreeLetterWindowsRegionName { get; set; }
        public string ThreeLetterISORegionName { get; set; }
        public string TwoLetterISORegionName { get; set; }
        public string NativeName { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }
        public string NumericCountryCode { get; set; }


        public virtual ICollection<CurrencyCourse> CurrencyCourses { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

    }

    public class CurrencyCourse
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Currency course is required!")]
        [DataType(DataType.Currency)]
        public double Course { get; set; }

        [Required(ErrorMessage = "The course date is required!")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual Currency Currency { get; set; }
    }

    public class IncomeExpenseGroup
    {
        public IncomeExpenseGroup()
        {
            IncomeExpenseItems = new HashSet<IncomeExpenseItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "income/expence type is required")]
        [StringLength(50)]
        public string Name { get; set; }

        public int CashFlowId { get; set; }

        [ForeignKey("CashFlowId")]
        public virtual CashFlow CashFlow { get; set; }

        public virtual ICollection<IncomeExpenseItem> IncomeExpenseItems { get; set; }

        //public override string ToString()
        //{
        //    return $"type: {Name,-30}";
        //}
    }

    public class IncomeExpenseItem
    {
        public IncomeExpenseItem()
        {
            Ledgers = new HashSet<Ledger>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "income / expense item name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        public int IncomeExpenseGroupId { get; set; }

        [ForeignKey("IncomeExpenseGroupId")]
        public virtual IncomeExpenseGroup IncomeExpenseGroup { get; set; }

        public virtual ICollection<Ledger> Ledgers { get; set; }
    }

    public class Ledger
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "An operation date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //[Range(0.0, Double.MaxValue)]
        [Required(ErrorMessage = "transaction sum is required")]
        [DataType(DataType.Text)]
        public double Sum { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }
        
        public int AccountId { get; set; }
        public int IncomeExpenseItemId { get; set; }
        public int PlanFactId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [ForeignKey("IncomeExpenseItemId")]
        public virtual IncomeExpenseItem IncomeExpenseItem { get; set; }

        [ForeignKey("PlanFactId")]
        public virtual PlanFact PlanFact { get; set; }

    }

    public class Person: IEquatable<Person>
    {
        public Person() => ApplicationUsers = new HashSet<ApplicationUser>();

        [Key]
        public string Id { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "First name is required!")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Last name is required!")]
        public string LastName { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Patronymic is required!")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Birth date is required!")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public bool Equals(Person p)
        {
            return 
                this.FirstName.ToLower().Equals(p.FirstName.ToLower()) && 
                this.LastName.ToLower().Equals(p.LastName.ToLower()) &&
                this.Patronymic.ToLower().Equals(p.Patronymic.ToLower());
        }

        public override string ToString() => $"{LastName} {FirstName} {Patronymic} ({Birthday.ToShortDateString()})";
    }

    public class PlanFact
    {
        public PlanFact()
        {
            Ledgers = new HashSet<Ledger>();
        }

        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "plan/fact type is required")]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Ledger> Ledgers { get; set; }
    }        
}
