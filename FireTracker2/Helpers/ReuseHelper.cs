using FireTracker2.Models;
using FireTracker2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker2.Helpers
{
    public class ReuseHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static RoleHelp roleHelp = new RoleHelp();
    }
}