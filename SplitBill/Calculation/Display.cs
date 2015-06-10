using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using SplitBill.Calculation;
using SplitBill.Models;

namespace SplitBill.Calculation
{
    public class Display
    {
        private BillingDBContext splitBill = new BillingDBContext();
        private ApplicationDbContext appDB = new ApplicationDbContext();

        public List<string> month()
        {
            List<string> amonth = new List<string>();
            amonth.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            return amonth;
        }

        public List<string> finalMessage(string month)
        {
            Calculate calc = new Calculate();
            List<double> perAmount = new List<double>();
            List<string> finalMessage = new List<string>();
            double perHead = calc.perHead(month);

            var forEachTotal = splitBill.Billings.Where(h => h.BillingMonth == month).
                            GroupBy(h => new { h.PaidBy }).
                            Select(group => new { PaidBy = group.Key.PaidBy, Amount = group.Sum(s => s.PaidAmount) }).
                            OrderByDescending(item => item.Amount).ToList();

            foreach (var item in forEachTotal)
            {
                perAmount.Add(item.Amount - perHead);
            }

            for (int i = 0; i < forEachTotal.Count; i++)
            {
                if (forEachTotal[i].Amount > perHead)
                {
                    finalMessage.Add(forEachTotal[i].PaidBy + " has to take $" + Math.Abs(perAmount[i]));
                }
                else if (forEachTotal[i].Amount == perHead)
                {
                    finalMessage.Add(forEachTotal[i].PaidBy + " has nothing to do. ");
                }
                else if (forEachTotal[i].Amount < perHead)
                {
                    finalMessage.Add(forEachTotal[i].PaidBy + " needs to give $" + Math.Abs(perAmount[i]));
                }
            }
            return finalMessage;
        }
    }
}