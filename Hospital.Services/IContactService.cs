using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Services
{
    public interface IContactService 
    {
        PagedResult<ContactViewModel> GetAll(int pageNumber, int pageSize);
   
        ContactViewModel GetContactById(int ContactId);

        void UpdateContact(ContactViewModel contact);

        void InsertContact(ContactViewModel contact);

        void DeleteContact(int id);
    }
}
