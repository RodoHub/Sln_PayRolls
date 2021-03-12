using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPayRoll_.Controllers;
namespace WebPayRoll_.Models.Classes
{
    public class MPayRoll
    {
        public int PayRollID { get; set; }
        public Nullable<System.DateTime> InitialPeriod { get; set; }
        public Nullable<System.DateTime> EndPeriod { get; set; }
        public Nullable<System.DateTime> PayRollDate { get; set; }
        public Nullable<int> PayRollInfoID { get; set; }   
        
        public MPayRollInfo PayRollInfo_ 
        {
            get { return new PayRollInfoController().GetByInfoID((int)this.PayRollInfoID); }
        }
        public MEmployees Employee 
        {
            get { return new EmployeesController().Get( (int)new PayRollInfoController().GetByInfoID((int)this.PayRollInfoID).EmployeeID ); }
        }

    }
}