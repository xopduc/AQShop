using AQShop.Data.Infrastruture;
using AQShop.Model.Models;

namespace AQShop.Data.Repositoties
{
    public interface ISupportOnlineRepository : IRepository<SupportOnline>
    {
    }

public class SupportOnlineRepository : RepositoryBase<SupportOnline>, ISupportOnlineRepository
{
    public SupportOnlineRepository(IDbFactory dbFactory) : base(dbFactory)
    {
    }
}
}