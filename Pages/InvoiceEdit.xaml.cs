using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using курсовая_работа.Models;

namespace курсовая_работа.Pages
{
    public partial class InvoiceEdit : Page
    {
        private GOODS_ARIVAL GoodsForInvoice;
        private GOODS_CARE GoodsForConsumle;

        public string ProductName { get; set; }

        public InvoiceEdit(GOODS_ARIVAL goodsForInvoice)
        {
            InitializeComponent();
            GoodsForInvoice = goodsForInvoice;
            ProductName = goodsForInvoice.DELIVERY.NAMES;
            labeltovar.Content = $"Изменение товара: {ProductName}";
        }

        public InvoiceEdit(GOODS_CARE goodsForConsumle)
        {
            InitializeComponent();
            GoodsForConsumle = goodsForConsumle;
            ProductName = goodsForConsumle.DELIVERY.NAMES;
            labeltovar.Content = $"Изменение товара: {ProductName}";
        }

        private void registButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (GoodsForInvoice != null )
                {

                    if (GoodsForInvoice.COUNTS <= 0 || GoodsForInvoice.PRICE <= 0)
                    {
                        MessageBox.Show("Количество и цена должны быть положительными и не нулевыми", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (COUNTSBox.Text == "" || PRICEBox.Text == "")
                    {
                        MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    App.SR1Entities.SaveChanges();
                        MessageBox.Show("Товар добавлен или изменен");

                        var invoice = App.SR1Entities.INVOICEs.FirstOrDefault(i => i.INVOICE_ID == GoodsForInvoice.INVOICE_ID);
                        if (invoice != null)
                        {
                            invoice.FINAL_SUM = App.SR1Entities.GOODS_ARIVAL
                                .Where(g => g.INVOICE_ID == GoodsForInvoice.INVOICE_ID)
                                .Sum(g => g.PRICE * g.COUNTS);

                            App.SR1Entities.SaveChanges();
                        }

                        NavigationService.GoBack();
                    
                    
                }
                if (GoodsForConsumle != null)
                {
                    if (GoodsForConsumle.COUNTS <= 0 || GoodsForConsumle.PRICE <= 0)
                    {
                        MessageBox.Show("Количество и цена должны быть положительными и не нулевыми", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (COUNTSBox.Text == "" || PRICEBox.Text == "")
                    {
                        MessageBox.Show("Заполните поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    App.SR1Entities.SaveChanges();
                    MessageBox.Show("Товар добавлен или изменен");

                    var consumbel = App.SR1Entities.CONSUMABLES.FirstOrDefault(i => i.CONSUMABLES_ID == GoodsForConsumle.CONSUMABLES_ID);
                    if (consumbel != null)
                    {
                        consumbel.FINAL_SUM = App.SR1Entities.GOODS_CARE
                            .Where(g => g.CONSUMABLES_ID == GoodsForConsumle.CONSUMABLES_ID)
                            .Sum(g => g.PRICE * g.COUNTS);

                        App.SR1Entities.SaveChanges();
                    }
                    NavigationService.GoBack();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          
            if (GoodsForInvoice != null)
            {
                this.DataContext = GoodsForInvoice;
            }
            else if (GoodsForConsumle != null)
            {
                this.DataContext = GoodsForConsumle;
            }
        }

  
        private void PRICEBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            string text = textBox.Text;

            if (text.Length > 12)
            {
                textBox.Clear();
                MessageBox.Show("Длина не может превышать 12 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
                return;
            }


            if (!Regex.IsMatch(text, @"^(?!.*[.,][.,])\d*(?:[.,]\d{0,2})?$"))
            {
                textBox.Text = text.Substring(0, text.Length - 1);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void COUNTSBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            string text = textBox.Text + e.Text;

            if (text.Length > 12)
            {
                textBox.Clear();
                MessageBox.Show("Длина не может превышать 12 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
                return;
            }
            else
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

      
    }
}
