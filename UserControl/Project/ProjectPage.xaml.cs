using System;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;
using UsersApp.UserControl.Project;

namespace UsersApp.UserControl
{
    public partial class ProjectPage : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly DB _context;
        private readonly ProjectService _projectService;
        private bool _disposed;
        private UsersApp.Models.Employee currentEmployee;

        public ProjectPage()
        {
            InitializeComponent();
            _context = new DB();
            _projectService = new ProjectService(_context);
            LoadProjects();
            Unloaded += ProjectPage_Unloaded;
        }


        public ProjectPage(UsersApp.Models.Employee employee)
        {
            InitializeComponent();
            _context = new DB();
            _projectService = new ProjectService(_context);
            currentEmployee = employee;
            LoadProjects();
            Unloaded += ProjectPage_Unloaded;
        }

        private void ProjectPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void LoadProjects()
        {
            var projects = _projectService.GetAllProjects();
            ProjectsListView.ItemsSource = projects;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Role.Name == "Менеджер")
            {
                var projectsControl = new NewProjectPage(_context, OverlayContentControl);
                projectsControl.ProjectCreated += (s, project) =>
                {
                    LoadProjects();
                };
                OverlayContentControl.Content = projectsControl;
            }
            else
            {
                MessageBox.Show("Нет доступа");
            }

        }
        private void EditProject_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee?.Role?.Name == "Менеджер")
            {
                var button = sender as Button;
                if (button != null && button.Tag != null)
                {
                    if (int.TryParse(button.Tag.ToString(), out int projectId))
                    {
                        var project = _projectService.GetProjectById(projectId);
                        if (project != null)
                        {
                            var projectPanelPage = new ProjectPanel(_context, project, OverlayContentControl);
                            OverlayContentControl.Content = projectPanelPage;
                        }
                        else
                        {
                            MessageBox.Show("Проект не найден");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Некорректные данные проекта");
                }
            }
            else
            {
                MessageBox.Show("Нет доступа");
            }
        }

        private void DeleteProject_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Role.Name == "Менеджер")
            {
                var button = sender as Button;
                if (button != null)
                {
                    int projectId = (int)button.Tag;
                    _projectService.DeleteProject(projectId);
                    LoadProjects();
                }
            }
            else
            {
                MessageBox.Show("Нет доступа");
            }
        }

        private void ProjectItem_Click(object sender, RoutedEventArgs e)
        {
            var border = sender as Border;
            if (border != null)
            {
                var project = border.DataContext as UsersApp.Models.Project;
                if (project != null)
                {
                    var projectDetailPage = new ProjectDetailPage(_context, project, OverlayContentControl);
                    OverlayContentControl.Content = projectDetailPage;
                }
            }
        }
        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }
    }
}
