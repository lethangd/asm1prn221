using BusinessObject;
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
using System.Windows.Shapes;

namespace MyStoreWPFApp
{
    /// <summary>
    /// Interaction logic for StaffDashboard.xaml
    /// </summary>
    public partial class StaffDashboard : Window
    {
		public Staff Staff { get; set; }
		public StaffDashboard()
        {
            InitializeComponent();
			MainFrame.Navigate(new HomePage());
		}
		private void NavigateToHome(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new HomePage());
		}

		private void NavigateToProducts(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new ProductsPage());
		}

		private void NavigateToOrders(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new OrdersPage(Staff.StaffId));
		}
		private void NavigateToReportOrders(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new ReportOrdersPage(Staff.StaffId));
		}

		private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void NavigateToProfile(object sender, RoutedEventArgs e)
        {
            var profileView = new ProfileView(Staff);
            profileView.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var profileView = new UpdateProfilePage(Staff);
            profileView.Show();
        }
    }
}
