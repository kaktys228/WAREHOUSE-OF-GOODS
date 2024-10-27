using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using курсовая_работа.Class;
using курсовая_работа.Models;

namespace курсовая_работа.Pages
{
    /// <summary>
    /// Логика взаимодействия для addInoices.xaml
    /// </summary>
    public partial class addInoices : Page
    {
        INVOICE _invoice;
        public addInoices()
        {
            InitializeComponent();
            _invoice = new INVOICE();
        }

    

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _invoice;
            PROVIDERComboBox.ItemsSource = App.SR1Entities.PROVIDERS.ToList();
            StockComboBox.ItemsSource = App.SR1Entities.STOCKs.ToList();
            TovarBox.ItemsSource = App.SR1Entities.DELIVERies.ToList();
            tovarList = App.SR1Entities.GOODS_ARIVAL.Where(c => c.INVOICE.INVOICE_ID == _invoice.INVOICE_ID).ToList();
            TovarGrid.ItemsSource = tovarList;


           
        }

        private void registButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PROVIDERComboBox.SelectedItem != null &&
                    StockComboBox.SelectedItem != null && TovarBox.SelectedItem!=null)
                {
                    if (tovarList.Count == 0)
                    {
                        MessageBox.Show("Пожалуйста, добавьте хотя бы один товар", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                        if (_invoice.INVOICE_ID == 0)
                        {
                            App.SR1Entities.INVOICEs.Add(_invoice);

                        }
                        else
                        {
                            List<GOODS_ARIVAL> oRDER_TOVARs = App.SR1Entities.GOODS_ARIVAL.Where(c => c.INVOICE_ID == _invoice.INVOICE_ID).ToList();
                            App.SR1Entities.GOODS_ARIVAL.RemoveRange(oRDER_TOVARs);
                        }

                        decimal totalInvoiceSum = 0; 

                        
                        foreach (var item in tovarList)
                        {
                            App.SR1Entities.GOODS_ARIVAL.Add(item);
                            totalInvoiceSum += item.PRICE * item.COUNTS;
                        }

                        _invoice.FINAL_SUM = totalInvoiceSum;

                        App.SR1Entities.SaveChanges();
                        MessageBox.Show("накладная добавлена или изменена");
                        NavigationService.GoBack();
                   
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {

            }
        }


        List<GOODS_ARIVAL> tovarList = new List<GOODS_ARIVAL>();
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (curenttovar != null)
            {
                if (!string.IsNullOrWhiteSpace(PriceBox.Text) && !string.IsNullOrWhiteSpace(CountsBox.Text))
                {
                    if (decimal.TryParse(PriceBox.Text, out decimal price) && int.TryParse(CountsBox.Text, out int count))
                    {
                        if (price >= 0 && count >= 0)
                        {
                           
                            if (!tovarList.Any(item => item.DELIVERY == curenttovar))
                            {
                                GOODS_ARIVAL goodsarival = new GOODS_ARIVAL()
                                {
                                    DELIVERY = curenttovar,
                                    PRICE = price,
                                    COUNTS = count
                                };
                                tovarList.Add(goodsarival);

                                TovarGrid.ItemsSource = null;
                                TovarGrid.ItemsSource = tovarList;
                            }
                            else
                            {
                                MessageBox.Show("Этот товар уже добавлен", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Цена и количество не могут быть отрицательными", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, введите корректные значения цены и количества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите значения цены и количества", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }





        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (curentarival != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить заказ?", "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    tovarList.Remove(curentarival);
                }

            }
            TovarGrid.ItemsSource = null;
            TovarGrid.ItemsSource = tovarList;
        }

        DELIVERY curenttovar;
        GOODS_ARIVAL curentarival;
       

        private void TovarGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            curentarival = TovarGrid.SelectedItem as GOODS_ARIVAL; 
        }

        private void TovarBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curenttovar = TovarBox.SelectedItem as DELIVERY;
        }
    }
}
