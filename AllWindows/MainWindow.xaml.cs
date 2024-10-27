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
using курсовая_работа.Pages;

namespace курсовая_работа
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.mainframe.Navigate(new autoresation());
        }

        private void BackButtno_Click(object sender, RoutedEventArgs e)
        {
            mainframe.GoBack();
        }

        private void mainframe_ContentRendered(object sender, EventArgs e)
        {
            if ( mainframe.Content is autoresation  )
            {
                BackButtno.Visibility = Visibility.Collapsed;
            }
            else
            {
                BackButtno.Visibility = Visibility.Visible;
            }

        }

    }
}
