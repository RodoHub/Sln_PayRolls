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
    public class RolesController: ApiController
    {
        /// <summary>
        /// Holds instance of PayRoll
        /// </summary>
        private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();

        /// <summary>
        /// Get Role
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public MRoles Get(int id)
        {
            return this.Get().Where(item => item.RoleID.ToString() == id.ToString()).ToList().FirstOrDefault();
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <returns></returns>
        public List<MRoles> Get()
        {
            return PayRollDB.Role_Cat.ToList().MapTo<Role_Cat, MRoles>();
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
