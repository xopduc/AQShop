using AQShop.Common;
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
    public interface IProductService
    {
        Product Add(Product product);
        void Update(Product product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        //IEnumerable<Product> GetAllByParentID(int parentID);
        Product GetByID(int id);
        void Save();
        IEnumerable<Product> GetLatestProducts(int number);
        IEnumerable<Product> GetHotProducts(int number);

    }
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository
                              , ITagRepository tagRepository
                              , IProductTagRepository productTagRepository
                              , IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
        }

        public Product Add(Product product)
        {
            var productAdd = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!String.IsNullOrEmpty(product.Tags))
            {
                String[] tags = product.Tags.Split(',');
                foreach(var tagItem in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tagItem);
                    if(_tagRepository.Count(x=>x.ID == tagId) == 0)
                    {
                        var tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tagItem;
                        tag.Type = CommonConstant.ProductTag;

                        _tagRepository.Add(tag);
                        _unitOfWork.Commit();
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = productAdd.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                    _unitOfWork.Commit();

                }
            }
            return productAdd;
        }

        public Product Delete(int id)
        {
           return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if(!String.IsNullOrEmpty(keyword))
            {
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            return _productRepository.GetAll(); ;

        }

        //public IEnumerable<Product> GetAllByParentID(int parentID)
        //{
        //    return _productRepository.GetMulti(x=>x.ParentID  == parentID);
        //}

        public Product GetByID(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
             _productRepository.Update(product);
        }

        public IEnumerable<Product> GetLatestProducts(int top)
        {
            var latestProducts = _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreateDate).Take(top);
            return latestProducts;
        }

        public IEnumerable<Product> GetHotProducts(int top)
        {
            var hotProducts = _productRepository.GetMulti(x => x.Status == true && x.HotFlag == true).Take(top);
            return hotProducts;
        }
    }
}
