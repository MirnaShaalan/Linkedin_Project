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
    public class ConnectionsController : Controller
    {


        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        ApplicationDbContext context = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Invitations()
        {
            List<Connection> C = new List<Connection>();
            C = context.Connections.ToList();
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var _user = C.FirstOrDefault(u => u.connection_user_id == user.Id);
            if (C.FirstOrDefault(u => u.connection_user_id == user.Id) != null)
            {
                if (_user.Accepted == false)
                {
                    var invUser = context.Users.FirstOrDefault(u => u.Id == _user.user_id);
                    return PartialView("~/Views/Connections/Invitations.cshtml", invUser);
                }
                else
                {
                    return Content("There're no Invitations");

                }
            }
            else
            {
                return Content("");

            }
        }

    }
}