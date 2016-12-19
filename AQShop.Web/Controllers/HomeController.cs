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
        private IProductService _productService;
        private ICommonService _commonServie;

        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _commonServie = commonService;
            _productService = productService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var slideModel = _commonServie.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var latestProductModel = _productService.GetLatestProducts(3);
            var latestProductView = Mapper.Map <IEnumerable<Product>, IEnumerable<ProductViewModel>>(latestProductModel);
            var topSaleProductModel = _productService.GetHotProducts(3);
            var topSaleProductView = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;
            homeViewModel.LatestProducts = latestProductView;
            homeViewModel.TopSaleProducts = topSaleProductView;
            return View(homeViewModel);
        }


        [ChildActionOnly]
        public ActionResult Header()
        {
            
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = _commonServie.GetFooterCommon();
            var footer = Mapper.Map<Footer, FooterViewModel>(model);

            return PartialView(footer);
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