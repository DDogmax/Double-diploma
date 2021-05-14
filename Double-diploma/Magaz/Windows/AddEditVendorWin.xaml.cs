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
    /// Логика взаимодействия для AddEditVendorWin.xaml
    /// </summary>
    public partial class AddEditVendorWin : Window
    {
        private bool _isEdit;

        private Vendor EditVendor { get; set; }

        public AddEditVendorWin()
        {
            InitializeComponent();
            txtxHeader.Text = "Добавление поставщика";

            _isEdit = false;

        }

        public AddEditVendorWin(Vendor vendor)
        {
            InitializeComponent();
            txtxHeader.Text = "Изменение поставщика";

            EditVendor = vendor;
            _isEdit = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isEdit == true)
            {
                txtAddressVendor.Text = EditVendor.AddressVendor;
                txtNameVendor.Text = EditVendor.NameCompany;
                txtPhoneVendor.Text = EditVendor.PhoneVendor;
                txtINNVendor.Text = EditVendor.INN.ToString();
                txtKPPVendor.Text = EditVendor.KPP;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)// сохранение поставщика 
        {
            if (_isEdit == false)
            {
                AppData.AddVendor(new Vendor
                {
                    NameCompany = txtNameVendor.Text,
                    AddressVendor = txtAddressVendor.Text,
                    INN = Int32.Parse(txtINNVendor.Text),
                    PhoneVendor = txtPhoneVendor.Text,
                    KPP = txtKPPVendor.Text
                });
                context.SaveChanges();
                MessageBox.Show("Поставщик добавлен");
                this.Hide();
            }

            else if (_isEdit == true)// изменеие данных поставщика
            {
                EditVendor.AddressVendor = txtAddressVendor.Text;
                EditVendor.NameCompany = txtNameVendor.Text;
                EditVendor.PhoneVendor = txtPhoneVendor.Text;
                EditVendor.INN = Int32.Parse(txtINNVendor.Text);
                EditVendor.KPP = txtKPPVendor.Text;
                context.SaveChanges();

                MessageBox.Show("Данные обновлены");
                this.Close();
            }
        }

       
    }
}
