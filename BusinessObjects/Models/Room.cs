using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int HomestayId { get; set; }

    public string RoomName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal PricePerNight { get; set; }

    public int MaxOccupancy { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<BookingRoom> BookingRooms { get; set; } = new List<BookingRoom>();

    public virtual Homestay Homestay { get; set; } = null!;

    public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
}
