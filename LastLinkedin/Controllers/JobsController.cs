using LastLinkedin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastLinkedin.Controllers
{
    public class JobsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        ApplicationDbContext db;

        public JobsController()
        {
            db = new ApplicationDbContext();

        }


        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {

            return View();

        }

        [HttpGet]
        public ActionResult Recommendations()
        {
            return PartialView(db.Jobs);
        }


        [HttpPost]
        public ActionResult Search(String title)
        {
            var result = context.Jobs.Where(j => j.Title == title).ToList();
            return View(result);
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    {
        //        if (user != null)
        //        {
        //            return View();
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home", new { area = "" });
        //        }
        //    }
        //}
    }
}