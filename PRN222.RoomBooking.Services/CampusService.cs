using PRN222.RoomBooking.Repositories.Data;
using PRN222.RoomBooking.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.RoomBooking.Services
{
    internal class CampusService : ICampusService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CampusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Campus>> GetCampus()
        {
            return await _unitOfWork.CampusRepository().GetAllAsync();
        }
    }
}
