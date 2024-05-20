using BusinessObject.Models;
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
	/// Interaction logic for StaffPage.xaml
	/// </summary>
	public partial class StaffPage : Page
	{

        MyStoreContext context;
		public StaffPage()
		{
            InitializeComponent();
            context = new MyStoreContext();
            lsStaff.ItemsSource = context.Staffs.ToList();
            List<int> roles = context.Staffs.Select(c => c.Role).Distinct().ToList();
            cbRole.ItemsSource = roles;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtStaffName.Text.Length == 0 || txtStaffName.Text.Length == 0 || cbRole.SelectedIndex < 0)
            {
                MessageBox.Show("Please fill in all fields");
            }
            else
            {
                try
                {
                    Staff staff = new Staff
                    {
                        Name = txtStaffName.Text,
                        Role = int.Parse(cbRole.SelectedValue.ToString()),
                        Password = txtStaffPassword.Text,

                    };

                    context.Staffs.Add(staff);
                    context.SaveChanges();
                    //MessageBox.Show("Added!");
                    lsStaff.ItemsSource = context.Staffs.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(cbRole.SelectedValue.ToString());
                }
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lsStaff.SelectedItem is Staff selectedStaff)
            {
                int staffId = selectedStaff.StaffId;

                Staff Staff = context.Staffs.Find(staffId);
                Staff.Name = txtStaffName.Text;
                Staff.Password = txtStaffPassword.Text;
                Staff.Role = int.Parse(cbRole.SelectedValue.ToString());
                try
                {
                    context.Staffs.Update(Staff);
                    context.SaveChanges();
                    //MessageBox.Show("Updated!");
                    lsStaff.ItemsSource = context.Staffs.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsStaff.SelectedItem is Staff selectedStaff)
            {
                int staffId = selectedStaff.StaffId;

                Staff Staff = context.Staffs.Find(staffId);

                try
                {
                    context.Staffs.Remove(Staff);
                    context.SaveChanges();
                    //MessageBox.Show("Removed!");
                    lsStaff.ItemsSource = context.Staffs.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            if (txtSearch == null)
            {
                lsStaff.ItemsSource = context.Staffs.ToList();
            }
            else
            {
                List<Staff> listSearch = context.Staffs.Where(p => p.Name.ToLower().Trim().Contains(txtSearch.Text)).ToList();
                lsStaff.ItemsSource = listSearch;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cbRole.SelectedIndex = -1;
            txtStaffName.Clear();
            txtStaffPassword.Clear();
            txtSearch.Clear();

            lsStaff.ItemsSource = context.Staffs.ToList();

        }
    }
}
