using System.Collections.Generic;

namespace Hospital.Models
{
    public class Insurance
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string PolicyNumber { get; set; }


        public string StartDate { get; set; }


        public string EndDate { get; set; }


        public decimal DiscountPercentage { get; set; }


        public bool IsActive { get; set; } = true;



        public ICollection<PatientInsurance> PatientInsurances
        {
            get;
            set;
        }
=
new List<PatientInsurance>();



        public ICollection<Bill> Bills
        {
            get;
            set;
        }
        =
        new List<Bill>();

    }
}