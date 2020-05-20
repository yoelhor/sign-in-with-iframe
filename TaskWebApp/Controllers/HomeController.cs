using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TaskWebApp.Utils;

namespace TaskWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.IsAuthenticated && // Only if user sign-in
                (Request.UrlReferrer) != null && Request.UrlReferrer.AbsoluteUri.ToLower().Contains("b2clogin.com") // and user comes back from B2C
                )
            {
                var identityProvider = ClaimsPrincipal.Current.Claims.Where(c => c.Type.ToLower().Contains("identityprovider"))
                   .Select(c => c.Value).SingleOrDefault();

                if (identityProvider == "local")
                    return RedirectToAction("Index", "LoginCompleted");
            }

            return View();

        }


        [Authorize]
        public ActionResult Claims()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Error(string message)
        {
            ViewBag.Message = message;

            return View("Error");
        }
    }
}