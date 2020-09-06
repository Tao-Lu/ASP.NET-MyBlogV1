using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace WebUI.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // sync data from cookie to session
            // do not need to check data is in cookie or session
            // get data from session all the time

            //if (filterContext.HttpContext.Request.Cookies["userId"] != null && filterContext.HttpContext.Session["userId"] == null)
            //{
            //    filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
            //    filterContext.HttpContext.Session["userRoles"] = filterContext.HttpContext.Request.Cookies["userRoles"].Value;
            //}

            if (filterContext.HttpContext.Session["userId"] == null)
            {
                if (filterContext.HttpContext.Request.Cookies["userId"] != null)
                {
                    filterContext.HttpContext.Session["userId"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
                    filterContext.HttpContext.Session["userRoles"] = filterContext.HttpContext.Request.Cookies["userRoles"].Value;
                }
                else
                {
                    IIdentity identity = filterContext.HttpContext.User.Identity;

                    if (identity.IsAuthenticated)
                    {
                        string userId = ((FormsIdentity)identity).Ticket.UserData.Split('#')[0];
                        string userRoles = ((FormsIdentity)identity).Ticket.UserData.Split('#')[1];

                        filterContext.HttpContext.Session["userId"] = userId;
                        filterContext.HttpContext.Session["userRoles"] = String.Join(",", userRoles);
                    }

                    
                }
            }


            base.OnAuthorization(filterContext);

            
        }
    }
}