using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using MyStoreWPFApp.OrderView;

namespace MyStoreWPFApp
{
	/// <summary>
	/// Interaction logic for OrdersPage.xaml
	/// </summary>
	public partial class OrdersPage : Page
	{
		public int StaffID { get; set; }
		public IOrderRepository orderRepository = new OrderRepository();
		public IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();

        public OrdersPage(int staffID)
		{
			InitializeComponent();
			StaffID = staffID;
			Staff staff = StaffDAO.Instance.GetStaffById(StaffID);
            if (StaffID == 0 || staff.Role==0)
			{
                OrdersDataGrid.ItemsSource = orderRepository.GetOrders();
			}
			else
			{
                OrdersDataGrid.ItemsSource = orderRepository.GetOrdersByStaffID(StaffID);
			}
		}

		private void SearchOrders_Click(object sender, RoutedEventArgs e)
		{
			DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
			DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;
			Staff staff = StaffDAO.Instance.GetStaffById(StaffID);
			if (StaffID == 0 || staff.Role == 0)
			{
				OrdersDataGrid.ItemsSource = orderRepository.GetOrdersByDate(startDate, endDate);
			}
			else
			{
				OrdersDataGrid.ItemsSource = orderRepository.GetOrdersByDateAndStaffID(startDate, endDate, StaffID);
			}
		}

		private void DetailOrder_Click(object sender, RoutedEventArgs e)
		{
			if (OrdersDataGrid.SelectedItem is Order order)
			{
				OrderDetailWindow orderDetailWindow = new OrderDetailWindow(orderDetailRepository.GetOrderDetailsByOrderID(order.OrderId));
				orderDetailWindow.Show();
			}
		}

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is AddOrder existingAddOrder)
                {
                    existingAddOrder.Activate();
                    return;
                }
            }
            AddOrder addOrder = new AddOrder(StaffID);
			addOrder.Show();
		}
        private void LoadOrders()
        {
            var orders = orderRepository.GetOrders();
            OrdersDataGrid.ItemsSource = orders;
        }
        private void AdminDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy hàng hiện tại từ DataGrid
            Order selectedOrder = OrdersDataGrid.SelectedItem as Order;
            if (selectedOrder == null)
            {
                MessageBox.Show("Please select an order to delete.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Xác nhận với người dùng trước khi xóa
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete Order ID {selectedOrder.OrderId}?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Xóa Order từ cơ sở dữ liệu
                    orderRepository.RemoveOrder(selectedOrder.OrderId);
                    MessageBox.Show("Order deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cập nhật lại DataGrid
                    LoadOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AdminEditButton_Click(object sender, RoutedEventArgs e)
        {
            EditOrder editOrder = new EditOrder();
            editOrder.Show();
        }
    }
}
