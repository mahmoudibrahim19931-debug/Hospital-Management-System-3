using System;

namespace Hospital.Models
{
    public class Bill
    {

        public int Id { get; set; }



        public string PatientId { get; set; }

        public ApplicationUser Patient { get; set; }




        public string DoctorId { get; set; }

        public ApplicationUser Doctor { get; set; }




        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }





        public decimal Amount { get; set; }



        public decimal OriginalAmount
        {
            get;
            set;
        }



        public decimal DiscountAmount
        {
            get;
            set;
        }



        public decimal FinalAmount
        {
            get;
            set;
        }





        public int? InsuranceId
        {
            get;
            set;
        }



        public Insurance Insurance
        {
            get;
            set;
        }




        public bool IsPaid
        {
            get;
            set;
        }




        public DateTime CreatedDate
        {
            get;
            set;
        }




        public DateTime? PaidDate
        {
            get;
            set;
        }


    }
}