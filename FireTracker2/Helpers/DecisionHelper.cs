using FireTracker.Models;
using FireTracker2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker2.Helpers
{
    public class DecisionHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static RoleHelp roleHelp = new RoleHelp();
        private static ProjectHelper projectHelper = new ProjectHelper();


        //public static bool IsTicketEditableByUser(Ticket ticket)
        //{
        //    var user = db.Users();
        //}
    }
}