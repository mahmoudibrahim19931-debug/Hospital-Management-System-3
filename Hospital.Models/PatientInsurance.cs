using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Models
{
    public class PatientInsurance
    {

        public int Id { get; set; }



        public string PatientId { get; set; }



        [ForeignKey(nameof(PatientId))]
        public ApplicationUser Patient
        {
            get;
            set;
        }



        public int InsuranceId
        {
            get;
            set;
        }



        [ForeignKey(nameof(InsuranceId))]
        public Insurance Insurance
        {
            get;
            set;
        }



        public bool IsActive
        {
            get;
            set;
        }
        =
        true;
        public DateTime StartDate { get; set; }


        public DateTime? EndDate { get; set; }


        public bool IsApproved { get; set; }


        public DateTime CreatedDate { get; set; }
    }
}