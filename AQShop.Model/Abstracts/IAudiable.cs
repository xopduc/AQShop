using System;

namespace AQShop.Model.Abstracts
{
    public interface IAudiable
    {
        DateTime? CreateDate { get; set; }

        String CreateBy { get; set; }

        DateTime? UpdateDate { get; set; }

        String UpdateBy { get; set; }

        String MetaKeyword { get; set; }

        String MetaDescription { get; set; }

        bool Status { get; set; }
    }
}