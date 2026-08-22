using Microsoft.AspNetCore.Http;
using System;

namespace Hospital.ViewModels
{
    public class MedicalFileViewModel
    {

        public int Id { get; set; }


        public string PatientId { get; set; }

        public string DoctorId { get; set; }


        public string FileName { get; set; }

        public string FilePath { get; set; }


        public DateTime UploadDate { get; set; }

      

        public IFormFile File { get; set; }


    }
}