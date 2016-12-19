﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AQShop.Web.Models
{
    public class PostViewModel
    {      
        public int ID { get; set; }
        
        public string Name { get; set; }
      
        public string Alias { get; set; }
       
        public int CategoryID { get; set; }
       
        public string Image { get; set; }
       
        public string Description { get; set; }

        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public DateTime? CreateDate { get; set; }
      
        public String CreateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
             
        public String UpdateBy { get; set; }
              
        public String MetaKeyword { get; set; }
               
        public String MetaDescription { get; set; }

        public bool Status { get; set; }

        public virtual PostCategoryViewModel PostCategory { get; set; }

        public virtual IEnumerable<PostTagViewModel> PostTags { get; set; }
    }
}