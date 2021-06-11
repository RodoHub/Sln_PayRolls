using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Entities
{
    /// <summary>
    /// Api Model PayRollInfo: Holds information related to PayRoll information detailed
    /// </summary>
    public class AM_PayRollInfo
    {
        /// <summary>
        /// ID for PayRoll Info
        /// </summary>
        public int PayRollInfoID { get; set; }

        /// <summary>
        /// (Amount) Base Salary for Employee
        /// </summary>
        public Nullable<decimal> BaseSalary { get; set; }

        /// <summary>
        /// (Percentage %) BreakFast Deduction
        /// </summary>
        public Nullable<decimal> BreakfastDeduction { get; set; }

        /// <summary>
        /// (Percentage %) Saving Deduction
        /// </summary>
        public Nullable<decimal> SavingDeduction { get; set; }

        /// <summary>
        /// Status of PayRoll Info
        /// </summary>
        public Nullable<bool> Active { get; set; }

        /// <summary>
        /// Creation Date of PayRoll info
        /// </summary>
        public Nullable<System.DateTime> CreationDate { get; set; }

        /// <summary>
        /// Employee ID
        /// </summary>
        public Nullable<int> EmployeeID { get; set; }

        /// <summary>
        /// (Amount) BreakFast Deduction
        /// </summary>
        public Nullable<decimal> BreakfastDeductionAmount 
        {
            get { return this.BreakfastDeduction * this.BaseSalary; }
        }

        /// <summary>
        /// (Amount) Saving Deduction
        /// </summary>
        public Nullable<decimal> SavingDeductionAmount
        {
            get { return this.SavingDeduction * this.BaseSalary; }
        }

        /// <summary>
        /// (Amount) Total Deductions
        /// </summary>
        public Nullable<decimal> TotalDeductions
        {
            get { return this.BreakfastDeductionAmount + this.SavingDeductionAmount; }
        }

        /// <summary>
        /// (Amount) Net Salary
        /// </summary>
        public Nullable<decimal> NetSalary
        {
            get { return this.BaseSalary - (this.BreakfastDeductionAmount + this.SavingDeductionAmount); }
        }

        /// <summary>
        /// Token authorization
        /// </summary>
        public Nullable<System.Guid> Token { get; set; }

    }
}