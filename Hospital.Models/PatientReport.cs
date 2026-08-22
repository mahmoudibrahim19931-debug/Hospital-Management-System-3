using System;
using System.Collections.Generic;

namespace Hospital.Models
{
    public class PatientReport
    {
        public int Id { get; set; }

        public string Diagnose { get; set; }


        public string Prescription { get; set; }


        public string DoctorId { get; set; }

        public ApplicationUser Doctor { get; set; }


        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }


        public DateTime CreatedDate { get; set; }


        public ICollection<PrescribedMedicine>
            PrescribedMedicines
        {
            get;
            set;
        }
        =
        new List<PrescribedMedicine>();

    }
}