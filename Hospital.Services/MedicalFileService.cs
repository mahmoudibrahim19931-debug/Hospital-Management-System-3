using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hospital.Services
{
    public class MedicalFileService : IMedicalFileService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;



        public MedicalFileService(

            IUnitOfWork unitOfWork,

            IWebHostEnvironment environment)

        {

            _unitOfWork = unitOfWork;

            _environment = environment;

        }




        public void Upload(MedicalFileViewModel vm)
        {

            if (vm.File == null)
                return;



            var uploadsFolder = Path.Combine(

                _environment.WebRootPath,

                "uploads",

                "medical"

            );




            if (!Directory.Exists(uploadsFolder))
            {

                Directory.CreateDirectory(

                    uploadsFolder);

            }




            var uniqueName =

                Guid.NewGuid()

                +

                Path.GetExtension(

                    vm.File.FileName);




            var filePath = Path.Combine(

                uploadsFolder,

                uniqueName);



            using (var stream = new FileStream(

                filePath,

                FileMode.Create))
            {

                vm.File.CopyTo(stream);

            }




            var medicalFile = new MedicalFile
            {

                PatientId = vm.PatientId,


                DoctorId = vm.DoctorId,


                FileName = vm.File.FileName,


                FilePath =

                    "/uploads/medical/"

                    +

                    uniqueName,


                UploadDate = DateTime.Now

            };




            _unitOfWork
                .GenericRepository<MedicalFile>()
                .Add(medicalFile);



            _unitOfWork.Save();

        }






        public List<MedicalFileViewModel>

            GetPatientFiles(

                string patientId)
        {


            return _unitOfWork

                .GenericRepository<MedicalFile>()

                .GetAll(

                    x => x.PatientId == patientId

                    )

                .OrderByDescending(

                    x => x.UploadDate)

                .Select(x =>

                    new MedicalFileViewModel
                    {

                        Id = x.Id,

                        PatientId = x.PatientId,

                        DoctorId = x.DoctorId,

                        FileName = x.FileName,

                        FilePath = x.FilePath,

                        UploadDate = x.UploadDate

                    }

                )

                .ToList();

        }








        public List<MedicalFileViewModel>

            GetDoctorFiles(

                string doctorId)
        {


            return _unitOfWork

                .GenericRepository<MedicalFile>()

                .GetAll(

                    x => x.DoctorId == doctorId

                    )

                .OrderByDescending(

                    x => x.UploadDate)

                .Select(x =>

                    new MedicalFileViewModel
                    {

                        Id = x.Id,

                        PatientId = x.PatientId,

                        DoctorId = x.DoctorId,

                        FileName = x.FileName,

                        FilePath = x.FilePath,

                        UploadDate = x.UploadDate

                    }

                )

                .ToList();

        }


    }
}