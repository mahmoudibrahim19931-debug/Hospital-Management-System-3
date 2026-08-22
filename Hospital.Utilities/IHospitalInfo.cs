using System;
using System.Collections.Generic;
using System.Text;
using Hospital.ViewModels;

namespace Hospital.Utilities
{
    public interface IHospitalInfo
    {
        PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize);

        HospitalInfoViewModel GetHospitalById(int HospitalId);

        void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo);

        void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo);

        void DeleteHospitalInfo(int id);

    }
}
