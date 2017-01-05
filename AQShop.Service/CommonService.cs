using AQShop.Common;
using AQShop.Data.Repositoties;
using AQShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AQShop.Service
{
    public interface ICommonService
    {
        Footer GetFooterCommon();
        IEnumerable<Slide> GetSlides();  
    }
    public class CommonService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private ISlideRepository _slideRepository;
        

        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository)
        {
            _footerRepository = footerRepository;
            _slideRepository = slideRepository;
        }

        public Footer GetFooterCommon()
        {
            return _footerRepository.GetSingleByCondition(c => c.ID == CommonConstants.DefaultIdFooter);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetAll();
        }
    }
}
