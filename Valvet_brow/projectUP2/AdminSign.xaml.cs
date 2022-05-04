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

namespace projectUP2
{
    /// <summary>
    /// Логика взаимодействия для AdminSign.xaml
    /// </summary>
    public partial class AdminSign : Window
    {
        int a;
        public AdminSign()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            while(a < 3)
            {
                a++;   
                if (a > 3)
                {
                    MessageBox.Show("Доступ запрещен!");
                    break;
                }
            
                if (login.Password == "0000")
                {
                    stringBuilder.AppendLine("Доступ разрешен!");
                    AdminWindow adm = new AdminWindow();
                    adm.Show();
                    this.Close();
                    break;
                }
                if (login.Password != "0000")
                {
                    MessageBox.Show("Неверный код, попробуйте снова");
                    break;
                }

            }
        }
    }
}
