using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using WebApi_PayRoll.Models.DB;
using WebApi_PayRoll.Models.Api;
using WebApi_PayRoll.Models.Entities;
using WebApi_PayRoll.Models.Extensions;

namespace WebApi_PayRoll.Controllers
{
    /// <summary>
    /// Controller for handeling Authorizations on 'PayRolls'
    /// </summary>
    public class AuthController : ApiController
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
        /// Generate Token Auth
        /// </summary>
        /// <param name="token">ID</param>
        /// <returns></returns>
        [HttpPost]
        public Responser CreateTokenAuth(string userId = "")
        {
            try
            {
                bool isValid = false;

                if (!string.IsNullOrWhiteSpace(userId)) 
                { 
                    //Generate a Token
                        AM_User userLogged = new AM_User();
                        Guid tokenLogged = Guid.NewGuid();
                        userLogged.Token = tokenLogged.ToString();
                        userLogged.UserID = int.Parse( userId );

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

                            isValid = true;
                            responser_.Data = tokenLogged.ToString();
                        }                
                        else
                        {
                            isValid = false;
                            responser_.Data = string.Empty;                    
                        }
                }
                else
                {
                    isValid = false;
                    responser_.Data = string.Empty;
                }

                responser_.Status = (isValid ? 1 : 0);
                responser_.StatusMessage = (isValid ? "Token Auth generated successfully" : "Error at generating Token Auth");
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
        /// Generate Token Auth
        /// </summary>
        /// <param name="token">ID</param>
        /// <returns></returns>
        [HttpPost]
        public Responser DisableTokenAuth(string userId = "")
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userId)) 
                { 
                    //Remove token from user
                        AM_User userToLogOff = new AM_User();
                        userToLogOff.UserID = int.Parse( userId );
                        Employees_Tab empTab = PayRollDB.Employees_Tab.Where( item => item.EmployeeID == userToLogOff.UserID ).ToList().FirstOrDefault();
                        userToLogOff.Token = empTab.Token.ToString();
                        empTab.Token = null;
                        
                        PayRollDB.SaveChanges();
                    
                    //Disable Token in OAuth's Table                                    
                        List<TokenAuth> tokenRecords = PayRollDB.TokenAuth.Where( item => item.Token.ToString().Trim() == userToLogOff.Token.Trim()).ToList();
                    
                        if(tokenRecords.Count() > 0) 
                        { 
                            foreach(TokenAuth item_ in tokenRecords) 
                            {
                                item_.TokenStatusID = PayRollDB.TokenStatus.Where(item => item.Name == "Inactive").ToList().FirstOrDefault().TokenStatusID;
                            }
                    
                            PayRollDB.SaveChanges();                                        
                        }

                    responser_.Status = 0;
                    responser_.StatusMessage = "Token Auth disabled successfully";
                }
                else 
                { 
                    responser_.Status = 0;
                    responser_.StatusMessage = "There was not any Token Auth to disable";               
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
        /// Validates Login a User authorization
        /// </summary>
        /// <param name="token">ID</param>
        /// <returns></returns>
        [HttpPost]
        public Responser ValidateAuthorization(string token = "")
        {
            try
            {

                //Validates Token
                    bool isValid = true;

                    if (string.IsNullOrWhiteSpace(token))
                    {
                        isValid = false;
                    }
                    else
                    { 
                        if (PayRollDB.TokenAuth.Where(item => item.Token.ToString() == token).Count() > 0)
                        {
                            isValid = ( PayRollDB.TokenAuth.Where(item => item.Token.ToString() == token).FirstOrDefault().TokenStatusID == 1 ? true : false );
                        }   
                        else
                        {
                            isValid = false;
                        }
                    }

                    responser_.Status = (isValid ? 0 : -2);
                    responser_.StatusMessage = (isValid ? "Authorization OK" : "Current user is unauthorized for this service.");
                    responser_.Data = isValid;

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
