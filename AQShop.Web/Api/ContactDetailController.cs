using AQShop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AQShop.Service;
using AQShop.Web.Models;
using AutoMapper;
using AQShop.Model.Models;

namespace AQShop.Web.Api
{
    [RoutePrefix("api/contactDetail")]
    [Authorize]
    public class ContactDetailController : ApiControllerBase
    {
        private IContactDetailService _contactDetailService;
        public ContactDetailController(IErrorService errorService, IContactDetailService contactDetaiService) : base(errorService)
        {
            _contactDetailService = contactDetaiService;
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Edit(HttpRequestMessage request, ContactDetailViewModel contactDetailViewModel)
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
                    var contactDetail = Mapper.Map<ContactDetailViewModel, ContactDetail>(contactDetailViewModel);
                    _contactDetailService.UpdateContactDetail(contactDetail);
                    _contactDetailService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, contactDetailViewModel);
                }
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ContactDetailViewModel contactDetailViewModel)
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
                    var contactDettail = Mapper.Map<ContactDetailViewModel, ContactDetail>(contactDetailViewModel);
                    _contactDetailService.AddContactDetail(contactDettail);
                    _contactDetailService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, contactDetailViewModel);
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

                        var oldPage = _contactDetailService.DeleteById(int.Parse(id));
                    }
                    _contactDetailService.Save();

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

                var oldContactDetail = _contactDetailService.DeleteById(id);
                _contactDetailService.Save();

                var pageViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(oldContactDetail);

                response = request.CreateResponse(HttpStatusCode.OK, oldContactDetail);

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

                var contactDetail = _contactDetailService.GetById(id);

                var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);

                response = request.CreateResponse(HttpStatusCode.OK, contactDetailViewModel);

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
                var contactDetailList = _contactDetailService.GetAll();

                totalRow = contactDetailList.Count();

                var query = contactDetailList.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var ListViewModel = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(query);

                var pagination = new PaginationSet<ContactDetailViewModel>()
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
