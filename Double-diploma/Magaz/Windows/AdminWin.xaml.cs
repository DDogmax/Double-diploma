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

namespace Magaz.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWinxaml.xaml
    /// </summary>
    public partial class AdminWin : Window //окно администратора (AdminRole)
    {
        public AdminWin()
        {
            InitializeComponent();
            txbUser.Text = $"{HelperClass.DataUser.User.LastName.Replace (" ","")} {HelperClass.DataUser.User.FirstName.Replace (" ","")} {HelperClass.DataUser.User.MiddleName.Replace (" ","")}";
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e) //презод на страницу товара
        {
            ActionWin actionWin = new ActionWin();
            this.Hide();
            actionWin.ShowDialog();
            this.Show();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e) //переход на страницу пользователей
        {
            EmployeeWin employeeWin = new EmployeeWin();
            this.Hide();
            employeeWin.ShowDialog();
            this.Show();
        }

        private void Vendor_Click(object sender, RoutedEventArgs e) //переход на страницу поставок
        {
            VendorWin vendor = new VendorWin();
            this.Hide();
            vendor.ShowDialog();
            this.Show();
        }
    }
}
