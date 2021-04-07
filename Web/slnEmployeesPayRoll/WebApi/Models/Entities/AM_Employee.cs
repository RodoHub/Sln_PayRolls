using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Entities
{
    /// <summary>
    /// Api Model Employee: Holds information related to Employees
    /// </summary>
    public class AM_Employee
    {
        /// <summary>
        /// Employee ID
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee Last Names
        /// </summary>
        public string LastNames { get; set; }

        /// <summary>
        /// Employee Admission Date 
        /// </summary>
        public Nullable<System.DateTime> AdmissionDate { get; set; }

        /// <summary>
        /// Employee Role ID
        /// </summary>
        public Nullable<int> RoleID { get; set; }

        /// <summary>
        /// Employee Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Employee Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Employee Status
        /// </summary>
        public Nullable<bool> Active { get; set; }


        /// <summary>
        /// Employee Role Name
        /// </summary>
        private string roleName;

        /// <summary>
        /// Employee Role Name
        /// </summary>
        public string RoleName 
        {
            get
            {
                return (roleName == null ? string.Empty : roleName);
            }
            set
            {
                roleName = value;
            }
        }

        /// <summary>
        /// Token authorization
        /// </summary>
        private Nullable<System.Guid> token;

        /// <summary>
        /// Token authorization
        /// </summary>
        public Nullable<System.Guid> Token 
        {
            get 
            { 
                return (token == null ? new Guid() :  token);
            }
            set
            {
                token = value;
            }
        }
    }
}