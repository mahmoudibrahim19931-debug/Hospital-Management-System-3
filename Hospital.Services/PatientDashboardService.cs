using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System.Linq;
using static Hospital.Models.Appointment;

namespace Hospital.Services
{
    public class PatientDashboardService : IPatientDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILabService _labService;
        private readonly IAppointmentService _appointmentService;
        private readonly IBillService _billService;

        public PatientDashboardService(
            IUnitOfWork unitOfWork,
            IAppointmentService appointmentService,
            ILabService labService,
            IBillService billService)
        {
            _unitOfWork = unitOfWork;
            _appointmentService = appointmentService;
            _labService = labService;
            _billService = billService;
        }

        public PatientDashboardViewModel GetDashboard(string patientId)
        {
            var patient = _unitOfWork
                .GenericRepository<ApplicationUser>()
                .GetById(patientId);

            var appointments = _unitOfWork
                .GenericRepository<Appointment>()
                .GetAll(
                    x => x.PatientId == patientId,
                    includeProperties: "Doctor,Patient")
                .ToList();
            var insurance = _unitOfWork
    .GenericRepository<PatientInsurance>()
    .GetAll(

        x => x.PatientId == patientId
        &&
        x.IsActive,

        includeProperties:
        "Insurance"

    )
    .FirstOrDefault();

            var pendingInsurance =

    _unitOfWork
    .GenericRepository<PatientInsurance>()
    .GetAll(

        x => x.PatientId == patientId

        &&

        !x.IsApproved

        &&

        !x.IsActive,

        includeProperties:
        "Insurance"

        )

    .OrderByDescending(x => x.CreatedDate)

    .FirstOrDefault();

            var model = new PatientDashboardViewModel();


            model.PatientId =
                patient.Id;


            model.PatientName =
                patient.Name;


            model.TotalAppointments =
                appointments.Count;


            model.PendingAppointments =
                appointments.Count(x =>
                    x.Status ==
                    AppointmentStatus.Pending);


            model.ApprovedAppointments =
                appointments.Count(x =>
                    x.Status ==
                    AppointmentStatus.Approved);


            model.CompletedAppointments =
                appointments.Count(x =>
                    x.Status ==
                    AppointmentStatus.Completed);


            model.CancelledAppointments =
                appointments.Count(x =>
                    x.Status ==
                    AppointmentStatus.Cancelled);



            model.Appointments =

                appointments
                .Select(x => new AppointmentViewModel(x))
                .ToList();



            model.Labs =
                _labService.GetPatientLabs(patientId);



            model.Bills =
                _billService.GetPatientBills(patientId);



            model.InsuranceName =
                insurance?.Insurance?.Name;


            model.ActiveInsuranceName =


    insurance
    ?.Insurance
    ?.Name;




            model.ActiveDiscount =


                insurance
                ?.Insurance
                ?.DiscountPercentage

                ?? 0m;


            model.DiscountPercentage =
                insurance?.Insurance?.DiscountPercentage
                ?? 0m;

            model.HasPendingInsurance =

    pendingInsurance != null;




            model.PendingInsuranceName =


                pendingInsurance
                ?.Insurance
                ?.Name;

            return model;
        }
    }
}