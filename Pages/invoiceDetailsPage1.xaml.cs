using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Логика взаимодействия для invoiceDetailsPage1.xaml
    /// </summary>
    public partial class invoiceDetailsPage1 : Page
    {
       INVOICE selectedInvoice;
       GOODS_ARIVAL goodsForInvoice;
        GOODS_CARE goodsForConsumle;
        CONSUMABLE _selectedConsumable;

        public invoiceDetailsPage1(INVOICE invoice)
        {
            InitializeComponent();
            selectedInvoice = invoice;
            goodsForInvoice = new GOODS_ARIVAL();
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = App.SR1Entities.GOODS_ARIVAL.Where(g => g.INVOICE_ID == selectedInvoice.INVOICE_ID).ToList();
            InvoiceLabel.Content = $"Детальное описание накладной приходной №{selectedInvoice.INVOICE_ID}";
        }

        public invoiceDetailsPage1(CONSUMABLE selectedConsumable)
        {
            InitializeComponent();
            _selectedConsumable = selectedConsumable;
            goodsForConsumle = new GOODS_CARE();
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = App.SR1Entities.GOODS_CARE.Where(g => g.CONSUMABLES_ID == _selectedConsumable.CONSUMABLES_ID).ToList();
            InvoiceLabel.Content = $"Детальное описание накладной расходной №{_selectedConsumable.CONSUMABLES_ID}";
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goodsForInvoice = DataGrid.SelectedItem as GOODS_ARIVAL;
            goodsForConsumle = DataGrid.SelectedItem as GOODS_CARE;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                if (DataGrid.SelectedItem is GOODS_ARIVAL)
                {
                    goodsForInvoice = DataGrid.SelectedItem as GOODS_ARIVAL;
                    NavigationService.Navigate(new InvoiceEdit(goodsForInvoice));
                }
                else if (DataGrid.SelectedItem is GOODS_CARE)
                {
                    goodsForConsumle = DataGrid.SelectedItem as GOODS_CARE;
                    NavigationService.Navigate(new InvoiceEdit(goodsForConsumle));
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
