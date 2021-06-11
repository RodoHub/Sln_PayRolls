using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPayRoll_.Controllers;
using WebPayRoll_.Models.Api;

namespace WebPayRoll_.Models.Classes
{
    public class MPayRoll : MBase
    {
        /// <summary>
        /// Main Contructor
        /// </summary>
        public MPayRoll() 
        { 
        
        }

        /// <summary>
        /// Main Contructor
        /// </summary>
        public MPayRoll(Nullable<System.Guid> token)
        {
            this.Token = token;
        }

        public int PayRollID { get; set; }
        public Nullable<System.DateTime> InitialPeriod { get; set; }
        public Nullable<System.DateTime> EndPeriod { get; set; }
        public Nullable<System.DateTime> PayRollDate { get; set; }
        public Nullable<int> PayRollInfoID { get; set; }   
        
        /// <summary>
        /// Get PayRollInfo by ID
        /// </summary>
        /// <returns></returns>
        public MPayRollInfo GetPayRollInfoByID(System.Nullable<System.Guid> token)
        {
            WebApiPayRoll clientSwagger_ApiPayRoll = new WebApiPayRoll();

            return clientSwagger_ApiPayRoll.PayRollInfo.Get(new AMPayRollInfo() { Token = token, PayRollInfoID = (int?)this.PayRollInfoID }).Data.GetFromJSon<MPayRollInfo>();
        }


        /// <summary>
        /// Get Employee corresponding to current PayRoll
        /// </summary>
        /// <returns></returns>
        public MEmployees GetEmployee(System.Nullable<System.Guid> token) 
        {
            WebApiPayRoll clientSwagger_ApiPayRoll = new WebApiPayRoll();

            return clientSwagger_ApiPayRoll.Employees.Get(new AMEmployee() { Token = token, EmployeeID = this.GetPayRollInfo(token).EmployeeID }).Data.GetFromJSon<MEmployees>();
        }

        /// <summary>
        /// Get PayRoll Info
        /// </summary>
        /// <returns></returns>
        public MPayRollInfo GetPayRollInfo(System.Nullable<System.Guid> token) 
        {
            WebApiPayRoll clientSwagger_ApiPayRoll = new WebApiPayRoll();

            return clientSwagger_ApiPayRoll.PayRollInfo.Get(new AMPayRollInfo() { Token = token, EmployeeID = this.GetPayRollInfoByID(token).EmployeeID }).Data.GetFromJSon<MPayRollInfo>();
        }




    }
}