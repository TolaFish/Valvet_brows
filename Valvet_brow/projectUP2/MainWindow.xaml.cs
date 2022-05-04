using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace projectUP2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            var _service=BeautifEntities.GetContext().Service.ToList();
            InitializeComponent();
            
            listbox1.ItemsSource= BeautifEntities.GetContext().Service.ToList();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminSign adminSign = new AdminSign();
            adminSign.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Update_Search();
        }

        private void search_bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_Search();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BeautifEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listbox1.ItemsSource = BeautifEntities.GetContext().Service.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void Update_Search()
        {
            var currentService = BeautifEntities.GetContext().Service.ToList();
            currentService = currentService.Where(p => p.Title.ToLower().Contains(search_bar.Text.ToLower())).ToList();
            if (Cmb_fil.SelectedIndex == 1)
            {
                currentService = currentService.OrderByDescending(p => p.Cost).ToList();
            }
            if (Cmb_fil.SelectedIndex == 2)
            {
                currentService = currentService.OrderBy(p => p.Cost).ToList();
            }
            if (Cmb_fil.SelectedIndex == 3)
            {
                currentService = currentService.OrderBy(p => p.Discount).ToList();
            }
            if (Cmb_fil.SelectedIndex == 4)
            {
                currentService = currentService.OrderBy(p => p.DurationInSeconds).ToList();
            }

            listbox1.ItemsSource = currentService;
            Count2.Content = "Услуг в списке " + listbox1.Items.Count + " из " + BeautifEntities.GetContext().Service.Count();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Service _service;

            _service = (sender as Button).DataContext as Service;
            if (_service.Description == null)
            {
                _service.Description = "Описания нет";
            }
            _service.Cost = (_service.Cost / 100) * Convert.ToDecimal(_service.Discount);
            More_About_Service.DataContext = _service;
            Close_More.Visibility = Visibility.Visible;
        }

        private void Close_More_Click(object sender, RoutedEventArgs e)
        {
            More_About_Service.DataContext = null;
            Close_More.Visibility = Visibility.Hidden;
        }

        private void Cmb_fil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Update_Search();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ZapisEditAdd zapisEditAdd = new ZapisEditAdd((sender as Button).DataContext as Service);
            zapisEditAdd.Show();
            this.Close();
        }
    }
}
