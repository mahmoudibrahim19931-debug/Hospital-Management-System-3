using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class BillService : IBillService
    {
        private readonly INotificationService _notificationService;
        private readonly IUnitOfWork _unitOfWork;

        public BillService(
            IUnitOfWork unitOfWork,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }


        public void Create(BillViewModel vm)
        {

            var exists = _unitOfWork
                .GenericRepository<Bill>()
                .GetAll(x =>
                    x.AppointmentId == vm.AppointmentId)
                .FirstOrDefault();

            if (exists != null)
                return;



            decimal originalAmount = vm.Amount;

            decimal discountAmount = 0;

            decimal finalAmount = originalAmount;

            

            var bill = new Bill
            {

                PatientId = vm.PatientId,

                DoctorId = vm.DoctorId,

                AppointmentId = vm.AppointmentId,


                Amount = finalAmount,

                OriginalAmount = originalAmount,

                DiscountAmount = discountAmount,

                FinalAmount = finalAmount,

                InsuranceId = null,


                IsPaid = false,

                CreatedDate = DateTime.Now

            };



            _unitOfWork
                .GenericRepository<Bill>()
                .Add(bill);



            _unitOfWork.Save();



            Console.WriteLine("================================");
            Console.WriteLine("Bill Created");
            Console.WriteLine($"Patient : {bill.PatientId}");
            Console.WriteLine($"Appointment : {bill.AppointmentId}");
            Console.WriteLine($"Amount : {bill.Amount}");
            Console.WriteLine("================================");




            _notificationService.Create(

                bill.PatientId,

                $"New bill created : {bill.Amount} EGP",

                "Patient",

                "Bills",

                "Index",

                "/Patient/Bills"

            );

        }





        public BillViewModel CalculatePayment(
    int billId,
    int insuranceId)
        {

            var bill = _unitOfWork
     .GenericRepository<Bill>()
     .GetAll(

         x => x.Id == billId,

         includeProperties:
         "Doctor,Insurance"

         )
     .FirstOrDefault();



            if (bill == null)
                return null;



            var insurance = _unitOfWork
                .GenericRepository<Insurance>()
                .GetById(insuranceId);



            decimal discount = 0;


            decimal finalAmount =
                bill.OriginalAmount;



            if (insurance != null
                &&
                insurance.IsActive)
            {


                discount =

                    bill.OriginalAmount
                    *
                    insurance.DiscountPercentage
                    /
                    100m;



                finalAmount =

                    bill.OriginalAmount
                    -
                    discount;


            }




            return new BillViewModel
            {

                Id = bill.Id,

                AppointmentId = bill.AppointmentId,

                PatientId = bill.PatientId,

                DoctorId = bill.DoctorId,

                DoctorName = bill.Doctor?.Name,



                OriginalAmount =
                    bill.OriginalAmount,



                DiscountAmount =
                    discount,



                FinalAmount =
                    finalAmount,



                InsuranceId =
                    insurance?.Id,



                InsuranceName =
                    insurance?.Name


            };

        }


        public void Pay(
    int billId,
    int insuranceId)
        {


            var bill = _unitOfWork
                .GenericRepository<Bill>()
                .GetById(billId);



            if (bill == null)
                return;


            if (bill.IsPaid)
                return;

            var insurance = _unitOfWork
                .GenericRepository<Insurance>()
                .GetById(insuranceId);




            decimal discount = 0;


            decimal finalAmount =
                bill.OriginalAmount;




            if (insurance != null
                &&
                insurance.IsActive)
            {


                discount =

                    bill.OriginalAmount
                    *
                    insurance.DiscountPercentage
                    /
                    100m;




                finalAmount =

                    bill.OriginalAmount
                    -
                    discount;

            }




            bill.InsuranceId =
                insuranceId;



            bill.DiscountAmount =
                discount;



            bill.FinalAmount =
                finalAmount;



            bill.Amount =
                finalAmount;



            bill.IsPaid =
                true;



            bill.PaidDate =
                DateTime.Now;




            _unitOfWork
                .GenericRepository<Bill>()
                .Update(bill);



            _unitOfWork.Save();





            _notificationService.Create(

                bill.PatientId,

                $"Bill paid ({finalAmount} EGP)",

                "Patient",

                "Bills",

                "Index",

                "/Patient/Bills"

            );


        }

        public BillViewModel GetById(int id)
        {

            var bill = _unitOfWork
                .GenericRepository<Bill>()
                .GetAll(

                    x => x.Id == id,

                    includeProperties:
                    "Patient,Doctor,Appointment,Insurance"

                    )
                .FirstOrDefault();



            if (bill == null)
                return null;



            return ConvertToVm(bill);

        }






        public List<BillViewModel> GetPatientBills(string patientId)
        {

            Console.WriteLine("Service Patient");

            Console.WriteLine(patientId);



            var bills =
                _unitOfWork
                .GenericRepository<Bill>()
                .GetAll(

                    x => x.PatientId == patientId,

                    includeProperties:
                    "Patient,Doctor,Insurance"

                )
                .ToList();



            Console.WriteLine("Bills Found");

            Console.WriteLine(bills.Count);




            return bills
                .Select(x => new BillViewModel
                {

                    Id = x.Id,

                    PatientId = x.PatientId,

                    AppointmentId = x.AppointmentId,

                    Amount = x.Amount,

                    IsPaid = x.IsPaid,

                    CreatedDate = x.CreatedDate,

                    PaidDate = x.PaidDate,

                    OriginalAmount = x.OriginalAmount,

                    DiscountAmount = x.DiscountAmount,

                    FinalAmount = x.FinalAmount,

                    InsuranceId = x.InsuranceId,

                    InsuranceName = x.Insurance?.Name,

                    PatientName = x.Patient?.Name,

                    DoctorName = x.Doctor?.Name,

                    DoctorId = x.DoctorId

                })
                .ToList();


        }





        public bool Exists(int appointmentId)
        {

            return _unitOfWork
                .GenericRepository<Bill>()
                .GetAll(

                    x => x.AppointmentId == appointmentId

                    )
                .Any();

        }






        public List<BillViewModel> GetAll()
        {

            var bills = _unitOfWork
                .GenericRepository<Bill>()
                .GetAll(

                    includeProperties:
                    "Patient,Doctor,Insurance"

                    )
                .ToList();



            return bills
                .Select(ConvertToVm)
                .ToList();

        }





        private BillViewModel ConvertToVm(Bill x)
        {

            return new BillViewModel
            {

                Id = x.Id,


                PatientId = x.PatientId,

                PatientName = x.Patient?.Name,



                DoctorId = x.DoctorId,

                DoctorName = x.Doctor?.Name,



                AppointmentId = x.AppointmentId,



                Amount = x.Amount,



                OriginalAmount = x.OriginalAmount,

                DiscountAmount = x.DiscountAmount,

                FinalAmount = x.FinalAmount,



                InsuranceId = x.InsuranceId,

                InsuranceName = x.Insurance?.Name,



                IsPaid = x.IsPaid,



                CreatedDate = x.CreatedDate,

                PaidDate = x.PaidDate

            };

        }


    }
}