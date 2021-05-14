using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Magaz
{
    /// <summary>
    /// Логика взаимодействия для AddEditWin.xaml
    /// </summary>
    public partial class AddEditWin : Window
    {
        string pathImage = null;

        private Product AddProduct { get; }

        private Product productEdit;

        private bool _isEdit;

        public AddEditWin()
        {
            InitializeComponent();
            cmbProductCategory.ItemsSource = context.CategoryProduct.ToList();
            cmbProductCategory.DisplayMemberPath = "NameCategory";
            cmbProductCategory.SelectedIndex = 0;

            cmbUnitOfMeasure.ItemsSource = context.Measure.ToList();
            cmbUnitOfMeasure.DisplayMemberPath = "NameMeasure";
            cmbUnitOfMeasure.SelectedIndex = 0;

            AddProduct = new Product();
            _isEdit = false;
        }

        public AddEditWin(Product product)
        {
            InitializeComponent();
            productEdit = product;
            _isEdit = true;

            cmbProductCategory.ItemsSource = context.CategoryProduct.ToList();
            cmbProductCategory.DisplayMemberPath = "NameCategory";

            cmbUnitOfMeasure.ItemsSource = context.Measure.ToList();
            cmbUnitOfMeasure.DisplayMemberPath = "NameMeasure";


            txtProductName.Text = product.ProductName;
            txtProductDescription.Text = product.ProductDescription;
            txtProductPrice.Text = product.ProductPrice.ToString();
            txtProductCount.Text = product.countProduct.ToString();
            cmbProductCategory.SelectedItem = context.CategoryProduct.Where(i => i.idCategory == product.idProductCategory).ToList().FirstOrDefault();
            cmbUnitOfMeasure.SelectedItem = context.Measure.Where(i => i.idMeasure == product.idUnitOfMeasure).ToList().FirstOrDefault();

            if (product.ProductImage != null)// загрузка изображения из базы данных
            {
                using (MemoryStream stream = new MemoryStream(product.ProductImage))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imageProduct.Source = bitmapImage;
                }
            }
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)// выбор изображениея 
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == true)
            {
                imageProduct.Source = new BitmapImage(new Uri(openFile.FileName));
                pathImage = openFile.FileName;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Поле название товара не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductPrice.Text))
            {
                MessageBox.Show("Поле Количество товара не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductCount.Text))
            {
                MessageBox.Show("Поле Цена товара не может быть пустым", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            try
            {
                if (_isEdit == false)
                {
                    if (pathImage != null)// если фото нет то прередаём значение null
                    {
                        AddProduct.ProductImage = File.ReadAllBytes(pathImage);
                    }
                    else
                    {
                        AddProduct.ProductImage = null;
                    }

                    if (pathImage == null)// если нету фото, то спрашиваем хочет ли рользователь сохранить без фотографии
                    {
                        var resMess = MessageBox.Show("Фото не выбрано. Сохранить товар без фото?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resMess == MessageBoxResult.No)
                        {
                            return;
                        }
                    }

                    AddProduct.ProductName = txtProductName.Text;
                    AddProduct.ProductDescription = txtProductDescription.Text;
                    AddProduct.ProductPrice = Convert.ToDecimal(txtProductPrice.Text);
                    AddProduct.countProduct = Convert.ToInt32(txtProductCount.Text);

                    AddProduct.idProductCategory = context.CategoryProduct
                    .Where(i => i.NameCategory == cmbProductCategory.Text)
                    .Select(i => i.idCategory).FirstOrDefault();

                    AddProduct.idUnitOfMeasure = context.Measure.Where(i => i.NameMeasure == cmbUnitOfMeasure.Text)
                    .Select(i => i.idMeasure).FirstOrDefault();

                    AppData.AddProduct(AddProduct); // добавление товара
                    context.SaveChanges();
                    MessageBox.Show($"Товар {txtProductName.Text} добавлен");
                }

                if (_isEdit == true) //изменение товара
                {
                    if (pathImage != null)
                    {
                        productEdit.ProductImage = File.ReadAllBytes(pathImage); 
                    }

                    productEdit.ProductName = txtProductName.Text;
                    productEdit.ProductDescription = txtProductDescription.Text;
                    productEdit.ProductPrice = Convert.ToDecimal(txtProductPrice.Text);
                    productEdit.countProduct = Convert.ToInt32(txtProductCount.Text);

                    productEdit.idProductCategory = context.CategoryProduct
                    .Where(i => i.NameCategory == cmbProductCategory.Text)
                    .Select(i => i.idCategory).FirstOrDefault();

                    productEdit.idUnitOfMeasure = context.Measure.Where(i => i.NameMeasure == cmbUnitOfMeasure.Text)
                    .Select(i => i.idMeasure).FirstOrDefault();

                    context.SaveChanges();

                    MessageBox.Show("Данные успешно сохранены");

                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

           
        }

        // запрет ввода всех символов кроме цифр и точки в поле цена
        private void txtProductPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789.".IndexOf(e.Text) < 0;
        }

        // запрет ввода всех символов кроме цифр и точки в поле Количество
        private void txtProductCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
