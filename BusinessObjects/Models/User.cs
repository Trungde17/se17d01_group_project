using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
