namespace Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public string Description { get; set; }

        public string DoctorId { get; set; }

        public ApplicationUser Doctor { get; set; }

        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }

        public AppointmentStatus Status { get; set; }

        public enum AppointmentStatus
        {
            Pending,

            Approved,

            Cancelled,

            Completed,

            Waiting,

            InProgress,

            Done,

            PatientPending,

            PatientConfirmed
        }


    }
}