using System.Collections.Generic;

namespace Hospital.Models
{
    public class Medicine
    {

        public int Id { get; set; }



        public string Name { get; set; }



        public string Type { get; set; }



        public decimal Cost { get; set; }



        public string Description { get; set; }





        public int Quantity { get; set; }





        public int MinimumQuantity { get; set; }





        public int? SupplierId { get; set; }



        public Supplier Supplier { get; set; }





        public ICollection<MedicineReport> MedicineReport
        { get; set; }




        public ICollection<PrescribedMedicine>
            PrescribedMedicine
        { get; set; }

    }
}