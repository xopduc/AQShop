using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AQShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { get; set; }
        public IEnumerable<ProductViewModel> LatestProducts { get;set;}
        public IEnumerable<ProductViewModel> TopSaleProducts { get; set; }
    }
}