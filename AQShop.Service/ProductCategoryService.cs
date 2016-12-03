using AQShop.Data.Infrastruture;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory postCategory);
        void Update(ProductCategory postCategory);
        ProductCategory Delete(int id);
        IEnumerable<ProductCategory> GetAll();
        IEnumerable<ProductCategory> GetAll(string keyword);
        IEnumerable<ProductCategory> GetAllByParentID(int parentID);
        ProductCategory GetByID(int id);
        void Save();

    }
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
           return _productCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
           return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if(!String.IsNullOrEmpty(keyword))
            {
                return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            return _productCategoryRepository.GetAll(); ;

        }

        public IEnumerable<ProductCategory> GetAllByParentID(int parentID)
        {
            return _productCategoryRepository.GetMulti(x=>x.ParentID  == parentID);
        }

        public ProductCategory GetByID(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory postCategory)
        {
             _productCategoryRepository.Update(postCategory);
        }
    }
}
