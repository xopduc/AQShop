using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AQShop.Web.Models
{
    public class ProductCategoryViewModel
    {
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Alias { get; set; }

      
        public String Description { get; set; }

        public int? ParentID { get; set; }
        public int? DisplayOrder { get; set; }

    
        public string Image { get; set; }

        public bool? HomeFlag { get; set; }

        public DateTime? CreateDate { get; set; }
        
        public String CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public String UpdateBy { get; set; }
      
        public String MetaKeyword { get; set; }
     
        public String MetaDescription { get; set; }

        public bool Status { get; set; }

        public virtual IEnumerable<ProductViewModel> Products { get; set; }
    }
}