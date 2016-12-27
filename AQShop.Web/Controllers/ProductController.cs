using AQShop.Common;
using AQShop.Model.Models;
using AQShop.Service;
using AQShop.Web.Infrastructure.Core;
using AQShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AQShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Category
        public ActionResult Category(int id, int page = 1, string sortOrder = "popular")
        {
            int pageSize =(int.Parse)(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 1;
            var productModel = _productService.GetListProductByCategoryId(id,page, sortOrder, pageSize,out totalRow);
            var productViewModel = AutoMapper.Mapper.Map < IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)(Math.Ceiling((double)(totalRow / pageSize)));

            var category = _productCategoryService.GetByID(id);         
            ViewBag.Category = AutoMapper.Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPages = totalPage,
            };

            return View(paginationSet);
        }

        // GET: Product
        public ActionResult Detail(int id)
        {
            var model = _productService.GetByID(id);
            var modelView = AutoMapper.Mapper.Map<Product, ProductViewModel>(model);
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(model.MoreImages);
            ViewBag.MoreImages = listImages;

            //get tags of product
            var tags = _productService.GetListTagByProductId(id);
            ViewBag.Tags = AutoMapper.Mapper.Map <IEnumerable<Tag>, IEnumerable<TagViewModel>>(tags);

            var relateModel = _productService.GetRelateProducts(id, 6);
            ViewBag.RelateProducts = AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relateModel);


            return View(modelView);
        }

        public ActionResult GetListProductByTagId(string tagId, int page = 1, string sortOrder = "")
        {
            int pageSize = (int.Parse)(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 1;
            var model = _productService.GetListProductByTagId(tagId, page, pageSize, sortOrder ,out totalRow);
            var searchView = AutoMapper.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            int totalPage = (int)(Math.Ceiling((double)(totalRow / pageSize)));
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = searchView,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPages = totalPage,
            };

            ViewBag.tag = AutoMapper.Mapper.Map<Tag, TagViewModel>(_productService.GetTagByTagId(tagId));
            return View(paginationSet);
        }

        public JsonResult GetListNameProduct(string keyword)
        {
            var model = _productService.GetListNameProduct(keyword);
            return Json(new
            {
                data = model
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1,string sortOrder = "popular")
        {
            int pageSize = (int.Parse)(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 1;
            var model = _productService.Search(keyword,page,sortOrder,pageSize,out totalRow);
            var searchView = AutoMapper.Mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(model);
            int totalPage = (int)(Math.Ceiling((double)(totalRow / pageSize)));
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = searchView,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPages = totalPage,
            };
            return View(paginationSet);
        }
    }
}