using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IMedicineService
    {
        List<MedicineViewModel> GetAll();

        MedicineViewModel GetById(int id);

        MedicineViewModel GetDetails(int id);

        void Create(MedicineViewModel model);

        void Restock(int id, int quantity);
    }
}