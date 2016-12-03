using System;
using System.ComponentModel.DataAnnotations;

namespace AQShop.Model.Abstracts
{
    public class Audiable:IAudiable
    {
        public DateTime? CreateDate { get; set; }

        [MaxLength(256)]
        public String CreateBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(256)]
        public String UpdateBy { get; set; }

        [MaxLength(256)]
        public String MetaKeyword { get; set; }

        [MaxLength(256)]
        public String MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}