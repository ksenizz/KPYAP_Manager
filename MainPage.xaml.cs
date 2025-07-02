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
using System.Windows.Shapes;
using UsersApp.Models;
using UsersApp.Services;
using UsersApp.UserControl;
using System.Diagnostics;

namespace UsersApp
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private Employee currentEmployee;
        public AuthWindow()
        {
            InitializeComponent();
        }
        public AuthWindow(Employee employee)
        {
            currentEmployee = employee;
            InitializeComponent();
        }

        private void BorderMouse(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            var projectsControl = new StaticPage(currentEmployee);
            MainContentControl.Content = projectsControl;

        }

        private void BtnProjects_Click(object sender, RoutedEventArgs e)
        {
            var projectsControl = new ProjectPage(currentEmployee);
            MainContentControl.Content = projectsControl;

        }

        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            var projectsControl = new TaskPage(currentEmployee);
            MainContentControl.Content = projectsControl;
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            var projectsControl = new AccountPage(currentEmployee);
            MainContentControl.Content = projectsControl;
        }
        private void Information_Button(object sender, RoutedEventArgs e)
        {
            string chmFilePath = @"C:\Users\HONOR\Desktop\MyHelpProject\Help.chm";

            if (System.IO.File.Exists(chmFilePath))
            {
                Process.Start(new ProcessStartInfo(chmFilePath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Файл справки не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
