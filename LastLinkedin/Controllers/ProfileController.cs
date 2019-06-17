using LastLinkedin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LastLinkedin.Controllers
{
    public class ProfileController : Controller
    {
        //   ApplicationUser user;
        ApplicationDbContext context;
        List<WorkExperience> workexps;
        public ProfileController()
        {
            //user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            context = new ApplicationDbContext();
            workexps = new List<WorkExperience>();
        }
        // GET: Profile
        public ActionResult Index(string id)
        {
            ApplicationUser current = context.Users.FirstOrDefault(u => u.Id == id);
            Session["cur_usr"] = id;
            return View(current);
        }



        public ActionResult GetData(string id)
        {
            //ViewData["name"] = current.firstName;
            var current = context.Users.FirstOrDefault(u => u.Id == id);
            string c;
            if(current.Address!=null)
                c = current.Address.Country;

            //var cuuruserjob = context.UserJobs.FirstOrDefault(cu => cu.UserID == current.Id);
            //var currjob = context.Jobs.FirstOrDefault(j => j.ID == cuuruserjob.JobID);

            return PartialView(current);

            // return PartialView("~/Views/Profile/GetData.cshtml", current);
        }



        //***************Edit User Data*********//
        public ActionResult Editintro(string id)
        {
            var current = context.Users.FirstOrDefault(u => u.Id == id);

            var currjobid = current.UserJobs.FirstOrDefault(c => c.UserID == current.Id);
            var currentjob = context.Jobs.FirstOrDefault(j => j.ID == currjobid.JobID);

            return PartialView(current);

        }

        [HttpPost]
        public ActionResult Editintro(ApplicationUser newuser, string job)
        {
            ApplicationUser olduser = context.Users.FirstOrDefault(u => u.Id == newuser.Id);
            olduser.firstName = newuser.firstName;
            olduser.lastName = newuser.lastName;
            olduser.Address.Country = newuser.Address.Country;

            olduser.UserJobs.FirstOrDefault(j => j.UserID == olduser.Id).Job.Title = job;
            context.SaveChanges();
            return RedirectToAction("GetData", olduser);

        }

        //**************************Image Part*********************************//

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Createimage(string id, HttpPostedFileBase upload)
        //{
        //    ApplicationUser user = context.Users.FirstOrDefault(i => i.Id == id);
        //    string path = "";

        //    if (ModelState.IsValid)
        //    {
        //        path = System.IO.Path.Combine(Server.MapPath("~/images"), upload.FileName);
        //        upload.SaveAs(path);
        //        if (Request.Form["changeprofile"] != null)
        //        {
        //            user.Image = upload.FileName;
        //        }
        //        else if (Request.Form["changecover"] != null)
        //        {
        //            user.Imagecover = upload.FileName;
        //        }


        //        context.Entry(user).State = EntityState.Modified;
        //        context.SaveChanges();
        //    }

        //    return RedirectToAction("Index", new { Id = user.Id });
        //}




        //***********Experiences****************************************//


        //Get Experience
        public ActionResult GetExperiences(string id)
        {
            var current = context.Users.FirstOrDefault(u => u.Id == id);

            var userExperience = context.UserWorkExperiences.Where(u => u.UserID == current.Id).ToList();

            ViewBag.id = current.Id;

            foreach (var item in userExperience)
            {
                workexps.Add(context.WorkExperiences.FirstOrDefault(w => w.ID == item.WorkExperienceID));

            }
            return PartialView(workexps);
        }

        //Add Experience
        public ActionResult AddExperience()
        {


            return PartialView();
        }

        [HttpPost]
        public ActionResult AddExperience(WorkExperience work, string id, UserWorkExperience userwork)
        {
            //userwork.UserID = D);
            var current = context.Users.FirstOrDefault(u => u.Id == id);
            // context.WorkExperiences.Add(work);
            context.Entry(work).State = EntityState.Added;
            
            userwork.UserID = current.Id;
            userwork.WorkExperienceID = work.ID;

            //  context.UserWorkExperiences.Add(userwork);
            context.Entry(userwork).State = EntityState.Added;
            //var nn = context.UserWorkExperiences.FirstOrDefault(u => u.WorkExperienceID == work.ID);

            context.SaveChanges();
            //  return PartialView("GetExperiences", context.WorkExperiences.ToList());
            
            var we = context.UserWorkExperiences.Where(w => w.UserID == current.Id).ToList();
            List<WorkExperience> cur_workExp = new List<WorkExperience>();
            foreach (var item in we)
            {
                cur_workExp.Add(context.WorkExperiences.FirstOrDefault(w=>w.ID==item.WorkExperienceID));
            }
            return PartialView("GetExperiences", cur_workExp);
        }

        //Edit Experience
        [HttpGet]
        public ActionResult EditExperience(int id)
        {
            
            var singleExperience = context.WorkExperiences.FirstOrDefault(c => c.ID == id);

            return PartialView(singleExperience);
        }
        [HttpPost]
        public ActionResult EditExperience(WorkExperience newwork)
        {
            //WorkExperience oldwork = context.WorkExperiences.FirstOrDefault(u => u.ID == newwork.ID);
            
            context.Entry(newwork).State = EntityState.Modified;
            
            context.SaveChanges();

            string id = Session["cur_usr"].ToString();
            var we = context.UserWorkExperiences.Where(w => w.UserID == id).ToList();
            List<WorkExperience> cur_workExp = new List<WorkExperience>();
            foreach (var item in we)
            {
                cur_workExp.Add(context.WorkExperiences.FirstOrDefault(w => w.ID == item.WorkExperienceID));
            }
            return PartialView("GetExperiences", cur_workExp);

        }
        //Delete Experience
        [HttpGet]
        public ActionResult DeleteExperience(int id)
        {
            var cur_we = context.UserWorkExperiences.FirstOrDefault(w => w.WorkExperienceID == id);
            
            context.Entry(cur_we).State = EntityState.Deleted;

            context.SaveChanges();
            var we = context.UserWorkExperiences.Where(w => w.UserID == cur_we.UserID).ToList();
            List<WorkExperience> cur_workExp = new List<WorkExperience>();
            foreach (var item in we)
            {
                cur_workExp.Add(context.WorkExperiences.FirstOrDefault(w => w.ID == item.WorkExperienceID));
            }
            return PartialView("GetExperiences", cur_workExp);
        }


        //******************************Interests*************************//




        //*************************Recommendations*************************//
        //Recommend someone
        public ActionResult CreateRecommendation()
        {
            ViewBag.recommended = new SelectList(context.Users.ToList(), "Id", "Email", 0);
            return PartialView();
        }
        //[HttpPost]
        //public ActionResult CreateRecommendation(Recommendation r,ApplicationUser email,string id)
        //{
        //  var  current = context.Users.FirstOrDefault(c => c.Id == id);
        //    ApplicationUser uemail = context.Users.FirstOrDefault(u => u.Email == email.Email);
        //    r.date_of_recommendation = DateTime.Now;
        //    context.Recommendations.Add(r);
        //    context.SaveChanges();
        //    return PartialView("CreateRecommendation", current);
        //}
        [HttpPost]
        public ActionResult CreateRecommendation(Recommendation r, string id)
        {
            // HomeController h = new HomeController();
            var current = context.Users.FirstOrDefault(u => u.Id == id);

            r.User_recommending_id = current.Id;

            r.date_of_recommendation = DateTime.Now;

            // context.Recommendations.Add(r);
            context.Entry(r).State = EntityState.Added;
            context.SaveChanges();

            var cc = context.Recommendations.Where(u => u.User_recommending_id == current.Id).ToList();
            return PartialView("GivenRecommendation" ,cc);
        }


        [HttpGet]
        public ActionResult RecievedRecommendation(string id)
        {
            var current = context.Users.FirstOrDefault(u => u.Id == id);

            ViewBag.cid = current.Id;
            return PartialView(context.Recommendations.ToList());


            // RedirectToAction("GivenRecommendation",current);

        }

        //public ActionResult GivenRecommendation(string id)
        //{
        //    var current = context.Users.FirstOrDefault(u => u.Id == id);

        //    ViewBag.ccid = current.Id;
        //    return PartialView(context.Recommendations.ToList());


        //    // RedirectToAction("GivenRecommendation",current);

        //}

        //public ActionResult GivenRecommendation(string id,Recommendation r)
        //{
        //    var current = context.Users.FirstOrDefault(u => u.Id == id);
        //    if (r.User_being_recommended_id == current.Id)
        //    {
        //        return PartialView(r);
        //    }
        //    else
        //    {
        //        RedirectToAction("RecievedRecommendation");
        //    }
        //}
    }


}