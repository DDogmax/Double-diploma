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

namespace Magaz
{
    /// <summary>
    /// Логика взаимодействия для ActionWin.xaml
    /// </summary>
    public partial class ActionWin : Window
    {
        ObservableCollection<Product> productList = new ObservableCollection<Product>();

        public ActionWin()
        {
            InitializeComponent();

            productList = new ObservableCollection<Product>(context.Product);
            ListProduct.ItemsSource = productList;

            txtNameEmpl.Text = $" {HelperClass.DataUser.User.LastName.Replace(" ", "")} {HelperClass.DataUser.User.FirstName.Replace(" ", "")} {HelperClass.DataUser.User.MiddleName.Replace(" ", "")}";//вывод Имя+Фамилия+Отчество

        }

        public ActionWin(Employee employee)
        {
            InitializeComponent();

            productList = new ObservableCollection<Product>(context.Product);
            ListProduct.ItemsSource = productList;
            txtNameEmpl.Text = employee.LastName.Replace(" ","") + " " + employee.FirstName.Replace(" ","") + " " + employee.MiddleName.Replace(" ","");//удаление пробелов

            if (employee.idRole == 1)
            {
                btnAddProduct.Visibility = Visibility.Hidden;
                btnDeleteProduct.Visibility = Visibility.Hidden;
                btnEditProduct.Visibility = Visibility.Hidden;
            }
        }

        void BrushButton()
        {
            btnMainPage.BorderThickness = new Thickness(0);
            btnAppliances.BorderThickness = new Thickness(0);
            btnSmartphone.BorderThickness = new Thickness(0);
            btnTV.BorderThickness = new Thickness(0);
            btnPC.BorderThickness = new Thickness(0);
            btnOffice.BorderThickness = new Thickness(0);
            btnAccessories.BorderThickness = new Thickness(0);
            btnMainPage.BorderThickness = new Thickness(0);
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product);
            ListProduct.ItemsSource = productList;

            btnMainPage.BorderThickness = new Thickness(2);
            btnMainPage.BorderBrush = Brushes.Black;
        }

        private void btnAppliances_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product.Where(i => i.idProductCategory == 1));
            ListProduct.ItemsSource = productList;

            btnAppliances.BorderThickness = new Thickness(2);
            btnAppliances.BorderBrush = Brushes.Black;
        }

        private void btnSmartphone_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product.Where(i => i.idProductCategory == 2));
            ListProduct.ItemsSource = productList;

            btnSmartphone.BorderThickness = new Thickness(2);
            btnSmartphone.BorderBrush = Brushes.Black;

        }

        private void btnTV_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product.Where(i => i.idProductCategory == 3).ToList());
            ListProduct.ItemsSource = productList;

            btnTV.BorderThickness = new Thickness(2);
            btnTV.BorderBrush = Brushes.Black;
        }

        private void btnPC_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product.Where(i => i.idProductCategory == 4));
            ListProduct.ItemsSource = productList;

            btnPC.BorderThickness = new Thickness(2);
            btnPC.BorderBrush = Brushes.Black;
        }

        private void btnOffice_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product.Where(i => i.idProductCategory == 5));
            ListProduct.ItemsSource = productList;

            btnOffice.BorderThickness = new Thickness(2);
            btnOffice.BorderBrush = Brushes.Black;

        }

        private void btnAccessories_Click(object sender, RoutedEventArgs e)
        {
            BrushButton();
            productList = new ObservableCollection<Product>(context.Product.Where(i => i.idProductCategory == 6));
            ListProduct.ItemsSource = productList;

            btnAccessories.BorderThickness = new Thickness(2);
            btnAccessories.BorderBrush = Brushes.Black;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e) // поиск товаров по имени или описанию
        {
            ListProduct.ItemsSource = productList
                .Where(i => i.ProductName.Contains(txtSearch.Text)
                || i.ProductDescription.Contains(txtSearch.Text)
                || i.idProduct.ToString() == txtSearch.Text);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)//добавление товара
        {
            AddEditWin addEditWin = new AddEditWin();
            this.Opacity = 0.3;
            addEditWin.ShowDialog();
            this.Opacity = 1;
            ListProduct.ItemsSource = new ObservableCollection<Product>(context.Product);

        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)//изменение данных о товаре
        {
            if (ListProduct.SelectedItem is Product product)
            {
                AddEditWin addEditWin = new AddEditWin(product);
                this.Opacity = 0.3;
                addEditWin.ShowDialog();
                this.Opacity = 1;
                ListProduct.ItemsSource = new ObservableCollection<Product>(context.Product);
            }
            else
            {
                MessageBox.Show("Запись не выбрана");
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)//удаление товара
        {
            if (ListProduct.SelectedItem is Product product)
            {
                var result = MessageBox.Show("Удалить выбранный товар?", "Удаление товара", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                }                
            }
            else
            {
                MessageBox.Show("Запись не выбрана");
            }

            ListProduct.ItemsSource = new ObservableCollection<Product>(context.Product);

        }

        private void ListProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (ListProduct.SelectedItem is Product product)
                {
                    var result = MessageBox.Show("Удалить выбранный товар?", "Удаление товара", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        context.Product.Remove(product);
                        context.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Запись не выбрана");
                }

                ListProduct.ItemsSource = new ObservableCollection<Product>(context.Product);
            }
        }
    }
}
