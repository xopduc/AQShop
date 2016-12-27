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
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        private IPageService _pageService;

        public PageController(IErrorService errorService, IPageService pageService) : base(errorService)
        {
            this._pageService = pageService;
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Edit(HttpRequestMessage request, PageViewModel pageViewModel)
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
                    var page = Mapper.Map<PageViewModel, Page>(pageViewModel);
                    _pageService.Update(page);
                    _pageService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, pageViewModel);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel pageViewModel)
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
                    var page = Mapper.Map<PageViewModel, Page>(pageViewModel);
                    _pageService.Add(page);
                    _pageService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, pageViewModel);
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

                        var oldPage = _pageService.Delete(int.Parse(id));
                    }
                    _pageService.Save();
               
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

                var oldPage = _pageService.Delete(id);
                _pageService.Save();

                var pageViewModel = Mapper.Map<Page,PageViewModel>(oldPage);

                response = request.CreateResponse(HttpStatusCode.OK, pageViewModel);

                return response;
            });
        }
        
        [Route("GetById/{id:int}")]
        public HttpResponseMessage GetByAlias(HttpRequestMessage request, string alias)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;

                var page = _pageService.GetPageByAlias(alias);

                var PageViewModel = Mapper.Map<Page, PageViewModel>(page);

                response = request.CreateResponse(HttpStatusCode.OK, PageViewModel);

                return response;
            });
        }

        [Route("GetAll")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize)
        {
            return CreateHttpResponse(request, ()
                =>
            {
                HttpResponseMessage response = null;               
                int totalRow = 0;
                var pageList = _pageService.GetAll();

                totalRow = pageList.Count();

                var query = pageList.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var ListViewModel = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(query);

                var pagination = new PaginationSet<PageViewModel>()
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
