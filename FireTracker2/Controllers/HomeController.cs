using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FireTracker2.Models;
using System.Configuration;
using System.Net.Mail;

namespace FireTracker2.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(Email model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email From:<bold>{0}</bold>({1})</p><p>Message:</p>";
                    var from = "FireTracker<Admin@Firetracker.com>";
                    model.Body = "This Message is from bug tracker";
                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = "BugTracker Contact Email",
                        Body = string.Format(body, model.FromName, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);
                    return View(new Email());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }
        public ActionResult Contact()
        {
            Email model = new Email();
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        //register view via link
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Unauthorized()
        {
            TempData["AccessDenied"] = "AccessDenied";
            return View("Unauthorized");
        }
    }
}