using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface ILabService
    {
        void Create(LabViewModel vm);

        void Update(LabViewModel vm);

        LabViewModel GetById(int id);

        List<LabViewModel> GetPatientLabs(string patientId);

        List<LabViewModel> GetPendingLabs(string search = "");
    }
}