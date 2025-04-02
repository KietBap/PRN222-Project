using PRN222.RoomBooking.Repositories.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class Room
{
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Room Name is required")]
    public string RoomName { get; set; } = null!;

    [Required(ErrorMessage = "Capacity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public RoomStatus Status { get; set; }

    public int CampusId { get; set; } // Không cần [Required] vì int không thể null

    public virtual Campus? Campus { get; set; } // Cho phép null để tránh lỗi nếu dữ liệu chưa nạp

    public virtual ICollection<RoomSlot> RoomSlots { get; set; } = new List<RoomSlot>();
}
