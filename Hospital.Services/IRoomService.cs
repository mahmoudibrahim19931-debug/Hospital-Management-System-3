using Hospital.Models;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public interface IRoomService
    {
        PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize);

        RoomViewModel? GetRoomById(int roomId);

        void InsertRoom(RoomViewModel room);

        void UpdateRoom(RoomViewModel room);

        void DeleteRoom(int id);

        List<HospitalInfo> GetHospitals();

        List<ApplicationUser> GetPatients();

   
        bool RoomNumberExists(string roomNumber, int roomId = 0);
    }
}
