using AQShop.Data.Infrastruture;
using AQShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Data.Repositoties
{
    public interface IVisitorStatisticRepository:IRepository<VisitorStatistic>
    {
     
    }
    public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitorStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory):base(dbFactory)
        {
        }
      
    }
}
