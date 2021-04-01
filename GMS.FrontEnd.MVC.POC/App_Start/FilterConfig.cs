using System.Web;
using System.Web.Mvc;

namespace GMS.FrontEnd.MVC.POC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
