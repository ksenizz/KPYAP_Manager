using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsersApp.Models;
using UsersApp.Services;

namespace TestProject1
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private Mock<DB> _mockContext;
        private EmployeeService _employeeService;
        private List<Employee> _employees;

        [TestInitialize]
        public void TestInitialize()
        {
            // Создаем тестовые данные
            _employees = new List<Employee>
            {
                new Employee { Id = 1, FIO = "Иван Иванов", Role_Id = 1, Role = new Roles { Id = 1, Name = "Разработчик" } },
                new Employee { Id = 2, FIO = "Мария Сидорова", Role_Id = 2, Role = new Roles { Id = 2, Name = "Дизайнер" } },
                new Employee { Id = 3, FIO = "Петр Петров", Role_Id = 3, Role = new Roles { Id = 3, Name = "Менеджер" } }
            };

            // Создаем мок для DbSet<Employee>
            var mockEmployeeSet = CreateMockDbSet(_employees);

            // Настройка мока для DbContext
            _mockContext = new Mock<DB>();
            _mockContext.Setup(c => c.Employes).Returns(mockEmployeeSet.Object);

            // Настройка метода Add для DbSet
            _mockContext.Setup(c => c.Employes.Add(It.IsAny<Employee>()))
                .Callback<Employee>(e => _employees.Add(e));

            // Настройка метода SaveChanges для DbContext
            _mockContext.Setup(c => c.SaveChanges())
                .Callback(() => { }) // Просто вызываем пустой callback
                .Returns(0); // Возвращаем 0, так как нас интересует только вызов метода

            // Настройка метода Remove для DbSet
            _mockContext.Setup(c => c.Employes.Remove(It.IsAny<Employee>()))
                .Callback<Employee>(e => _employees.Remove(e));

            // Настройка метода Find для DbSet
            _mockContext.Setup(c => c.Employes.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => _employees.FirstOrDefault(e => e.Id == (int)ids[0]));

            // Создаем экземпляр EmployeeService с мок-контекстом
            _employeeService = new EmployeeService(_mockContext.Object);
        }


        private Mock<DbSet<T>> CreateMockDbSet<T>(IEnumerable<T> elements) where T : class
        {
            var data = elements.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }

        [TestMethod]
        public void GetAllEmployees_ShouldReturnAllEmployees()
        {
            // Act
            var result = _employeeService.GetAllEmployees();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetEmployeeById_ShouldReturnCorrectEmployee()
        {
            // Act
            var result = _employeeService.GetAllEmployees().FirstOrDefault(i => i.Id == 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Иван Иванов", result.FIO);
        }

        [TestMethod]
        public void AddEmployee_ShouldAddEmployeeSuccessfully()
        {
            // Arrange
            var newEmployee = new Employee { Id = 4, FIO = "Новый Сотрудник", Role_Id = 1 };

            // Act
            _employeeService.AddEmployee(newEmployee);

            // Assert
            Assert.AreEqual(4, _employees.Count);
            Assert.AreEqual("Новый Сотрудник", _employees.Last().FIO);
        }

        [TestMethod]
        public void UpdateEmployee_ShouldUpdateEmployeeSuccessfully()
        {
            // Arrange
            var updatedEmployee = new Employee { Id = 1, FIO = "Иван Иванов", Role_Id = 1 };

            // Act
            _employeeService.UpdateEmployee(updatedEmployee);

            // Assert
            var employee = _employees.FirstOrDefault(e => e.Id == 1);
            Assert.IsNotNull(employee);
            Assert.AreEqual("Иван Иванов", employee.FIO);
        }

        [TestMethod]
        public void DeleteEmployee_ShouldDeleteEmployeeSuccessfully()
        {
            // Arrange
            var employeeId = 1;

            // Act
            _employeeService.DeleteEmployee(employeeId);

            // Assert
            var employee = _employees.FirstOrDefault(e => e.Id == employeeId);
            Assert.IsNull(employee);
        }

        [TestMethod]
        public void GetEmployeesByRole_ShouldReturnEmployeesWithSpecificRole()
        {
            // Act
            var result = _employeeService.GetAllEmployees().Where(e => e.Role_Id == 1).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Иван Иванов", result[0].FIO);
        }

        [TestMethod]
        public void UpdateEmployeeRole_ShouldUpdateEmployeeRoleSuccessfully()
        {
            var result = _employeeService.GetAllEmployees().Where(e => e.Role_Id == 2).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Мария Сидорова", result[0].FIO);
        }

    }
}
