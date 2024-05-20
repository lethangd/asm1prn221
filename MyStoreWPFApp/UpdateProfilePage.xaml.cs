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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MyStoreWPFApp
{
    /// <summary>
    /// Interaction logic for UpdateProfilePage.xaml
    /// </summary>
    public partial class UpdateProfilePage : Window
    {
        private Staff _staff;
        public UpdateProfilePage(Staff staff)
        {
            InitializeComponent();
            _staff = staff;
            LoadProfile();
        }

        private void LoadProfile()
        {
            txtUsername.Text = _staff.Name;
            txtPassword.Password = _staff.Password;
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new MyStoreContext())
            {
                var staff = context.Staffs.FirstOrDefault(s => s.StaffId == _staff.StaffId);
                if (staff != null)
                {
                    staff.Name = txtUsername.Text;
                    staff.Password = txtPassword.Password;
                    context.SaveChanges();
                    MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Staff not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
