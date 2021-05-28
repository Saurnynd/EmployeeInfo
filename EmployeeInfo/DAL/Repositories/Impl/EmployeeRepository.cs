using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.EF;
namespace DAL.Repositories.Impl
{
  
    public class EmployeeRepository
        : BaseRepository<Employee>, IEmployeeRepository
    {

        internal EmployeeRepository(EmployeeInfoContext context)
            : base(context)
        {
        }
    }
}
