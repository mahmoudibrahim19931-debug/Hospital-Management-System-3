using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class SupplierService : ISupplierService
    {

        private readonly IUnitOfWork _unitOfWork;



        public SupplierService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        public List<SupplierViewModel> GetAll()
        {

            return _unitOfWork
                .GenericRepository<Supplier>()
                .GetAll()
                .Select(x =>
                    new SupplierViewModel(x))
                .ToList();

        }




        public SupplierViewModel GetById(int id)
        {

            var supplier = _unitOfWork
                .GenericRepository<Supplier>()
                .GetById(id);



            if (supplier == null)
                return null;



            return new SupplierViewModel(
                supplier);

        }




        public void Create(
            SupplierViewModel model)
        {

            var supplier =
                model.ConvertViewModel();



            _unitOfWork
                .GenericRepository<Supplier>()
                .Add(supplier);



            _unitOfWork.Save();

        }


    }
}