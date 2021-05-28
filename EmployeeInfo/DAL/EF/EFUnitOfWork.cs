using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
namespace DAL.EF
{
    public class EFUnitOfWork
        : IUnitOfWork
    {
        private EmployeeInfoContext db;
        private EmployeeRepository emplRepository;
        private ScheduleRepository scheduleRepository;

        public EFUnitOfWork(EmployeeInfoContext context)
        {
            db = context;
        }
        public IEmployeeRepository Employees
        {
            get
            {
                if (emplRepository == null)
                    emplRepository = new EmployeeRepository(db);
                return emplRepository;
            }
        }

        public IScheduleRepository Schedules
        {
            get
            {
                if (scheduleRepository == null)
                    scheduleRepository = new ScheduleRepository(db);
                return scheduleRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
