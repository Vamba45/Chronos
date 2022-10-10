using Chronos.Models;
using Chronos.Windows;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Chronos.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        private Employee _currentEmployee = new Employee();
        public AddEmployeePage(Employee selectedEmployee)
        {
            InitializeComponent();

            if (selectedEmployee != null)
            {
                _currentEmployee = selectedEmployee;

                DtData.ItemsSource =
                EmployeeControlEntities.GetContext().Employement.
                Where(p => p.id_Employee == selectedEmployee.id).OrderBy(p => p.DateStart).ToList();
            }

            DataContext = _currentEmployee;

            ComboEmployeePosition.ItemsSource = EmployeeControlEntities.GetContext().Position.ToList();
            ComboEmployeeStatus.ItemsSource = EmployeeControlEntities.GetContext().Status.ToList();
        }

        private bool IsNum(string str)
        {
            string numbers = "0123456789";

            for (int i = 0; i < str.Length; i++)
            {
                if (numbers.Contains(str[i]) == false)
                {
                    return false;
                }
            }

            return true;
        }
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentEmployee.EmplName))
                s.AppendLine("Поле \"Имя\" пустое");
            if (string.IsNullOrWhiteSpace(_currentEmployee.EmplSurname))
                s.AppendLine("Поле \"Фамилия\" пустое");

            if (!string.IsNullOrEmpty(_currentEmployee.Tel))
            {
                if ((IsNum(_currentEmployee.Tel) == false) || _currentEmployee.Tel.Length != 11)
                    s.AppendLine("Номер телефона указан неверно");
            }

            if (string.IsNullOrWhiteSpace(_currentEmployee.PasportSeries))
                s.AppendLine("Серия паспорта не указана");
            if (!string.IsNullOrWhiteSpace(_currentEmployee.PasportSeries))
                if (IsNum(_currentEmployee.PasportSeries) == false || _currentEmployee.PasportSeries.Length != 4)
                    s.AppendLine("Серия паспорта указана неверно");

            if (string.IsNullOrWhiteSpace(_currentEmployee.PasportNumber))
                s.AppendLine("Номер паспорта не указан");
            if (!string.IsNullOrWhiteSpace(_currentEmployee.PasportNumber))
                if (IsNum(_currentEmployee.PasportNumber) == false||  _currentEmployee.PasportNumber.Length != 6)
                    s.AppendLine("Номер паспорта указана неверно");

            if (_currentEmployee.Position == null)
                s.AppendLine("Выберите должность");

            if (_currentEmployee.Status == null)
                s.AppendLine("Выберите статус");

            return s;
        }
        void LoadData(Employee employee)
        {
            DtData.ItemsSource = EmployeeControlEntities.GetContext().Employement.Where(p => p.id_Employee == employee.id).OrderBy(p => p.DateStart).ToList();
        }
        private void DtDataLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentEmployee.id == 0) return;

                AddEmployenmentWindow window = new AddEmployenmentWindow(_currentEmployee);


                if (window.ShowDialog() == true)
                {
                    EmployeeControlEntities.GetContext().Employement.Add(window.currentItem);
                    EmployeeControlEntities.GetContext().SaveChanges();

                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadData(_currentEmployee);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DtData.SelectedItem == null) return;

                MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись? ", "Удаление", MessageBoxButton.OKCancel,
                MessageBoxImage.Question);

                if (messageBoxResult == MessageBoxResult.OK)
                {
                    Employement deletedItem = DtData.SelectedItem as Employement;

                    EmployeeControlEntities.GetContext().Employement.Remove(deletedItem);
                    EmployeeControlEntities.GetContext().SaveChanges();

                    LoadData(_currentEmployee);

                    MessageBox.Show("Запись удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();

            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }

            if (_currentEmployee.id == 0)
            {
                int id = EmployeeControlEntities.GetContext().Employee.ToList().OrderBy(x => x.id).Last().id;

                _currentEmployee.id = id + 1;

                EmployeeControlEntities.GetContext().Employee.Add(_currentEmployee);
            }

            EmployeeControlEntities.GetContext().SaveChanges();

            MessageBox.Show("Запись Изменена");

            Manager.MainFrame.GoBack();
        }

        private void DtData_LoadingRow(object sender, DataGridRowEventArgs e)
        {

        }
    }
}
