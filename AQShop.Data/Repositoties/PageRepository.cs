using AQShop.Data.Infrastruture;
using AQShop.Model.Models;

namespace AQShop.Data.Repositoties
{
    public interface IPageRepository : IRepository<Page>
    {
    }

    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}