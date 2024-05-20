using BusinessObject.Models;
using DataAccess;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.IO;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void NavigateToHome(object sender, RoutedEventArgs e)
		{
			string username = txtUsername.Text;
			string password = txtPassword.Password;
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			List<Staff> accounts = configuration.GetSection("Admin").Get<List<Staff>>();

			foreach (Staff acc in accounts)
			{
				if (acc.Name.Equals(username) && acc.Password.Equals(password))
				{
					AdminDashboard adminDashboard = new AdminDashboard { Staff = acc };
					adminDashboard.Show();
					this.Hide();
					return;
				}
			}

			StaffDAO staffDAO = new StaffDAO();
			Staff staff = staffDAO.GetStaffLogin(username, password);

			if (staff != null && staff.Role > 0)
			{
				StaffDashboard staffDashboard = new StaffDashboard {Staff = staff };
				staffDashboard.Show();
				this.Hide();
			}
			else if (staff != null && staff.Role == 0)
			{
				AdminDashboard adminDashboard = new AdminDashboard{ Staff = staff };
				adminDashboard.Show();
				this.Hide();
			}
			else
			{
				MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

	}
}