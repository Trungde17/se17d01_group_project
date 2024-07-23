using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int UserId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public decimal TotalPrice { get; set; }

    public byte? BookingStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();

    public virtual User User { get; set; } = null!;
}
