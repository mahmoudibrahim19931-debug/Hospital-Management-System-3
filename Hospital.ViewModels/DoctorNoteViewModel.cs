using System;
using System.Collections.Generic;


namespace Hospital.ViewModels
{
    public class DoctorNoteViewModel
    {

        public int Id { get; set; }


        public string PatientId { get; set; }


        public string DoctorId { get; set; }


        public string Note { get; set; }


        public DateTime CreatedDate { get; set; }

    }
}