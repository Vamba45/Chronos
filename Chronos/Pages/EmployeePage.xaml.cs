using Chronos.Models;
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

namespace Chronos.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DataGridEmpl.ItemsSource = null;

                EmployeeControlEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                List<Employee> employees = EmployeeControlEntities.GetContext().Employee.OrderBy(p => p.EmplSurname).ToList();

                DataGridEmpl.ItemsSource = employees;
            }
        }

        private void DataGridEmpl_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEmployeePage((sender as Button).DataContext as Employee));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEmployeePage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmpl = DataGridEmpl.SelectedItems.Cast<Employee>().ToList();

            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedEmpl.Count()} записей???",
            "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    Employee x = selectedEmpl[0];

                    var empl_s = EmployeeControlEntities.GetContext().Employement.Where(p => p.id_Employee == x.id).ToList();

                    EmployeeControlEntities.GetContext().Employement.RemoveRange(empl_s);
                    EmployeeControlEntities.GetContext().Employee.Remove(x);
                    EmployeeControlEntities.GetContext().SaveChanges();

                    MessageBox.Show("Записи удалены");

                    List<Employee> employees = EmployeeControlEntities.GetContext().Employee.OrderBy(p => p.EmplSurname).ToList();

                    DataGridEmpl.ItemsSource = null;
                    DataGridEmpl.ItemsSource = employees;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
