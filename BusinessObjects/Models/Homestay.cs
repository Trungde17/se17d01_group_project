using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Homestay
{
    public int HomestayId { get; set; }

    public string HomestayName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<HomestayImage> HomestayImages { get; set; } = new List<HomestayImage>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
