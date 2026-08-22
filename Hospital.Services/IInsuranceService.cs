using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Services
{
    public interface IInsuranceService
    {

        List<Insurance> GetAll();


        Insurance GetById(int id);


        void Create(Insurance insurance);


        void Update(Insurance insurance);


        void Delete(int id);

    }
}
