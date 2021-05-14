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
using System.Collections.ObjectModel;
using static Magaz.AppData;

namespace Magaz.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeWin.xaml
    /// </summary>
    public partial class EmployeeWin : Window
    {
        public EmployeeWin()
        {
            InitializeComponent();
            ListEmployee.ItemsSource = context.Employee.ToList();

            txtNameEmpl.Text = $" {HelperClass.DataUser.User.LastName.Replace(" ", "")} {HelperClass.DataUser.User.FirstName.Replace(" ", "")} {HelperClass.DataUser.User.MiddleName.Replace(" ", "")}";

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) //добавленеи пользоваетелй 
        {
            AddEditEmployeeWin addEditEmployeeWin = new AddEditEmployeeWin();
            this.Hide();
            addEditEmployeeWin.ShowDialog();
            ListEmployee.ItemsSource = context.Employee.ToList();
            this.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)// удалеие пользователей
        {
            if (ListEmployee.SelectedItem is Employee employee)
            {
                context.Employee.Remove(employee);
                context.SaveChanges();
                MessageBox.Show("Сотрудник удален");
                ListEmployee.ItemsSource = context.Employee.ToList();
            }
            else
            {
                MessageBox.Show("Сотрудник не выбран");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)//изменеие данных пользователей
        {
            if (ListEmployee.SelectedItem is Employee employee)
            {
                AddEditEmployeeWin addEditEmployeeWin = new AddEditEmployeeWin(employee);
                this.Hide();
                addEditEmployeeWin.ShowDialog();
                ListEmployee.ItemsSource = context.Employee.ToList();
                this.Show();
            }
            else
            {
                MessageBox.Show("Сотрудник не выбран");// вывод предубпреждениея если полбзователбь не выбран
            }
        }

        private void ListEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
