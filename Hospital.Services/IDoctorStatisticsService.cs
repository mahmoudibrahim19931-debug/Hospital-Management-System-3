using System;
using System.Collections.Generic;
using System.Text;

using Hospital.ViewModels;

namespace Hospital.Services
{
    public interface IDoctorStatisticsService
    {

        DoctorStatisticsViewModel GetStatistics(
            string doctorId);

    }
}