using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Schedule
    {
        public List<DateTime> Schedules { get; set; }
        public string Description { get; set; }
    }
}
