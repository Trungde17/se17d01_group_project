using BusinessObjects.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyHomestayWpf.ViewModel
{
    public class BookingViewModel: INotifyPropertyChanged
    {
        private User user;
        private Room _selectedRoom;
        private IRoomService roomService;
        private IBookingService bookingService;
        private ObservableCollection<Room> rooms;
        private DateTime datedBooking;
        private DateOnly dateCheckin;
        private DateOnly dateCheckout;
        private decimal totalAmount;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Room> AvailableRooms { get; set; }        
        public ObservableCollection<Room>Rooms
        {
            get => rooms;
            set
            {
                rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }
        public decimal TotalAmount
        {
            get => totalAmount; set
            {
                totalAmount = value; OnPropertyChanged(nameof(TotalAmount));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public DateTime DatedBooking
        {
            get => datedBooking;
            set
            {
                datedBooking = value;
                OnPropertyChanged(nameof(DatedBooking));
            }
        }
        public DateOnly DateCheckin
        {
            get => dateCheckin;
            set
            {
                dateCheckin = value;
                OnPropertyChanged(nameof(DateCheckin));
            }
        }

        public DateOnly DateCheckout
        {
            get => dateCheckout;
            set
            {
                dateCheckout = value;
                OnPropertyChanged(nameof(DateCheckout));
            }
        }
        public BookingViewModel(IRoomService roomService, IBookingService bookingService, User user)
        {
            this.user = user;
            this.roomService = roomService;
            this.bookingService = bookingService;
            this.DatedBooking = DateTime.Now;
            AvailableRooms = new ObservableCollection<Room>();
        }

        public void SearchAvailableRooms(DateTime? checkInDateTime, DateTime? checkOutDateTime)
        {
            if (checkInDateTime.HasValue && checkOutDateTime.HasValue)
            {
                DateOnly checkIn = DateOnly.FromDateTime(checkInDateTime.Value);
                DateOnly checkOut = DateOnly.FromDateTime(checkOutDateTime.Value);               
                if (checkIn > DateOnly.FromDateTime(DateTime.Now) && checkOut > DateOnly.FromDateTime(DateTime.Now) && (checkOut.ToDateTime(new TimeOnly(0, 0)) - checkIn.ToDateTime(new TimeOnly(0, 0))).TotalDays > 0)
                {
                    DateCheckin = checkIn;
                    DateCheckout = checkOut;
                    var availableRooms = FindAvailableRooms(checkIn, checkOut);
                    AvailableRooms.Clear();
                    foreach (var room in availableRooms)
                    {
                        AvailableRooms.Add(room);
                    }
                }
            }
        }

        private IEnumerable<Room> FindAvailableRooms(DateOnly checkIn, DateOnly checkOut)
        {
            List<Booking> bookings = bookingService.GetBookings();
            List<Room> rooms = roomService.GetRooms();

            List<Booking> overlappingBookings = bookings.Where(b =>
                b.CheckInDate <= checkOut &&
                b.CheckOutDate >= checkIn).ToList();

            foreach (Room room in rooms)
            {
                bool isRoomBooked = overlappingBookings.Any(booking => booking.BookingRooms.Any(br => br.RoomId == room.RoomId));
                if (!isRoomBooked)
                {
                    yield return room;
                }
            }
        }

        public void AddRoomToBooking()
        {
            if (Rooms == null)Rooms = new ObservableCollection<Room>();
            if (!Rooms.Any(r => r.RoomId == SelectedRoom.RoomId))
            {
                Rooms.Add( SelectedRoom);
                CalculateToTalAmount();
            }
        }       

        public void AddBooking()
        {
                List<BookingRoom> bookingRooms = new List<BookingRoom>();
                foreach (Room r in Rooms)
                {
                    bookingRooms.Add(new BookingRoom() { RoomId = r.RoomId});
                }
                Booking booking = new Booking() { UserId = user.UserId, CheckInDate = DateCheckin, CheckOutDate = DateCheckout, TotalPrice = TotalAmount, BookingRooms = bookingRooms };
                bookingService.Add(booking);
                Rooms.Clear();
            
        }

        public void CalculateToTalAmount()
        {
            decimal total = 0;
            foreach(Room r in Rooms)
            {
                total += r.PricePerNight * (decimal)((DateCheckout.ToDateTime(new TimeOnly(0, 0)) - DateCheckin.ToDateTime(new TimeOnly(0, 0))).TotalDays);
            }
            TotalAmount = total;
        }
    }
}
   
