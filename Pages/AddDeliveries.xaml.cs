using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace курсовая_работа.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddDeliveries.xaml
    /// </summary>
    public partial class AddDeliveries : Page
    {
        private DELIVERY _delivery;

        public AddDeliveries()
        {
            InitializeComponent();
            _delivery=new DELIVERY();
        }

        public AddDeliveries(DELIVERY delivery)
        {
            InitializeComponent();
            _delivery = delivery;
        }
        private void NumberValidationPreviewTextInputHandler(object sender, TextCompositionEventArgs e)
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




        private void registButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(LoginBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Text) || string.IsNullOrWhiteSpace(PriceBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_delivery.COUNTS <= 0 || _delivery.PRICE <= 0)
                {
                    MessageBox.Show("Количество и цена должны быть положительными и не нулевыми", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_delivery.DELIVERY_ID == 0)
                {
                    App.SR1Entities.DELIVERies.Add(_delivery);
                }

                App.SR1Entities.SaveChanges();
                MessageBox.Show("Товар добавлен или изменен");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext=_delivery;
        }

        private void PriceTextBoxTextChangedHandler(object sender, TextChangedEventArgs e)
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

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            string text = textBox.Text;

            if (text.Length > 30)
            {
                textBox.Clear();
                MessageBox.Show("Длина не может превышать 30 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
                return;
            }
        }
    }
}
