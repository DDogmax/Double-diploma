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
using System.Windows.Shapes;
using static Magaz.AppData;

namespace Magaz.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployeeWin.xaml
    /// </summary>
    public partial class AddEditEmployeeWin : Window
    {
        private Employee AddEmployee { get; set; }  // для добавления пользователя
        private Employee EditEmployee { get; set; } // для редакторования пользователя
        private bool _isEdit;

        public AddEditEmployeeWin()
        {
            InitializeComponent();
            cmbEmployeeRole.ItemsSource = context.Role.ToList();
            cmbEmployeeRole.DisplayMemberPath = "NameRole";
            cmbEmployeeRole.SelectedIndex = 0;
            AddEmployee = new Employee();
            txtxHeader.Text = "Добавление сотрудника";
            _isEdit = false;
        }

        public AddEditEmployeeWin(Employee employee)
        {
            InitializeComponent();
            cmbEmployeeRole.ItemsSource = context.Role.ToList();
            cmbEmployeeRole.DisplayMemberPath = "NameRole";

            AddEmployee = new Employee();
            txtxHeader.Text = "Редактирование сотрудника";
            _isEdit = true;
            EditEmployee = employee;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isEdit == true)
            {
                txtFirstName.Text = EditEmployee.FirstName;
                txtLastName.Text = EditEmployee.LastName;
                txtMiddleName.Text = EditEmployee.MiddleName;
                txtLogin.Text = EditEmployee.Login;
                txtPassword.Text = EditEmployee.Password;
                cmbEmployeeRole.SelectedIndex = Convert.ToInt32(EditEmployee.idRole) - 1;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)// введение данных пользователя
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Поле Фамилия не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("Поле Имя не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLogin.Text))
                {
                    MessageBox.Show("Поле Логин не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Поле Логин не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var uniqueUser = context.Employee.Where(i => i.Login == txtLogin.Text).FirstOrDefault();


                if (_isEdit == false)// доьавление пользователя 
                {
                    if (uniqueUser == null)
                    {
                        var result = MessageBox.Show("Вы уверены?", "Добавление нового сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            AddEmployee.LastName = txtLastName.Text;
                            AddEmployee.FirstName = txtFirstName.Text;
                            AddEmployee.MiddleName = txtMiddleName.Text;
                            AddEmployee.Login = txtLogin.Text;
                            AddEmployee.Password = txtPassword.Text;
                            AddEmployee.idRole = 1;

                            AppData.AddEmployee(AddEmployee);
                            context.SaveChanges();
                            MessageBox.Show("Сотрудник добавлен");
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин занят", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }
                else if (_isEdit == true)// изменение пользователя 
                {
                    if (uniqueUser == null || uniqueUser.Login == EditEmployee.Login)
                    {
                        var result = MessageBox.Show("Вы уверены?", "Изменение данных сотрудника", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            EditEmployee.FirstName = txtFirstName.Text;
                            EditEmployee.LastName = txtLastName.Text;
                            EditEmployee.MiddleName = txtMiddleName.Text;
                            EditEmployee.Login = txtLogin.Text;
                            EditEmployee.Password = txtPassword.Text;
                            EditEmployee.idRole = cmbEmployeeRole.SelectedIndex + 1;

                            context.SaveChanges();
                            MessageBox.Show("Изменения сохранены");
                            this.Close();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Логин занят", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = HelperClass.stringGen.FIO().IndexOf(e.Text) < 0;
        }

    }
}
