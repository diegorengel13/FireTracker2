﻿using FireTracker2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FireTracker2.Helpers
{
    public class ProjectHelper
    {
        private  RoleHelp roleHelp = new RoleHelp();
        private  ApplicationDbContext db = new ApplicationDbContext();


        public bool IsUserOnProject(string userId, int projectId)
        {
            var project = db.Projects.Find(projectId);
            var flag = project.Users.Any(u => u.Id == userId);
            return (flag);
        }
        public ICollection<Project> ListUserProjects(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            var projects = user.Projects.ToList();
            return (projects);
        }
        public void AddUserToProject(string userId, int projectId)
        {
            if (!IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var newUser = db.Users.Find(userId);

                proj.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        public void RemoveUserFromProject(string userId, int projectId)
        {
            if (IsUserOnProject(userId, projectId))
            {
                Project proj = db.Projects.Find(projectId);
                var delUser = db.Users.Find(userId);

                proj.Users.Remove(delUser);
                db.Entry(proj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public ICollection<ApplicationUser> UsersOnProject(int projectId)
        {
            var project = db.Projects.Find(projectId);
            var users = project.Users;
            return users;
        }
        public ICollection<string> UsersInRoleOnProject(int projectId, string roles)
        {
            var users = UsersOnProject(projectId);
            var people = new List<string>();
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (roleHelp.IsUserInRole(user.Id, roles))
                    {
                        people.Add(user.FullName);

                    }
                };
               
            }
            return people;
        }
       
        public ICollection<ApplicationUser> UsersNotOnProject(int projectId)
        {
            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
        }

    }
}
//    public class UserProjectHelper
//    {
//        private UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>
//            (new UserStore<ApplicationUser>(new ApplicationDbContext()));
//        ApplicationDbContext db = new ApplicationDbContext();
//        public bool IsUserOnProject(string userId, int projectId)
//        {
//            if (db.Projects.Find(projectId).Users.Contains(db.Users.Find(userId)))
//            {
//                return true;
//            }
//            return false;
//        }
//        public void AddUserToProject (string userId, int projectId)
//        {
//            if (!IsUserOnProject(userId, projectId))
//            {
//                var project = db.Projects.Find(projectId);
//                project.Users.Add(db.Users.Find(userId));
//                db.Entry(project).State = EntityState.Modified; // saves this obj instance.
//                db.SaveChanges();
//            };
//        }
     
//        public void RemoveUserFromProject(string userId, int projectId)
//        {
//            if (IsUserOnProject(userId, projectId))
//            {
//                Project proj = db.Projects.Find(projectId);
//                var delUser = db.Users.Find(userId);

//                proj.Users.Remove(delUser);
//                db.Entry(proj).State = EntityState.Modified;
//                db.SaveChanges();
//            }
//        }
//        public ICollection<ApplicationUser>ListUsersOnProject(int projectId)
//        {
//            return db.Projects.Find(projectId).Users;
//        }
//        public ICollection<Project>ListProjectsForUser(string userId)
//        {
//            return db.Users.Find(userId).Projects;
//        }
//        public ICollection<ApplicationUser>ListUsersNotOnProject(int projectId)
//        {
//            return db.Users.Where(u => u.Projects.All(p => p.Id != projectId)).ToList();
//        }

//    }
//}