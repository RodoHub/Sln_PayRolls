using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPayRoll_.Controllers;
namespace WebPayRoll_.Models.Classes
{
    public class MEmployees
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string LastNames { get; set; }
        public Nullable<System.DateTime> AdmissionDate { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Active { get; set; }

        public string RoleName 
        {
            get 
            {
                return new RolesController().Get().Where(r => r.RoleID.ToString().Trim() == this.RoleID.ToString()).Select(r => r.Name).FirstOrDefault();
            }
        }

        public MPayRollInfo PayRollInfo_
        {
            get 
            {
                return new PayRollInfoController().Get(this.EmployeeID);
            }
        }

        private MPayRoll payRoll_;
        public MPayRoll PayRoll_
        {
            get
            {
                return new PayRollController().Get(this.PayRollInfo_.PayRollInfoID);
            }
        }

    }
}