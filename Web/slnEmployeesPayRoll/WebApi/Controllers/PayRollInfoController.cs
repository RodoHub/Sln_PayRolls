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
    public class PayRollInfoController: AuthController
    {
        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayRollDB_ PayRollDB = new PayRollDB_();

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
        public PayRollInfoController() 
        {

        }

        ///// <summary>
        ///// Get an Employee from PayRoll
        ///// </summary>
        ///// <param name="employee">Employee model</param>
        ///// <returns></returns>
        //[HttpPost]
        //public Responser Get([FromBody] AM_Employee employee)
        //{
        //    try 
        //    {
        //        responser_ = this.ValidateAuthorization(employee.Token.ToString());

        //        if (responser_.Data.Get<bool>()) 
        //        {
        //            responser_.Data = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, AM_Employee>().Where(item => item.EmployeeID.ToString() == employee.EmployeeID.ToString()).ToList().FirstOrDefault();
        //            responser_.Status = 0;
        //            responser_.StatusMessage = "Operation Get successfully";
        //        }

        //        return responser_;
        //    }
        //    catch (Exception e) 
        //    {
        //        responser_.Data = null ;
        //        responser_.Status = -1;
        //        responser_.StatusMessage = e.Message.ToString();

        //        return responser_;
        //    }
        //}

        ///// <summary>
        ///// Get Employees from PayRoll
        ///// </summary>
        ///// <param name="employee">Employee model</param>
        ///// <returns></returns>
        //[HttpPost]
        //public Responser GetAll([FromBody] AM_Employee employee)
        //{
        //    try 
        //    {
        //        responser_ = this.ValidateAuthorization(employee.Token.ToString());

        //        if (responser_.Data.Get<bool>()) 
        //        {
        //            responser_.Data = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, AM_Employee>().ToList();
        //            responser_.Status = 0;
        //            responser_.StatusMessage = "Operation Get successfully";
        //        }

        //        return responser_;
        //    }
        //    catch (Exception e) 
        //    {
        //        responser_.Data = null ;
        //        responser_.Status = -1;
        //        responser_.StatusMessage = e.Message.ToString();

        //        return responser_;
        //    }
        //}

        /// <summary>
        /// Adds a PayRollInfo for an employee within Employee PayRoll
        /// </summary>
        /// <param name="payRollInfo_">PayRollInfo to add to current Employee</param>
        /// <returns></returns>
        public Responser Post([FromBody] AM_PayRollInfo payRollInfo_)
        {
            try 
            {
                responser_ = this.ValidateAuthorization(payRollInfo_.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    PayRollInfo_Tab payRollInfo = new PayRollInfo_Tab();
                    payRollInfo.EmployeeID = payRollInfo_.EmployeeID;
                    payRollInfo.BaseSalary = payRollInfo_.BaseSalary;
                    payRollInfo.BreakfastDeduction = payRollInfo_.BreakfastDeduction;
                    payRollInfo.SavingDeduction = payRollInfo_.SavingDeduction;
                    payRollInfo.CreationDate = DateTime.Now;
                    payRollInfo.Active = payRollInfo.Active;

                    PayRollDB.PayRollInfo_Tab.Add(payRollInfo);
                    PayRollDB.SaveChanges();

                    string lastInserted = PayRollDB.PayRollInfo_Tab.OrderByDescending(bt => bt.PayRollInfoID).Take(1).Select(bt => bt.PayRollInfoID).FirstOrDefault().ToString();

                    responser_.Data = lastInserted;
                    responser_.Status = 0;
                    responser_.StatusMessage = "Operation Post for PayRollInfo successfully.";
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
        /// Updates a PayRollInfo for an employee within Employee PayRoll
        /// </summary>
        /// <param name="payRollInfo_">PayRollInfo to add to current Employee</param>
        /// <returns></returns>
        public Responser Put([FromBody] AM_PayRollInfo payRollInfo_)
        {
            try 
            {
                responser_ = this.ValidateAuthorization(payRollInfo_.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    PayRollInfo_Tab payRollInfo = PayRollDB.PayRollInfo_Tab.Where(bt => bt.EmployeeID == payRollInfo_.EmployeeID).FirstOrDefault();

                    if(payRollInfo != null) 
                    {
                        payRollInfo.BaseSalary = payRollInfo_.BaseSalary;
                        payRollInfo.BreakfastDeduction = payRollInfo_.BreakfastDeduction;
                        payRollInfo.SavingDeduction = payRollInfo_.SavingDeduction;
                        PayRollDB.SaveChanges();

                        responser_.StatusMessage = "Operation Put for PayRollInfo successfully.";
                        responser_.Data = payRollInfo_.PayRollInfoID;
                    }
                    else
                    {
                        responser_.StatusMessage = "PayRollInfo not found: Operation Put for PayRollInfo Failed.";
                        responser_.Data = payRollInfo_.PayRollInfoID;
                    }

                    responser_.Status = 0;
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
        /// Deletes a PayRollInfo for an employee within Employee PayRoll
        /// </summary>
        /// <param name="payRollInfo_">PayRollInfo to add to current Employee</param>
        /// <returns></returns>
        public Responser Delete([FromBody] AM_PayRollInfo payRollInfo_)
        {
            try
            {
                responser_ = this.ValidateAuthorization(payRollInfo_.Token.ToString());

                if (responser_.Data.Get<bool>()) 
                {
                    PayRollInfo_Tab payRollInfo = PayRollDB.PayRollInfo_Tab.Where(bt => bt.EmployeeID == payRollInfo_.EmployeeID).FirstOrDefault();

                    if(payRollInfo != null) 
                    {
                        PayRollDB.PayRollInfo_Tab.Remove(payRollInfo);
                        PayRollDB.SaveChanges();
                
                        responser_.StatusMessage = "Operation Delete for PayRollInfo successfully.";
                        responser_.Data = payRollInfo_.PayRollInfoID;
                    }
                    else
                    {
                        responser_.StatusMessage = "PayRollInfo not found: Operation Delete for PayRollInfo Failed.";
                        responser_.Data = payRollInfo_.PayRollInfoID;
                    }

                    responser_.Status = 0;

                }

                return responser_;
            }
            catch (Exception e)
            {
                responser_.Data = null;
                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();

                return responser_;
            }
        }
    }
}
