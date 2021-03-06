﻿using System;
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

        public List<int> years()
        {
            List<int> yearList = new List<int>();

            for (int i = 2015; i < 2020; i++)
            {
                yearList.Add(i);
            }

            return yearList;
        }

        public List<string> usersList()
        {
            List<string> usersList = new List<string>();

            var queryUsers = (from users in splitBill.Users select users).ToList();
            foreach (var item in queryUsers)
            {
                usersList.Add(item.Name);
            }
            return usersList;
        }

        public List<string> month()
        {
            List<string> amonth = new List<string>();
            amonth.AddRange(DateTimeFormatInfo.CurrentInfo.MonthNames);
            return amonth;
        }

        public List<string> finalMessage(string month, int year)
        {
            int noOfUsers = splitBill.Users.Count();
            Calculate calc = new Calculate();
            List<double> perAmount = new List<double>(noOfUsers);
            List<string> finalMessage = new List<string>();
            double perHead = calc.perHead(month, year);

           
            

            var forEachTotal = splitBill.Billings.Where(h => h.BillingMonth == month && h.BillingYear == year).
                            GroupBy(h => new { h.PaidBy }).
                            Select(group => new { PaidBy = group.Key.PaidBy, Amount = group.Sum(s => s.PaidAmount) }).
                            OrderByDescending(item => item.Amount).ToList();

            var usersTotalList = from u in splitBill.Users select u.Name;

            var notPaidUserList = usersTotalList.Except(forEachTotal.Select(a => a.PaidBy)).ToList();

            for (int i = 0; i < forEachTotal.Count; i++)
            {
                perAmount.Add(forEachTotal[i].Amount - perHead);
            }

                //foreach (var item in forEachTotal)
                //{
                //    perAmount.Add(item.Amount - perHead);
                //}
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

            if (notPaidUserList.Count != 0)
            {
                foreach (var user in notPaidUserList)
                {
                    finalMessage.Add(user + " needs to give $" + perHead);
                }
            }

            return finalMessage;
        }
    }
}