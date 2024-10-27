using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using курсовая_работа.Pages;

namespace курсовая_работа
{
    /// <summary>
    /// Логика взаимодействия для autoresation.xaml
    /// </summary>
    public partial class autoresation : Page
    {
        public autoresation()
        {
            InitializeComponent();
            
        }

        private void DisplayGreeting(string fullName)
        {
            DateTime currentTime = DateTime.Now;
            string greeting = "Доброй ночи";

            if (currentTime.Hour >= 4 && currentTime.Hour < 12)
            {
                greeting = "Доброе утро";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 17)
            {
                greeting = "Добрый день";
            }
            else if (currentTime.Hour >= 17 && currentTime.Hour < 24)
            {
                greeting = "Добрый вечер";
            }

            MessageBox.Show($"{greeting}, {fullName}");
        }
      
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;
            string password2 = TextBox.Text;
            string encryptedLogin = PasswordEncryptor.EncryptPassword(LoginBox.Text);
            string encryptedPassword = PasswordEncryptor.EncryptPassword(PasswordBox.Password);
            string encryptedPassword2 = PasswordEncryptor.EncryptPassword(PasswordBox.Password);

            EMPLOYEE user = App.SR1Entities.EMPLOYEES.Include("ROL").FirstOrDefault(c =>
                (c.LOGINS == encryptedLogin && (c.PASSWORDS == encryptedPassword || c.PASSWORDS == encryptedPassword2)));

            if (user != null)
            {
                if (user.ROL.NAMES == "Админ")
                {
                    this.NavigationService.Navigate(new AdminMenu(user));
                }
                else if (user.ROL.NAMES == "Менеджер")
                {
                    this.NavigationService.Navigate(new Manager(user));
                }
                else if (user.ROL.NAMES == "Приемщик")
                {
                    this.NavigationService.Navigate(new Provider(user));
                }
                else
                {
                    MessageBox.Show("Недопустимая роль для пользователя");
                }
                DisplayGreeting($"{user.FIRSTNAME} {user.LASTNAME}");
            }
            else
            {
                MessageBox.Show("Неверные учетные данные");
            }
        }

        private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Visible)
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                TextBox.Text = PasswordBox.Password;
                TextBox.Visibility = Visibility.Visible;
            }
            else
            {
                TextBox.Visibility = Visibility.Collapsed;
                PasswordBox.Visibility = Visibility.Visible;
            }

        }
    }
}
