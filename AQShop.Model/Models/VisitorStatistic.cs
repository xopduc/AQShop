﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AQShop.Model.Models
{
    [Table("VisitorStatics")]
    public class VisitorStatistic
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public DateTime VisitedDate { get; set; }

        [MaxLength(50)]
        public string IPAddress { get; set; }
    }
}