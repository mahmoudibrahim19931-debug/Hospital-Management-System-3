using Hospital.ViewModels;
using System.Collections.Generic;

namespace Hospital.Services
{
    public interface IBillService
    {

        void Create(BillViewModel vm);


        BillViewModel CalculatePayment(
        int billId,
        int insuranceId);


        void Pay(
                int billId,
                int insuranceId);


        BillViewModel GetById(int id);


        List<BillViewModel> GetPatientBills(string patientId);

        bool Exists(int appointmentId);

        List<BillViewModel> GetAll();

        


    }
}