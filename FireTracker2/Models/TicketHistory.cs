using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Updated { get; set; }
        public virtual Ticket Ticket { get; set; }

        

    }
}