using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Services
{
    public interface IDoctorDashboardService
    {
        DoctorDashboardViewModel GetDashboard(string doctorId);

    }
}
