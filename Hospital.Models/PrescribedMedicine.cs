using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Models
{
    public class PrescribedMedicine
    {
        public int Id { get; set; }


        public int MedicineId { get; set; }

        public Medicine Medicine { get; set; }



        public int PatientReportId { get; set; }

        public PatientReport PatientReport { get; set; }



        public string Dose { get; set; }


        public string Frequency { get; set; }


        public int DurationDays { get; set; }


        public string Notes { get; set; }


    }
}
