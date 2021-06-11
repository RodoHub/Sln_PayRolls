using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_PayRoll.Models.Entities
{
    /// <summary>
    /// Api Model PayRoll: Holds information related to PayRolls
    /// </summary>
    public class AM_PayRoll
    {
        public int PayRollID { get; set; }
        public Nullable<System.DateTime> InitialPeriod { get; set; }
        public Nullable<System.DateTime> EndPeriod { get; set; }
        public Nullable<System.DateTime> PayRollDate { get; set; }
        public Nullable<int> PayRollInfoID { get; set; }           
    }
}