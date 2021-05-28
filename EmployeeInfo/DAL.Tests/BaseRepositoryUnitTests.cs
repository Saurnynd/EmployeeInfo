using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using DAL.Repositories.Impl;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Tests
{
    class TestEmployeeRepository
        : BaseRepository<Employee>
    {
        public TestEmployeeRepository(DbContext context)
            : base(context)
        {
        }
    }
    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputStreetInstance_CalledAddMethodOfDBSetWithStreetInstance()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<EmployeeInfoContext>()
                .Options;
            var mockContext = new Mock<EmployeeInfoContext>(opt);
            var mockDbSet = new Mock<DbSet<Employee>>();
            mockContext
                .Setup(context =>
                    context.Set<Employee>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            var repository = new TestEmployeeRepository(mockContext.Object);

            Employee expectedEmployee = new Mock<Employee>().Object;

            //Act
            repository.Create(expectedEmployee);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedEmployee
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<EmployeeInfoContext>()
                .Options;
            var mockContext = new Mock<EmployeeInfoContext>(opt);
            var mockDbSet = new Mock<DbSet<Employee>>();
            mockContext
                .Setup(context =>
                    context.Set<Employee>(
                        ))
                .Returns(mockDbSet.Object);
            //EFUnitOfWork uow = new EFUnitOfWork(mockContext.Object);
            //IStreetRepository repository = uow.Streets;
            var repository = new TestEmployeeRepository(mockContext.Object);

            Employee expectedEmployee = new Employee() { EmployeeID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedEmployee.EmployeeID)).Returns(expectedEmployee);

            //Act
            repository.Delete(expectedEmployee.EmployeeID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedEmployee.EmployeeID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedEmployee
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
            // Arrange
            DbContextOptions opt = new DbContextOptionsBuilder<EmployeeInfoContext>()
                .Options;
            var mockContext = new Mock<EmployeeInfoContext>(opt);
            var mockDbSet = new Mock<DbSet<Employee>>();
            mockContext
                .Setup(context =>
                    context.Set<Employee>(
                        ))
                .Returns(mockDbSet.Object);

            Employee expectedStreet = new Employee() { EmployeeID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedStreet.EmployeeID))
                    .Returns(expectedStreet);
            var repository = new TestEmployeeRepository(mockContext.Object);

            //Act
            var actualStreet = repository.Get(expectedStreet.EmployeeID);

            // Assert
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedStreet.EmployeeID
                    ), Times.Once());
            Assert.Equal(expectedStreet, actualStreet);
        }


    }
}
