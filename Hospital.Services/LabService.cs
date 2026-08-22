using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class LabService : ILabService
    {
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public LabService(

    IUnitOfWork unitOfWork,

    INotificationService notificationService)

        {

            _unitOfWork = unitOfWork;

            _notificationService =
                notificationService;

        }

        public void Create(LabViewModel vm)
        {
            var lab = new Lab
            {
                LabNumber = Guid.NewGuid()
        .ToString()
        .Substring(0, 8),

                PatientId = vm.PatientId,

                DoctorId = vm.DoctorId,

                TestType = vm.TestType,

                TestCode = vm.TestCode,

                Weight = vm.Weight,

                Height = vm.Height,

                BloodPressure = vm.BloodPressure,

                Temperature = vm.Temperature,


                TestResult = "",


                CreatedDate = DateTime.Now,


                ResultDate = null,


                IsDone = false
            };

            _unitOfWork
                .GenericRepository<Lab>()
                .Add(lab);

            _unitOfWork.Save();
            _notificationService.Create(

    lab.PatientId,

    "Lab result is ready",

    "Patient",

    "Home",

    "Index",

    "/Patient/Home"

);
        }



        public void Update(LabViewModel vm)
        {
            var lab = _unitOfWork
                .GenericRepository<Lab>()
                .GetById(vm.Id);


            if (lab == null)
                return;


            lab.TestResult = vm.TestResult;

            lab.ResultDate = DateTime.Now;

            lab.IsDone = true;


            _unitOfWork
                .GenericRepository<Lab>()
                .Update(lab);


            _unitOfWork.Save();

            _notificationService.Create(

    lab.PatientId,

    "Lab result is ready",

    "Patient",

    "Home",

    "Index",

    "/Patient/Home"

);
        }




        public LabViewModel GetById(int id)
        {
            var lab = _unitOfWork
                .GenericRepository<Lab>()
                .GetAll(
                    x => x.Id == id,
                    includeProperties: "Patient,Doctor")
                .FirstOrDefault();


            if (lab == null)
                return null;


            return new LabViewModel
            {

                Id = lab.Id,

                PatientId = lab.PatientId,

                DoctorId = lab.DoctorId,

                PatientName = lab.Patient?.Name,

                DoctorName = lab.Doctor?.Name,

                TestType = lab.TestType,

                TestCode = lab.TestCode,

                TestResult = lab.TestResult,

                IsDone = lab.IsDone,

                CreatedDate = lab.CreatedDate,

                ResultDate = lab.ResultDate

            };
        }




        public List<LabViewModel> GetPatientLabs(string patientId)
        {

            var labs = _unitOfWork
                .GenericRepository<Lab>()
                .GetAll(
                    x => x.PatientId == patientId,
                    includeProperties:
                    "Patient,Doctor")
                .ToList();


            return labs.Select(x => new LabViewModel
            {

                Id = x.Id,

                PatientId = x.PatientId,

                DoctorId = x.DoctorId,

                PatientName = x.Patient?.Name,

                DoctorName = x.Doctor?.Name,

                TestType = x.TestType,

                TestCode = x.TestCode,

                TestResult = x.TestResult,

                IsDone = x.IsDone,

                CreatedDate = x.CreatedDate,

                ResultDate = x.ResultDate

            }).ToList();

        }




        public List<LabViewModel> GetPendingLabs(string search = "")
        {

            var labs = _unitOfWork
                .GenericRepository<Lab>()
                .GetAll(
                    includeProperties:
                    "Patient,Doctor")
                .ToList();



            if (!string.IsNullOrEmpty(search))
            {

                labs = labs
                    .Where(x =>

                        x.TestType.Contains(search)

                        ||

                        x.Patient.Name.Contains(search)

                    )
                    .ToList();

            }




            return labs
                .Select(x => new LabViewModel
                {

                    Id = x.Id,

                    PatientId = x.PatientId,

                    DoctorId = x.DoctorId,


                    PatientName =
                        x.Patient?.Name,


                    DoctorName =
                        x.Doctor?.Name,


                    TestType =
                        x.TestType,


                    TestCode =
                        x.TestCode,


                    TestResult =
                        x.TestResult,


                    IsDone =
                        x.IsDone,


                    CreatedDate =
                        x.CreatedDate,


                    ResultDate =
                        x.ResultDate

                })
                .ToList();

        }


    }
}