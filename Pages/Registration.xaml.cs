using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Security.Cryptography;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using курсовая_работа.Models;
using System.Data.Entity;
using курсовая_работа.Class;
using System.Text.RegularExpressions;

namespace курсовая_работа
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        EMPLOYEE _eMPLOYEE;
        public Registration()
        {
            InitializeComponent();
            _eMPLOYEE = new EMPLOYEE();
        }

        public Registration(EMPLOYEE eMPLOYEE)
        {
            InitializeComponent();
            _eMPLOYEE = eMPLOYEE;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^А-Яа-яЁёA-Za-z]");



            e.Handled = regex.IsMatch(e.Text);
        }

        private void PhoneBox_PreviewTextInput1(object sender, TextCompositionEventArgs e)
        {
            string phoneNumber = ((TextBox)sender).Text;

          
            int digitCount = phoneNumber.Count(char.IsDigit);

       
            const int maxDigitCount = 11;

            if (digitCount >= maxDigitCount || (!char.IsDigit(e.Text, 0) && e.Text != "(" && e.Text != ")" && e.Text != " "))
            {
                e.Handled = true;
                return;
            }

       
            phoneNumber += e.Text;

      
            if (digitCount == 0)
                phoneNumber = "+7 (";

          
            else if (digitCount == 4)
                phoneNumber = phoneNumber.Insert(phoneNumber.Length - 1, ") ");

        
            else if (digitCount == 7)
                phoneNumber = phoneNumber.Insert(phoneNumber.Length - 1, "-");

        
            else if (digitCount == 9)
                phoneNumber = phoneNumber.Insert(phoneNumber.Length - 1, "-");

        
            else if (digitCount == 10)
                phoneNumber = phoneNumber.Insert(phoneNumber.Length, "");

      
            ((TextBox)sender).Text = phoneNumber;

   
            ((TextBox)sender).CaretIndex = phoneNumber.Length;

            e.Handled = true;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FirstNameBox.PreviewTextInput += TextBox_PreviewTextInput;
            LastNameBox.PreviewTextInput += TextBox_PreviewTextInput;
            MidleNameBox.PreviewTextInput += TextBox_PreviewTextInput;

          
          
            this.DataContext = _eMPLOYEE;
            RoleComboBox.ItemsSource = App.SR1Entities.ROLS.ToList();   
            StockComboBox.ItemsSource = App.SR1Entities.STOCKs.ToList();
           
        }

        private void registButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(LoginBox.Text) &&
                    !string.IsNullOrWhiteSpace(PasswordBox.Text) &&
                    !string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(MidleNameBox.Text) &&
                    !string.IsNullOrWhiteSpace(PhoneBox.Text) &&
                    RoleComboBox.SelectedItem != null &&
                    StockComboBox.SelectedItem != null)
                {
                    if (PhoneBox.Text.Length < 18)
                    {
                        MessageBox.Show("Пожалуйста, полностью заполните номер телефона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string encryptedLogin = PasswordEncryptor.EncryptPassword(LoginBox.Text);
                    string encryptedPassword = PasswordEncryptor.EncryptPassword(PasswordBox.Text);

                    if (_eMPLOYEE.EMPLOYEES_ID == 0)
                    {
                        _eMPLOYEE.LOGINS = encryptedLogin;
                        _eMPLOYEE.PASSWORDS = encryptedPassword;
                        App.SR1Entities.EMPLOYEES.Add(_eMPLOYEE);
                    }
                    else
                    {
                        _eMPLOYEE.LOGINS = encryptedLogin;
                        _eMPLOYEE.PASSWORDS = encryptedPassword;
                        App.SR1Entities.Entry(_eMPLOYEE).State = EntityState.Modified;
                    }

                    App.SR1Entities.SaveChanges();
                    MessageBox.Show("Пользователь добавлен или изменен");
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void FirstNameBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void LastNameBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void MidleNameBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            string text = textBox.Text;

            if (text.Length > 300)
            {
                textBox.Clear();
                MessageBox.Show("Длина не может превышать 30 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
                return;
            }
        }

        private void PasswordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            string text = textBox.Text;

            if (text.Length > 300)
            {
                textBox.Clear();
                MessageBox.Show("Длина не может превышать 30 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
                return;
            }
        }
    }
}