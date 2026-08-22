namespace Hospital.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string RoomNumber { get; set; }

        public RoomType Type { get; set; }


        public RoomStatus Status { get; set; }

        public int Capacity { get; set; }

        public int OccupiedBeds { get; set; }


        public decimal DailyRate { get; set; }

 
        public int Floor { get; set; }


        public string? Wing { get; set; }

  
        public string? Notes { get; set; }

        public bool IsOccupied { get; set; }

        public string? PatientId { get; set; }

        public ApplicationUser? Patient { get; set; }

        public int HospitalId { get; set; }

        public HospitalInfo Hospital { get; set; }

      
    }
}