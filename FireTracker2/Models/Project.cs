using FireTracker.Models;
using FireTracker2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FireTracker2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string AvatarUrl { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        public Project()
        {
            Users = new HashSet<ApplicationUser>();
        }

    }
}