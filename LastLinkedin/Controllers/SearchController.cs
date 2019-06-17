using LastLinkedin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastLinkedin.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result(string email)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            return View(user);
        }
    }


}