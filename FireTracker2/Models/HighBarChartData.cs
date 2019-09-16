using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FireTracker2.Models
{
    public class HighBarChartData
    {
        public string label { get; set; }
        public int value { get; set; }
    }
    public class FusionChartData
    {
        public string label { get; set; }
        public string value { get; set; }
    }
    public class PriorityBarData
    {
        public List<string> label { get; set; }
        public List<int> value { get; set; }
        public PriorityBarData()
        {
            label = new List<string>();
            value = new List<int>();
        }
    }
}