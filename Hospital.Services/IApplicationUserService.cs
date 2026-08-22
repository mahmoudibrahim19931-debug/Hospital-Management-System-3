using Hospital.Utilities;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public interface IApplicationUserService
    {
        PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize);

        PagedResult<ApplicationUserViewModel> GetAllDoctor(int PageNumber, int PageSize);

        PagedResult<ApplicationUserViewModel> GetAllPatient(int PageNumber, int PageSize);

        PagedResult<ApplicationUserViewModel> SearchDoctor(int PageNumber, int PageSize, string Spicility = null);

        ApplicationUserViewModel GetById(string id);

        List<ApplicationUserViewModel> GetDoctors();

        List<ApplicationUserViewModel> GetPatients();

        void UpdateDoctor(string id, bool isDoctor);
    }
}