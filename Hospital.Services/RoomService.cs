using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using Hospital.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RoomService : IRoomService
    {
        private IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            _unitOfWork.GenericRepository<Room>().Delete(model);
            _unitOfWork.Save();
        }

        public PagedResult<RoomViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new RoomViewModel();
            int totalCount;
            List<RoomViewModel> vmList = new List<RoomViewModel>();

            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork
     .GenericRepository<Room>()
     .GetAll(includeProperties: "Hospital,Patient")
     .Skip(ExcludeRecords)
     .Take(pageSize)
     .ToList();

                totalCount = _unitOfWork.GenericRepository<Room>()
                    .GetAll()
                    .ToList()
                    .Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }
            var result = new PagedResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

        public RoomViewModel? GetRoomById(int roomId)
        {
            var model = _unitOfWork
                .GenericRepository<Room>()
                .GetAll(includeProperties: "Hospital,Patient")
                .FirstOrDefault(x => x.Id == roomId);

            if (model == null)
                return null;

            return new RoomViewModel
            {
                Id = model.Id,
                RoomNumber = model.RoomNumber,
                Type = model.Type,

                Status = model.Status,

                Capacity = model.Capacity,
                OccupiedBeds = model.OccupiedBeds,
                DailyRate = model.DailyRate,
                Floor = model.Floor,
                Wing = model.Wing,
                Notes = model.Notes,

                HospitalId = model.HospitalId,
                HospitalName = model.Hospital?.Name,

                PatientId = model.PatientId,
                PatientName = model.Patient?.Name,

                IsOccupied = model.IsOccupied
            };
        }

        public RoomViewModel GetHospitalById(int HospitalId)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(HospitalId);
            var vm = new RoomViewModel(model);
            return vm;
        }


        public void InsertRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            if (RoomNumberExists(model.RoomNumber))
                throw new Exception("Room Number already exists.");
            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModel Room)
        {
            var model = new RoomViewModel().ConvertViewModel(Room);
            var ModelById = _unitOfWork.GenericRepository<Room>().GetById(model.Id);

            ModelById.Type = Room.Type;
            ModelById.RoomNumber = Room.RoomNumber;
            ModelById.Status = Room.Status;

            if (RoomNumberExists(Room.RoomNumber, Room.Id))
                throw new Exception("Room Number already exists.");

            _unitOfWork.GenericRepository<Room>().Update(ModelById);
            _unitOfWork.Save();
        }
        public List<HospitalInfo> GetHospitals()
        {
            return _unitOfWork
                .GenericRepository<HospitalInfo>()
                .GetAll()
                .ToList();
        }
        public List<ApplicationUser> GetPatients()
        {
            return _unitOfWork
                .GenericRepository<ApplicationUser>()
                .GetAll()
                .Where(x => !x.IsDoctor)
                .ToList();
        }

        public bool RoomNumberExists(string roomNumber, int roomId = 0)
        {
            return _unitOfWork
                .GenericRepository<Room>()
                .GetAll()
                .Any(x =>
                    x.RoomNumber == roomNumber &&
                    x.Id != roomId);
        }

        private List<RoomViewModel> ConvertModelToViewModelList(List<Room> modelList)
        {
            
            return modelList.Select(x => new RoomViewModel(x)).ToList();
        }

    }
} 
