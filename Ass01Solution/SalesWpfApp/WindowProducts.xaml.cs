using DataAccess.DTOs.Product;
using DataAccess.Repositories;
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
    /// Interaction logic for WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts : Window
    {
        private readonly IProductRepository _productRepository;


        public decimal? StartPrice
        {
            get
            {
                return (string.IsNullOrEmpty(txtStartPrice.Text)) ? null : Convert.ToDecimal(txtStartPrice.Text);
            }
            set
            {
                txtStartPrice.Text = value.ToString();
            }
        }

        public decimal? EndPrice
        {
            get
            {
                return (string.IsNullOrEmpty(txtEndPrice.Text)) ? null : Convert.ToDecimal(txtEndPrice.Text);
            }
            set
            {
                txtEndPrice.Text = value.ToString();
            }
        }

        public string Keyword
        {
            get { return txtKeyword.Text; }
            set { txtKeyword.Text = value; }
        }

        public WindowProducts()
        {
            InitializeComponent();

            if (_productRepository is null)
            {
                _productRepository = new ProductRepository();
            }

            this.Loaded += WindowProducts_Loaded;
            btnUpdate.Click += BtnUpdate_Click;
            btnCreate.Click += BtnCreate_Click;
            txtKeyword.TextChanged += TxtKeyword_TextChanged;
            btnSubmit.Click += BtnSubmit_Click;
            btnReset.Click += BtnReset_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentProduct = dgProducts.SelectedItem as GetProductDto;

                if (currentProduct is null)
                {
                    throw new Exception("Please select a product for deleting");
                }

                _productRepository.DeleteProduct(currentProduct.ProductId);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Reset filter?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            ResetFilter();
            LoadProducts();
        }

        private void ResetFilter()
        {
            Keyword = string.Empty;
            StartPrice = null;
            EndPrice = null;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void TxtKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadProducts();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var detailsWindow = new ProductDetailsWindow
            {
                IsUpdate = false
            };

            detailsWindow.Show();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var currentProduct = dgProducts.SelectedItem as GetProductDto;

                var productDetailsWindow = new ProductDetailsWindow
                {
                    ProductId = currentProduct?.ProductId
                };

                productDetailsWindow.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void WindowProducts_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                if (StartPrice > EndPrice)
                {
                    throw new Exception("Start price must be smaller than end price");
                }

                var products = _productRepository.GetProducts(Keyword, StartPrice, EndPrice);
                dgProducts.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
