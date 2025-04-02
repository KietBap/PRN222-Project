using PRN222.RoomBooking.Repositories.Enums;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class Booking
{
    public int BookingId { get; set; }
    public string UserCode { get; set; } = null!;
    public string? Purpose { get; set; }
    public DateOnly BookingDate { get; set; }
    public BookingStatus BookingStatus { get; set; }
    public DateTime? CreatedDate { get; set; }
    public virtual User UserCodeNavigation { get; set; } = null!;
    public virtual ICollection<RoomSlot> RoomSlots { get; set; } = new List<RoomSlot>();
}