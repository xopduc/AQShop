using AQShop.Data.Infrastruture;
using AQShop.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AQShop.Data.Repositoties
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByAlias(string alias);
        IEnumerable<Product> GetListProductByTagId(string tagId, int page, int pageSize, out int totalRow);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetByAlias(string alias)
        {
            return this.DbContext.Products.Where(x => x.Alias == alias);
        }

        public IEnumerable<Product> GetListProductByTagId(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from product in DbContext.Products
                        join productTag in DbContext.ProductTags
                        on product.ID equals productTag.ProductID
                        where productTag.TagID == tagId
                        select product;
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreateDate).Skip((page-1)* pageSize).Take(pageSize);

        }
    }
}