using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels
{
    public class RoomViewModel
    {

        public int Id { get; set; }

        public string RoomNumber { get; set; }

        public RoomType Type { get; set; }

        public RoomStatus Status { get; set; }

        public int Capacity { get; set; }

        public int OccupiedBeds { get; set; }

        public decimal DailyRate { get; set; }

        public int Floor { get; set; }

        public string? Wing { get; set; }

        public string? Notes { get; set; }

        public bool IsOccupied { get; set; }

        public string? PatientId { get; set; }

        public int HospitalId { get; set; }

        public string? HospitalName { get; set; }

        public string? PatientName { get; set; }

        public List<SelectListItem>? Hospitals { get; set; }

        public List<SelectListItem>? Patients { get; set; }

        public RoomViewModel()
        {
        }

        public RoomViewModel(Room model)
        {
            Id = model.Id;
            RoomNumber = model.RoomNumber;
            Type = model.Type;
            Status = model.Status;

            Capacity = model.Capacity;
            OccupiedBeds = model.OccupiedBeds;
            DailyRate = model.DailyRate;
            Floor = model.Floor;
            Wing = model.Wing;
            Notes = model.Notes;

            IsOccupied = model.IsOccupied;
            PatientId = model.PatientId;
            HospitalId = model.HospitalId;

            HospitalName = model.Hospital?.Name;
            PatientName = model.Patient?.Name;
            
        }
        public Room ConvertViewModel(RoomViewModel model)
        {
            return new Room
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

                IsOccupied = model.IsOccupied,
                PatientId = model.PatientId,

                HospitalId = model.HospitalId
            };
        }

    }
}
