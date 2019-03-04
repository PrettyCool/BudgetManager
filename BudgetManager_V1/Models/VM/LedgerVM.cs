using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManager_V1.Models.VM
{
    public enum LedgerSort
    {
        Date, DateDesc,
        Sum, SumDesc,
        Comment, CommentDesc,
        Account, AccountDesc,
        IncomeExpenseItem, IncomeExpenseItemDesc,
        PlanFact, PlanFactDesc
    }
}