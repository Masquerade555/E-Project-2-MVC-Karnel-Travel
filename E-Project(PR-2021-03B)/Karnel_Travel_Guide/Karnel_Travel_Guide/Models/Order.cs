using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karnel_Travel_Guide.Models
{
    public class Order
    {
        public IEnumerable<tb_orders> Tb_Orders { get; set; }
        public List<tb_orders> Orders { get; set; }

        public IEnumerable<tb_tour> Tb_Tours { get; set; }
        public IEnumerable<tb_transportation> Tb_Transportations { get; set; }
        public IEnumerable<tb_restaurant> Tb_Restaurants { get; set; }
        public IEnumerable<tb_resorts> Tb_Resorts { get; set; }
        public IEnumerable<tb_packages> Tb_Packages { get; set; }
        public IEnumerable<tb_hotels> Tb_Hotels { get; set; }

        public List<tb_tour> Tours { get; set; }
        public List<tb_transportation> Transportations { get; set; }
        public List<tb_restaurant> Restaurants { get; set; }
        public List<tb_resorts> Resorts { get; set; }
        public List<tb_packages> Packages { get; set; }
        public List<tb_hotels> Hotels { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public List<int> ProductId { get; set; }
    }

}