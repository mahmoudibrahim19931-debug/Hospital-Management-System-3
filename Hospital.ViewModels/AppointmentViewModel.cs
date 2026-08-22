using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using static Hospital.Models.Appointment;

namespace Hospital.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public string DoctorId { get; set; }

        public string PatientId { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public AppointmentStatus Status { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public List<SelectListItem> Doctors { get; set; }
    = new List<SelectListItem>();

        public List<SelectListItem> Patients { get; set; }
            = new List<SelectListItem>();

        public AppointmentViewModel()
        {
        }

        public AppointmentViewModel(Appointment model)
        {
            Id = model.Id;
            Name = model.Name;
            Type = model.Type;
            CreatedDate = model.CreatedDate;
            Description = model.Description;
            DoctorId = model.DoctorId;
            PatientId = model.PatientId;

            DoctorName = model.Doctor?.Name;
            PatientName = model.Patient?.Name;
            Status = model.Status;
            AppointmentDate = model.AppointmentDate;
        }

        public Appointment ConvertViewModel(AppointmentViewModel model)
        {
            return new Appointment
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                CreatedDate = model.CreatedDate,
                Description = model.Description,
                Status = model.Status,
                DoctorId = model.DoctorId,
                PatientId = model.PatientId,
                AppointmentDate = model.AppointmentDate
            };

        }
    }
}