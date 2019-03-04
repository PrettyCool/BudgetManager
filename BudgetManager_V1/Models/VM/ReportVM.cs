using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BudgetManager_V1.Models;

namespace BudgetManager_V1.Models.VM
{
    public class IncomeExpenseByMonthVm
    {
        public List<Ledger> LedgerData { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CurrentSelection { get; set; }//any date of a reporting month

        public SelectedForPeriod SelectedForPeriod { get; set; }

        public ReportType ReportType { get; set; }
    }
    public enum SelectedForPeriod
    {
        Current,
        Previous,
        Next
    }
    public enum ReportType
    {
        Plan,
        Fact,
        Budget
    }

    public class TotalByCategoriesVm
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Currency UserBalanceCurrency { get; set; } // a user will contain this himself, but needed to display
        public IDictionary<string,double> CategorySum { get; set; }
    }
}