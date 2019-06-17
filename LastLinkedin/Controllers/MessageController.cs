using LastLinkedin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LastLinkedin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        // GET: Message

         string userid;

        ApplicationDbContext contxt = new ApplicationDbContext();

        
        
        public ActionResult Index(string id)
        {
            Session["curId"] = id;
            var user = contxt.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }

        public ActionResult GetAllMsg(string id)
        {

            //var msgs = contxt.Messages.Where(m => m.UserId == id);

            //return PartialView(msgs);
            var usr = contxt.Users.FirstOrDefault(u=>u.Id==id);
            var users = contxt.Users.ToList();
            users.Remove(usr);
            return PartialView(users);
        }

        public ActionResult GetMsgUser(string Rid)
        {
            Session["RecId"] = Rid;
            string id = Session["curId"].ToString();
            var msgs_Receiver = contxt.Messages.Where(m => m.ReceiverId == Rid&&m.UserId==id).ToList();
            var msgs_sender = contxt.Messages.Where(m=>m.UserId==Rid&&m.ReceiverId==id).ToList();

            var msgs = msgs_Receiver.Concat(msgs_sender).ToList();
            msgs.Sort((a, b) => a.Date.CompareTo(b.Date));

            return PartialView(msgs);
        }

        public ActionResult AddMessage(string id,string msg)
        {
            userid = id;
            Message m = new Message() { UserId = userid, ReceiverId = Session["RecId"].ToString(), Msg = msg, Date = DateTime.Now.ToString(), Read = 0 };
            contxt.Entry(m).State=System.Data.Entity.EntityState.Added;
            contxt.SaveChanges();
            string Recevid = Session["RecId"].ToString();
            var msgs_Receiver = contxt.Messages.Where(a=>a.ReceiverId == Recevid&&a.UserId == userid).ToList();
            var msgs_sender = contxt.Messages.Where(a=>a.UserId == Recevid && a.ReceiverId == userid).ToList();

            var msgs = msgs_Receiver.Concat(msgs_sender).ToList();
            msgs.Sort((a, b) => a.Date.CompareTo(b.Date));

            return PartialView("GetMsgUser", msgs);
        }

        public ActionResult Search(string username)
        {
            var usr = contxt.Users.FirstOrDefault(u => u.firstName+" "+u.lastName == username);
            Session["RecId"] = usr.Id;
            string id = Session["curId"].ToString();
            var msgs_Receiver = contxt.Messages.Where(m => m.ReceiverId == usr.Id && m.UserId == id).ToList();
            var msgs_sender = contxt.Messages.Where(m => m.UserId == usr.Id && m.ReceiverId == id).ToList();

            var msgs = msgs_Receiver.Concat(msgs_sender).ToList();
            msgs.Sort((a, b) => a.Date.CompareTo(b.Date));

            return PartialView("GetMsgUser", msgs);
        }


    }
}