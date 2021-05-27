using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public int Position { get; set; }
        public Schedule EmplSchedule { get; set; }
    }
}
