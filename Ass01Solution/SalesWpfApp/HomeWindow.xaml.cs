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

namespace SalesWpfApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            btnMembers.Click += BtnMembers_Click;
            btnProducts.Click += BtnProducts_Click;
            btnOrders.Click += BtnOrders_Click;
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            new WindowOrders().Show();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            new WindowProducts().Show();
        }

        private void BtnMembers_Click(object sender, RoutedEventArgs e)
        {
            new WindowMembers().Show();
        }
    }


}
