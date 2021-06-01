using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using BLL.DTO;
using DAL.Repositories.Interfaces;
using AutoMapper;
using DAL.UnitOfWork;
using CLL.Security;
using CLL.Security.Identity;
namespace BLL.Services.Impl
{
    public class WorkerService : IWorkerService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;

        public WorkerService(
            IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(
                    nameof(unitOfWork));
            }
            _database = unitOfWork;
        }

        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<EmployeeDTO> GetEmployee(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }
            var userId = user.UserId;
            var employeeEntities =
                _database
                    .Employees
                    .Find(z => z.EmployeeID == userId, pageNumber, pageSize);
            var mapper =
                new MapperConfiguration(
                    cfg => cfg.CreateMap<Employee, EmployeeDTO>()
                    ).CreateMapper();
            var employeeDto =
                mapper
                    .Map<IEnumerable<Employee>, List<EmployeeDTO>>(
                        employeeEntities);
            return employeeDto;
        }

        public void AddEmployee(EmployeeDTO employee)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            validate(employee);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>()).CreateMapper();
            var EmployeeEntity = mapper.Map<EmployeeDTO, Employee>(employee);
            _database.Employees.Create(EmployeeEntity);
        }

        private void validate(EmployeeDTO employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                throw new ArgumentException("Name повинне містити значення!");
            }
        }
    }
}
