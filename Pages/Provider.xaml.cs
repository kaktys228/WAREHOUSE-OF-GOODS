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
    /// Логика взаимодействия для Provider.xaml
    /// </summary>
    public partial class Provider : Page
    {

        EMPLOYEE user;

        public Provider(EMPLOYEE curentuser)
        {
            InitializeComponent();
            user = curentuser;

            if (curentuser.ROLS_ID == 1 || curentuser.ROLS_ID==2)
            {
                PrintButton.Visibility = Visibility.Visible;
            }
            else
            {
                PrintButton.Visibility = Visibility.Collapsed;
            }
        }




        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            INVOICE selectedInvoice = (INVOICE)DataGrid.SelectedItem;
            if (selectedInvoice != null)
            {
                               
                NavigationService.Navigate(new invoiceDetailsPage1(selectedInvoice));
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource=App.SR1Entities.INVOICEs.ToList(); 
            DataGrid2.ItemsSource=App.SR1Entities.CONSUMABLES.ToList();
        }

        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CONSUMABLE selectedConsumable = (CONSUMABLE)DataGrid2.SelectedItem;
            if (selectedConsumable != null)
            {
                NavigationService.Navigate(new invoiceDetailsPage1(selectedConsumable));

            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PrintInvoice(user));
        }
    }
}
