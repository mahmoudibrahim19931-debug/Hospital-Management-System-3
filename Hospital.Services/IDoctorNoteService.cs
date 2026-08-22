using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IDoctorNoteService
    {

        void Create(
            string patientId,
            string doctorId,
            string note);



        List<DoctorNoteViewModel>
            GetPatientNotes(
                string patientId);

    }
}