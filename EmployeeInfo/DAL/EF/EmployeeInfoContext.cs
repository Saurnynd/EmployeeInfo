using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.EF
{
    public class EmployeeInfoContext
        : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        
        public EmployeeInfoContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
