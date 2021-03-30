using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Entities
{
    /// <summary>
    /// Api Model User: Holds information related to User's Roles
    /// </summary>
    public class AM_Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}