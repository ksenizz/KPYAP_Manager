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

namespace UsersApp.UserControl.Task
{
    /// <summary>
    /// Логика взаимодействия для TaskDetailPage.xaml
    /// </summary>
    public partial class TaskDetailPage : System.Windows.Controls.UserControl
    {
        private readonly DB _context;
        private readonly ContentControl _overlayContentControl;

        public TaskDetailPage(DB context, Tasks task, ContentControl overlayContentControl)
        {
            InitializeComponent();
            _context = context;
            _overlayContentControl = overlayContentControl;
            DataContext = task;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _overlayContentControl.Content = new TaskPage();
        }
    }
}
