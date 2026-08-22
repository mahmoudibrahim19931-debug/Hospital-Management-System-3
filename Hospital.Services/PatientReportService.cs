using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class PatientReportService : IPatientReportService
    {
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public PatientReportService(
    IUnitOfWork unitOfWork,
    INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;

            _notificationService =
                notificationService;
        }

        public List<PatientReportViewModel> GetPatientReports(string patientId)
        {
            var reports = _unitOfWork
                .GenericRepository<PatientReport>()
                .GetAll(
                    x => x.PatientId == patientId,
                    includeProperties:
                    "Doctor,Patient,PrescribedMedicines.Medicine")
                .ToList();

            return reports
                .Select(x => new PatientReportViewModel(x))
                .ToList();
        }

        public void CreateReport(

    string doctorId,

    string patientId,

    string diagnose,

    string prescription,

    List<int> medicineIds)

        {

            var report = new PatientReport
            {

                DoctorId = doctorId,


                PatientId = patientId,


                Diagnose = diagnose,


                Prescription = prescription,


                CreatedDate = DateTime.Now

            };




            _unitOfWork
                .GenericRepository<PatientReport>()
                .Add(report);



            _unitOfWork.Save();




            if (medicineIds != null)
            {

                foreach (var medicineId in medicineIds)
                {

                    var prescribedMedicine =
                        new PrescribedMedicine
                        {

                            PatientReportId = report.Id,


                            MedicineId = medicineId,

                            Dose = "As Directed"

                        };




                    _unitOfWork
                        .GenericRepository<PrescribedMedicine>()
                        .Add(prescribedMedicine);





                    var medicine =

                        _unitOfWork
                        .GenericRepository<Medicine>()
                        .GetById(medicineId);




                    if (medicine != null)
                    {

                        if (medicine.Quantity > 0)
                        {

                            medicine.Quantity--;



                            _unitOfWork
                                .GenericRepository<Medicine>()
                                .Update(medicine);

                        }

                    }


                }


            }




            _unitOfWork.Save();




            _notificationService.Create(

                patientId,


                "New Medical Report Available",


                "Patient",


                "Home",


                "Index",


                "/Patient/Home"

            );


        }

        public PatientReportViewModel GetById(int id)
        {
            var report = _unitOfWork
                .GenericRepository<PatientReport>()
                .GetAll(
                    x => x.Id == id,
                    includeProperties:
                    "Doctor,Patient,PrescribedMedicines.Medicine")
                .FirstOrDefault();

            if (report == null)
                return null;

            return new PatientReportViewModel(report);
        }
    }
}