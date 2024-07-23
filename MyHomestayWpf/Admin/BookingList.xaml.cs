using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls; // Add this line
using BusinessObjects.Models;
using Services;

namespace MyHomestayWpf
{
    public partial class BookingList : Window
    {
        private readonly IBookingService bookingService;

        public BookingList()
        {
            bookingService = new BookingService();
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            List<Booking> bookings = bookingService.GetBookings();
            BookingsDataGrid.ItemsSource = bookings;
        }

        private void EditBooking_Click(object sender, RoutedEventArgs e)
        {
            int bookingId = (int)((Button)sender).Tag;
            // Implement edit logic here
            MessageBox.Show($"Edit Booking with ID: {bookingId}");
        }

        private void DeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            int bookingId = (int)((Button)sender).Tag;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this booking?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Implement delete logic here
                MessageBox.Show($"Deleted Booking with ID: {bookingId}");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            this.Close();
            adminPage.Show();
        }
    }
}
