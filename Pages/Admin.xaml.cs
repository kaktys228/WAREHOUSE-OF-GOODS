using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        EMPLOYEE _eMPLOYEE;
        EMPLOYEE curentuser;
        public Admin(EMPLOYEE _curentuser)
        {
            InitializeComponent();
            _eMPLOYEE = new EMPLOYEE();
            curentuser = _curentuser;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _eMPLOYEE = DataGrid.SelectedItem as EMPLOYEE;
        }
       
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = null;
            DataGrid.ItemsSource = App.SR1Entities.EMPLOYEES.ToList();
            List<ROL> statuslist = new List<ROL>
            {
                new ROL {NAMES = "Все" }
            };
            foreach (var item in App.SR1Entities.ROLS.ToList())
            {
                statuslist.Add(item);
            }
            FilterComboBox.ItemsSource = statuslist;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.SelectedItem != null)
                {
                    _eMPLOYEE = DataGrid.SelectedItem as EMPLOYEE;
                    NavigationService.Navigate(new Registration(_eMPLOYEE));
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.SelectedItem != null)
                {
                    _eMPLOYEE = DataGrid.SelectedItem as EMPLOYEE;

                   
                    if (_eMPLOYEE.ROL.NAMES == "Админ" && _eMPLOYEE.EMPLOYEES_ID == curentuser.EMPLOYEES_ID)
                    {
                        MessageBox.Show("Вы не можете удалить самого себя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            App.SR1Entities.EMPLOYEES.Remove(_eMPLOYEE);
                            App.SR1Entities.SaveChanges();
                            DataGrid.ItemsSource = null;
                            DataGrid.ItemsSource = App.SR1Entities.EMPLOYEES.ToList();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
        ROL rol =  new ROL();
        private void FilterComboBox_change(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                rol = (ROL)FilterComboBox.SelectedItem;
                if (rol == null || rol.NAMES == "Все")
                {
                    DataGrid.ItemsSource = App.SR1Entities.EMPLOYEES.ToList();
                }
                else
                {
                    DataGrid.ItemsSource = App.SR1Entities.EMPLOYEES.Where(c => c.ROL.ROLS_ID == rol.ROLS_ID).ToList();
                }
            }
             catch 
            {

            }

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.ToLower();
            var Filter = App.SR1Entities.EMPLOYEES.Where(p => p.FIRSTNAME.ToLower().Contains(search)
            || p.LASTNAME.ToLower().Contains(search) || p.MIDLENAME.ToLower().Contains(search)
            || p.ROL.NAMES.ToLower().Contains(search)).ToList();
            DataGrid.ItemsSource = Filter;
        }
    }
}