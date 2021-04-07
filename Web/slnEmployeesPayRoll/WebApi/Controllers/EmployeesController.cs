using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi_PayRoll.Models.DB;
using WebApi_PayRoll.Models.Api;
using WebApi_PayRoll.Models.Entities;
using WebApi_PayRoll.Models.Extensions;

namespace WebApi_PayRoll.Controllers
{
    /// <summary>
    /// Controller for handeling Employees on 'PayRolls'
    /// </summary>
    public class EmployeesController: AuthController
    {
        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private PayRollDB_ PayRollDB;

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private Responser responser_ = new Responser();

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private bool IsAuthorized { get; set; }


        /// <summary>
        /// Initialize Api Controller
        /// </summary>
        public EmployeesController() 
        {

        }

        /// <summary>
        /// Get an Employee from PayRoll
        /// </summary>
        /// <param name="employee">Employee model</param>
        /// <returns></returns>
        [HttpPost]
        public Responser Get([FromBody] AM_Employee employee)
        {
            try 
            {
                PayRollDB = new PayRollDB_();

                responser_ = this.ValidateAuthorization(employee.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    responser_.Data = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, AM_Employee>().Where(item => item.EmployeeID.ToString() == employee.EmployeeID.ToString()).ToList().FirstOrDefault();
                    responser_.Status = 0;
                    responser_.StatusMessage = "Operation Get successfully";
                }

                return responser_;
            }
            catch (Exception e) 
            {
                responser_.Data = null ;
                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();

                return responser_;
            }
        }

        /// <summary>
        /// Get Employees from PayRoll
        /// </summary>
        /// <param name="employee">Employee model</param>
        /// <returns></returns>
        [HttpPost]
        public Responser GetAll([FromBody] AM_Employee employee)
        {
            try 
            {
                PayRollDB = new PayRollDB_();

                responser_ = this.ValidateAuthorization(employee.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    responser_.Data = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, AM_Employee>().ToList();
                    responser_.Status = 0;
                    responser_.StatusMessage = "Operation Get successfully";
                }

                return responser_;
            }
            catch (Exception e) 
            {
                responser_.Data = null ;
                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();

                return responser_;
            }
        }

        /// <summary>
        /// Adds an employee within Employee PayRoll
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns></returns>
        public Responser Post([FromBody] AM_Employee employee)
        {
            try 
            {
                PayRollDB = new PayRollDB_();

                responser_ = this.ValidateAuthorization(employee.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    Employees_Tab employees = new Employees_Tab();

                    employees.Name = employee.Name;
                    employees.LastNames = employee.LastNames;
                    employees.Email = employee.Email;
                    employees.Password = employee.Password;
                    employees.RoleID = employee.RoleID;
                    employees.Active = employee.Active;
                    employees.AdmissionDate = DateTime.Now;

                    PayRollDB.Employees_Tab.Add(employees);
                    PayRollDB.SaveChanges();

                    string lastInserted = PayRollDB.Employees_Tab.OrderByDescending(bt => bt.EmployeeID).Take(1).Select(bt => bt.EmployeeID).FirstOrDefault().ToString();

                    responser_.Data = lastInserted;
                    responser_.Status = 0;
                    responser_.StatusMessage = "Operation Post for Employee successfully.";
                }

                return responser_;
            }
            catch (Exception e) 
            {
                responser_.Data = null ;
                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();

                return responser_;
            }
        }

        /// <summary>
        /// Updates an employee within Employee PayRoll
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns></returns>
        public Responser Put([FromBody] AM_Employee employee)
        {
            try 
            {
                PayRollDB = new PayRollDB_();

                responser_ = this.ValidateAuthorization(employee.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    Employees_Tab employees = PayRollDB.Employees_Tab.Where(bt => bt.EmployeeID == employee.EmployeeID ).FirstOrDefault();

                    if(employees != null) 
                    { 
                        employees.Name = employee.Name;
                        employees.LastNames = employee.LastNames;
                        employees.Email = employee.Email;
                        employees.Password = employee.Password;
                        employees.RoleID = employee.RoleID;
                        employees.Active = employee.Active;
                        employees.AdmissionDate = DateTime.Now;

                        PayRollDB.Employees_Tab.Add(employees);
                        PayRollDB.SaveChanges();

                        string lastInserted = PayRollDB.Employees_Tab.OrderByDescending(bt => bt.EmployeeID).Take(1).Select(bt => bt.EmployeeID).FirstOrDefault().ToString();

                        responser_.Data = lastInserted;
                        responser_.StatusMessage = "Operation Put for Employee successfully.";
                    }
                }
                else 
                {
                    responser_.Data = employee.EmployeeID;
                    responser_.StatusMessage = "Employee ID not found: Operation Put for Employee successfully.";
                }

                responser_.Status = 0;

                return responser_;
            }
            catch (Exception e) 
            {
                responser_.Data = null ;
                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();

                return responser_;
            }
        }

        /// <summary>
        /// Deletes an employee within Employee PayRoll
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns></returns>
        public Responser Delete([FromBody] AM_Employee employee)
        {
            try 
            {
                PayRollDB = new PayRollDB_();

                responser_ = this.ValidateAuthorization(employee.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    Employees_Tab employees = PayRollDB.Employees_Tab.Where(bt => bt.EmployeeID == employee.EmployeeID ).FirstOrDefault();

                    if(employees != null) 
                    {
                        PayRollDB.Employees_Tab.Remove(employees);
                        PayRollDB.SaveChanges();

                        responser_.Data = employee.EmployeeID;
                        responser_.StatusMessage = "Operation Delete for Employee successfully.";
                    }
                }
                else 
                {
                    responser_.Data = employee.EmployeeID;
                    responser_.StatusMessage = "Employee ID not found: Operation Delete for Employee successfully.";
                }

                responser_.Status = 0;

                return responser_;
            }
            catch (Exception e) 
            {
                responser_.Data = null ;
                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();

                return responser_;
            }


        }
    }
}
