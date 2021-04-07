using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPayRoll_.Models.DB;
using WebPayRoll_.Models.Classes;
using WebPayRoll_.Models.Extensions;
using WebPayRoll_.Models.Api;

namespace WebPayRoll_.Controllers
{
    public class HomeController : Controller
    {

        #region "------------------------------------------- Properties & Members -----------------------------------------"

            /// <summary>
            /// Holds the instance of Client Api PayRoll 'via Swagger'
            /// </summary>
             private readonly WebApiPayRoll clientSwagger_ApiPayRoll = new WebApiPayRoll();


            /// <summary>
            /// Holds instance of PayRoll
            /// </summary>
            private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();


            /// <summary>
            /// Holds an Auth Token
            /// </summary>
            private string Token 
            { 
                get 
                { 
                    return (Session["Token"] == null ? string.Empty : Session["Token"].ToString());
                }
            }

        #endregion

        #region "------------------------------------------- Initials -----------------------------------------"

            /// <summary>
            /// Action for: Index 
            /// </summary>
            /// <returns></returns>
            public ActionResult Index()
            {
                ViewBag.Title = "Home Page";

                return View();
            }

        #endregion


        #region "------------------------------------------- Login -----------------------------------------"

            /// <summary>
            /// Login a User within system
            /// </summary>
            /// <returns></returns>
            public ActionResult UserLogin(string user="", string pws = "", int SignOut = 0)
            {
                if(SignOut != 0) 
                {                                      
                    if(Session["UserIDLogged"]!= null) 
                    {
                        clientSwagger_ApiPayRoll.Access.LogOff(Session["UserIDLogged"].ToString());                
                    }

                    Session["UserLogged"] = null;
                    Session["RoleLogged"] = null;
                    Session["UserIDLogged"] = null;
                    
                    return View();
                }

                if(!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pws))
                {
                    MUser userToLogin = clientSwagger_ApiPayRoll.Access.Login(new Models.AMLoginEntity() { UserName = user, Password = pws }).Data.GetFromJSon<MUser>();

                    if (userToLogin == null) 
                    {
                        return Json("Failed", JsonRequestBehavior.AllowGet);
                    }

                    Session["UserLogged"] = userToLogin.Name;
                    Session["RoleLogged"] = userToLogin.RoleName;
                    Session["UserIDLogged"] = userToLogin.UserID;
                    Session["Token"] = userToLogin.Token;

                    return Json(Url.Content("~/Home/" + (userToLogin.RoleName == "Admin" ? "DashboardAdmin" : "DashboardAdmin")), JsonRequestBehavior.AllowGet);

                }
                else 
                {
                    return View();
                }
            }


        #endregion

        #region "------------------------------------------- Dashboard Admin -----------------------------------------"

            /// <summary>
            /// Admin Dashboard
            /// </summary>
            /// <returns></returns>
            public ActionResult DashboardAdmin() 
            {
                return View();
            }

            /// <summary>
            /// Get employees
            /// </summary>
            /// <returns></returns>
            public JsonResult GetEmployeeNames(int id, int active) 
            {           
                List<MEmployees> listEmployees = clientSwagger_ApiPayRoll.Employees.GetAll(new Models.AMEmployee { Token = Guid.Parse( this.Token.ToUpper() ) }).Data.GetAllFromJSon<MEmployees>().ToList();
                
                listEmployees = (active == -1 ? listEmployees : listEmployees.Where(item => item.Active == (active == 1 ? true : false)).ToList());

                if (id != 0) 
                { 
                    listEmployees = listEmployees.Where(item => item.EmployeeID == id).ToList();
                }
               
                List<MCatalogs> catalog = new List<MCatalogs>();
            
                listEmployees.ForEach( item => catalog.Add( new MCatalogs() { ID = item.EmployeeID, Name = string.Format("{0} {1}", item.Name, item.LastNames) }) );

                return Json(catalog, JsonRequestBehavior.AllowGet);
            }

            /// <summary>
            /// Check if email exists
            /// </summary>
            /// <returns></returns>
            public JsonResult EmailExists(string email = "") 
            {
                int emailCount = 0;

                if (email == "")
                { 
                    return Json((emailCount), JsonRequestBehavior.AllowGet);
                }


                emailCount = clientSwagger_ApiPayRoll.Employees.GetAll(new Models.AMEmployee() { Token = Guid.Parse(this.Token.ToUpper()) }).Data.GetAllFromJSon<Models.AMEmployee>().Where(item => item.Email.Trim() == email.Trim()).ToList().Count();
                
                return Json((emailCount), JsonRequestBehavior.AllowGet);
            }

