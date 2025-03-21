using System;
using System.Collections.Generic;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class Campus
{
    public int CampusId { get; set; }

    public string CampusName { get; set; } = null!;

    public string? Location { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
