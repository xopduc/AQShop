using AQShop.Common;
using AQShop.Data.Infrastruture;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AQShop.Service
{
    public interface IProductService
    {
        Tag GetTagByTagId(string tagId);

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

        IEnumerable<Product> GetListProductByCategoryId(int categoryId, int page, string sortOrder, int pageSize, out int totalRow);

        IEnumerable<Product> Search(string keyword, int page, string sortOrder, int pageSize, out int totalRow);

        IEnumerable<String> GetListNameProduct(string keyword);

        IEnumerable<Product> GetRelateProducts(int id, int top);

        IEnumerable<Tag> GetListTagByProductId(int productId);

        IEnumerable<Product> GetListProductByTagId(string tagId, int page, int pageSize, string sort, out int totalRow);

        void IncreaseViewCount(int productId);
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
                foreach (var tagItem in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tagItem);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        var tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tagItem;
                        tag.Type = CommonConstants.ProductTag;

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
            if (!String.IsNullOrEmpty(keyword))
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
            if (!String.IsNullOrEmpty(product.Tags))
            {
                String[] tags = product.Tags.Split(',');
                _productTagRepository.DeleteMulti(pt => pt.ProductID == product.ID);
                foreach (var tagItem in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tagItem);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        var tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tagItem;
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                        _unitOfWork.Commit();
                    }
                   
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                    _unitOfWork.Commit();
                }
            }
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

        public IEnumerable<Product> GetListProductByCategoryId(int categoryId, int page, string sortOrder, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            switch (sortOrder)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "new":
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;

                case "price":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default: break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListNameProduct(string keyword)
        {           
            return _productRepository.GetMulti(x => x.Name.Contains(keyword)).Select(x => x.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, string sortOrder, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
            switch (sortOrder)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "new":
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;

                case "price":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default: break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelateProducts(int id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            var query = _productRepository.GetMulti(x =>x.Status && x.CategoryID == product.CategoryID && x.ID != id).OrderByDescending(x=>x.CreateDate).Take(top);
            return query;
        }

        public IEnumerable<Tag> GetListTagByProductId(int productId)
        {
            var tags = _productTagRepository.GetMulti(x => x.ProductID == productId, new string[] { "Tag" }).Select(y=>y.Tag);
            return tags;
        }

        public IEnumerable<Product> GetListProductByTagId(string tagId, int page, int pageSize,string sort, out int  totalRow)
        {
            var query = _productRepository.GetListProductByTagId(tagId,page, pageSize, out totalRow);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "new":
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;

                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;

                case "price":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default: break;
            }
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void IncreaseViewCount(int productId)
        {
            var product = _productRepository.GetSingleById(productId);
            if(product.ViewCount.HasValue)
            {
                product.ViewCount += 1;
            }
            else
            {
                product.ViewCount = 1;
            }
        }

        public Tag GetTagByTagId(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x=>x.ID == tagId);
        }
    }
}