using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface ISupplierService
    {

        List<SupplierViewModel> GetAll();


        SupplierViewModel GetById(int id);


        void Create(SupplierViewModel model);


    }
}