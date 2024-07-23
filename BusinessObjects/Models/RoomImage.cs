using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class RoomImage
{
    public int RoomImageId { get; set; }

    public int RoomId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
