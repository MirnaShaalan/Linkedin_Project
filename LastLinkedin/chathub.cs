using LastLinkedin.Controllers;
using LastLinkedin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LastLinkedin
{
    public class chathub:Hub
    {
    //    ApplicationDbContext contxt;
    //    string userID;
    //    public ApplicationUser user;
    //    MessageController m;
    //    public chathub()
    //    {
    //        contxt = new ApplicationDbContext();
    //        m = new MessageController();
    //        userID = "6034433b-c3da-4f5f-b363-998712aa7378";
    //        user = contxt.Users.FirstOrDefault(u => u.Id == userID);
    //    }

        
    //    public override Task OnConnected()
    //    {
            

    //        return base.OnConnected();
    //    }

    //    public void sendmessage(string message, string time)
    //    {
    //        ///to send message to all clients (all clients contain the function of receivemessage)
    //        string name = user.firstName + " " + user.lastName;
    //        Clients.All.receivemessge(message, time);
    //        foreach (var item in contxt.Users)
    //            AddMessage(user.Id, item.Id, message, time);
    //        contxt.SaveChanges();


    //    }

    //    public void sendmessageone(string message, string time)
    //    {
    //        ///to send message to all clients (all clients contain the function of receivemessage)
    //        string name = user.firstName + " " + user.lastName;
    //        Clients.Client("0f2850ca-47a3-4322-9686-f26994ff1a43").receivemessge(message, time);
             
    //        AddMessage(user.Id, "0f2850ca-47a3-4322-9686-f26994ff1a43", message, time);
    //        contxt.SaveChanges();


    //    }

        

    //    public override Task OnDisconnected(bool stopCalled)
    //    {
         
    //        return base.OnDisconnected(stopCalled);
    //    }

       


    //    public void AddMessage(string userId, string ReceivId, string msg, string t)
    //    {
    //        Message m = new Message() { UserId = userId, ReceiverId = ReceivId, Msg = msg, Date = t, Read = 0 };
    //        contxt.Messages.Add(m);
    //    }

        
    }
}