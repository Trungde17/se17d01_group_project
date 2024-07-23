using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class HomestayImage
{
    public int HomestayImageId { get; set; }

    public int HomestayId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual Homestay Homestay { get; set; } = null!;
}
