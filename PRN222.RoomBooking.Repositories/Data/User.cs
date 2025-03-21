using System;
using System.Collections.Generic;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class User
{
    public string UserCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CampusId { get; set; }

    public int DepartmentId { get; set; }

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Campus Campus { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;
}
