using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using FireTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FireTracker2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string AvatarUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }
       
        public string DisplayName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
       
        public ApplicationUser()
        {
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketAttachments = new HashSet<TicketAttachment>();
            Projects = new HashSet<Project>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
       
        public System.Data.Entity.DbSet<FireTracker.Models.TicketAttachment> TicketAttachments { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.Ticket> Tickets { get; set; }
        public System.Data.Entity.DbSet<FireTracker2.Models.Project> Projects { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.TicketPriority> TicketPriorities { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.TicketStatus> TicketStatus { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.TicketComment> TicketComments { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.TicketHistory> TicketHistories { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.TicketTypes> TicketTypes { get; set; }
        public System.Data.Entity.DbSet<FireTracker.Models.TicketNotification> TicketNotifications { get; set; }
    }
}