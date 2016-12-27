using System;
using AQShop.Data.Infrastruture;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using System.Collections;
using System.Collections.Generic;

namespace AQShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetById(int id);

        ContactDetail GetDefaultContactDetail();

        void AddContactDetail(ContactDetail contactDetail);

        void UpdateContactDetail(ContactDetail contactDetail);

        ContactDetail DeleteById(int id);

        IEnumerable<ContactDetail> GetAll();

        void Save();
    }

    public class ContactDetailService : IContactDetailService
    {
        private IContactDetailRepository _contactDetailRepository;
        private IUnitOfWork _unitOfWork;

        public ContactDetailService(IUnitOfWork unitOfWork, IContactDetailRepository contactDetailRepository)
        {
            _contactDetailRepository = contactDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddContactDetail(ContactDetail contactDetail)
        {
            _contactDetailRepository.Add(contactDetail);
         
        }

        public ContactDetail DeleteById(int id)
        {
            var contactDetail = _contactDetailRepository.GetSingleById(id);
            return _contactDetailRepository.Delete(contactDetail);
            
        }

        public IEnumerable<ContactDetail> GetAll()
        {
            return _contactDetailRepository.GetAll();
        }

        public ContactDetail GetById(int id)
        {
            return _contactDetailRepository.GetSingleByCondition(x=>x.ID == id);
        }

        public ContactDetail GetDefaultContactDetail()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateContactDetail(ContactDetail contactDetail)
        {
            _contactDetailRepository.Update(contactDetail);
           
        }
    }
}