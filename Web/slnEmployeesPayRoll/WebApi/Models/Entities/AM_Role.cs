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
        /// <summary>
        /// Role ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Role Description
        /// </summary>
        public string Description { get; set; }

    }
}