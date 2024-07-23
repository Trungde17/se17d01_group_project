using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
            InitializePlaceholders();
        }

        private void LoadData()
        {
            List<Booking> bookings = bookingService.GetBookings();
            BookingsDataGrid.ItemsSource = bookings;
        }

        private void AddBooking_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out DateOnly checkInDate, out DateOnly checkOutDate, out decimal totalPrice, out byte bookingStatus))
            {
                return;
            }

            try
            {
                Booking newBooking = new Booking
                {
                    UserId = int.Parse(UserIdTextBox.Text),
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    TotalPrice = totalPrice,
                    BookingStatus = bookingStatus
                };

                bookingService.AddBooking(newBooking);
                LoadData();
                ClearTextBoxes();
                InitializePlaceholders();
                MessageBox.Show("Booking added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateBooking_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out DateOnly checkInDate, out DateOnly checkOutDate, out decimal totalPrice, out byte bookingStatus))
            {
                return;
            }

            try
            {
                if (BookingsDataGrid.SelectedItem is Booking selectedBooking)
                {
                    selectedBooking.UserId = int.Parse(UserIdTextBox.Text);
                    selectedBooking.CheckInDate = checkInDate;
                    selectedBooking.CheckOutDate = checkOutDate;
                    selectedBooking.TotalPrice = totalPrice;
                    selectedBooking.BookingStatus = bookingStatus;

                    bookingService.UpdateBooking(selectedBooking);
                    LoadData();
                    ClearTextBoxes();
                    InitializePlaceholders();
                    MessageBox.Show("Booking updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs(out DateOnly checkInDate, out DateOnly checkOutDate, out decimal totalPrice, out byte bookingStatus)
        {
            checkInDate = default;
            checkOutDate = default;
            totalPrice = 0;
            bookingStatus = 0;

            if (string.IsNullOrWhiteSpace(UserIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(CheckInDateTextBox.Text) ||
                string.IsNullOrWhiteSpace(CheckOutDateTextBox.Text) ||
                string.IsNullOrWhiteSpace(TotalPriceTextBox.Text) ||
                BookingStatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(UserIdTextBox.Text, out _))
            {
                MessageBox.Show("User ID must be a valid number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!DateOnly.TryParse(CheckInDateTextBox.Text, out checkInDate))
            {
                MessageBox.Show("Check-In Date must be a valid date in the format yyyy-mm-dd.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!DateOnly.TryParse(CheckOutDateTextBox.Text, out checkOutDate))
            {
                MessageBox.Show("Check-Out Date must be a valid date in the format yyyy-mm-dd.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (checkInDate >= checkOutDate)
            {
                MessageBox.Show("Check-In Date must be earlier than Check-Out Date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(TotalPriceTextBox.Text, out totalPrice) || totalPrice <= 0)
            {
                MessageBox.Show("Total Price must be a valid number greater than 0.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!byte.TryParse((BookingStatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(), out bookingStatus) ||
                (bookingStatus != 0 && bookingStatus != 1))
            {
                MessageBox.Show("Booking Status must be 0 or 1.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void EditBooking_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is int bookingId)
            {
                Booking booking = bookingService.GetBookingById(bookingId);
                if (booking != null)
                {
                    UserIdTextBox.Text = booking.UserId.ToString();
                    CheckInDateTextBox.Text = booking.CheckInDate.ToString("yyyy-MM-dd");
                    CheckOutDateTextBox.Text = booking.CheckOutDate.ToString("yyyy-MM-dd");
                    TotalPriceTextBox.Text = booking.TotalPrice.ToString();
                    BookingStatusComboBox.SelectedItem = booking.BookingStatus == 0 ? BookingStatusComboBox.Items[0] : BookingStatusComboBox.Items[1];
                }
            }
        }

        private void DeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is int bookingId)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        bookingService.DeleteBooking(bookingId);
                        LoadData();
                        MessageBox.Show("Booking deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminPage adminPage = new AdminPage();
            this.Close();
            adminPage.Show();
        }

        private void ClearTextBoxes()
        {
            UserIdTextBox.Clear();
            CheckInDateTextBox.Clear();
            CheckOutDateTextBox.Clear();
            TotalPriceTextBox.Clear();
            BookingStatusComboBox.SelectedItem = null;
        }

        private void InitializePlaceholders()
        {
            SetPlaceholderText(UserIdTextBox, "Enter User ID");
            SetPlaceholderText(CheckInDateTextBox, "yyyy-mm-dd");
            SetPlaceholderText(CheckOutDateTextBox, "yyyy-mm-dd");
            SetPlaceholderText(TotalPriceTextBox, "Enter Total Price");
        }

        private void SetPlaceholderText(TextBox textBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetPlaceholderText(textBox, textBox.Tag.ToString());
            }
        }

        private void BookingsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
