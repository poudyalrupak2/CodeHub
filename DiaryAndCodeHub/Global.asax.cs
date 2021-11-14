using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static System.Net.Mime.MediaTypeNames;

namespace DiaryAndCodeHub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            if (ex != null)
            {

                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(ConfigurationManager.AppSettings["Email"]);
                msg.To.Add(new MailAddress("poudyalrupak2@gmail.com"));
                msg.Subject = "Error in code hub";
                msg.IsBodyHtml = true;
                msg.Body = ex.ToString();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["password"]);
                smtpClient.Credentials = credentials;

                smtpClient.EnableSsl = true;
                smtpClient.Send(msg);
            }

        }
    }
}
