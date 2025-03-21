using PRN222.RoomBooking.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.Services
{
    public interface ICampusService
    {
        Task<List<Campus>> GetCampus();
    }
}
