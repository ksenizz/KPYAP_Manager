using System;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;

namespace UsersApp.UserControl
{
    public partial class ProjectPanel : System.Windows.Controls.UserControl
    {
        private readonly DB _context;
        private readonly UsersApp.Models.Project _project;
        private readonly ProjectService _projectService;
        private readonly ContentControl _overlayContentControl;
        private UsersApp.Models.Employee currentEmployee;

        public ProjectPanel(DB context, UsersApp.Models.Project project, ContentControl overlayContentControl)
        {
            InitializeComponent();
            _context = context;
            _project = project;
            _overlayContentControl = overlayContentControl;
            _projectService = new ProjectService(_context);

            if (_project.Employees == null)
            {
                _context.Entry(_project).Reference(p => p.Employees).Load();
            }

            DataContext = _project;
            LoadEmployees();
            LoadProjectStatuses();
        }

        public ProjectPanel(DB context, UsersApp.Models.Project project, ContentControl overlayContentControl, UsersApp.Models.Employee employee)
        {
            InitializeComponent();
            _context = context;
            _project = project;
            _overlayContentControl = overlayContentControl;
            _projectService = new ProjectService(_context);
            currentEmployee = employee;

            if (_project.Employees == null)
            {
                _context.Entry(_project).Reference(p => p.Employees).Load();
            }

            DataContext = _project;
            LoadEmployees();
            LoadProjectStatuses();
        }
        private void LoadEmployees()
        {
            var employeeService = new EmployeeService(_context);
            var employees = employeeService.GetAllEmployees();
            ExecutorComboBox.ItemsSource = employees;
        }

        private void LoadProjectStatuses()
        {
            var statuses = _projectService.GetAllProjectStatuses();
            StatusComboBox.ItemsSource = statuses;
        }

        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {
            if (ExecutorComboBox.SelectedItem is UsersApp.Models.Employee selectedEmployee)
            {
                _project.EmployeeId = selectedEmployee.Id;
                _project.Employees = selectedEmployee; 
            }

            _projectService.UpdateProject(_project);
            MessageBox.Show("Проект успешно сохранен!");
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _overlayContentControl.Content = new ProjectPage(currentEmployee);
        }
    }
}
