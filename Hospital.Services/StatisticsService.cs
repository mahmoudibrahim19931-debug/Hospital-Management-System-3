using Hospital.Repositories;
using Hospital.ViewModels;
using System.Linq;
using Hospital.Models;

namespace Hospital.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext _context;

        public StatisticsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public StatisticsViewModel GetStatistics()
        {
            return new StatisticsViewModel
            {

                DoctorsCount =
        _context.ApplicationUsers
        .Count(x => x.IsDoctor),



                PatientsCount =
        _context.ApplicationUsers
        .Count(x => !x.IsDoctor),



                RoomsCount =
        _context.Rooms.Count(),



                AppointmentsCount =
        _context.Appointments.Count(),




                BillsCount =
        _context.Bills.Count(),




                PaidBills =
        _context.Bills.Count(x => x.IsPaid),




                UnpaidBills =
        _context.Bills.Count(x => !x.IsPaid),




                Revenue =
        _context.Bills
        .Where(x => x.IsPaid)
        .Sum(x => x.Amount),




                LabsCount =
        _context.Labs.Count(),




                PendingLabs =
        _context.Labs.Count(x => !x.IsDone),




                LatestDoctors =
        _context.ApplicationUsers
        .Where(x => x.IsDoctor)
        .OrderByDescending(x => x.Id)
        .Take(5)
        .ToList(),




                DepartmentStatistics =
        _context.Departments
        .Select(d => new DepartmentStatisticsViewModel
        {

            DepartmentName = d.Name,


            DoctorsCount =
                _context.ApplicationUsers.Count(u =>

                    u.DepartmentId == d.Id

                    &&

                    u.IsDoctor)

        })
        .ToList(),

        LowStockMedicines =

                _context.Medicines

                .Where(x =>

                x.Quantity <= x.MinimumQuantity)

                .Select(x => new MedicineViewModel
                {

                    Id = x.Id,


                    Name = x.Name,


                    Quantity = x.Quantity,


                    MinimumQuantity =
                        x.MinimumQuantity


                })

                .ToList(),

            };
        }
    }
}