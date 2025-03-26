using PRN222.RoomBooking.Repositories.Enums;
using System;
using System.Collections.Generic;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class RoomSlot
{
    public int RoomSlotId { get; set; }

    public int RoomId { get; set; }

    public int SlotNumber { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public RoomSlotStatus Status { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
