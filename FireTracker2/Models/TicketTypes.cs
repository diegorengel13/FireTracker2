﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker.Models
{
    public class TicketTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public TicketTypes()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}