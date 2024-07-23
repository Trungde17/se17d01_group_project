using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace MyHomestayWpf
{
    public partial class MyBookingWindow : Window
    {
        public User Customer { get; set; }
        private readonly IUserService customerService;
        private readonly IBookingService _bookingsService;
        private readonly IRoomService roomService;
        private ObservableCollection<Booking> myBookings;
        private Booking selectedBooking;
        private ObservableCollection<Room> selectedRooms;
        private decimal selectedBookingTotalAmount;

        public ObservableCollection<Booking> MyBookings
        {
            get { return myBookings; }
            set
            {
                myBookings = value;
                OnPropertyChanged(nameof(MyBookings));
            }
        }
        public Booking SelectedBooking
        {
            get => selectedBooking;
            set
            {
                selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }
        public ObservableCollection<Room> SelectedRooms
        {
            get => selectedRooms;
            set
            {
                selectedRooms = value;
                OnPropertyChanged(nameof(SelectedRooms));
            }
        }
        public decimal SelectedBookingTotalAmount
        {
            get => selectedBookingTotalAmount;
            set
            {
                selectedBookingTotalAmount = value;
                OnPropertyChanged(nameof(SelectedBookingTotalAmount));
            }
        }
        public MyBookingWindow()
        {
            InitializeComponent();
            customerService = new UserService();
            
            Customer = customerService.GetUsers().FirstOrDefault();
            roomService = new RoomSevice();
            _bookingsService = new BookingServices();
            List<Booking> bookings = _bookingsService.GetBookings();
            SelectedRooms = new ObservableCollection<Room>();
            MyBookings = new ObservableCollection<Booking>(bookings.Where(b => b.UserId == Customer.UserId).ToList() ?? new List<Booking>());
            DataContext = this;
        }
        public MyBookingWindow(User customer) : this()
        {
            Customer = customer;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                SelectedBooking = e.AddedItems[0] as Booking;
            }
            SetSelectedBookingRooms();
            SelectedBookingTotalAmount = SelectedBooking.TotalPrice;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SetSelectedBookingRooms()
        {
            SelectedRooms.Clear();
            List<BookingRoom> selectedbookingRooms = SelectedBooking.BookingRooms.ToList();
            foreach(BookingRoom booking_room in selectedbookingRooms)
            {
                SelectedRooms.Add(roomService.GetRooms().FirstOrDefault(r=>r.RoomId==booking_room.RoomId));
            }
        }
    }
}

