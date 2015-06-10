using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SplitBill.Models;
using SplitBill.Calculation;

namespace SplitBill.Controllers
{
    public class ReportsController : Controller
    {
        private BillingDBContext db = new BillingDBContext();
        
        private Calculate calculate = new Calculate();
        private Display disp = new Display();

        // GET: Reports
        public ActionResult Index(string searchString, string billingMonth)
        {
            
            var monthList = new List<string>();

            var monthQuery = from d in db.Billings
                           orderby d.BillingMonth
                           select d.BillingMonth;

            monthList.AddRange(monthQuery.Distinct());
            ViewBag.billingMonth = new SelectList(monthList);

            var movies = from m in db.Billings
                         select m;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    movies = movies.Where(s => s.Title.Contains(searchString));
            //}

            if (!string.IsNullOrEmpty(billingMonth))
            {
                movies = movies.Where(x => x.BillingMonth == billingMonth);
                ViewBag.total = calculate.Total(billingMonth);
                ViewBag.perHead = calculate.perHead(billingMonth);
                ViewBag.finalMessage = disp.finalMessage(billingMonth);
            }
            
            return View(movies);
        }

        
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
