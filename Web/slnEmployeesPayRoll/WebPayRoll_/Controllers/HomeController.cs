using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPayRoll_.Models.DB;
using WebPayRoll_.Models.Classes;
using WebPayRoll_.Models.Extensions;

namespace WebPayRoll_.Controllers
{
    public class HomeController : Controller
    {

        #region "------------------------------------------- Properties & Members -----------------------------------------"

            /// <summary>
            /// Holds instance of PayRoll
            /// </summary>
            private readonly PayrollEmployeesEntities PayRollDB = new PayrollEmployeesEntities();

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
                    Session["UserLogged"] = null;
                    Session["RoleLogged"] = null;
                    Session["UserIDLogged"] = null;
                    
                    return View();
                }

                if(!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pws))
                {
                    MEmployees employee = new EmployeesController().Get().Where(u => u.Email == user && u.Password == pws).FirstOrDefault();

                    if (employee == null) 
                    {
                        return Json("Failed", JsonRequestBehavior.AllowGet);
                    }

                    if(employee.Active == false)
                    {
                        return Json("Inactive", JsonRequestBehavior.AllowGet);
                    }

                    string roleName = new RolesController().Get(int.Parse(employee.RoleID.ToString())).Name;
                    string userName = string.Format("{0} {1}", employee.Name, employee.LastNames);

                    Session["UserLogged"] = userName;
                    Session["RoleLogged"] = roleName;
                    Session["UserIDLogged"] = employee.EmployeeID;

                    return Json(Url.Content("~/Home/" + (roleName == "Admin" ? "DashboardAdmin" : "DashboardAdmin")), JsonRequestBehavior.AllowGet);
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
                EmployeesController employees = new EmployeesController();
            
                List<MEmployees> listEmployees = (active == -1 ? employees.Get() : employees.Get().Where(item => item.Active == (active == 1 ? true : false)).ToList());

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
                EmployeesController employees = new EmployeesController();

                int emailCount = 0;

                if (email == "")
                { 
                    return Json((emailCount), JsonRequestBehavior.AllowGet);
                }

                emailCount = employees.Get().Where(item => item.Email.Trim() == email.Trim()).ToList().Count();
                
                return Json((emailCount), JsonRequestBehavior.AllowGet);
            }

            /// <summary>
            /// Employees management
            /// </summary>
            /// <returns></returns>
            public ActionResult EmployeesManagement(int active = -1) 
            {
                EmployeesController employees = new EmployeesController();
            
                ViewBag.ActiveFlag = (active == 1 ? true : false);

                return View((active == -1 ? employees.Get() : employees.Get().Where(item => item.Active == (active == 1 ? true : false) )));
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
                EmployeesController employees = new EmployeesController();

                List<MPayRoll> listPayRolls = null;

                ViewBag.EmployeeSelected = employeeID;

                listPayRolls = new PayRollController().Get().Where(item => ((bool)item.Employee.Active) == true).OrderByDescending(item => item.PayRollDate).ToList();
                
                if(employeeID != 0) 
                { 
                    listPayRolls = new PayRollController().Get().Where(item => item.Employee.EmployeeID == employeeID).OrderByDescending(item => item.PayRollDate).ToList();
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
                EmployeesController employees = new EmployeesController();

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
                        employees = new EmployeesController();
                        listEmployees = employees.Get().Where(item => item.Active == true).ToList();

                    //Generates Payroll
                        string msgResult = string.Empty;
                        PayRollController payRollController = new PayRollController();
                        foreach(MEmployees employee in listEmployees) 
                        {                           
                                if (
                                    payRollController.Get().Where
                                    (
                                        item => item.PayRollInfoID == employee.PayRollInfo_.PayRollInfoID &&
                                                ((DateTime)item.PayRollDate).Year == year &&
                                                ((DateTime)item.PayRollDate).Month == month
                                    ).Count() == 0) 
                                { 
                                    //Payroll
                                        payRollController.Post(dateFirst, dateLast, dateLast, employee.PayRollInfo_.PayRollInfoID);
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

                    listPayRolls = new PayRollController().Get().Where(item => ((DateTime)item.PayRollDate).Year == year && ((DateTime)item.PayRollDate).Month == month && ((bool)item.Employee.Active) == true).ToList();
                
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
                EmployeesController employees = new EmployeesController();
             
                MEmployeesEnt employee = employees.Get(int.Parse(id)).MapTo<MEmployees, MEmployeesEnt>();

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

                EmployeesController employees = new EmployeesController();

                switch (action) 
                { 
                    case "POST":
                        employees.Post(Name, LastNames, Email, Password, RoleID, Active, BaseSalary, BreakfastDeduction, SavingDeduction);
                        return Json("Employee created successfully", JsonRequestBehavior.AllowGet);

                    case "PUT":
                        employees.Put(int.Parse(EmployeeID), Name, LastNames, Email, Password, RoleID, Active, BaseSalary, BreakfastDeduction, SavingDeduction);

                        return Json("Employee updated successfully", JsonRequestBehavior.AllowGet);
                }
           
                return Json("null", JsonRequestBehavior.AllowGet);                
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

               EmployeesController employees = new EmployeesController();

                employees.Delete(int.Parse(EmployeeID));

                return Json("Employee deleted successfully", JsonRequestBehavior.AllowGet);
            }


            /// <summary>
            /// Get Employees
            /// </summary>
            /// <returns></returns>
            public JsonResult PayRollInfo(string id = "") 
            {
                PayRollInfoController payRollInfo = new PayRollInfoController();
             
                return Json(payRollInfo.Get(int.Parse(id)), JsonRequestBehavior.AllowGet);
            }

        #endregion


        #region "------------------------------------------- Dashboard Employees -----------------------------------------"


        #endregion


    }
}
