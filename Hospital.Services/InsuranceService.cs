using Hospital.Models;
using Hospital.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class InsuranceService : IInsuranceService
    {

        private readonly IUnitOfWork _unitOfWork;


        public InsuranceService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public List<Insurance> GetAll()
        {

            return _unitOfWork
                .GenericRepository<Insurance>()
                .GetAll()
                .ToList();

        }




        public Insurance GetById(int id)
        {

            return _unitOfWork
                .GenericRepository<Insurance>()
                .GetById(id);

        }




        public void Create(Insurance insurance)
        {

            _unitOfWork
                .GenericRepository<Insurance>()
                .Add(insurance);


            _unitOfWork.Save();

        }




        public void Update(Insurance insurance)
        {

            _unitOfWork
                .GenericRepository<Insurance>()
                .Update(insurance);


            _unitOfWork.Save();

        }




        public void Delete(int id)
        {

            var insurance =
                GetById(id);


            if (insurance == null)
                return;


            _unitOfWork
                .GenericRepository<Insurance>()
                .Delete(insurance);


            _unitOfWork.Save();

        }

    }
}