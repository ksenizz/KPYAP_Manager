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
using UsersApp.UserControl.Task;

namespace UsersApp.UserControl
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class TaskPage : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly DB _context;
        private readonly TaskService _taskService;
        private bool _disposed;
        private UsersApp.Models.Employee currentEmployee;

        public TaskPage()
        {
            InitializeComponent();
            _context = new DB();
            _taskService = new TaskService(_context);
            LoadTasks();
            Unloaded += TaskPage_Unloaded;
        }

        public TaskPage(UsersApp.Models.Employee employee)
        {
            InitializeComponent();
            _context = new DB();
            _taskService = new TaskService(_context);
            currentEmployee = employee;
            LoadTasks();
            Unloaded += TaskPage_Unloaded;
        }

        private void TaskPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void LoadTasks()
        {
            var tasks = _taskService.GetAllTasks();
            TasksListView.ItemsSource = tasks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Role.Name == "Менеджер")
            {
                var taskControl = new NewTaskPage(_context, OverlayContentControl);
                taskControl.TaskCreated += (s, task) =>
                {
                    LoadTasks();
                };
                OverlayContentControl.Content = taskControl;
            }
            else
            {
                MessageBox.Show("Нет доступа");
            }
        }
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int taskId = (int)button.Tag;
                var task = _taskService.GetTaskById(taskId);
                if (task != null)
                {
                    var editTaskPage = new EditTaskPage(_context, task, OverlayContentControl);
                    editTaskPage.TaskUpdated += (s, updatedTask) =>
                    {
                        LoadTasks();
                    };
                    OverlayContentControl.Content = editTaskPage;
                }
                else
                {
                    MessageBox.Show("Задача не найдена");
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (currentEmployee.Role.Name == "Менеджер")
            {
                var button = sender as Button;
            if (button != null)
            {
                int taskId = (int)button.Tag;
                _taskService.DeleteTask(taskId);
                LoadTasks();
            }
            }
            else
            {
                MessageBox.Show("Нет доступа");
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
