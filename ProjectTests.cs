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
    public class ProjectServiceTests
    {
        private Mock<DB> _mockContext;
        private ProjectService _projectService;
        private List<Project> _projects;
        private List<ProjectStatus> _projectStatuses;

        [TestInitialize]
        public void TestInitialize()
        {
            // Создаем тестовые данные для проектов
            _projects = new List<Project>
            {
                new Project { Id = 1, Name = "Проект 1", Description = "Описание проекта 1", ProjectStatusId = 1 },
                new Project { Id = 2, Name = "Проект 2", Description = "Описание проекта 2", ProjectStatusId = 2 }
            };

            // Создаем тестовые данные для статусов проектов
            _projectStatuses = new List<ProjectStatus>
            {
                new ProjectStatus { Id = 1, Name = "Активен" },
                new ProjectStatus { Id = 2, Name = "Завершен" }
            };

            // Создаем мок для DbSet<Project>
            var mockProjectSet = CreateMockDbSet(_projects);

            // Создаем мок для DbSet<ProjectStatus>
            var mockProjectStatusSet = CreateMockDbSet(_projectStatuses);

            // Настройка мока для DbContext
            _mockContext = new Mock<DB>();
            _mockContext.Setup(c => c.Projects).Returns(mockProjectSet.Object);
            _mockContext.Setup(c => c.ProjectStatus).Returns(mockProjectStatusSet.Object);

            // Настройка метода Add для DbSet<Project>
            _mockContext.Setup(c => c.Projects.Add(It.IsAny<Project>()))
                .Callback<Project>(p => _projects.Add(p));

            // Настройка метода SaveChanges для DbContext
            _mockContext.Setup(c => c.SaveChanges())
                .Callback(() => { })
                .Returns(0);

            // Настройка метода Remove для DbSet<Project>
            _mockContext.Setup(c => c.Projects.Remove(It.IsAny<Project>()))
                .Callback<Project>(p => _projects.Remove(p));

            // Настройка метода Find для DbSet<Project>
            _mockContext.Setup(c => c.Projects.Find(It.IsAny<object[]>()))
                .Returns<object[]>(ids => _projects.FirstOrDefault(p => p.Id == (int)ids[0]));

            // Создаем экземпляр ProjectService с мок-контекстом
            _projectService = new ProjectService(_mockContext.Object);
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
        public void GetAllProjects_ShouldReturnAllProjects()
        {
            // Act
            var result = _projectService.GetAllProjects();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetProjectById_ShouldReturnCorrectProject()
        {
            // Act
            var result = _projectService.GetProjectById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Проект 1", result.Name);
        }

        [TestMethod]
        public void AddProject_ShouldAddProjectSuccessfully()
        {
            // Arrange
            var newProject = new Project { Id = 3, Name = "Новый проект", Description = "Описание нового проекта", ProjectStatusId = 1 };

            // Act
            _projectService.AddProject(newProject);

            // Assert
            Assert.AreEqual(3, _projects.Count);
            Assert.AreEqual("Новый проект", _projects.Last().Name);
        }

        [TestMethod]
        public void UpdateProject_ShouldUpdateProjectSuccessfully()
        {
            // Arrange
            var updatedProject = new Project { Id = 1, Name = "Проект 1", Description = "Описание обновленного проекта", ProjectStatusId = 1 };

            // Act
            _projectService.UpdateProject(updatedProject);

            // Assert
            var project = _projects.FirstOrDefault(p => p.Id == 1);
            Assert.IsNotNull(project);
            Assert.AreEqual("Проект 1", project.Name);
        }

        [TestMethod]
        public void DeleteProject_ShouldDeleteProjectSuccessfully()
        {
            // Arrange
            var projectId = 1;

            // Act
            _projectService.DeleteProject(projectId);

            // Assert
            var project = _projects.FirstOrDefault(p => p.Id == projectId);
            Assert.IsNull(project);
        }

        [TestMethod]
        public void GetAllProjectStatuses_ShouldReturnAllProjectStatuses()
        {
            // Act
            var result = _projectService.GetAllProjectStatuses();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetProjectByStatus_ShouldReturnProjectsWithSpecificStatus()
        {
            // Act
            var result = _projectService.GetAllProjects().Where(p => p.ProjectStatusId == 1).ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Проект 1", result[0].Name);
        }

        [TestMethod]
        public void UpdateProjectStatus_ShouldUpdateProjectStatusSuccessfully()
        {
            // Arrange
            var project = _projects.FirstOrDefault(p => p.Id == 1);
            project.ProjectStatusId = 2;

            // Act
            _projectService.UpdateProject(project);

            // Assert
            var updatedProject = _projects.FirstOrDefault(p => p.Id == 1);
            Assert.IsNotNull(updatedProject);
            Assert.AreEqual(2, updatedProject.ProjectStatusId);
        }

    }
}