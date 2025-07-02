using System;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;
using UsersApp.UserControl.Employee;

namespace UsersApp.UserControl
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly EmployeeService _employeeService;
        private readonly DB _context;
        private bool _disposed;

        public EmployeePage()
        {
            InitializeComponent();
            _context = new DB();
            _employeeService = new EmployeeService(_context);
            LoadEmployees();
            Unloaded += EmployeePage_Unloaded;
        }

        private void EmployeePage_Unloaded(object sender, RoutedEventArgs e)
        {
            Dispose();
        }

        private void LoadEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            EmployeeListView.ItemsSource = employees;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newUserControl = new NewUser(_context);
            newUserControl.NewEmployeeAdded += (s, employee) =>
            {
                _employeeService.AddEmployee(employee);
                LoadEmployees();
            };
            NewUser.Content = newUserControl;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }

        private void EmployeeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int employeeId = (int)button.Tag;
                var employee = _employeeService.GetEmployeeById(employeeId);

                var editUserControl = new EditUser(_context, employee, NewUser);
                NewUser.Content = editUserControl;
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int employeeId = (int)button.Tag;

                MessageBoxResult result = MessageBox.Show(
                    "Вы уверены, что хотите удалить этого сотрудника?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _employeeService.DeleteEmployee(employeeId);
                    LoadEmployees();
                }
            }
        }

        private void ShowAllEmployees_Click(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
        }

        private void FilterDesigners_Click(object sender, RoutedEventArgs e)
        {
            var designers = _employeeService.GetAllDesigners();
            EmployeeListView.ItemsSource = designers;
        }

        private void FilterDevelopers_Click(object sender, RoutedEventArgs e)
        {
            var developers = _employeeService.GetAllDevelopers();
            EmployeeListView.ItemsSource = developers;
        }


    }
}