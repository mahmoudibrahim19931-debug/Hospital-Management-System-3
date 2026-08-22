using System;

namespace Hospital.ViewModels
{
    public class BillViewModel
    {

        public int Id { get; set; }


        public int AppointmentId { get; set; }



        public string PatientId { get; set; }

        public string PatientName { get; set; }




        public string DoctorId { get; set; }

        public string DoctorName { get; set; }





        public decimal Amount { get; set; }



        public decimal OriginalAmount { get; set; }



        public decimal DiscountAmount { get; set; }



        public decimal FinalAmount { get; set; }




        public int? InsuranceId { get; set; }



        public string InsuranceName { get; set; }




        public bool IsPaid { get; set; }




        public DateTime CreatedDate { get; set; }



        public DateTime? PaidDate { get; set; }



    }
}