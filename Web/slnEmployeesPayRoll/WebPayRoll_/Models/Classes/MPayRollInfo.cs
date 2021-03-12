using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPayRoll_.Controllers;
namespace WebPayRoll_.Models.Classes
{
    public class MPayRollInfo
    {
        public int PayRollInfoID { get; set; }
        public Nullable<decimal> BaseSalary { get; set; }
        public Nullable<decimal> BreakfastDeduction { get; set; }
        public Nullable<decimal> SavingDeduction { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<int> EmployeeID { get; set; }

        public Nullable<decimal> BreakfastDeductionAmount 
        {
            get { return this.BreakfastDeduction * this.BaseSalary; }
        }

        public Nullable<decimal> SavingDeductionAmount
        {
            get { return this.SavingDeduction * this.BaseSalary; }
        }

        public Nullable<decimal> TotalDeductions
        {
            get { return this.BreakfastDeductionAmount + this.SavingDeductionAmount; }
        }

        public Nullable<decimal> NetSalary
        {
            get { return this.BaseSalary - (this.BreakfastDeductionAmount + this.SavingDeductionAmount); }
        }

    }
}