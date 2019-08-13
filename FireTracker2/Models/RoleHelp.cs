using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker2.Models
{
    public class RoleHelp 
    {
        private UserManager<ApplicationUser> userManager = new
        UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        private ApplicationDbContext db = new ApplicationDbContext();
        public bool IsUserInRole(string userId, string userRoles)
        {
            return userManager.IsInRole(userId, userRoles);
        }
        public ICollection<string> ListUserRoles(string userId)
        {
            return userManager.GetRoles(userId);
        } 
        public bool AddUserToRole(string userId, string userRoles)
        {
            var result = userManager.AddToRole(userId, userRoles);
            return result.Succeeded;
        }
        public bool RemoveUserFromRole(string userId, string userRoles)
        {
            var result = userManager.RemoveFromRole(userId, userRoles);
            return result.Succeeded;
        }
        public ICollection<ApplicationUser> UserInRole(string userRoles)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (IsUserInRole(user.Id, userRoles))
                    resultList.Add(user);
            }
            return resultList;
        }
        public ICollection<ApplicationUser> UserNotInRole(string userRoles)
        {
            var resultList = new List<ApplicationUser>();
            var List = userManager.Users.ToList();
            foreach (var user in List)
            {
                if (!IsUserInRole(user.Id, userRoles))
                    resultList.Add(user);
            }
            return resultList;
        }



    }
}