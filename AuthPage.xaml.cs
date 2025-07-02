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

namespace UsersApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private EmployeeService _employeeService;
        private DB _context;

        public MainWindow()
        {
            InitializeComponent();
            _context = new DB();
            _employeeService = new EmployeeService(_context);
        }

        private void Button_Auth2_Click(object sender, RoutedEventArgs e)
        {
            var allEmployees = _employeeService.GetAllEmployees();

            string login = textBox_1.Text;
            string password = password_1.Password;

            Employee currentEmployee = allEmployees.FirstOrDefault(i => i.FIO == login);


            if (currentEmployee != null)
            {
                if (VerifyPas(password, currentEmployee.Password))
                {
                    switch (currentEmployee.Role.Name)
                    {
                        case "Менеджер":
                            var managerWindow = new AuthWindow(currentEmployee);
                            managerWindow.Show();
                            this.Close();
                            break;
                        case "Разработчик":
                            var developerWindow = new AuthWindow(currentEmployee);
                            developerWindow.Show();
                            this.Close();
                            break;
                        case "Дизайнер":
                            var designerWindow = new AuthWindow(currentEmployee);
                            designerWindow.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Роль не определена");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверные данные для входа");
                }

            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        public static bool VerifyPas(string password, string hash)
        {
            
            return BCrypt.Net.BCrypt.Verify(password, hash);

        }
    }
}
