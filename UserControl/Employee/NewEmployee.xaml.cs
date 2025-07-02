using System;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;

namespace UsersApp.UserControl
{
    public partial class NewUser : System.Windows.Controls.UserControl
    {
        private readonly DB _context;
        private readonly RoleService _roleService;

        public event EventHandler<UsersApp.Models.Employee> NewEmployeeAdded;

        public NewUser(DB context)
        {
            InitializeComponent();
            _context = context;
            _roleService = new RoleService(_context);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstNameTextBox.Text) ||
                string.IsNullOrEmpty(LastNameTextBox.Text) ||
                string.IsNullOrEmpty(ProfessionTextBox.Text) ||
                string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите данные во все поля.");
                return;
            }

            int roleId = _roleService.GetRoleIdByName(ProfessionTextBox.Text);



            var newEmployee = new UsersApp.Models.Employee
            {
                FIO = $"{FirstNameTextBox.Text} {LastNameTextBox.Text}",
                Role_Id = roleId,
                Password = HashPassword(PasswordBox.Password)

            };

            NewEmployeeAdded?.Invoke(this, newEmployee);

            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            ProfessionTextBox.Clear();
            PasswordBox.Clear();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var parent = Parent as ContentControl;
            if (parent != null)
            {
                parent.Content = null;
            }
        }

        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPas = BCrypt.Net.BCrypt.HashPassword(password,salt);
            return hashedPas;
        }
    }
}
