using System.Windows;
using BusinessObjects.Models;
using MyHomestayWpf.Admin;
using Services;

namespace MyHomestayWpf
{
    public partial class AdminPage : Window
    {
        private readonly IBookingService bookingService;
        private readonly IUserService userService;
        private readonly IRoomService roomService;
        private readonly IHomestayService homestayService;

        public AdminPage()
        {
            roomService = new RoomService();
            homestayService = new HomestayService();
            bookingService = new BookingService();
            userService = new UserService();
            InitializeComponent();
        }

        private void BookingList_Click(object sender, RoutedEventArgs e)
        {
            BookingList bookingList = new BookingList();
            bookingList.Show();
            this.Close();
        }

        private void UserList_Click(object sender, RoutedEventArgs e)
        {
            UserList userList = new UserList();
            userList.Show();
            this.Close();
        }

        private void RoomList_Click(object sender, RoutedEventArgs e)
        {
            RoomList roomList = new RoomList();
            roomList.Show();
            this.Close();
        }

        private void HomestayList_Click(object sender, RoutedEventArgs e)
        {
            RoomHomestay homestay = new RoomHomestay();
            homestay.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
