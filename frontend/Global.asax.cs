﻿using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Helpers;
using System.Security.Claims;
using BrockAllen.MembershipReboot.Ef;
using System.Data.Entity;

namespace AgendaTech.View
{  
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DefaultMembershipRebootDatabase>());

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.ConfigureContainer();
        }

        void Session_Start(object sender, EventArgs e)
        {
            string cookieHeaders = string.Empty;

            cookieHeaders = HttpContext.Current.Request.Headers["Cookie"];

            if ((cookieHeaders != null) && (cookieHeaders.IndexOf("ASP.NET_SessionId") >= 0))            
                HttpContext.Current.Session["SessaoExpirada"] = true;
            
            Response.Redirect("~/Login");
        }
    }
}