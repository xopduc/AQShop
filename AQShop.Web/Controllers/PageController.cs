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
    public class PageController : Controller
    {
        private IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var model = _pageService.GetPageByAlias(alias);
            var pageView = AutoMapper.Mapper.Map<Page, PageViewModel>(model);
            return View(pageView);
        }
    }
}