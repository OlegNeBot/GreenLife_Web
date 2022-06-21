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
using GreenLifeLib;

namespace GLFinder
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTBox.Text = "";
            string email = EmailTBox.Text;
            string password = Account.ToHash(PassTBox.Password.Trim());
            try
            {
                await using (Context db = new())
                {
                    var account = db.Account.Where(p => p.Email.Equals(email))
                                            .Where(p => p.Password.Equals(password))
                                            .FirstOrDefault();
                    if (account == null)
                    {
                        MessageTBox.Text = "Неправильный email или пароль!";
                    }
                    else if (account.RoleId != 3)
                    {
                        MessageTBox.Text = "У Вас нет прав \nна использование приложения!";
                    }
                    else
                    {
                        MainWindow mw = new();
                        mw.Show();
                        Close();
                    }

                }
            }
            catch (System.InvalidOperationException)
            {
                MessageTBox.Text = "Отсутствует подключение \nк базе данных!";
            }
        }
    }
}
