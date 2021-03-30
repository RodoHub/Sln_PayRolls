using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPayRoll_.Models.DB;
using WebPayRoll_.Models.Classes;
using WebPayRoll_.Models.Extensions;
using WebPayRoll_.Models.Api;
namespace WebPayRoll_.Controllers
{
    public class EmployeesController: AuthController
    {
        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private bool IsAuthorized { get; set; }


        /// <summary>
        /// Initialize Api Controller
        /// </summary>
        /// <param name="token">Authorization token</param>
        public EmployeesController(string token = "") 
        {
            this.IsAuthorized = this.ValidateAuthorization(token).Data.Get<bool>();
        }

        /// <summary>
        /// Get Employee
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public MEmployees Get(int id)
        {            
            return (IsAuthorized ? this.Get().Where(item => item.EmployeeID.ToString() == id.ToString()).ToList().FirstOrDefault() : new MEmployees());
        }

        /// <summary>
        /// Get Employees
        /// </summary>
        /// <returns></returns>
        public List<MEmployees> Get()
        {
            return (IsAuthorized ? PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, MEmployees>() : new List<MEmployees>());
        }

        // POST: api/Login
        public void Post
        (
              string Name
            , string LastNames
            , string Email
            , string Password
            , string RoleID
            , bool Active
            , float BaseSalary
            , float BreakfastDeduction
            , float SavingDeduction
        )
        {
            try 
            {
                Employees_Tab employees = new Employees_Tab();

                employees.Name = Name;
                employees.LastNames = LastNames;
                employees.Email = Email;
                employees.Password = Password;
                employees.RoleID = (int?)int.Parse(RoleID);
                employees.Active = Active;
                employees.AdmissionDate = DateTime.Now;

                PayRollDB.Employees_Tab.Add(employees);
                PayRollDB.SaveChanges();

                string lastInserted = PayRollDB.Employees_Tab.OrderByDescending(bt => bt.EmployeeID).Take(1).Select(bt => bt.EmployeeID).FirstOrDefault().ToString();

                PayRollInfo_Tab payRollInfo = new PayRollInfo_Tab();
                payRollInfo.EmployeeID = int.Parse( lastInserted );
                payRollInfo.BaseSalary = decimal.Parse( BaseSalary.ToString() );
                payRollInfo.BreakfastDeduction = decimal.Parse(BreakfastDeduction.ToString());
                payRollInfo.SavingDeduction = decimal.Parse(SavingDeduction.ToString());
                payRollInfo.CreationDate = DateTime.Now;
                payRollInfo.Active = Active;

                PayRollDB.PayRollInfo_Tab.Add(payRollInfo);
                PayRollDB.SaveChanges();

            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        // PUT: api/Login
        public void Put
        (
             int EmployeeID
            , string Name
            , string LastNames
            , string Email
            , string Password
            , string RoleID
            , bool Active
            , float BaseSalary
            , float BreakfastDeduction
            , float SavingDeduction
        )
        {
            try
            {
                Employees_Tab employees = PayRollDB.Employees_Tab.Where(bt => bt.EmployeeID == EmployeeID ).FirstOrDefault();

                employees.Name = Name;
                employees.LastNames = LastNames;
                employees.Email = Email;
                employees.Password = Password;
                employees.RoleID = (int?)int.Parse(RoleID);
                employees.Active = Active;

                PayRollDB.SaveChanges();

                PayRollInfo_Tab payRollInfo = PayRollDB.PayRollInfo_Tab.Where(bt => bt.EmployeeID == EmployeeID).FirstOrDefault();
                payRollInfo.BaseSalary = decimal.Parse(BaseSalary.ToString());
                payRollInfo.BreakfastDeduction = decimal.Parse(BreakfastDeduction.ToString());
                payRollInfo.SavingDeduction = decimal.Parse(SavingDeduction.ToString());

                PayRollDB.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        // DELETE: api/Login/5
        public void Delete(int id)
        {
            try
            {
                PayRollInfo_Tab payRollInfo = PayRollDB.PayRollInfo_Tab.Where(bt => bt.EmployeeID == id).FirstOrDefault();
                PayRollDB.PayRollInfo_Tab.Remove(payRollInfo);
                PayRollDB.SaveChanges();

                Employees_Tab employees = PayRollDB.Employees_Tab.Where(bt => bt.EmployeeID == id).FirstOrDefault();
                PayRollDB.Employees_Tab.Remove(employees);
                PayRollDB.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
