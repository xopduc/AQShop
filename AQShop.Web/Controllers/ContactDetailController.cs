using AQShop.Common;
using AQShop.Data.Infrastruture;
using AQShop.Model.Models;
using AQShop.Service;
using AQShop.Web.Models;
using AutoMapper;
using BotDetect.Web.Mvc;
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
        IFeedbackService _feedbackService;

        public ContactDetailController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;

    }
        // GET: ContactDetail
        public ActionResult Index()
        {
          
            var feedbackViewModel = new FeedbackViewModel();
            feedbackViewModel.ContactDetail = GetDetail();
            return View(feedbackViewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback();
                newFeedback = AutoMapper.Mapper.Map<FeedbackViewModel, Feedback>(feedbackViewModel);
                newFeedback.CreatedDate = DateTime.Now;
                _feedbackService.Create(newFeedback);
                _feedbackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Message = "";
                feedbackViewModel.Email = "";
            }
            feedbackViewModel.ContactDetail = GetDetail();

            return View("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContactDetail();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
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