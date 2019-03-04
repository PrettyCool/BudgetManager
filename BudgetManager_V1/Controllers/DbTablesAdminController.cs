using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetManager_V1.Controllers
{
    /// <summary>
    /// handling db tables: view, create, edit, delete
    /// </summary>
    public class DbTablesAdminController : Controller
    {
        public ActionResult Index() => View();
    }
}