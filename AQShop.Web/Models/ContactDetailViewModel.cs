using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AQShop.Web.Models
{
    public class ContactDetailViewModel
    {        
        public int ID { set; get; }
      
        public string Name { set; get; }
      
        public string Phone { set; get; }
      
        public string Email { set; get; }
      
        public string Website { set; get; }
       
        public string Address { set; get; }

        public string Other { set; get; }

        //Lat for Google Map
        public double? Lat { set; get; }

        //Long for Google Map
        public double? Lng { set; get; }

        public bool Status { set; get; }
    }
}