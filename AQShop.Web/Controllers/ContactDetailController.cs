using AQShop.Data.Infrastruture;
using AQShop.Model.Models;
using AQShop.Service;
using AQShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AQShop.Web.Controllers
{
    public class ContactDetailController : Controller
    {
        public IContactDetailService _contactDetailService;   

        public ContactDetailController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
          
        }
        // GET: ContactDetail
        public ActionResult Index()
        {
            var model = _contactDetailService.GetDefaultContactDetail();
            var contactDetailView = AutoMapper.Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            var feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.ContactDetail = contactDetailView;
            return View(feedbackViewModel);
        }

        //[HttpPost]
        //public ActionResult Index(ContactDetail viewModel)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var model = _contactDetailService.GetDefaultContactDetail();
        //        var contactDetailView = AutoMapper.Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
        //    }
           
        //    return View(contactDetailView);
        //}
    }
}