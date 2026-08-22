using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Services
{
    public interface IStatisticsService
    {
        StatisticsViewModel GetStatistics();
    }
}