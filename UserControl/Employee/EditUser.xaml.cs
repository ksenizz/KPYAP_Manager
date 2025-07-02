using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UsersApp.Models;
using UsersApp.Services;

namespace UsersApp.UserControl.Employee
{
    public partial class EditUser : System.Windows.Controls.UserControl
    {
        private readonly DB _context;
        private readonly UsersApp.Models.Employee _employee;
        private readonly ContentControl _parentContentControl;

        public EditUser(DB context, UsersApp.Models.Employee employee, ContentControl parentContentControl)
        {
            InitializeComponent();
            _context = context;
            _employee = employee;
            _parentContentControl = parentContentControl;

            FIOTextBox.Text = employee.FIO;
            
            var roles = _context.Roles.ToList();
            RoleComboBox.ItemsSource = roles;
            RoleComboBox.DisplayMemberPath = "Name";
            RoleComboBox.SelectedItem = roles.FirstOrDefault(r => r.Id == employee.Role_Id);
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            _employee.FIO = FIOTextBox.Text;
            _employee.Password = HashPassword(PasswordBox.Password);

            if (RoleComboBox.SelectedItem is Roles selectedRole)
            {
                _employee.Role_Id = selectedRole.Id;
            }

            _context.SaveChanges();
            MessageBox.Show("Изменения сохранены успешно.");
        }
        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPas = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPas;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseControl();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseControl();
        }

        private void CloseControl()
        {
            if (_parentContentControl != null && _parentContentControl.Content == this)
            {
                _parentContentControl.Content = null;
            }
        }
    }
}

