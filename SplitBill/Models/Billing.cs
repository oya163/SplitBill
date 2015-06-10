using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SplitBill.Models
{
    public class Billing
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime PaidDate { get; set; }
        public string PaidBy { get; set; }
        public double PaidAmount { get; set; }
        public string BillingMonth { get; set; }
    }

    public class Report
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime PaidDate { get; set; }
        public string PaidBy { get; set; }
        public double PaidAmount { get; set; }
        public string BillingMonth { get; set; }
    }

    public class Month
    {
        public int monthId { get; set; }
        public string month { get; set; }
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }


    public class BillingDBContext : DbContext
    {
        public DbSet<Billing> Billings { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Month> Months { get; set; }

        public DbSet<User> Users { get; set; }
    }
}