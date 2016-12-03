using AQShop.Data.Infrastruture;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using System.Collections.Generic;
using System;

namespace AQShop.Service
{
    public interface IErrorService
    {
        Error Create(Error error);
           
        void Save();
    }

    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository postRepoitory, IUnitOfWork unitOfWork)
        {
            this._errorRepository = postRepoitory;
            this._unitOfWork = unitOfWork;
        }

        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}