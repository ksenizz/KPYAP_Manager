using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;

namespace UsersApp.UserControl
{
    public partial class NewProjectPage : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly DB _context;
        private readonly ContentControl _overlayContentControl;
        private readonly EmployeeService _employeeService;
        private readonly ProjectService _projectService;
        private bool _disposed;

        public event EventHandler< UsersApp.Models.Project> ProjectCreated;

        public NewProjectPage(DB context, ContentControl overlayContentControl)
        {
            InitializeComponent();
            _context = context;
            _overlayContentControl = overlayContentControl;
            _employeeService = new EmployeeService(_context);
            _projectService = new ProjectService(_context);
            LoadEmployees();
            LoadProjectStatuses();
            Unloaded += NewProjectPage_Unloaded;
        }

        private void NewProjectPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void LoadEmployees()
        {
            var employees = _employeeService.GetAllManagers();
            ExecutorComboBox.ItemsSource = employees;
        }

        private void LoadProjectStatuses()
        {
            var statuses = _projectService.GetAllProjectStatuses();
            StatusComboBox.ItemsSource = statuses;
        }
        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ProjectNameTextBox.Text) ||
                    string.IsNullOrEmpty(ProjectDescriptionTextBox.Text) ||
                    StartDatePicker.SelectedDate == null ||
                    EndDatePicker.SelectedDate == null ||
                    StatusComboBox.SelectedItem == null ||
                    ExecutorComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                var newProject = new UsersApp.Models.Project
                {
                    Name = ProjectNameTextBox.Text,
                    Description = ProjectDescriptionTextBox.Text,
                    CreatedDate = StartDatePicker.SelectedDate.Value,
                    UpdatedDate = EndDatePicker.SelectedDate.Value,
                    ProjectStatus = StatusComboBox.SelectedItem as ProjectStatus,
                    EmployeeId = (ExecutorComboBox.SelectedItem as UsersApp.Models.Employee).Id,
                    Employees = ExecutorComboBox.SelectedItem as UsersApp.Models.Employee 
                };

                _projectService.AddProject(newProject);
                ProjectCreated?.Invoke(this, newProject);

                ProjectNameTextBox.Clear();
                ProjectDescriptionTextBox.Clear();
                StartDatePicker.SelectedDate = null;
                EndDatePicker.SelectedDate = null;
                StatusComboBox.SelectedIndex = -1;
                ExecutorComboBox.SelectedIndex = -1;

                MessageBox.Show("Проект успешно создан!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _overlayContentControl.Content = new ProjectPage();
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
