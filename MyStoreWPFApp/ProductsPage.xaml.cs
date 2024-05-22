using BusinessObject.Models;
using DataAccess;
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

namespace MyStoreWPFApp
{
	/// <summary>
	/// Interaction logic for ProductsPage.xaml
	/// </summary>
	public partial class ProductsPage : Page
	{
        MyStoreContext context;
        public ProductsPage()
		{
			InitializeComponent();
            InitializeComponent();
            context = new MyStoreContext();
            LoadCategoryIds();
            lsProduct.ItemsSource = context.Products.ToList();
        }

        private void LoadCategoryIds()
        {
            List<int> categoryIds = context.Categories.Select(c => c.CategoryId).Distinct().ToList();
            cbCategoryId.ItemsSource = categoryIds;
        }






        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                Product product = new Product
                {
                    ProductName = txtProductName.Text,
                    CategoryId = int.Parse(cbCategoryId.SelectedValue.ToString()),
                    UnitPrice = int.Parse(txtUnitPrice.Text),

                };

                context.Products.Add(product);
                context.SaveChanges();
                MessageBox.Show("Added!");
                LoadCategoryIds();
                lsProduct.ItemsSource = context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lsProduct.SelectedItem == null) return;
            int productId = int.Parse(txtProductId.Text);
            Product product = context.Products.Find(productId);
            product.ProductName = txtProductName.Text;
            product.CategoryId = int.Parse(cbCategoryId.SelectedValue.ToString());
            product.UnitPrice = int.Parse(txtUnitPrice.Text);
            try
            {
                context.Products.Update(product);
                context.SaveChanges();
                MessageBox.Show("Updated!");

                LoadCategoryIds();
                lsProduct.ItemsSource = context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsProduct.SelectedItem == null) return;
            int productId = int.Parse(txtProductId.Text);
            Product product = context.Products.Find(productId);
            product.ProductName = txtProductName.Text;
            product.CategoryId = int.Parse(cbCategoryId.SelectedValue.ToString());
            product.UnitPrice = int.Parse(txtUnitPrice.Text);
            try
            {
                context.Products.Remove(product);
                context.SaveChanges();
                MessageBox.Show("Remove!");

                LoadCategoryIds();
                lsProduct.ItemsSource = context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

            txtProductNamesearch.Clear();
            txtProductId.Clear();
            txtProductName.Clear();
            cbCategoryId.SelectedIndex = -1;
            txtUnitPrice.Clear();


            lsProduct.ItemsSource = context.Products.ToList();


        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtProductNamesearch.Text.ToLower();
            var filter = context.Products.Where(p => p.ProductName
            .ToLower()
            .Contains(searchText))
            .OrderBy(p => p.ProductName)
            .ToList();
            lsProduct.ItemsSource = filter;

        }
    }
}
