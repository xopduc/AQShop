using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Data.Infrastruture
{
    public class DbFactory : Disposable, IDbFactory
    {
        private AQShopDbContext dbContext;
        public AQShopDbContext Init()
        {
            return dbContext ?? (dbContext = new AQShopDbContext());
        }

        protected override void DisposeCore()
        {
            if(dbContext != null)
            {
                base.DisposeCore();
            }
            
        }
    }
}
