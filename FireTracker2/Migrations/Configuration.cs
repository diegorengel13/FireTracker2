using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using FireTracker2.Models;
using System.Web.Configuration;



namespace FireTracker2.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<FireTracker2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(FireTracker2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                roleManager.Create(new IdentityRole { Name = "ProjectManager" });
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            if (!context.Roles.Any(r => r.Name == "None"))
            {
                roleManager.Create(new IdentityRole { Name = "None" });
            }
          

            //Instance of UserManager
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            //If users don't exist, create these users
            #region Seeded Users
            if (!context.Users.Any(u => u.Email == "AdminAlex@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "AdminAlex@Mailinator.com",
                    Email = "AdminAlex@Mailinator.com",
                    FirstName = "Alex",
                    LastName = "Amari",
                    DisplayName = "Alex"
                }, "Admin+3775!");
            }
            if (!context.Users.Any(u => u.Email == "DevDale@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DevDale@Mailinator.com",
                    Email = "DevDale@Mailinator.com",
                    FirstName = "Dale",
                    LastName = "Daniels",
                    DisplayName = "Dale"
                }, "Dev+0627!");
            }
            if (!context.Users.Any(u => u.Email == "SubmitterSam@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "SubmitterSam@Mailinator.com",
                    Email = "SubmitterSam@Mailinator.com",
                    FirstName = "Samantha",
                    LastName = "Sadler",
                    DisplayName = "Sam"
                }, "Sub+0416!");
            }
            if (!context.Users.Any(u => u.Email == "PMPatty@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "PMPatty@Mailinator.com",
                    Email = "PMPatty@Mailinator.com",
                    FirstName = "Patty",
                    LastName = "Michaels",
                    DisplayName = "Pat"
                }, "PM+0416!");
            }

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@mailinator.com",
                    Email = "DemoProjectManager@mailinator.com",
                    FirstName = "Alex",
                    LastName = "Rodriguez",
                    DisplayName = "PM",
                    AvatarUrl = "/Uploads/Alex.png"
                }, WebConfigurationManager.AppSettings["DemoUserPass"]);
            }
            if (!context.Users.Any(u => u.Email == "DemoAdmin@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@mailinator.com",
                    Email = "DemoAdmin@mailinator.com",
                    FirstName = "Anita",
                    LastName = "Smith",
                    DisplayName = "Admin",
                    AvatarUrl = "/Uploads/Anita.png"
                }, WebConfigurationManager.AppSettings["DemoUserPass"]);
            }
            if (!context.Users.Any(u => u.Email == "DemoDeveloper@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@mailinator.com",
                    Email = "DemoDeveloper@mailinator.com",
                    FirstName = "Alfred",
                    LastName = "Wiznowski",
                    DisplayName = "Developer",
                    AvatarUrl = "/Uploads/Alfred.png"
                },  WebConfigurationManager.AppSettings["DemoUserPass"]);
            }
            if (!context.Users.Any(u => u.Email == "DemoSubmitter@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@mailinator.com",
                    Email = "DemoSubmitter@mailinator.com",
                    FirstName = "Claire",
                    LastName = "O'Malley",
                    DisplayName = "Submitter",
                    AvatarUrl = "/Uploads/Claire.png"
                },  WebConfigurationManager.AppSettings["DemoUserPass"]);
            }
            #endregion
            //Assign seeded users to seeded roles
            #region Role Assignments
            //set variable in first one so i can use that variable for the rest of them
            var userId = userManager.FindByEmail("AdminAlex@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("PMPatty@Mailinator.com").Id;
            userManager.AddToRole(userId, "ProjectManager");

            userId = userManager.FindByEmail("DevDale@Mailinator.com").Id;
            userManager.AddToRole(userId, "Developer");

            userId = userManager.FindByEmail("SubmitterSam@Mailinator.com").Id;
            userManager.AddToRole(userId, "Submitter");
            #endregion
            #region
            var pmId = userManager.FindByEmail("DemoProjectManager@mailinator.com").Id;
            userManager.AddToRole(pmId, "ProjectManager");

            var adminId = userManager.FindByEmail("DemoAdmin@mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

            var subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");
            #endregion
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.TicketPriorities.AddOrUpdate(t => t.Name,
                new TicketPriority { Id = 1, Name = "Immediate", Description = "Ticket Requires Immediate Action"},
                new TicketPriority { Id = 2, Name = "High", Description = "Ticket Requires Attention ASAP" },
                new TicketPriority { Id = 3, Name = "Medium", Description = "Ticket needs assesing but no immediate action required" },
                new TicketPriority { Id = 4, Name = "Low", Description = "Ticket Does not require any imediate action" },
                new TicketPriority { Id = 5, Name = "None", Description = "No action required, ticket is only for update or information" }
                );

            context.TicketStatus.AddOrUpdate(t => t.Name,
                new TicketStatus { Id = 1, Name = "Submitted/Unassigned" },
                new TicketStatus { Id = 2, Name = "Completed" },
                new TicketStatus { Id = 3, Name = "In Progress" },
                new TicketStatus { Id = 4, Name = "Submitted/Assigned" }
                );
            context.TicketTypes.AddOrUpdate(t => t.Name,
                new TicketType
                {
                    Id = 5,
                    Name = "Bug",
                    Description = "An Error has occured in the project which has resulted in the application crashing or user is seeing an error message"
                },
                new TicketType { Id = 1, Name = "Defect", Description = "information not being displayed correctly or functions are miscalculating" },
                new TicketType { Id = 2, Name = "Function Request", Description = "A request for a new function or feature has been requested" },
                new TicketType { Id = 3, Name = "Complaint", Description = "A general complaint has been made on the application" },
                new TicketType { Id = 4, Name = "Documentation Request", Description = "Request of documents on the application has been made" }
                );
            context.Projects.AddOrUpdate(t => t.Name,
                new Project { Id = 1, Name = "Frogger_Blogger", Description = "Blog Project" },
                new Project { Id = 2, Name = "Portfolio", Description = "Personal Portfolio" },
                new Project { Id = 3, Name = "Bug Tracker", Description = "Bug Tracker Project" });
            context.Tickets.AddOrUpdate(t => t.Title,
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 1, TicketPriorityId = 5, TicketStatusId = 2, TicketTypeId = 1, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 1, TicketPriorityId = 5, TicketStatusId = 2, TicketTypeId = 1, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 1, TicketPriorityId = 3, TicketStatusId = 2, TicketTypeId = 2, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 1, TicketPriorityId = 4, TicketStatusId = 2, TicketTypeId = 1, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 2, TicketPriorityId = 5, TicketStatusId = 2, TicketTypeId = 5, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 2, TicketPriorityId = 5, TicketStatusId = 2, TicketTypeId = 1, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 3, TicketPriorityId = 5, TicketStatusId = 2, TicketTypeId = 4, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 2, TicketPriorityId = 4, TicketStatusId = 2, TicketTypeId = 1, Created = DateTime.Now },
                new Ticket { Title = "New Ticket", Description = "this is a new ticket", ProjectId = 2, TicketPriorityId = 3, TicketStatusId = 2, TicketTypeId = 3, Created = DateTime.Now }


                );
            context.SaveChanges();

        }
    }
}
