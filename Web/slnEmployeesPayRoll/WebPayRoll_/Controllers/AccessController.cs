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
    public class AccessController : ApiController
    {

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();

        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly Responser responser_ = new Responser();


        /// <summary>
        /// Logins a User 
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpPost]
        public Responser Login([FromBody] LoginEntity loginEntity_)
        {
            try
            {

                List<MEmployees> employees = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, MEmployees>();

                employees = employees.Where( item => item.Email == loginEntity_.UserName && item.Password == loginEntity_.Password && item.Active == true).ToList();

                if (employees.Count > 0)
                {
                    //Get logged user
                        MUser userLogged = new MUser();
                        
                        MEmployees employee = employees.FirstOrDefault();
                        userLogged.UserID = employee.EmployeeID;
                        userLogged.Name = string.Format("{0} {1}", employee.Name, employee.LastNames);
                        userLogged.UserName = employee.Email;
                        userLogged.Password = employee.Password;
                        userLogged.RoleName = employee.RoleName;

                        responser_.Status = 0;
                        responser_.StatusMessage = "Login Successfully";
                        responser_.Data = userLogged;

                    //Generate a Token
                        Guid tokenLogged = new Guid();
                        userLogged.Token = tokenLogged.ToString();

                        Employees_Tab empTab = PayRollDB.Employees_Tab.Where( item => item.EmployeeID == userLogged.UserID ).ToList().FirstOrDefault();
                        
                        if(empTab != null) 
                        { 
                            //Record Token in OAuth's Table
                                TokenAuth tokenRecords = new TokenAuth();
                                tokenRecords.Token = tokenLogged;
                                tokenRecords.CreationDate = DateTime.Now;
                                tokenRecords.TokenStatusID = PayRollDB.TokenStatus.Where( item => item.Name == "Active" ).ToList().FirstOrDefault().TokenStatusID;
                                PayRollDB.TokenAuth.Add(tokenRecords);
                                PayRollDB.SaveChanges();
                            
                            //Set token to user
                                empTab.Token = tokenLogged;
                                PayRollDB.SaveChanges();
                        }

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
        /// Logins a User 
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
                    List<MEmployees> employees = PayRollDB.Employees_Tab.ToList().MapTo<Employees_Tab, MEmployees>();

                    employees = employees.Where( item => item.EmployeeID == int.Parse(id) ).ToList();
                
                    if (employees.Count > 0)
                    {
                        //Get logged user
                            MUser userToLogOff = new MUser();

                            MEmployees employee = employees.FirstOrDefault();
                            userToLogOff.UserID = employee.EmployeeID;
                            userToLogOff.Token = employee.Token.ToString();

                        //Disable Token
                            Employees_Tab empTab = PayRollDB.Employees_Tab.Where( item => item.EmployeeID == userToLogOff.UserID ).ToList().FirstOrDefault();
                        
                            if(empTab != null) 
                            { 
                                //Remove token from user
                                    empTab.Token = null;
                                    PayRollDB.SaveChanges();

                                //Disable Token in OAuth's Table                                    
                                    List<TokenAuth> tokenRecords = PayRollDB.TokenAuth.Where( item => item.Token.ToString().Trim() == userToLogOff.Token.Trim()).ToList();

                                    if(tokenRecords.Count > 0) 
                                    { 
                                        foreach(TokenAuth item_ in tokenRecords) 
                                        {
                                            item_.TokenStatusID = PayRollDB.TokenStatus.Where(item => item.Name == "Inactive").ToList().FirstOrDefault().TokenStatusID;
                                        }
    
                                        PayRollDB.SaveChanges();                                        
                                    }
                            }

                            responser_.Status = 0;
                            responser_.StatusMessage = "LogOff Successfully";
                            responser_.Data = userToLogOff;
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
