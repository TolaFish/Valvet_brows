﻿using System;
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

namespace projectUP2
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {


            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += new EventHandler(timerTick); // Timer and update Searching
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }
        private void Update_Search()
        {
            var currentService = BeautifEntities.GetContext().Service.ToList();
            currentService = currentService.Where(p => p.Title.ToLower().Contains(search_bar.Text.ToLower())).ToList();
            if (Cmb_fil2.SelectedIndex == 1)
            {
                currentService = currentService.OrderByDescending(p => p.Cost).ToList();
            }
            if (Cmb_fil2.SelectedIndex == 2)
            {
                currentService = currentService.OrderBy(p => p.Cost).ToList();
            }
            if (Cmb_fil2.SelectedIndex == 3)
            {
                currentService = currentService.OrderBy(p => p.Discount).ToList();
            }
            if (Cmb_fil2.SelectedIndex == 4)
            {
                currentService = currentService.OrderBy(p => p.DurationInSeconds).ToList();
            }

            listbox1.ItemsSource = currentService;
            Count1.Content = "Услуг в списке " + listbox1.Items.Count + " из " + BeautifEntities.GetContext().Service.Count();
        }
        private void Button_Click(object sender, RoutedEventArgs e)//Add
        {
            editadd editadd = new editadd(null);
            editadd.Show();
            this.Close();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)//Edit
        {
            editadd editadd = new editadd((sender as Button).DataContext as Service);
            editadd.Show();
            this.Close();

        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                BeautifEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listbox1.ItemsSource = BeautifEntities.GetContext().Service.ToList();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Delete
        {
            Service _service;



            _service = (sender as Button).DataContext as Service;
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить услугу?", "Внимание!", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                try
                {
                    BeautifEntities.GetContext().Service.Remove(_service);
                    BeautifEntities.GetContext().SaveChanges();
                    MessageBox.Show("Услуга: " + _service.Title + "Была успешно удалена!");
                    listbox1.Items.Refresh();
                    listbox1.ItemsSource = BeautifEntities.GetContext().Service.ToList();
                    Count1.Content = "Услуг в списке: " + listbox1.Items.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Spisokzapis spisokzapis = new Spisokzapis();
            spisokzapis.Show();
            this.Close();
        }

        private void Cmb_fil2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update_Search();
        }

        private void search_bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_Search();
        }
        private void timerTick(object sender, EventArgs e)
        {
            Update_Search();
        }
    }
}
