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
    public class PayRollInfoController : ApiController
    {
        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();

        /// <summary>
        /// Get PayRoll info
        /// </summary>
        /// <returns></returns>
        public MPayRollInfo Get(int id)
        {
            return this.Get().Where(item => item.EmployeeID.ToString() == id.ToString()).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Get PayRoll info by PayRoll Info ID
        /// </summary>
        /// <returns></returns>
        public MPayRollInfo GetByInfoID(int id)
        {
            return this.Get().Where(item => item.PayRollInfoID.ToString() == id.ToString()).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Get PayRoll info
        /// </summary>
        /// <returns></returns>
        public List<MPayRollInfo> Get()
        {
            return PayRollDB.PayRollInfo_Tab.ToList().MapTo<PayRollInfo_Tab, MPayRollInfo>().ToList();
        }

        // POST: api/Login
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
