using AQShop.Model.Models;
using AQShop.Service;
using AQShop.Web.Infrastructure.Core;
using AQShop.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AQShop.Web.Api
{
    [RoutePrefix("api/productCategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Edit(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryViewModel);
                    _productCategoryService.Update(productCategory);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, productCategoryViewModel);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryViewModel);
                    _productCategoryService.Add(productCategory);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, productCategoryViewModel);
                }
                return response;
            });
        }

        [Route("DeleteMulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string ids)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;
                if (!String.IsNullOrEmpty(ids))
                {
                    foreach (var id in ids.Split(','))
                    {
                        var oldProductCategory = _productCategoryService.Delete(int.Parse(id));
                    }
                    _productCategoryService.Save();

                    //var productCategoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [Route("DeleteById")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;

                var oldProductCategory = _productCategoryService.Delete(id);
                _productCategoryService.Save();

                var productCategoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);

                response = request.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);

                return response;
            });
        }

        [Route("GetById/{id:int}")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;

                var productCategory = _productCategoryService.GetByID(id);

                var CategoryProductViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);

                response = request.CreateResponse(HttpStatusCode.OK, CategoryProductViewModel);

                return response;
            });
        }

        [Route("GetAllParentProductCategory")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;

                var productCategoryList = _productCategoryService.GetAll();

                var CategoryListViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategoryList);

                //_postCategoryService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, CategoryListViewModel);
                // }
                return response;
            });
        }

        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;
                //if (!ModelState.IsValid)
                //{
                //    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                //}
                //else
                //{
                int totalRow = 0;
                var productCategoryList = _productCategoryService.GetAll(keyword);

                totalRow = productCategoryList.Count();

                var query = productCategoryList.OrderBy(x => x.CreateDate).Skip(page * pageSize).Take(pageSize);

                var CategoryListViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var pagination = new PaginationSet<ProductCategoryViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    Items = CategoryListViewModel.ToList(),
                    TotalPages = (int)(Math.Ceiling((decimal)totalRow / pageSize))
                };
                //_postCategoryService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, pagination);
                // }
                return response;
            });
        }
    }
}