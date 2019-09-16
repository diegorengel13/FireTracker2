using FireTracker2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FireTracker2.Helpers
{
    public class HistoryHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static RoleHelp roleHelp = new RoleHelp();
        public static void SaveHistory(Ticket oldTicket, Ticket newTicket)
        {
            foreach(var property in WebConfigurationManager.AppSettings["TrackedHistoryProperties"].Split(','))
            {
                var oldValue = oldTicket.GetType().GetProperty(property).GetValue(oldTicket, null).ToString();
                var newValue = newTicket.GetType().GetProperty(property).GetValue(newTicket, null).ToString();
                if(oldValue != newValue)
                {
                    var newHistory = new TicketHistory
                    {
                        UserId = HttpContext.Current.User.Identity.GetUserId(),
                        Updated = newTicket.Updated,
                        PropertyName = property,
                        NewValue = newValue,
                        OldValue = oldValue,
                        TicketId = newTicket.Id
                    };
                    db.TicketHistories.Add(newHistory);
                }
            }
            db.SaveChanges();
        }
    }
}