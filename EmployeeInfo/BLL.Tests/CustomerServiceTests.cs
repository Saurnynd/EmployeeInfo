using BLL.Services.Impl;
using BLL.Services.Interfaces;
using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using CLL.Security;
using CLL.Security.Identity;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
namespace BLL.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void Ctor_InputNull_ThrowArgumentNullException()
        {
            // Arrange
            IUnitOfWork nullUnitOfWork = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new WorkerService(nullUnitOfWork));
        }

        [Fact]
        public void GetCustomers_UserIsAdmin_ThrowMethodAccessException()
        {
            // Arrange
            User user = new Worker(1, "test");
            SecurityContext.SetUser(user);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            IWorkerService workerService = new WorkerService(mockUnitOfWork.Object);

            // Act
            // Assert
            Assert.Throws<MethodAccessException>(() => workerService.GetEmployee(0));
        }

        [Fact]
        public void GetEmployees_EmployeesFromDAL_CorrectMappingToEmployeesDTO()
        {
            // Arrange
            User user = new Admin(1, "test");
            SecurityContext.SetUser(user);
            var workerService = GetWorkerService();

            // Act
            var actualEmployeeDto = workerService.GetEmployee(0).First();

            // Assert
            Assert.True(
                actualEmployeeDto.EmployeeID == 1
                && actualEmployeeDto.Name == "testN"
                && actualEmployeeDto.Surname == "testD"
                && actualEmployeeDto.Position == 5
                );
        }

        IWorkerService GetWorkerService()
        {
            var mockContext = new Mock<IUnitOfWork>();
            var expectedEmployee = new Employee() { EmployeeID = 1, Name = "testN", Surname = "testD", Position = 5};
            var mockDbSet = new Mock<IEmployeeRepository>();
            mockDbSet.Setup(z =>
                z.Find(
                    It.IsAny<Func<Employee, bool>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                  .Returns(
                    new List<Employee>() { expectedEmployee }
                    );
            mockContext
                .Setup(context =>
                    context.Employees)
                .Returns(mockDbSet.Object);


            IWorkerService workerService = new WorkerService(mockContext.Object);

            return workerService;
        }
    }
}
