using FireTracker2.Helpers;
using FireTracker2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireTracker2.Controllers
{
    public class HighChartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleHelp roleHelper = new RoleHelp();
        private ProjectHelper projHelp = new ProjectHelper();
    
        //private JsonResult GetHardCodedHighPieData()
        //{
        //    var dataSet = new List<HighBarChartData>();
        //    dataSet.Add(new HighBarChartData { label: 'New/UnAssigned', value: 10 });
        //    dataSet.Add(new HighBarChartData { label: 'Assigned', value: 3 });
        //    dataSet.Add(new HighBarChartData { label: 'UnAssigned', value: 30 });
        //    dataSet.Add(new HighBarChartData { label: 'InProgress', value: 60 });
        //    dataSet.Add(new HighBarChartData { label: 'OnHold', value: 80 });
        //    dataSet.Add(new HighBarChartData { label: 'Complete', value: 2 });
        //    dataSet.Add(new HighBarChartData { label: 'Archived', value: 300 });
        //    dataSet.Add(new HighBarChartData { label: 'New/UnAssigned', value: 450 });
        //    return Json(dataSet);
        //}
        public JsonResult GetHighPieData()
        {
            var dataSet = new List<HighBarChartData>();
            foreach (var ticketStatus in db.TicketStatus.ToList())
            {
                var value = db.TicketStatus.Find(ticketStatus.Id).Tickets.Count();
                dataSet.Add(new HighBarChartData { label = ticketStatus.Name, value = value });
            };
            return Json(dataSet);
        }
        public JsonResult GetPriorityData(string priority)
        {
            var dataSet = new HighBarChartData();
            dataSet.label = priority;
            dataSet.value = db.Tickets.Where(t => t.TicketPriority.Name == priority).Count();
            return Json(dataSet);
        }
        public JsonResult GetStatusData(string status)
        {
            var dataSet = new HighBarChartData();
            dataSet.label = status;
            dataSet.value = db.Tickets.Where(t => t.TicketStatus.Name == status).Count();
            return Json(dataSet);
        }
        public JsonResult GetPriorityData2()
        {
            var tickets = db.Tickets.ToList();
            var aCount = 0;
            var bCount = 0;
            var cCount = 0;
            var dCount = 0;
            var eCount = 0;
            foreach (var ticketPriority in tickets)
            {
                if (ticketPriority.TicketPriority.Name == "High")
                {
                    aCount++;
                }
                if (ticketPriority.TicketPriority.Name == "Immediate")
                {
                    bCount++;
                }
                if (ticketPriority.TicketPriority.Name == "Medium")
                {
                    cCount++;
                }
                if (ticketPriority.TicketPriority.Name == "Low")
                {
                    dCount++;
                }
                if (ticketPriority.TicketPriority.Name == "None")
                {
                    eCount++;
                }
            }
            var dataSet = new PriorityBarData
            {
                label = { "High", "Immediate", "Medium", "Low", "None" },
                value = { aCount, bCount, cCount, dCount, eCount }

            };

            return Json(dataSet);
        }
    }
}
