using System;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;

namespace UsersApp.UserControl
{
    public partial class NewTaskPage : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly DB _context;
        private readonly TaskService _taskService;
        private readonly EmployeeService _employeeService;
        private readonly ProjectService _projectService;
        private bool _disposed;
        private readonly ContentControl _overlayContentControl;
        
        public event EventHandler<Tasks> TaskCreated;

        public NewTaskPage(DB context, ContentControl overlayContentControl)
        {
            InitializeComponent();
            _context = context;
            _taskService = new TaskService(_context);
            _employeeService = new EmployeeService(_context);
            _projectService = new ProjectService(_context);

            LoadEmployees();
            LoadTaskStatuses();
            LoadProjects();

            Unloaded += NewTaskPage_Unloaded;
            _overlayContentControl = overlayContentControl;
        }

        private void NewTaskPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        private void LoadEmployees()
        {
            var employees = _employeeService.GetAllDevelopersAndDesigners();
            ExecutorComboBox.ItemsSource = employees;
        }

        private void LoadTaskStatuses()
        {
            var statuses = _taskService.GetAllTaskStatuses();
            StatusComboBox.ItemsSource = statuses;
        }

        private void LoadProjects()
        {
            var projects = _projectService.GetAllProjects();
            ProjectComboBox.ItemsSource = projects;
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TaskNameTextBox.Text) ||
                    string.IsNullOrEmpty(TaskDescriptionTextBox.Text) ||
                    DeadlineDatePicker.SelectedDate == null ||
                    StatusComboBox.SelectedItem == null ||
                    ExecutorComboBox.SelectedItem == null ||
                    ProjectComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

                var newTask = new Tasks
                {
                    Name = TaskNameTextBox.Text,
                    Description = TaskDescriptionTextBox.Text,
                    Deadline = DeadlineDatePicker.SelectedDate.Value,
                    StatusId = (StatusComboBox.SelectedItem as TaskStatus).Id,
                    EmployeeId = (ExecutorComboBox.SelectedItem as UsersApp.Models.Employee).Id,
                    ProjectId = (ProjectComboBox.SelectedItem as UsersApp.Models.Project).Id
                };

                _taskService.AddTask(newTask);
                TaskCreated?.Invoke(this, newTask);

                TaskNameTextBox.Clear();
                TaskDescriptionTextBox.Clear();
                DeadlineDatePicker.SelectedDate = null;
                StatusComboBox.SelectedIndex = -1;
                ExecutorComboBox.SelectedIndex = -1;
                ProjectComboBox.SelectedIndex = -1;

                MessageBox.Show("Задача успешно создана!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
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
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _overlayContentControl.Content = new TaskPage();

        }
    }
}
