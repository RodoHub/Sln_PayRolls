using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Entities
{
    /// <summary>
    /// Api Model User: Holds information related to Users
    /// </summary>
    public class AM_User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}