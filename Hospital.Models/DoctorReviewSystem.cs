using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Models
{
    public class DoctorReviewSystem
    {
        public int Id { get; set; }

        public string DoctorId { get; set; }

        public ApplicationUser Doctor { get; set; }

        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
