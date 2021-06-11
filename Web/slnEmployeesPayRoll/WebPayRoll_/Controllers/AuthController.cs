using System;
using System.Linq;
using System.Web.Http;
using WebPayRoll_.Models.Api;
using WebPayRoll_.Models.Classes;
using WebPayRoll_.Models.DB;

namespace WebPayRoll_.Controllers
{
    public class AuthController : ApiController
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
