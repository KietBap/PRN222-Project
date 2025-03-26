using PRN222.RoomBooking.Repositories.Enums;
using System;
using System.Collections.Generic;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int Capacity { get; set; }

    public RoomStatus Status { get; set; }

    public int CampusId { get; set; }

    public virtual Campus Campus { get; set; } = null!;

    public virtual ICollection<RoomSlot> RoomSlots { get; set; } = new List<RoomSlot>();
}
