using AQShop.Model.Models;
using AQShop.Service;
using AQShop.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AQShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        public HomeController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        [ChildActionOnly]
        public ActionResult Header()
        {
            
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult Footer()
        {
         

            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var category = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(category);
        }
    }
}