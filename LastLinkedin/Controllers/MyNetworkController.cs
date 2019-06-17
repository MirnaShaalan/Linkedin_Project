using System;
using LastLinkedin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace LastLinkedin.Controllers
{
    public class MyNetworkController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();
        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        public static List<ApplicationUser> L = new List<ApplicationUser>();
        public static List<ApplicationUser> I = new List<ApplicationUser>();


        public static List<Connection> C = new List<Connection>();


        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {

            return View();

        }



        //List<Connection> C = new List<Connection>();
        //var users = context.Users.Where(u => (u.Id != user.Id));
        //C = user.Connections.ToList();


        //if (C != null)
        //{
        //    foreach (var item in users)
        //    {
        //        var x = 0;
        //        foreach (var con in C)
        //        {
        //            if ((item.Id != con.connection_user_id))
        //            {
        //                x++;
        //            }

        //        }

        //        if (x == C.Count)
        //        {
        //            if (!L.Contains(item))
        //            {
        //                L.Add(item);

        //            }

        //        }
        //    }
        //    return View(L);

        //}
        //else
        //{
        //    return View();
        //}


        //List<Connection> C = new List<Connection>();
        //C = context.Connections.ToList();
        //var _user = C.FirstOrDefault(u => u.connection_user_id == user.Id);
        //if (C.FirstOrDefault(u => u.connection_user_id == user.Id) != null)
        //{
        //    if (_user.Accepted == false)
        //    {
        //        var invUser = context.Users.FirstOrDefault(u => u.Id == _user.user_id);
        //        ViewBag.users = users;
        //        ViewBag.invUser = invUser;
        //        return View();
        //    }
        //    else
        //    {
        //        ViewBag.users = users;
        //        return View();
        //    }
        //}
        //else
        //{
        //    ViewBag.users = users;
        //    return View();

        [HttpGet]
        public ActionResult Invitations()
        {
            C.Clear();
            I.Clear();
            var users = context.Users.Where(u => (u.Id != user.Id)).ToList();
            C = user.Connections1.Where(c => c.Accepted != true).ToList();
            var connections = C.Where(c => (c.connection_user_id == user.Id) && (c.Accepted == false)).ToList();
            //context.Users.Where(u => (u.Id != user.Id));
            var connectionsId = connections.Select(s => s.user_id).ToList();

            foreach (var itemn in connectionsId)
            {
                var connectionDetails = context.Users.Where(u => u.Id == itemn).FirstOrDefault();
                I.Add(connectionDetails);

            }
            // for(int i=0;i<connectionsId.Count;i++)

            //where()
            //I.Clear();
            return PartialView(I);
        }

        [Authorize]
        [HttpPost]
        public void Invitations(string id)
        {
            // var currentUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            // var incUser = context.Users.FirstOrDefault(u => u.Id == id);
            //C = user.Connections1.ToList();

            //var connected = C.Where(u => (u.connection_user_id == user.Id) && (u.user_id == id)).ToList();


            //foreach(var item in connected)
            //{
            //    item.Accepted = true;
            //    context.Entry(item).State = System.Data.Entity.EntityState.Modified;

            //}
            var con = context.Connections.FirstOrDefault(C => C.connection_user_id == user.Id && C.user_id == id);
            con.Accepted = true;
            context.Entry(con).State = System.Data.Entity.EntityState.Modified;


            context.SaveChanges();


        }




        [Authorize]
        [HttpPost]
        public void Recommendations(string id)
        {
            var currentUser = context.Users.FirstOrDefault(u => u.Id == user.Id);
            var incUser = context.Users.FirstOrDefault(u => u.Id == id);
            Connection C = new Connection()
            {
                user_id = user.Id,
                connection_user_id = id,
                date_connection_made = DateTime.Now,
                User = currentUser,
                User1 = incUser,
                Accepted = false

            };
            context.Connections.Add(C);
            context.SaveChanges();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Recommendations()
        {
            C.Clear();
            L.Clear();
            //var users = context.Users.Where(u => (u.Id != user.Id));
            //return View(users);


            // List<Connection> C = new List<Connection>();
            var users = context.Users.Where(u => (u.Id != user.Id)).ToList();
            C = context.Connections.ToList();


            if (C != null)
            {
                L.Clear();
                var x = 0;
                foreach (var item in users)
                {

                    foreach (var con in C)
                    {
                        if ((item.Id != con.connection_user_id) && (item.Id != con.user_id))
                        {
                            x++;
                        }
                        if (con.Accepted == true)
                        {
                            x--;
                        }

                    }

                    if ((x == C.Count()))
                    {
                        if (!L.Contains(item))
                        {
                            L.Add(item);

                        }

                    }
                }

                return PartialView(L);

            }
            else
            {
                return PartialView();
            }

        }

        [HttpGet]
        [Authorize]
        public ActionResult Count()
        {
            C = user.Connections.ToList();
            var con = context.Connections.ToList();
            var result = con.Where(a => (a.Accepted == true) && (a.connection_user_id == user.Id)).ToList();

            return PartialView(result);

        }

        [HttpGet]
        [Authorize]
        public ActionResult Friends()
        {
            C.Clear();
            L.Clear();
            var users = context.Users.ToList();
            C = context.Connections.ToList();
            var f = C.Where(c => ((c.connection_user_id == user.Id) || (c.user_id == user.Id)) && (c.Accepted == true)).ToList();


            if (C != null)
            {

                //L.Clear();
                foreach (var item in f)
                {
                    if (item.connection_user_id == user.Id)
                    {
                        L.Add(users.Where(u => u.Id == item.user_id).FirstOrDefault());
                    }
                    if (item.user_id == user.Id)
                    {
                        L.Add(users.Where(u => u.Id == item.connection_user_id).FirstOrDefault());

                    }



                }

                return PartialView(L);


            }
            else
            { return PartialView(); }
        }
    }
}