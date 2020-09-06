using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Filters;

namespace WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // my filters
            filters.Add(new AuthorizationFilter());
        }
    }
}
