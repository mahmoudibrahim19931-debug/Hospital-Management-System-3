using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels
{
    public class MedicineViewModel
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string Type { get; set; }


        public decimal Cost { get; set; }


        public string Description { get; set; }



        public int Quantity { get; set; }



        public int MinimumQuantity { get; set; }



        public int? SupplierId { get; set; }



        public string SupplierName { get; set; }

        public List<SelectListItem> Suppliers { get; set; }
    = new();

        public List<MedicineUsageViewModel> UsageHistory
    = new();

        public bool IsLowStock
        {
            get
            {
                return Quantity <= MinimumQuantity;
            }
        }

        public MedicineViewModel()
        {

        }



        public MedicineViewModel(Medicine model)
        {

            Id = model.Id;


            Name = model.Name;


            Type = model.Type;


            Cost = model.Cost;


            Description = model.Description;



            Quantity = model.Quantity;



            MinimumQuantity = model.MinimumQuantity;



            SupplierId = model.SupplierId;



            SupplierName =
                model.Supplier?.Company;

        }



        public Medicine ConvertViewModel(
            MedicineViewModel model)
        {

            return new Medicine
            {

                Id = model.Id,


                Name = model.Name,


                Type = model.Type,


                Cost = model.Cost,


                Description = model.Description,



                Quantity = model.Quantity,



                MinimumQuantity =
                    model.MinimumQuantity,



                SupplierId =
                    model.SupplierId

            };

        }

    }
}