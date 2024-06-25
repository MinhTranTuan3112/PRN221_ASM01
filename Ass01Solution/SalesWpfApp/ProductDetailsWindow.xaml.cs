using BusinessObject;
using DataAccess.DTOs.Category;
using DataAccess.DTOs.Product;
using DataAccess.Repositories;
using DataAccess.Validators.Product;
using Mapster;
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
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        public int? ProductId { get; set; }

        public bool IsUpdate { get; set; } = true;

        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;

        public ProductDetailsWindow()
        {
            InitializeComponent();

            if (_productRepository is null)
            {
                _productRepository = new ProductRepository();
            }

            if (_categoryRepository is null)
            {
                _categoryRepository = new CategoryRepository();
            }

            this.Loaded += ProductDetailsWindow_Loaded;

            btnSave.Click += BtnSave_Click;
        }

        private void PerformCreate()
        {
            var createProductDto = this.DataContext as CreateProductDto;
            var createValidator = new CreateProductValidator();

            if (createProductDto is null)
            {
                return;
            }

            var validationResult = createValidator.Validate(createProductDto);

            if (!validationResult.IsValid)
            {
                string errorMessage = validationResult.ToString("\n");

                throw new Exception(errorMessage);
            }

            _productRepository.AddProduct(createProductDto);

            MessageBox.Show("Create product success");
            this.Close();

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!IsUpdate)
                {
                    PerformCreate();
                    return;
                }

                var product = this.DataContext as GetProductDetailsDto;

                var updatingProduct = product.Adapt<Product>();
                
                updatingProduct.Category = cbCategory.SelectedItem.Adapt<Category>();

                var updateValidator = new UpdateProductValidator();

                var validationResult = updateValidator.Validate(updatingProduct);

                if (!validationResult.IsValid)
                {
                    string errorMessage = validationResult.ToString("\n");

                    throw new Exception(errorMessage);
                }

                _productRepository.UpdateProduct(updatingProduct);

                MessageBox.Show("Update product success");
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProductDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                tbTitle.Text = IsUpdate ? "Product Details" : "Create New Product";
                btnSave.Content = IsUpdate ? "Save" : "Create";

                var categories = _categoryRepository.GetCategories();
                cbCategory.ItemsSource = categories;
                cbCategory.SelectedValuePath = "CategoryId";
                cbCategory.DisplayMemberPath = "CategoryName";

                if (ProductId is null)
                {
                    this.DataContext = new CreateProductDto();
                    return;
                }

                var product = _productRepository.GetProductDetailsById(Convert.ToInt32(ProductId));

                this.DataContext = product;
                cbCategory.SelectedValue = product?.Category.CategoryId;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
