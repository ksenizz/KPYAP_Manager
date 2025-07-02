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
using UsersApp.UserControl.Project;
using UsersApp.UserControl.Task;
using LiveCharts;
using LiveCharts.Wpf;


namespace UsersApp.UserControl
{
    public partial class StaticPage : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly DB _context;
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
        private UsersApp.Models.Employee currentEmployee;
        private readonly EmployeeService _employeeService;
        private bool _disposed;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Months { get; set; }
        public ChartValues<int> ProjectCounts { get; set; }


        public StaticPage(UsersApp.Models.Employee employee)
        {
            InitializeComponent();
            _context = new DB();
            _projectService = new ProjectService(_context);
            _taskService = new TaskService(_context);
            _employeeService = new EmployeeService(_context);

            currentEmployee = employee;
            LoadProjects();
            LoadTasks();
            LoadChartData();
            LoadEmployees();

            DataContext = this;
            Unloaded += StaticPage_Unloaded;
        }

        private void StaticPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }
        private void LoadChartData()
        {
            var projects = _projectService.GetAllProjects();

            // Группировка проектов по месяцам
            var projectsByMonth = projects
                .GroupBy(p => p.CreatedDate.Month)
                .OrderBy(g => g.Key);

            Months = projectsByMonth.Select(g => new DateTime(2023, g.Key, 1).ToString("MMMM")).ToArray();
            ProjectCounts = new ChartValues<int>(projectsByMonth.Select(g => g.Count()));
        }

        private void LoadProjects()
        {
            var projects = _projectService.GetAllProjects();
            ProjectsListView.ItemsSource = projects;
            ProjectsCountTextBlock.Text = $"Количество проектов: {projects.Count()}";
        }

        private void LoadTasks()
        {
            var tasks = _taskService.GetAllTasks();
            TasksListView.ItemsSource = tasks;
            TasksCountTextBlock.Text = $"Количество задач: {tasks.Count()}";
        }

        private void LoadEmployees()
        {
            var employees = _employeeService.GetAllEmployees(); // Предполагается, что у вас есть такой метод
            EmployeesCountTextBlock.Text = $"Количество сотрудников: {employees.Count()}";
        } 

        private void Button_Employee(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Role.Name == "Менеджер")
            {
                var employeeControl = new EmployeePage();
                StaticControl.Content = employeeControl;
            }
            else
            {
                MessageBox.Show ( "Нет доступа");
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
                    var projectDetail = _projectService.GetProjectById(project.Id);
                    if (projectDetail != null)
                    {
                        var projectDetailPage = new ProjectDetailPage(_context, projectDetail, OverlayContentControl);
                        OverlayContentControl.Content = projectDetailPage;
                    }
                }
            }
        }

        private void TaskItem_Click(object sender, RoutedEventArgs e)
        {
            var border = sender as Border;
            if (border != null)
            {
                var task = border.DataContext as Tasks;
                if (task != null)
                {
                    var taskDetailPage = new TaskDetailPage(_context, task, OverlayContentControl);
                    OverlayContentControl.Content = taskDetailPage;
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
