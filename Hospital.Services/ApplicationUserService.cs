using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public PagedResult<ApplicationUserViewModel> GetAll(
            int PageNumber,
            int PageSize)
        {
            int totalCount;

            List<ApplicationUserViewModel> vmList =
                new List<ApplicationUserViewModel>();


            try
            {
                int excludeRecords =
                    (PageSize * PageNumber) - PageSize;


                var modelList =
                    _unitOfWork
                    .GenericRepository<ApplicationUser>()
                    .GetAll()
                    .Skip(excludeRecords)
                    .Take(PageSize)
                    .ToList();


                totalCount =
                    _unitOfWork
                    .GenericRepository<ApplicationUser>()
                    .GetAll()
                    .Count();


                vmList =
                    ConvertModelToViewModelList(modelList);
            }
            catch
            {
                throw;
            }


            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,

                TotalItems = totalCount,

                PageNumber = PageNumber,

                PageSize = PageSize
            };
        }





        public PagedResult<ApplicationUserViewModel>
            GetAllDoctor(
            int PageNumber,
            int PageSize)
        {
            int totalCount;

            List<ApplicationUserViewModel> vmList =
                new List<ApplicationUserViewModel>();


            try
            {

                int excludeRecords =
                    (PageSize * PageNumber) - PageSize;



                var modelList =

                    _unitOfWork
                    .GenericRepository<ApplicationUser>()
                    .GetAll(

                        x => x.IsDoctor

                        )
                    .Skip(excludeRecords)
                    .Take(PageSize)
                    .ToList();




                totalCount =

                    _unitOfWork
                    .GenericRepository<ApplicationUser>()
                    .GetAll(

                        x => x.IsDoctor

                        )
                    .Count();




                vmList =
                    ConvertModelToViewModelList(modelList);

            }
            catch
            {
                throw;
            }



            return new PagedResult<ApplicationUserViewModel>
            {

                Data = vmList,

                TotalItems = totalCount,

                PageNumber = PageNumber,

                PageSize = PageSize

            };

        }






        public PagedResult<ApplicationUserViewModel>
            GetAllPatient(
            int PageNumber,
            int PageSize)
        {
            throw new NotImplementedException();
        }





        public PagedResult<ApplicationUserViewModel>
            SearchDoctor(
            int PageNumber,
            int PageSize,
            string Spicility = null)
        {
            throw new NotImplementedException();
        }






        public ApplicationUserViewModel
            GetById(string id)
        {

            var model =

                _unitOfWork
                .GenericRepository<ApplicationUser>()
                .GetById(id);



            if (model == null)
                return null;



            return new ApplicationUserViewModel(model);

        }






        public List<ApplicationUserViewModel>
            GetDoctors()
        {

            return

                _unitOfWork
                .GenericRepository<ApplicationUser>()
                .GetAll(

                    x => x.IsDoctor

                    )
                .Select(

                    x => new ApplicationUserViewModel(x)

                    )
                .ToList();

        }






        public List<ApplicationUserViewModel>
            GetPatients()
        {

            return

                _unitOfWork
                .GenericRepository<ApplicationUser>()
                .GetAll(

                    x => !x.IsDoctor

                    )
                .Select(

                    x => new ApplicationUserViewModel(x)

                    )
                .ToList();

        }







        public void UpdateDoctor(
            string id,
            bool isDoctor)
        {

            var model =

                _unitOfWork
                .GenericRepository<ApplicationUser>()
                .GetById(id);



            if (model == null)
                return;



            model.IsDoctor = isDoctor;



            _unitOfWork
                .GenericRepository<ApplicationUser>()
                .Update(model);



            _unitOfWork.Save();

        }







        private List<ApplicationUserViewModel>
            ConvertModelToViewModelList(
            List<ApplicationUser> modelList)
        {

            return modelList

                .Select(

                    x => new ApplicationUserViewModel(x)

                    )

                .ToList();

        }


    }
}