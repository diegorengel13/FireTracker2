using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FireTracker2.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "Title Must be between {2} and {1} Characters long.", MinimumLength = 4)]
        public string Title { get; set; }
        [StringLength(50, ErrorMessage = "Description Must be between {2} and {1} Characters long.", MinimumLength = 3)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        [Display(Name = "ProjectName")]
        public int ProjectId { get; set; }
        [Display(Name = "Type")]
        public int TicketTypeId { get; set; }
        [Display(Name = "Priority")]
        public int TicketPriorityId { get; set; }
        [Display(Name = "Status")]
        public int TicketStatusId { get; set; }
        [Display(Name = "Submitter")]
        public string OwnerUserId { get; set; }
        [Display(Name = "Developer")]
        public string AssignedToUserId { get; set; }
        /// <summary>
        /// virtual nav
        /// </summary>
        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }
      
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
        public Ticket()
        {
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketNotifications = new HashSet<TicketNotification>();
        }

            


    }
}