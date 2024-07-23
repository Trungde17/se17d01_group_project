using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using MyHomestayWpf.ViewModel;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MyHomestayWpf
{
    public partial class MainWindow : Window
    {
        public User User {  get; set; }
        private readonly IUserService _userService;
        public BookingViewModel BookingViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            User = _userService.GetUsers().FirstOrDefault();
            IRoomService roomService = new RoomSevice();
            IBookingService bookingService = new BookingServices();
            BookingViewModel = new BookingViewModel(roomService, bookingService, User);
            DataContext = this;
        }

        public MainWindow(User user):this()
        {
            User = user;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? checkInDateTime = CheckinDatePicker.SelectedDate;
            DateTime? checkOutDateTime = CheckoutDatePicker.SelectedDate;
            BookingViewModel.SearchAvailableRooms(checkInDateTime, checkOutDateTime);
        }

        private void RoomListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedRoom = listView.SelectedItem as Room;
            if (selectedRoom != null)
            {
                BookingViewModel.SelectedRoom = selectedRoom;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedBookingRoom = comboBox.SelectedItem as BookingRoom;
            if (selectedBookingRoom != null)
            {
                BookingViewModel.SelectedRoom = selectedBookingRoom.Room;
            }
        }

        private void BookRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookingViewModel.SelectedRoom != null)
            {
                BookingViewModel.AddRoomToBooking();
                BookingViewModel.DatedBooking = DateTime.Now;
            }
        }

        private void RemoveRoom_click(object sender, RoutedEventArgs e)
        {
            var selectedRoom = RoomComboBox.SelectedItem as Room;
            if (selectedRoom != null) { BookingViewModel.Rooms.Remove(selectedRoom); BookingViewModel.DatedBooking = DateTime.Now; }
        }
        private void ConfirmBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookingViewModel.Rooms != null && BookingViewModel.Rooms.Count > 0)
            {
                BookingViewModel.AddBooking();
                MessageBox.Show("booking successful!");
            }
            else MessageBox.Show("Your booking fail!");
        }

        private void CancelBookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingViewModel.Rooms.Clear();
            BookingViewModel.TotalAmount = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyBookingWindow myBookingWindow = new MyBookingWindow(User);
            myBookingWindow.Show();
        }
    }
}