using AQShop.Data.Infrastruture;
using AQShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Data.Repositoties
{
    public interface IContactDetailRepository: IRepository<ContactDetail>
    {

    }
    public class ContactDetailRepository : RepositoryBase<ContactDetail>, IContactDetailRepository
    {
        public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
