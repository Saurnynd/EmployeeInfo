using System;
using System.Collections.Generic;
using System.Text;
using DAL.Repositories.Interfaces;
namespace DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IScheduleRepository Schedules { get; }
        void Save();
    }
}
