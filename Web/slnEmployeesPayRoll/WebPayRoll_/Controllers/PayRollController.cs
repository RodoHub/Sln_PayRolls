using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebPayRoll_.Models.DB;
using WebPayRoll_.Models.Classes;
using WebPayRoll_.Models.Extensions;

namespace WebPayRoll_.Controllers
{
    public class PayRollController : ApiController
    {
        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();

        /// <summary>
        /// Get PayRoll info
        /// </summary>
        /// <returns></returns>
        public MPayRoll Get(int id)
        {
            return this.Get().Where(item => item.PayRollInfoID.ToString() == id.ToString()).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Get PayRoll info
        /// </summary>
        /// <returns></returns>
        public List<MPayRoll> Get()
        {
            return PayRollDB.PayRoll_Tab.ToList().MapTo<PayRoll_Tab, MPayRoll>().ToList();
        }

        // POST: api/Login
        public void Post
        (
              DateTime InitialPeriod
            , DateTime EndPeriod
            , DateTime PayRollDate
            , int PayRollInfoID
        )
        {
            try 
            {
                PayRoll_Tab payRoll = new PayRoll_Tab();

                payRoll.InitialPeriod = InitialPeriod;
                payRoll.EndPeriod = EndPeriod;
                payRoll.PayRollDate = PayRollDate;
                payRoll.PayRollInfoID = PayRollInfoID;

                PayRollDB.PayRoll_Tab.Add(payRoll);
                PayRollDB.SaveChanges();
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
    }
}
