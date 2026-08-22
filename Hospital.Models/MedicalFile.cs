using System;

namespace Hospital.Models
{
    public class MedicalFile
    {

        public int Id { get; set; }


        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }



        public string DoctorId { get; set; }

        public ApplicationUser Doctor { get; set; }



        public string FileName { get; set; }

        public string FilePath { get; set; }



        public DateTime UploadDate { get; set; }


    }
}