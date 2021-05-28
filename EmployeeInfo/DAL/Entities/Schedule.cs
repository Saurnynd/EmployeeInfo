using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Schedule
    {
        public string Schedules { get; set; }
        public string Description { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
