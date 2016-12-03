using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Data.Infrastruture
{
    public interface IDbFactory:IDisposable
    {
        AQShopDbContext Init();
      
    }
}
