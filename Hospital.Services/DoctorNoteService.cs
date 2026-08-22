using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class DoctorNoteService :
        IDoctorNoteService
    {

        private readonly IUnitOfWork _unitOfWork;


        public DoctorNoteService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public void Create(
            string patientId,
            string doctorId,
            string note)
        {

            var doctorNote =
                new DoctorNote
                {

                    PatientId = patientId,

                    DoctorId = doctorId,

                    Note = note,

                    CreatedDate = DateTime.Now

                };



            _unitOfWork
                .GenericRepository<DoctorNote>()
                .Add(doctorNote);



            _unitOfWork.Save();

        }




        public List<DoctorNoteViewModel>
            GetPatientNotes(
                string patientId)
        {


            var notes =

                _unitOfWork
                .GenericRepository<DoctorNote>()
                .GetAll(

                    x =>

                    x.PatientId == patientId

                )
                .OrderByDescending(

                    x => x.CreatedDate

                )
                .ToList();




            return notes
                .Select(

                    x =>

                    new DoctorNoteViewModel
                    {

                        Id = x.Id,

                        PatientId = x.PatientId,

                        DoctorId = x.DoctorId,

                        Note = x.Note,

                        CreatedDate = x.CreatedDate

                    }

                )
                .ToList();

        }


    }
}