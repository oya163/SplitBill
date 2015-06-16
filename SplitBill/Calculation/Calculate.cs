using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SplitBill.Models;
using SplitBill.Controllers;

namespace SplitBill.Calculation
{
    public class Calculate
    {
        private BillingDBContext splitBillDB = new BillingDBContext();
        private ApplicationDbContext appDB = new ApplicationDbContext();

        public double Total(string month, int year)
        {
            double total = 0;
            var items = from i in splitBillDB.Billings select i;
            items = items.Where(s => s.BillingMonth == month && s.BillingYear == year);
            total = items.Sum(amt => amt.PaidAmount);
            return total;
        }

        public double perHead(string month, int year)
        {
            double perHead = 0;
            int noOfPerson = splitBillDB.Users.Count();
            //var items = from i in splitBill.Billings select i;
            //items = items.Where(s => s.BillingMonth == month);
            //total = items.Sum(amt => amt.PaidAmount);
            perHead = Total(month,year)/ noOfPerson;
            return perHead;
        }

        

    }
}