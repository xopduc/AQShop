using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AQShop.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Category
        public ActionResult Category(int id)
        {
            return View();
        }

        // GET: Product
        public ActionResult Detail(int id)
        {
            return View();
        }
    }
}