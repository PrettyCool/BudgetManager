using BudgetManager_V1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetManager_V1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
        public ActionResult Help() => View();
    }
}