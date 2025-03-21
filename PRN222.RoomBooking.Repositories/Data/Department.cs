using System;
using System.Collections.Generic;

namespace PRN222.RoomBooking.Repositories.Data;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
