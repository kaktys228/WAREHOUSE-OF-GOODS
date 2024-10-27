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
using курсовая_работа.Models;

namespace курсовая_работа.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        EMPLOYEE _curentuser;
        public AdminMenu(EMPLOYEE user)
        {
            InitializeComponent();
            _curentuser = user;
        }



        private void User_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Admin(_curentuser));
        }

        private void Delyveries_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Manager(_curentuser));
        }

       

        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Provider(_curentuser));
        }
    }
}
