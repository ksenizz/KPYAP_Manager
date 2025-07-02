using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UsersApp.Models;
using UsersApp.Services;
using TaskStatus = UsersApp.Models.TaskStatus;

namespace TestProject1
{
    [TestClass]
    public class TaskServiceTests
    {
        private Mock<DB> _mockContext;
        private TaskService _taskService;
        private List<Tasks> _tasks;
        private List<TaskStatus> _taskStatuses;
        private List<Employee> _employees;
        private List<Project> _projects;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create test data
            _taskStatuses = new List<TaskStatus>
            {
                new TaskStatus { Id = 1, Name = "Pending" },
                new TaskStatus { Id = 2, Name = "In Progress" },
                new TaskStatus { Id = 3, Name = "Completed" }
            };

            _employees = new List<Employee>
            {
                new Employee { Id = 1, FIO = "Иван Иванов" },
                new Employee { Id = 2, FIO = "Мария Сидорова" }
            };

            _projects = new List<Project>
            {
                new Project { Id = 1, Name = "Project A" },
                new Project { Id = 2, Name = "Project B" }
            };

            _tasks = new List<Tasks>
            {
                new Tasks { Id = 1, Name = "Task 1", Description = "Description 1", Deadline = DateTime.Now.AddDays(10), StatusId = 1, EmployeeId = 1, ProjectId = 1, Status = _taskStatuses[0], Employee = _employees[0], Project = _projects[0] },
                new Tasks { Id = 2, Name = "Task 2", Description = "Description 2", Deadline = DateTime.Now.AddDays(5), StatusId = 2, EmployeeId = 2, ProjectId = 2, Status = _taskStatuses[1], Employee = _employees[1], Project = _projects[1] }
            };

            // Create mock DbSets
            var mockTaskSet = CreateMockDbSet(_tasks);
            var mockTaskStatusSet = CreateMockDbSet(_taskStatuses);
            var mockEmployeeSet = CreateMockDbSet(_employees);
            var mockProjectSet = CreateMockDbSet(_projects);

            // Setup mock for DbContext
            _mockContext = new Mock<DB>();
            _mockContext.Setup(c => c.Tasks).Returns(mockTaskSet.Object);
            _mockContext.Setup(c => c.TaskStatus).Returns(mockTaskStatusSet.Object);
            _mockContext.Setup(c => c.Employes).Returns(mockEmployeeSet.Object);
            _mockContext.Setup(c => c.Projects).Returns(mockProjectSet.Object);

            // Setup Add method for DbSet
            _mockContext.Setup(c => c.Tasks.Add(It.IsAny<Tasks>()))
                .Callback<Tasks>(t => _tasks.Add(t));

            // Setup SaveChanges method for DbContext
            _mockContext.Setup(c => c.SaveChanges())
                .Callback(() => { })
                .Returns(0);

            // Setup Remove method for DbSet
            _mockContext.Setup(c => c.Tasks.Remove(It.IsAny<Tasks>()))
                .Callback<Tasks>(t => _tasks.Remove(t));

            // Setup Find method for DbSet
            _mockContext.Setup(c => c.Tasks.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => _tasks.FirstOrDefault(t => t.Id == (int)ids[0]));

            // Create an instance of TaskService with the mock context
            _taskService = new TaskService(_mockContext.Object);
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
        public void GetAllTasks_ShouldReturnAllTasks()
        {
            // Act
            var result = _taskService.GetTaskById(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Task 2", result.Name);
        }

        [TestMethod]
        public void GetTaskById_ShouldReturnCorrectTask()
        {
            // Act
            var result = _taskService.GetTaskById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Task 1", result.Name);
        }

        [TestMethod]
        public void AddTask_ShouldAddTaskSuccessfully()
        {
            // Arrange
            var newTask = new Tasks { Id = 3, Name = "Task 3", Description = "Description 3", Deadline = DateTime.Now.AddDays(1), StatusId = 3, EmployeeId = 1, ProjectId = 1 };

            // Act
            _taskService.AddTask(newTask);

            // Assert
            Assert.AreEqual(3, _tasks.Count);
            Assert.AreEqual("Task 3", _tasks.Last().Name);
        }

        [TestMethod]
        public void DeleteTask_ShouldDeleteTaskSuccessfully()
        {
            // Arrange
            var taskId = 1;

            // Act
            _taskService.DeleteTask(taskId);

            // Assert
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            Assert.IsNull(task);
        }

        [TestMethod]
        public void DeleteTask2_ShouldDeleteTaskSuccessfully()
        {
            // Arrange
            var taskId = 2;

            // Act
            _taskService.DeleteTask(taskId);

            // Assert
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            Assert.IsNull(task);
        }

    }
}