            /// <summary>
            /// Employees management
            /// </summary>
            /// <returns></returns>
            public ActionResult EmployeesManagement(int active = -1) 
            {           
                ViewBag.ActiveFlag = (active == 1 ? true : false);

                return View
                       (
                            (
                                active == -1 ? 
                                clientSwagger_ApiPayRoll.Employees.GetAll(new Models.AMEmployee { Token = Guid.Parse( this.Token.ToUpper() ) }).Data.GetAllFromJSon<MEmployees>().ToList() : 
                                clientSwagger_ApiPayRoll.Employees.GetAll(new Models.AMEmployee { Token = Guid.Parse( this.Token.ToUpper() ) }).Data.GetAllFromJSon<MEmployees>().ToList().Where(item => item.Active == (active == 1 ? true : false) )
                            )
                       );
            }

            /// <summary>
            /// Check PayRoll Tickets
            /// </summary>
            /// <param name="generate">Flag to generate</param>
            /// <param name="year">Year</param>
            /// <param name="month">Month</param>
            /// <returns></returns>
            public ActionResult CheckPayRollTickets(int employeeID = 0) 
            {
                List<MPayRoll> listPayRolls = null;

                ViewBag.EmployeeSelected = employeeID;

                listPayRolls = new PayRollController().Get().Where(item => ((bool)item.GetEmployee(Guid.Parse(this.Token)).Active) == true).OrderByDescending(item => item.PayRollDate).ToList();
                
                if(employeeID != 0) 
                { 
                    listPayRolls = new PayRollController().Get().Where(item => item.GetEmployee(Guid.Parse(this.Token)).EmployeeID == employeeID).OrderByDescending(item => item.PayRollDate).ToList();
                }

                return View(listPayRolls);
            }

        /// <summary>
        /// Generate Monthly PayRoll
        /// </summary>
        /// <param name="generate">Flag to generate</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        public ActionResult GenerateMonthlyPayRoll(bool generate = false, int year = 0 , int month = 0, int action = -1) 
        {
                List<MEmployees> listEmployees = null;

                List<MPayRoll> listPayRolls = null;

                ViewBag.Year = year;
                ViewBag.Month = month;

                if(generate)
                {
                    //Get days by month
                        DateTime dateFirst= new DateTime(year, month, 1);
                        DateTime dateLast = new DateTime(year, month + 1, 1).AddDays(-1);

                    //Get employee
                        listEmployees = clientSwagger_ApiPayRoll.Employees.GetAll(new Models.AMEmployee { Token = Guid.Parse( this.Token.ToUpper() ) }).Data.GetAllFromJSon<MEmployees>().ToList().Where(item => item.Active == true).ToList();

                    //Generates Payroll
                        string msgResult = string.Empty;
                        PayRollController payRollController = new PayRollController();
                        foreach(MEmployees employee in listEmployees) 
                        {                           
                                if (
                                    payRollController.Get().Where
                                    (
                                        item => item.PayRollInfoID == employee.GetPayRollInfo(Guid.Parse(this.Token)).PayRollInfoID &&
                                                ((DateTime)item.PayRollDate).Year == year &&
                                                ((DateTime)item.PayRollDate).Month == month
                                    ).Count() == 0) 
                                { 
                                    //Payroll
                                        payRollController.Post(dateFirst, dateLast, dateLast, employee.GetPayRollInfo(Guid.Parse(this.Token)).PayRollInfoID);
                                        msgResult += string.Format("- <span style='color: rgba(0, 0, 0, 0.55); font-weight: 600;'>Employee [ {0} {1} ] - PayRoll generated successfully</span> <br>", employee.Name, employee.LastNames);                    
                                }
                                else
                                {
                                    msgResult += string.Format("- <span style='color: rgba(0, 0, 0, 0.55); font-weight: 600;'>Employee [ {0} {1} ] - PayRoll was already generated</span> <br>", employee.Name, employee.LastNames);
                                }
                        }
          
                        return Json(msgResult, JsonRequestBehavior.AllowGet);
                }
                else
                {           

                    listPayRolls = new PayRollController().Get().Where(item => ((DateTime)item.PayRollDate).Year == year && ((DateTime)item.PayRollDate).Month == month && ((bool)item.GetEmployee(Guid.Parse(this.Token)).Active) == true).ToList();
                
                    return View(listPayRolls);
                }
            }

