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

    [Authorize]
    public class HomeController : Controller
    {
        public ApplicationUser user;
        ApplicationDbContext context;
        List<Post> cur_posts;
        public HomeController()
        {
            user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            context = new ApplicationDbContext();
            cur_posts = new List<Post>();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return Redirect("http://localhost:58650/Account/Login");
            //return View();
        }

        [Authorize]
        public ActionResult Feeds()
        {

            return View();

        }


        /* 
         *         [Authorize]

         public ActionResult GetAll()
        {
            var posts = context.Posts.ToList();
            return PartialView(posts);
        }
        }*/


        [Authorize]
        public ActionResult GetAll()
        {
            var poss = context.Posts.ToList();
            var allcons = context.Connections.ToList();
            cur_posts = context.Posts.Where(p => p.ReadPost == 0 || p.UserId == user.Id).ToList();
            var cons1 = context.Connections.Where(u => u.user_id == user.Id && u.Accepted == true).ToList();
            var cons2 = context.Connections.Where(u => u.connection_user_id == user.Id && u.Accepted == true).ToList();

            if (cons1 != null)
            {
                Post pp = new Post();
                foreach (var item in cons1)
                {
                    pp = poss.FirstOrDefault(u => u.UserId == item.user_id);
                    if (cur_posts.FirstOrDefault(p => p.ID == pp.ID) == null)
                        cur_posts.Add(pp);
                }

            }
            if (cons2 != null)
            {
                Post pp = new Post();
                foreach (var item in cons2)
                {
                    pp = poss.FirstOrDefault(u => u.UserId == item.connection_user_id);
                    if (cur_posts.FirstOrDefault(p => p.ID == pp.ID) == null)
                        cur_posts.Add(pp);
                }

            }
            cur_posts.Sort((a, b) => b.PublishedPostDate.CompareTo(a.PublishedPostDate));
            return PartialView(cur_posts);

        }


        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post p)
        {
            //var userId = User.Identity.GetUserId();
            p.UserId = user.Id;
            p.PublishedPostDate = DateTime.Now;
            p.Liked = 0;
            p.User = context.Users.FirstOrDefault(u => u.Id == user.Id);
            //p.ReadPost = 0;
            context.Posts.Add(p);
            context.SaveChanges();
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pos = context.Posts.FirstOrDefault(p => p.ID == id);
            return PartialView(pos);
        }

        [HttpPost]
        public ActionResult Edit(Post pos)
        {
            pos.PublishedPostDate = DateTime.Now;
            pos.UserId = user.Id;
            pos.User = context.Users.FirstOrDefault(u => u.Id == user.Id);
            context.Entry(pos).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("GetAll");
        }
       

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var pos = context.Posts.FirstOrDefault(p => p.ID == id);
            //delete likes
            var lks = context.Likes.Where(l => l.Post.ID == id).ToList();
            foreach (var item in lks)
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;

            }
            //delete comments
            var coments = context.Comments.Where(l => l.Post.ID == pos.ID).ToList();
            foreach (var item in coments)
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;

            }
            //delete post
            context.Entry(pos).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Hide(int id)
        {
            var pos = context.Posts.FirstOrDefault(p => p.ID == id);

            var poss = context.Posts.ToList();
            var allcons = context.Connections.ToList();
            cur_posts = context.Posts.Where(p => p.ReadPost == 0 || p.UserId == user.Id).ToList();
            var cons1 = context.Connections.Where(u => u.user_id == user.Id && u.Accepted == true).ToList();
            var cons2 = context.Connections.Where(u => u.connection_user_id == user.Id && u.Accepted == true).ToList();

            if (cons1 != null)
            {
                Post pp = new Post();
                foreach (var item in cons1)
                {
                    pp = poss.FirstOrDefault(u => u.UserId == item.user_id);
                    if (cur_posts.FirstOrDefault(p => p.ID == pp.ID) == null)
                        cur_posts.Add(pp);
                }

            }
            if (cons2 != null)
            {
                Post pp = new Post();
                foreach (var item in cons2)
                {
                    pp = poss.FirstOrDefault(u => u.UserId == item.connection_user_id);
                    if (cur_posts.FirstOrDefault(p => p.ID == pp.ID) == null)
                        cur_posts.Add(pp);
                }

            }

            cur_posts.Remove(pos);
            return PartialView("GetAll", cur_posts);
        }


        [HttpGet]
        public ActionResult AddLike(int id)
        {
            var pos = context.Posts.FirstOrDefault(p => p.ID == id);
            var poslike = context.Likes.Where(l => l.Post.ID == id).ToList();
            Like lk = new Like();
            var flag = 0;

            if (poslike == null && user.Id == pos.UserId)
            {
                lk.UserId = pos.UserId;
                lk.PublishedID = user.Id;
                lk.Post = pos;
                pos.Liked = 1;
                context.Likes.Add(lk);
                context.Entry(pos).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            else if (poslike == null && user.Id != pos.UserId)
            {
                lk.UserId = pos.UserId;
                lk.PublishedID = user.Id;
                lk.Post = pos;
                pos.Liked = 0;
                context.Likes.Add(lk);
                context.Entry(pos).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            else if (poslike != null)
            {
                foreach (var item in poslike)
                {
                    if (item.PublishedID == user.Id)
                    {
                        //lk = context.Likes.FirstOrDefault(l => l.Post.ID == pos.ID && l.PublishedID == user.Id);
                        flag = 1;
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        pos.Liked = 0;
                        context.SaveChanges();

                    }
                }
                if (flag == 0)
                {
                    lk.UserId = pos.UserId;
                    lk.PublishedID = user.Id;
                    lk.Post = pos;
                    context.Likes.Add(lk);
                    context.Entry(pos).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            else
            {
                pos.Liked = 0;
                lk = context.Likes.FirstOrDefault(l => l.Post.ID == pos.ID && l.PublishedID == user.Id);
                context.Entry(lk).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();


            }

            return RedirectToAction("GetAll");


        }


        [HttpGet]
        public ActionResult DetailsLike(int id)
        {
            var pos = context.Posts.FirstOrDefault(p => p.ID == id);
            var likes = context.Likes.Where(l => l.Post.ID == pos.ID).ToList();
            List<ApplicationUser> usrs = new List<ApplicationUser>();
            ApplicationUser us;
            foreach (var item in likes)
            {
                us = context.Users.FirstOrDefault(u => u.Id == item.PublishedID);
                usrs.Add(us);
            }
            return PartialView(usrs);
        }

        [HttpPost]
        public ActionResult AddComment(int id, string Description)
        {
            var pos = context.Posts.FirstOrDefault(p => p.ID == id);
            Comment cm = new Comment();
            cm.UserId = pos.UserId;
            cm.PublishedID = user.Id;
            cm.Description = Convert.ToString(Description);
            cm.Post = pos;
            context.Comments.Add(cm);
            context.Entry(pos).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("GetAll");

        }

        [HttpGet]
        public ActionResult DetailsComment(int id)
        {

            var pos = context.Posts.FirstOrDefault(p => p.ID == id);
            var coments = context.Comments.Where(l => l.Post.ID == pos.ID).ToList();

            return PartialView("DetailsComment", coments);

        }

    }


}
