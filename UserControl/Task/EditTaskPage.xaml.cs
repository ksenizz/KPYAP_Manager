using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UsersApp.Models;
using UsersApp.Services;

namespace UsersApp.UserControl.Task
{
    /// <summary>
    /// Логика взаимодействия для EditTaskPage.xaml
    /// </summary>
    public partial class EditTaskPage : System.Windows.Controls.UserControl
    {
        private readonly DB _context;
        private readonly TaskService _taskService;
        private readonly EmployeeService _employeeService;
        private readonly ProjectService _projectService;
        private readonly ContentControl _overlayContentControl;
        private bool _disposed;
        private UsersApp.Models.Employee currentEmployee;

        public event EventHandler<Tasks> TaskUpdated;
        public EditTaskPage(DB context, Tasks task, ContentControl overlayContentControl)
        {
            InitializeComponent();
            _context = context;
            _taskService = new TaskService(_context);
            _employeeService = new EmployeeService(_context);
            _projectService = new ProjectService(_context);

            DataContext = task;

            LoadEmployees();
            LoadTaskStatuses();
            LoadProjects();

            Unloaded += EditTaskPage_Unloaded;
            _overlayContentControl = overlayContentControl;
 
        }
        public EditTaskPage(DB context, Tasks task, ContentControl overlayContentControl, Models.Employee employee)
        {
            InitializeComponent();
            _context = context;
            _taskService = new TaskService(_context);
            _employeeService = new EmployeeService(_context);
            _projectService = new ProjectService(_context);
            currentEmployee = employee;

            DataContext = task;

            LoadEmployees();
            LoadTaskStatuses();
            LoadProjects();

            Unloaded += EditTaskPage_Unloaded;
            _overlayContentControl = overlayContentControl;
            
        }
        private void EditTaskPage_Unloaded(object sender, RoutedEventArgs e)
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

        private void SaveTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataContext is Tasks task)
                {
                    _taskService.UpdateTask(task);
                    TaskUpdated?.Invoke(this, task);

                    MessageBox.Show("Изменения успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    _overlayContentControl.Content = new TaskDetailPage(_context, task, _overlayContentControl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _overlayContentControl.Content = new TaskPage(currentEmployee);
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





