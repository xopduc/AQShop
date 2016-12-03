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
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Edit(HttpRequestMessage request, ProductViewModel productViewModel)
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
                    var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
                    _productService.Update(product);
                    _productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, productViewModel);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productViewModel)
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
                    var product = Mapper.Map<ProductViewModel, Product>(productViewModel);
                    _productService.Add(product);
                    _productService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, productViewModel);
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
                        var oldProduct = _productService.Delete(int.Parse(id));
                    }
                    _productService.Save();

                    //var productViewModel = Mapper.Map<Product, ProductViewModel>(oldProduct);

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

                var oldProduct = _productService.Delete(id);
                _productService.Save();

                var productViewModel = Mapper.Map<Product, ProductViewModel>(oldProduct);

                response = request.CreateResponse(HttpStatusCode.OK, productViewModel);

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

                var product = _productService.GetByID(id);

                var ProductViewModel = Mapper.Map<Product, ProductViewModel>(product);

                response = request.CreateResponse(HttpStatusCode.OK, ProductViewModel);

                return response;
            });
        }

        [Route("GetAllParentProduct")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;

                var productList = _productService.GetAll();

                var ListViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productList);

                //_postService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, ListViewModel);
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
                var productList = _productService.GetAll(keyword);

                totalRow = productList.Count();

                var query = productList.OrderBy(x => x.CreateDate).Skip(page * pageSize).Take(pageSize);

                var ListViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var pagination = new PaginationSet<ProductViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    Items = ListViewModel.ToList(),
                    TotalPages = (int)(Math.Ceiling((decimal)totalRow / pageSize))
                };
                //_postService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, pagination);
                // }
                return response;
            });
        }
    }
}