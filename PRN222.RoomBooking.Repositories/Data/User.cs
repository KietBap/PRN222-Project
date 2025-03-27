using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class User
{
    public string UserCode { get; set; } = null!;
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public int CampusId { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Role { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public virtual Campus Campus { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;
}
