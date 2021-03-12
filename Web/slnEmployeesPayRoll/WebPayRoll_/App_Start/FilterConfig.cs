using System.Web;
using System.Web.Mvc;
using WebPayRoll_.Filters;

namespace WebPayRoll_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new FilterSession());
        }
    }
}
