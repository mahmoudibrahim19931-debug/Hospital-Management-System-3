using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IMedicalFileService
    {

        void Upload(MedicalFileViewModel vm);



        List<MedicalFileViewModel>
            GetPatientFiles(string patientId);



        List<MedicalFileViewModel>
            GetDoctorFiles(string doctorId);

    }
}