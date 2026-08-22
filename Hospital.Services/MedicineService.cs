using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<MedicineViewModel> GetAll()
        {

            return _unitOfWork
                .GenericRepository<Medicine>()
                .GetAll(
                    includeProperties:
                    "Supplier")
                .Select(x =>
                    new MedicineViewModel(x))
                .ToList();

        }

        public MedicineViewModel GetById(int id)
        {

            var medicine = _unitOfWork
                .GenericRepository<Medicine>()
                .GetAll(

                    x => x.Id == id,

                    includeProperties:
                    "Supplier"

                    )
                .FirstOrDefault();



            if (medicine == null)
                return null;



            return new MedicineViewModel(
                medicine);

        }

        public void Create(MedicineViewModel model)
        {
            var medicine = model.ConvertViewModel(model);

            _unitOfWork
                .GenericRepository<Medicine>()
                .Add(medicine);

            _unitOfWork.Save();
        }

        public void Restock(int id, int quantity)
        {

            var medicine = _unitOfWork
                .GenericRepository<Medicine>()
                .GetById(id);


            if (medicine == null)
                return;


            medicine.Quantity += quantity;


            _unitOfWork
                .GenericRepository<Medicine>()
                .Update(medicine);


            _unitOfWork.Save();

        }

        public MedicineViewModel GetDetails(int id)
        {

            var medicine = _unitOfWork
                .GenericRepository<Medicine>()
                .GetById(id);


            var vm = new MedicineViewModel(medicine);



            vm.UsageHistory =

                _unitOfWork
                .GenericRepository<PrescribedMedicine>()
                .GetAll(

                    x => x.MedicineId == id,

                    includeProperties:
                    "PatientReport.Patient,PatientReport.Doctor"

                    )


                .Select(x => new MedicineUsageViewModel
                {


                    PatientName =
                    x.PatientReport.Patient.Name,



                    DoctorName =
                    x.PatientReport.Doctor.Name,



                    ReportId =
                    x.PatientReport.Id,



                    Diagnose =
                    x.PatientReport.Diagnose


                })


                .ToList();



            return vm;

        }
    }
}