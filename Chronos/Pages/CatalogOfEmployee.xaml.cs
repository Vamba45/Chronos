using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Chronos.Models;
using Chronos.Windows;

namespace Chronos.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogOfEmployee.xaml
    /// </summary>
    public partial class CatalogOfEmployee : Page
    {
        int _itemcount = 0;
        int _allitemcount = 0;
        int _pagenumber = 0;
        int _pagecount = 0;

        List<Employee> employees;
        public CatalogOfEmployee()
        {
            InitializeComponent();

            var employeeTypes = EmployeeControlEntities.GetContext().Position.OrderBy(p => p.naimenovaniye).ToList();

            employeeTypes.Insert(0, new Position
            {
                naimenovaniye = "Все типы"
            }
            );

            ComboType.ItemsSource = employeeTypes;
            ComboType.SelectedIndex = 0;
        }
        void LoadData()
        {
            EmployeeControlEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            employees = EmployeeControlEntities.GetContext().Employee.OrderBy(p => p.EmplName).ToList();

            _allitemcount = employees.Count;
            _itemcount = _allitemcount;

            ListBoxEmployees.ItemsSource = EmployeeControlEntities.GetContext().Employee.OrderBy(p => p.EmplName).ToList();

            _pagenumber = 1;
            InitializeListBoxPages();

            int k = employees.Count - (_pagenumber - 1) * 10;

            if (k < 10)
                ListBoxEmployees.ItemsSource = employees.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxEmployees.ItemsSource = employees.GetRange((_pagenumber - 1) * 10, 10);
            TextBlockCount.Text = $" Результат запроса: {_itemcount} записей из {_allitemcount}";
        }
        public void InitializeListBoxPages()
        {
            ListBoxPageCount.Items.Clear();
            _pagecount = _itemcount / 10;
            if (_itemcount % 10 != 0)
                _pagecount++;
            for (int i = 1; i <= _pagecount; i++)
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = i.ToString();
                ListBoxPageCount.Items.Add(itm);
            }
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ListBoxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if ((_pagenumber > 1))
                _pagenumber--;
            ListBoxPageCount.SelectedIndex = _pagenumber - 1;
        }

        private void ListBoxPageCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if ((_pagenumber < _pagecount))
                _pagenumber++;
            ListBoxPageCount.SelectedIndex = _pagenumber - 1;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }
        }

        private void UpdateData()
        {
            var currentEmployees = EmployeeControlEntities.GetContext().Employee.OrderBy(p => p.EmplName).ToList();

            if (ComboType.SelectedIndex > 0)
                currentEmployees = currentEmployees.Where(p => p.id_Position == (ComboType.SelectedItem as Position).id).ToList();

            currentEmployees = currentEmployees.Where(p => p.GetFullName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            _pagenumber = 1;

            employees = currentEmployees;
            ListBoxEmployees.ItemsSource = currentEmployees;
            _itemcount = currentEmployees.Count;

            InitializeListBoxPages();

            int k = employees.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxEmployees.ItemsSource = employees.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxEmployees.ItemsSource = employees.GetRange((_pagenumber - 1) * 10, 10);
            TextBlockCount.Text = $" Результат запроса: {currentEmployees.Count} записей из {_allitemcount}";
        }
    }
}
