using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace AQShop.Web.Models
{
    public class ProductViewModel
    {       
        public int ID { get; set; }
        
        public string Name { get; set; }
      
        public string Alias { get; set; }
       
        public int CategoryID { get; set; }
      
        public string Image { get; set; }

        public string MoreImages { get; set; }

        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }
        public int? Warranty { get; set; }
      
        public string Description { get; set; }
        [AllowHtml]
        public string Content { get; set; }

        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public string Tags { get; set; }

        public int Quantity { get; set; }

        public decimal OriginalPrice { get; set; }

        public DateTime? CreateDate { get; set; }
      
        public String CreateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
       
        public String UpdateBy { get; set; }
       
        public String MetaKeyword { get; set; }
       
        public String MetaDescription { get; set; }

        public bool Status { get; set; }

        public virtual ProductCategoryViewModel ProductCategory { get; set; }

        public virtual IEnumerable<ProductTagViewModel> ProductTags { get; set; }
    }
}