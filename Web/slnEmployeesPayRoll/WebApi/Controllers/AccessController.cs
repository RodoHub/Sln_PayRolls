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
    /// Controller for handeling Access on 'PayRolls'
    /// </summary>
    public class AccessController : ApiController
    {

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayRollDB_ PayRollDB = new PayRollDB_();

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private Responser responser_ = new Responser();

        //[HttpGet]
        //public string GettingInfo() 
        //{
        //    return "example";
        
        //}

        /// <summary>
        /// Logins a User 
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost]
        public Responser Login([FromBody] AM_LoginEntity loginEntity_)
        {
            try
            {
                List<AM_Employee> employees = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, AM_Employee>();

                employees = employees.Where( item => item.Email == loginEntity_.UserName && item.Password == loginEntity_.Password && item.Active == true).ToList();

                if (employees.Count > 0)
                {
                    //Get logged user
                        AM_User userLogged = new AM_User();
                        
                        AM_Employee employee = employees.FirstOrDefault();
                        userLogged.UserID = employee.EmployeeID;
                        userLogged.Name = string.Format("{0} {1}", employee.Name, employee.LastNames);
                        userLogged.UserName = employee.Email;
                        userLogged.Password = employee.Password;
                        userLogged.RoleName = PayRollDB.Role_Cat.Where(item => item.RoleID == (int)employee.RoleID).FirstOrDefault().Name;

                    //Generate a Token
                        AuthController authController = new AuthController();                        
                        userLogged.Token = authController.CreateTokenAuth(userLogged.UserID.ToString()).Data as string;

                    //Get response
                        responser_.Status = 0;
                        responser_.StatusMessage = "Login Successfully";
                        responser_.Data = userLogged;

                }
                else 
                {
                    responser_.Status = -2;
                    responser_.StatusMessage = "Login failed: User or password is incorrect.";
                    responser_.Data = null;
                }

                return responser_;

            }
            catch (Exception e)
            {

                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();
                responser_.Data = null;

                return responser_;

            }
        }


        /// <summary>
        /// Logs Off a User 
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost]
        public Responser LogOff(string id = "")
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(id)) 
                { 
                    List<AM_Employee> employees = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, AM_Employee>();

                    employees = employees.Where( item => item.EmployeeID == int.Parse(id) ).ToList();
                
                    if (employees.Count > 0)
                    {
                        //Get logged user
                            AM_Employee employee = employees.FirstOrDefault();

                        //Disable Token
                            AuthController authController = new AuthController();
                            responser_ = authController.DisableTokenAuth(employee.EmployeeID.ToString());
                    }
                    else 
                    {
                        responser_.Status = -2;
                        responser_.StatusMessage = "LogOff failed";
                        responser_.Data = null;
                    }
                }

                return responser_;

            }
            catch (Exception e)
            {

                responser_.Status = -1;
                responser_.StatusMessage = e.Message.ToString();
                responser_.Data = null;

                return responser_;

            }
        }


    }
}
