using BusinessObjects.Models;
using DataAccessaLayers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyHomestayWpf.Admin
{
    public partial class RoomList : Window
    {
        private Homestay selectedHomestay;

        public RoomList()
        {
            InitializeComponent();
            LoadHomestays();
        }

        private void LoadHomestays()
        {
            try
            {
                var homestays = HomestayDAO.Instance.GetCustomers();
                dataGrid.ItemsSource = homestays;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading homestays: {ex.Message}");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is Homestay homestay)
            {
                selectedHomestay = homestay;
                DisplayHomestayDetails(homestay);
            }
        }

        private void DisplayHomestayDetails(Homestay homestay)
        {
            txtName.Text = homestay.HomestayName;
            txtAddress.Text = homestay.Address;
            txtPhone.Text = homestay.Phone;
            txtEmail.Text = homestay.Email;
            if (homestay.HomestayImages.Any())
            {
                imageDisplay.Source = new BitmapImage(new Uri(homestay.HomestayImages.First().ImageUrl));
            }
            else
            {
                imageDisplay.Source = null;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHomestay != null)
            {
                try
                {
                    selectedHomestay.HomestayName = txtName.Text;
                    selectedHomestay.Address = txtAddress.Text;
                    selectedHomestay.Phone = txtPhone.Text;
                    selectedHomestay.Email = txtEmail.Text;
                    selectedHomestay.UpdatedAt = DateTime.Now;

                    HomestayDAO.Instance.Update(selectedHomestay);
                    LoadHomestays(); // Refresh the data grid
                    MessageBox.Show("Homestay updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating homestay: {ex.Message}");
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // Assuming MainWindow is the main window to go back to
            AdminPage adminPage = new AdminPage();
            this.Close();
            adminPage.Show();
        }
    }
}
