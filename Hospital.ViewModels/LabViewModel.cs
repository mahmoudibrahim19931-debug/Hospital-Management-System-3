using System;

namespace Hospital.ViewModels
{
    public class LabViewModel
    {

        public int Id { get; set; }


        public string LabNumber { get; set; }


        public string PatientId { get; set; }

        public string PatientName { get; set; }



        public string DoctorId { get; set; }

        public string DoctorName { get; set; }



        public string TestType { get; set; }

        public string TestCode { get; set; }



        public int Weight { get; set; }

        public int Height { get; set; }

        public int BloodPressure { get; set; }

        public int Temperature { get; set; }



        public string TestResult { get; set; }



        public bool IsDone { get; set; }



        public DateTime CreatedDate { get; set; }

        public DateTime? ResultDate { get; set; }

    }

}