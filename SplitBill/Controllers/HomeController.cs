using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SplitBill.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Group Members";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Page Owner - Oyesh Mann Singh.";

            return View();
        }
    }
}