            /// <summary>
            /// Admin Dashboard
            /// </summary>
            /// <returns></returns>
            public JsonResult Roles() 
            {
                RolesController roles = new RolesController();
             
                return Json(roles.Get(), JsonRequestBehavior.AllowGet);
            }

            /// <summary>
            /// Get Employees
            /// </summary>
            /// <returns></returns>
            public JsonResult Employees(string id = "") 
            {
                
                MEmployeesEnt employee = clientSwagger_ApiPayRoll.Employees.Get(new Models.AMEmployee { EmployeeID = int.Parse(id), Token = Guid.Parse(this.Token.ToUpper()) }).Data.GetFromJSon<MEmployeesEnt>();

                return Json(employee, JsonRequestBehavior.AllowGet);
            }

            /// <summary>
            /// Create, Update, Delete Employees
            /// </summary>
            /// <returns></returns>
            public JsonResult EmployeesCU
            (
                 string EmployeeID
                , string Name
                , string LastNames
                , string Email
                , string Password
                , string RoleID
                , bool Active
                , float BaseSalary
                , float BreakfastDeduction
                , float SavingDeduction
                , string action = ""
            )
            {
                try
                {
                    Models.Responser responser = new Models.Responser();

                    switch (action) 
                    {                     
                        case "POST":
                            responser = clientSwagger_ApiPayRoll.Employees.Post(new Models.AMEmployee() { Token = Guid.Parse(this.Token.ToUpper()), Name = Name, LastNames = LastNames, Email = Email, Password = Password, RoleID = int.Parse(RoleID), Active = Active });
                            responser = clientSwagger_ApiPayRoll.PayRollInfo.Post(new Models.AMPayRollInfo() { Token = Guid.Parse(this.Token.ToUpper()), BaseSalary = BaseSalary, BreakfastDeduction = BreakfastDeduction, SavingDeduction = SavingDeduction, Active= Active, EmployeeID = int.Parse(responser.Data.ToString()) });
                            return Json("Employee created successfully", JsonRequestBehavior.AllowGet);

                        case "PUT":
                            responser = clientSwagger_ApiPayRoll.Employees.Put(new Models.AMEmployee() { Token = Guid.Parse(this.Token.ToUpper()), EmployeeID = int.Parse(EmployeeID), Name = Name, LastNames = LastNames, Email = Email, Password = Password, RoleID = int.Parse(RoleID), Active = Active });
                            responser = clientSwagger_ApiPayRoll.PayRollInfo.Put(new Models.AMPayRollInfo() { Token = Guid.Parse(this.Token.ToUpper()), EmployeeID = int.Parse(EmployeeID), BaseSalary = BaseSalary, BreakfastDeduction = BreakfastDeduction, SavingDeduction = SavingDeduction, Active = Active });

                            return Json("Employee updated successfully", JsonRequestBehavior.AllowGet);
                    }
           
                    return Json("null", JsonRequestBehavior.AllowGet);                
                
                }
                catch(Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }


            /// <summary>
            /// Create, Update, Delete Employees
            /// </summary>
            /// <returns></returns>
            public JsonResult EmployeesD
            (
                 string EmployeeID
            )
            {
                clientSwagger_ApiPayRoll.Employees.Delete(new Models.AMEmployee() { Token = Guid.Parse(this.Token.ToUpper()), EmployeeID = int.Parse(EmployeeID)});

                return Json("Employee deleted successfully", JsonRequestBehavior.AllowGet);
            }


            /// <summary>
            /// Get Employees's PayRollInfo
            /// </summary>
            /// <returns></returns>
            public JsonResult PayRollInfo(string id = "") 
            {           
                return Json(clientSwagger_ApiPayRoll.PayRollInfo.Get(new Models.AMPayRollInfo() { Token = Guid.Parse(this.Token), EmployeeID = int.Parse( id ) }).Data.GetFromJSon<Models.AMPayRollInfo>(), JsonRequestBehavior.AllowGet);
            }

        #endregion


        #region "------------------------------------------- Dashboard Employees -----------------------------------------"


        #endregion


    }
}
