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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Magaz.AppData;


namespace Magaz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class StartWin : Window
    {
        public StartWin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)// закрыть программу
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)// Войти под пользователем
        {

            try
            {
                Employee user = null;
                var users = context.Employee.ToList();
                user = users.Where(u => u.Login.Replace(" ", "").Equals(txtLogin.Text)).FirstOrDefault(); // Достаём Логин и Пароль пользователй из базы данных
                if (user != null)
                {
                    HelperClass.DataUser.User = user;
                    // оприделение роли пользователя. Админ или обычный пользователь
                    if (user.idRole == 1) // idRole=1 это обычный пользователь
                    {
                        ActionWin actionWin = new ActionWin(user);
                        this.Hide();
                        actionWin.ShowDialog();
                        this.Close();
                    }
                    else if (user.idRole == 2)// idRole=2 Это Администратор 
                    {
                        Windows.AdminWin adminWinxaml = new Windows.AdminWin();
                        adminWinxaml.ShowDialog();
                        this.Close();
                    }



                }
                else
                {
                    MessageBox.Show("Логин или пароль введены неверно");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.CapsLock)
            {
                txtCapsLk.Text = "Нажат СapsLock"; //оповещение о нажатом CapsLock
            }
        }
    }
}
