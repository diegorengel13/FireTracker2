using FireTracker2.Enumerators;
using FireTracker2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker2.Helpers
{
    public class TicketHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static RoleHelp roleHelp = new RoleHelp();
        public static ProjectHelper projHelp = new ProjectHelper();

        public static bool TicketDetailIsViewableByUser(int TicketId)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var roleName = roleHelp.ListUserRoles(userId).FirstOrDefault();
            var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName);

            switch (systemRole)
            {
                case SystemRole.Admin:
                    break;
                case SystemRole.ProjectManager:
                    break;
                case SystemRole.Developer:
                    break;
                case SystemRole.Submitter:
                    break;

            }
            return true;
        }
        public static bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelp.ListUserRoles(userId).FirstOrDefault();
            switch (myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.OwnerUserId == userId;
                case "ProjectManager":
                    var myProjects = projHelp.ListUserProjects(userId);
                    foreach(var project in myProjects)  
                    {
                        foreach(var projTicket in project.Tickets)
                        {
                            if (projTicket.Id == ticket.Id)
                                return true;
                        }
                    }
                    return false;
                case "Admin":
                    return true;
                default:
                    return false;
            }
         
        }
  

    }
}