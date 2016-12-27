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
    public interface IPageService
    {
        Page GetPageByAlias(string alias);

        Page Add(Page page);
        void Save();
        void Update(Page page);
        IEnumerable<Page> GetAll();
        Page Delete(int id);    

    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;
        public PageService(IUnitOfWork unitOfWork, IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            var pageAdd = _pageRepository.Add(page);
            _unitOfWork.Commit();           
            return pageAdd;
        }

        public Page Delete(int id)
        {
           return _pageRepository.Delete(id);
        }

        public Page GetPageByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }
    }
}
