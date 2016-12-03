using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AQShop.Model.Models
{
    [Table("Errors")]
    public class Error
    {
        [Key]
        public int ID { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreateDate { get; set; }
    }
}