using FireTracker.Models;
using FireTracker2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace FireTracker2.Helpers
{
    public class NotifyHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static void  CreateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var noChange = (oldTicket.AssignedToUserId == newTicket.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(oldTicket.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(newTicket.AssignedToUserId));
            if (noChange)
                return;
            if(assignment)
                GenerateAssignedNotification(oldTicket, newTicket);
            else if (unassignment)
                GenerateUnAssignedNotification(oldTicket, newTicket);
            else
            {
                GenerateAssignedNotification(oldTicket, newTicket);
                GenerateUnAssignedNotification(oldTicket, newTicket);
            }

        }
        public static void GenerateUnAssignedNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You have been unassigned from ticket{newTicket.Id} on {DateTime.Now} ",
                Read = false,
                RecepientId = oldTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"To acknowledge you have recieved this notification please mark read",
                TicketId = newTicket.Id
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        //public static void GetTypeByName(int? Id)
        //{
        //   var type = 
        //}
        public static void GenerateAssignedNotification(Ticket oldTicket,Ticket newTicket)
        {
            {
                var notification = new TicketNotification
                {
                    Created = DateTime.Now,
                    Subject = $"You have been assigned to ticket{newTicket.Id} on {DateTime.Now} ",
                    Read = false,
                    RecepientId = newTicket.AssignedToUserId,
                    SenderId = HttpContext.Current.User.Identity.GetUserId(),
                    NotificationBody = $"To acknowledge you have recieved this notification please mark read",
                    TicketId = newTicket.Id
                };
                db.TicketNotifications.Add(notification);
                db.SaveChanges();
            }
        }
        private static void CreateChangeNotification(Ticket oldTicket, Ticket newTicket)
        {
            var messageBody = new StringBuilder();
            foreach(var property in WebConfigurationManager.AppSettings["TrackedTicketProperties"].Split(','))
            {
                var oldValue = oldTicket.GetType().GetProperty(property).GetValue(oldTicket, null);
                var newValue = newTicket.GetType().GetProperty(property).GetValue(newTicket, null);
                if(oldValue != newValue)
                {
                    messageBody.AppendLine(new String('-', 45));
                    messageBody.AppendLine($"A change has been made to: {property}.");
                    messageBody.AppendLine($"The old value was: {oldValue.ToString()}");
                    messageBody.AppendLine($"The new value is: {newValue.ToString()}");
                }
            }
            if (!string.IsNullOrEmpty(messageBody.ToString()))
            {
                var message = new StringBuilder();
                message.AppendLine($"The following changes were made to one of your Tickets on {newTicket.Updated}");
                message.AppendLine(messageBody.ToString());
                var senderId = HttpContext.Current.User.Identity.GetUserId();
                var notification = new TicketNotification
                {
                    TicketId = newTicket.Id,
                    Created = DateTime.Now,
                    Subject = $"Achange has occured on {newTicket.Id}",
                    RecepientId = newTicket.AssignedToUserId,
                    SenderId = senderId,
                    NotificationBody = message.ToString(),
                    Read = false
                };
                db.TicketNotifications.Add(notification);
                db.SaveChanges();
            }
        }
        public static void ManageNotification(Ticket oldTicket, Ticket newTicket)
        {
            CreateAssignmentNotification(oldTicket, newTicket);
            CreateChangeNotification(oldTicket, newTicket);
        }

        public static List<TicketNotification> GetUnreadUserNotifications()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(t => t.RecepientId == userId && !t.Read).ToList();
        }
        public static int GetAllUserNotificationCount()
        {
            var count = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(t => t.RecepientId == count).Count();
        }
        public static int GetNewUserNotificationCount()
        {
            var usercount = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Where(t => t.RecepientId == usercount && !t.Read).Count();
        }
    }
}