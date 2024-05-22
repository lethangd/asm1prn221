﻿using BusinessObject;
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

namespace MyStoreWPFApp
{
	/// <summary>
	/// Interaction logic for OrdersPage.xaml
	/// </summary>
	public partial class ReportOrdersPage : Page
	{
		public int StaffID { get; set; }
		public IOrderRepository orderRepository = new OrderRepository();
		public IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
		public ReportOrdersPage(int staffID)
		{
			InitializeComponent();
			StaffID = staffID;
			Staff staff = StaffDAO.Instance.GetStaffById(StaffID);
			StartDatePicker.SelectedDate = DateTime.Today.AddMonths(-1);
			EndDatePicker.SelectedDate = DateTime.Today;
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

		private void viewDetail(object sender, RoutedEventArgs e)
		{
			if (OrdersDataGrid.SelectedItem is Order order)
			{
				OrderDetailWindow orderDetailWindow = new OrderDetailWindow(orderDetailRepository.GetOrderDetailsByOrderID(order.OrderId));
				orderDetailWindow.Show();
			}
		}
	}
}
