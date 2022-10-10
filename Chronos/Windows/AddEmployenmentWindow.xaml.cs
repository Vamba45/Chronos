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
using System.Windows.Shapes;

namespace Chronos.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployenmentWindow.xaml
    /// </summary>
    public partial class AddEmployenmentWindow : Window
    {
        public Employement currentItem { get; private set; }
        Employee currentEmployee;
        public AddEmployenmentWindow(Employee empl)
        {
            InitializeComponent();

            currentEmployee = empl;
            currentItem = new Employement();

            DataContext = currentItem;
        }

        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(TextBoxHourCount.Text))
            {
                int x = 0;

                if (!int.TryParse(TextBoxHourCount.Text, out x))
                    s.AppendLine("Количество потраченных часов должно быть числом!");
                else if (x <= 0)
                {
                    s.AppendLine("Количество часов не может быть отрицательным или равным нулю");
                }
            }

            if (DatePickerDateStart.SelectedDate is null)
                s.AppendLine("Укажите дату начала");
            if (DatePickerDateFinish.SelectedDate is null)
                s.AppendLine("Укажите дату завершения");

            return s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();

            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }

            currentItem.DateStart = Convert.ToDateTime(DatePickerDateStart.SelectedDate);
            currentItem.DateFinish = Convert.ToDateTime(DatePickerDateFinish.SelectedDate);

            currentItem.id_Employee = currentEmployee.id;
            currentItem.id = EmployeeControlEntities.GetContext().Employement.ToList().OrderBy(x => x.id).Last().id + 1;

            this.DialogResult = true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
