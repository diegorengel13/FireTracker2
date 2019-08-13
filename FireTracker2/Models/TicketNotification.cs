using FireTracker2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }

        //these2 properties below are user ids, so they should be strings. not ints -derrick (look at your db)
        public string RecepientId { get; set; }
        public string SenderId { get; set; }
        public string NotificationBody { get; set; }

        public DateTime Created { get; set; }
        public bool Read { get; set; }


        public virtual ApplicationUser Recipient { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}