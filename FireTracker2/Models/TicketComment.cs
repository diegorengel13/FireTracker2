using FireTracker2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Created { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}