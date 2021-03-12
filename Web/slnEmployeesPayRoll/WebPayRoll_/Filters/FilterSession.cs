using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPayRoll_.Controllers;

namespace WebPayRoll_.Filters
{
    public class FilterSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try 
            {
                if (HttpContext.Current.Session["UserLogged"] == null)
                {
                    if (filterContext.Controller is HomeController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Home/UserLogin");
                    }
                }
            }
            catch(Exception) 
            {
                filterContext.Result = new RedirectResult("~/Home/UserLogin");
            }
        }
    }
}