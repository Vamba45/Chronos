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

namespace Chronos.Windows
{
    /// <summary>
    /// Логика взаимодействия для Autentification.xaml
    /// </summary>
    public partial class Autentification : Window
    {
        public Autentification()
        {
            InitializeComponent();
        }

        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Login.Text))
                s.AppendLine("Поле \"Логин\" пустое");
            if (string.IsNullOrWhiteSpace(Password.Password))
                s.AppendLine("Поле \"Пароль\" пустое");

            return s;
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckFields().Length == 0)
            {
                if(Login.Text == "User" && Password.Password == "123456789")
                {
                    MainWindow main = new MainWindow(false);
                    main.Show();

                    this.Close();
                }

                if (Login.Text == "Admin" && Password.Password == "SWSUCase")
                {
                    MainWindow main = new MainWindow(true);
                    main.Show();

                    this.Close();
                }
            }
        }
    }
}
