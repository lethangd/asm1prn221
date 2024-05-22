using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyStoreWPFApp.OrderView
{
    public partial class AddOrder : Window
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailRepository _orderDetailRepository;
        private readonly ProductManager _productManager;
        private List<Product> _productNames;
        private List<OrderDetail> _orderDetailsList; // Danh sách để lưu các chi tiết đơn hàng
        private int _productId;

        public AddOrder(int staffID)
        {
            InitializeComponent();
            _orderRepository = new OrderRepository();
            _orderDetailRepository = new OrderDetailRepository();
            _productManager = new ProductManager();
            _orderDetailsList = new List<OrderDetail>(); // Khởi tạo danh sách

            OrderDatePicker.SelectedDate = DateTime.Today;
            StaffIdTextBox.Text = staffID.ToString();

            _productNames = _productManager.getAllProductName();
            ProductNameComboBox.ItemsSource = _productNames;
        }

        private void ProductNameComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Up && e.Key != Key.Down)
            {
                var filteredProducts = _productNames
                    .Where(product => product.ProductName.IndexOf(ProductNameComboBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                ProductNameComboBox.ItemsSource = filteredProducts;
                ProductNameComboBox.IsDropDownOpen = filteredProducts.Count > 0;
            }
        }
        private void ProductNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductNameComboBox.SelectedItem is Product selectedProduct)
            {
                // Lấy ProductId từ đối tượng Product được chọn
                _productId = selectedProduct.ProductId;
                // Sử dụng productId theo nhu cầu của bạn
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNameComboBox.SelectedItem is Product selectedProduct)
            {
                int productId = selectedProduct.ProductId;
                string productName = selectedProduct.ProductName;
                int quantity = Convert.ToInt32(QuantityTextBox.Text);
                int unitPrice = Convert.ToInt32(UnitPriceTextBox.Text);

                if (quantity <= 0 || unitPrice <= 0)
                {
                    MessageBox.Show("Please fill all fields with valid values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                OrderDetail orderDetail = new OrderDetail
                {
          
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                };
                OrderDetailsDataGrid.Items.Add(new
                {
                    ProductId = orderDetail.ProductId,
                    ProductName = _productManager.findProductNameById(orderDetail.ProductId),
                    Quantity = orderDetail.Quantity,
                    UnitPrice = orderDetail.UnitPrice
                });
                _orderDetailsList.Add(orderDetail); // Thêm chi tiết đơn hàng vào danh sách

                

                ProductNameComboBox.Text = "";
                QuantityTextBox.Text = "";
                UnitPriceTextBox.Text = "";
            }
            else
            {
                MessageBox.Show("Please select a product.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            int staffId = 1;
            DateTime orderDate = DateTime.Today;

            Order order = new Order
            {
                StaffId = staffId,
                OrderDate = orderDate
            };

            try
            {
                _orderRepository.InsertOrder(order);
                int orderId = _orderRepository.GetLastOrderID().OrderId;

                foreach (var orderDetail in _orderDetailsList)
                {
                    orderDetail.OrderId = orderId;
                    _orderDetailRepository.InsertOrderDetail(orderDetail);
                }

                MessageBox.Show("Order and order details added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _orderDetailsList.Clear();
                OrderDetailsDataGrid.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
