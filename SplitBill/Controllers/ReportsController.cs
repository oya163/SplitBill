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
        private Display display = new Display();

        // GET: Reports
        public ActionResult Index(string billingMonth, string billingYear)
        {
            var monthList = new List<string>();

            var monthQuery = from d in db.Billings
                             orderby d.BillingMonth
                             select d.BillingMonth;

            monthList.AddRange(monthQuery.Distinct());
            ViewBag.billingMonth = new SelectList(monthList);
            ViewBag.billingYear = new SelectList(display.years());
            
            var items = from m in db.Billings select m;
            int billYear = Convert.ToInt32(billingYear);

            if (!string.IsNullOrEmpty(billingMonth) && !string.IsNullOrEmpty(billingYear))
            {
                items = items.Where(x => x.BillingMonth == billingMonth && x.BillingYear.ToString() == billingYear) ;
                ViewBag.total = calculate.Total(billingMonth, billYear);
                ViewBag.perHead = calculate.perHead(billingMonth, billYear);
                ViewBag.finalMessage = display.finalMessage(billingMonth, billYear);
            }
            else
            {
                string currentMonth = DateTime.Now.ToString("MMMM");

                int currentYear = DateTime.Now.Year;
                items = items.Where(x => x.BillingMonth == currentMonth && x.BillingYear == currentYear);
            }

            return View(items);
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
