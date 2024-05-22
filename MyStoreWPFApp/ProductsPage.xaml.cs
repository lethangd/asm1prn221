using BusinessObject;
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
			LoadProductName();
			LoadUnitPrices();
			lsProduct.ItemsSource = context.Products.ToList();
		}

		private void LoadCategoryIds()
		{
			List<int> categoryIds = context.Categories.Select(c => c.CategoryId).Distinct().ToList();
			cbCategoryId.ItemsSource = categoryIds;
		}

		private void LoadProductName()
		{
			List<string> productname = context.Products.Select(c => c.ProductName).Distinct().ToList();
			cbProductName.ItemsSource = productname;
		}

		private void LoadUnitPrices()
		{
			List<int> unitprice = context.Products.Select(c => c.UnitPrice).Distinct().ToList();
			cbSearchUnitPrice.ItemsSource = unitprice;
		}


		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{



			try
			{
				Product product = new Product
				{
					//ProductId = int.Parse(txtProductId.Text),
					ProductName = txtProductName.Text,
					CategoryId = int.Parse(cbCategoryId.SelectedValue.ToString()),
					UnitPrice = int.Parse(txtUnitPrice.Text),

				};

				context.Products.Add(product);
				context.SaveChanges();
				MessageBox.Show("Added!");
				LoadCategoryIds();
				LoadProductName();
				LoadUnitPrices();
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
				LoadProductName();
				LoadUnitPrices();
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
				LoadProductName();
				LoadUnitPrices();
				lsProduct.ItemsSource = context.Products.ToList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void cbProductName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			String productName = (string)cbProductName.SelectedValue;
			lsProduct.ItemsSource = ProductManager.getListProductByProductName(productName);


		}

		private void cbSearchUnitPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (cbSearchUnitPrice.SelectedValue != null)
			{
				try
				{
					int unitPrice = int.Parse(cbSearchUnitPrice.SelectedValue.ToString());
					lsProduct.ItemsSource = ProductManager.getListProductByUnitPrice(unitPrice);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error processing selected unit price: {ex.Message}");
				}
			}

		}

		private void btnRefresh_Click(object sender, RoutedEventArgs e)
		{

			cbProductName.SelectedIndex = -1;
			cbSearchUnitPrice.SelectedIndex = -1;
			txtProductId.Clear();
			txtProductName.Clear();
			cbCategoryId.SelectedIndex = -1;
			txtUnitPrice.Clear();


			lsProduct.ItemsSource = context.Products.ToList();


		}
	}
}