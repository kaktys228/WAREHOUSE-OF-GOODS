using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        EMPLOYEE curentuser;
        DELIVERY _delivery;
        public Manager(EMPLOYEE user)
        {
            InitializeComponent();
            _delivery = new DELIVERY();
            curentuser=user;    
        }

     
       
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           _delivery= DataGrid.SelectedItem as DELIVERY;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.SelectedItem != null)
                {
                    NavigationService.Navigate(new AddDeliveries(_delivery));
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите товар для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {

            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        App.SR1Entities.DELIVERies.Remove(_delivery);
                        App.SR1Entities.SaveChanges();
                        DataGrid.ItemsSource = null;
                        DataGrid.ItemsSource = App.SR1Entities.DELIVERies.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите товар для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDeliveries());
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();
            var Filter = App.SR1Entities.DELIVERies.Where(p => p.NAMES.ToLower().Contains(search)).ToList();
            DataGrid.ItemsSource = Filter;
        }

     

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = App.SR1Entities.DELIVERies.ToList(); 
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate( new BuyDeliveryPage());
        }

        private void invoice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Provider(curentuser));
        }
    }
}
