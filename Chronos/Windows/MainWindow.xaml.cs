using System;
using System.Windows;
using Chronos.Models;
using Chronos.Pages;
using Chronos.Windows;

namespace Chronos
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mode = false;
        public MainWindow(bool mode_)
        {
            InitializeComponent();

            mode = mode_;

            if(!mode)
                BtnEditEmployes.IsEnabled = false;

            MainFrame.Navigate(new CatalogOfEmployee());
            Manager.MainFrame = MainFrame; 
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
                BtnEditEmployes.Visibility = Visibility.Collapsed;
            }
            else
            {
                BtnBack.Visibility = Visibility.Collapsed;
                BtnEditEmployes.Visibility = Visibility.Visible;
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
        }
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult x = MessageBox.Show("Вы действительно хотите выйти?",
            "Выйти", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (x == MessageBoxResult.Cancel)
                e.Cancel = true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void BtnEditEmployes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeePage());
        }

        private void btnOut_Click(object sender, RoutedEventArgs e)
        {
            Autentification aut = new Autentification();

            this.Close();

            aut.Show();
        }
    }
}
