using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karnel_Travel_Guide.Models
{
    public class Contact
    {
        public List<tb_tour> Tours { get; set; }
        public List<tb_transportation> Transportations { get; set; }
        public List<tb_restaurant> Restaurants { get; set; }
        public List<tb_resorts> Resorts { get; set; }
        public List<tb_packages> Packages { get; set; }
        public List<tb_hotels> Hotels { get; set; }

        public List<tb_category> Category { get; set; }
    }
}