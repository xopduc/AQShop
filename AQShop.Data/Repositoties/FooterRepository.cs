using AQShop.Data.Infrastruture;
using AQShop.Model.Models;

namespace AQShop.Data.Repositoties
{
    public interface IFooterRepository : IRepository<Footer>
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}