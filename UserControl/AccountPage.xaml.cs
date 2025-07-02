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

namespace UsersApp.UserControl
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : System.Windows.Controls.UserControl
    {
        private UsersApp.Models.Employee currentEmployee;
        public AccountPage()
        {
            InitializeComponent();
        }
        public AccountPage(UsersApp.Models.Employee employee)
        {
            this.currentEmployee = employee;
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            if (currentEmployee?.FIO != null)
            {
                string[] fioParts = currentEmployee.FIO.Split('_');

                if (fioParts.Length > 0)
                {
                    string name = fioParts[0];
                    label_name.Content = name;
                }

                if (fioParts.Length > 1)
                {
                    string last_name = fioParts[1];
                    Label_last_name.Content = last_name;
                }

                if (currentEmployee.Role != null)
                {
                    string role = currentEmployee.Role.Name;
                    label_role.Content = role;
                }
            }
        }

    }
}
