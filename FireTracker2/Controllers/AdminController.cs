using FireTracker2.Models;
using FireTracker2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireTracker2.Helpers;
using static FireTracker2.Controllers.AccountController;

namespace FireTracker2.Controllers
{
    [NoAuthorize(Roles = "Admin")]
    [RequireHttps]
    public class AdminController : Controller
    {
        private ProjectHelper projectHelper = new ProjectHelper();
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelp roleHelper = new RoleHelp();
        
        // GET: Admin
        public ActionResult UserIndex()
        {
            var users = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email,

            }).ToList();
            return View(users);
        }
    
        [HttpGet]
        public ActionResult ManageUserRole(string userId)
        {
            //How do i load up a dropdown list with role info
            //new SelectList(the list of data pushed into control)
            //what column is used to transmit my selection to user,
            // the column that we show to the user inside control,
            // if role already occuppied Show this ()
            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name","Name", currentRole);  
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRole(string userId, string roles)
        {
            //this is where i assign RoleHelper class to make sure my user is in role
            //the First thing i want to do is make sure i remove the user from all roles 
            foreach (var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }
            //if the incoming sole selection is not null i want to assign the user to the selected role
            if (!string.IsNullOrEmpty(roles))
            {
                roleHelper.AddUserToRole(userId, roles);
            }
            return RedirectToAction("UserIndex");
        }
    
        [Authorize(Roles ="Admin")]
        public ActionResult ManageRoles()
        {
            ViewBag.Users = new MultiSelectList(db.Users.ToList(), "Id", "FullName");
            ViewBag.RoleName = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();


         
        }
        [HttpPost]
        public ActionResult ManageRoles(List<string> users, string roles)
        {
            if (users != null)
            {
                //lets iterate over the incoming list of users that were selected from the form
                //and remove each of them from whatever role the occupy only to add them back to the selected role
                foreach (var userId in users)
                {
                    foreach (var role in roleHelper.ListUserRoles(userId))
                    {
                        roleHelper.RemoveUserFromRole(userId, role);
                    }
                    if (!string.IsNullOrEmpty(roles))
                    {
                        roleHelper.AddUserToRole(userId, roles);
                    }
                }
            }
            return View("ManageRoles");
        }
        /// get user projects
        [HttpGet]
       public ActionResult ManageUserProjects (string userId)
        {
              
            // i just need a list of projects that this user is on
            var myProjects = projectHelper.ListUserProjects(userId);
            ViewBag.Projects = new MultiSelectList(db.Projects.ToList(), "Id", "Name", myProjects.Select(p => p.Name));
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserProjects(List<int> projects, string userId)
        {
            foreach (var project in projectHelper.ListUserProjects(userId).ToList())
            {
                projectHelper.RemoveUserFromProject(userId, project.Id);
            }

            //if the incoming list is not null we will reassign them to all that are selected
            if(projects != null)
            {
                foreach(var projectId in projects)
                {
                    projectHelper.AddUserToProject(userId, projectId);
                }
            }
            return RedirectToAction("Index", "Projects");
        }
        [HttpGet]
        public ActionResult ManageProjectUsers (int? id)
        {
            var users = db.Users.Select(u => new UserProfileViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email,

            }).ToList();
            ViewBag.ProjectId = new MultiSelectList(db.Projects.ToList(), "Id", "Name");
            return View(users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageProjectUsers(int projectId, List<string> ProjectManager, List<string> Developer, List<string> Submitter)
        {
            foreach (var user in projectHelper.UsersOnProject(projectId).ToList())
            {
                projectHelper.RemoveUserFromProject(user.Id, projectId);
            }

            //if the incoming list is not null we will reassign them to all that are selected
            if (ProjectManager != null)
            {
                foreach (var projectManagerId in ProjectManager)
                {
                    projectHelper.AddUserToProject(projectManagerId, projectId);
                }
            }

            if (Submitter != null)
            {
                foreach (var submitterId in Submitter)
                {
                    projectHelper.AddUserToProject(submitterId, projectId);
                }
            }

            if (Developer != null)
            {
                foreach (var developerId in Developer)
                {
                    projectHelper.AddUserToProject(developerId, projectId);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Projects", new { id = projectId });
            
        }






        public ActionResult ManageUsers()
        {
            return View();
        }
    }
}