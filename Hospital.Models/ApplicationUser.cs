
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        public Gender Gender { get; set; }

        public string? Nationality { get; set; }

        public string? Address { get; set; }

        public DateTime DOB { get; set; }

        public string? Specilist { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        public string? City { get; set; }

   
        public bool IsDoctor { get; set; }
        public int? RoomId { get; set; }

        public virtual Room? Room { get; set; }

        [NotMapped]
        public ICollection<Appointment>? Appointments { get; set; }

        [NotMapped]
        public ICollection<PayRoll>? Payrolls { get; set; }

        public ICollection<PatientInsurance>

PatientInsurances
        {
            get;
            set;
        }
=
new List<PatientInsurance>();
    }


}

namespace Hospital.Models
{
    public enum Gender
    {
        Male, Female 
    }
}