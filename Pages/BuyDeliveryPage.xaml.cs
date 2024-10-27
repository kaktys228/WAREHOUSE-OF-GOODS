using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для BuyDeliveryPage.xaml
    /// </summary>
    public partial class BuyDeliveryPage : Page
    {
        INVOICE _iNVOICE;
        public BuyDeliveryPage()
        {
            InitializeComponent();
            _iNVOICE = new INVOICE();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _iNVOICE = DataGrid.SelectedItem as INVOICE;
        }
       
     
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new addInoices());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = App.SR1Entities.INVOICEs.ToList();
            List<STOCK> statuslist = new List<STOCK>
            {
                new STOCK {NAMES = "Все" }
            };
            foreach (var item in App.SR1Entities.STOCKs.ToList())
            {
                statuslist.Add(item);
            }
            FilterComboBox.ItemsSource = statuslist;
        }

   
        STOCK stock = new STOCK();
        private void FilterComboBox_change(object sender, SelectionChangedEventArgs e)
        {

            stock = (STOCK)FilterComboBox.SelectedItem;
            if (stock == null || stock.NAMES == "Все")
            {
                DataGrid.ItemsSource = App.SR1Entities.INVOICEs.ToList();
            }
            else
            {
                DataGrid.ItemsSource = App.SR1Entities.INVOICEs.Where(c => c.STOCK.STOCK_ID == stock.STOCK_ID).ToList();
            }

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGrid.ItemsSource = App.SR1Entities.INVOICEs.Where(c => c.INVOICE_ID.ToString().Contains(SearchBox.Text)).ToList();
        }
    }
}
