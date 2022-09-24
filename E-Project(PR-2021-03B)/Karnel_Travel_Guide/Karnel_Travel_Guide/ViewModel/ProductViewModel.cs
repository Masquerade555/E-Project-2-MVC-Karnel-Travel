using Karnel_Travel_Guide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karnel_Travel_Guide.ViewModel
{
    public class ProductViewModel
    {
        public IEnumerable<tb_tour> Tb_Tours { get; set; }
        public IEnumerable<tb_transportation> Tb_Transportations { get; set; }
        public IEnumerable<tb_restaurant> Tb_Restaurants { get; set; }
        public IEnumerable<tb_resorts> Tb_Resorts { get; set; }
        public IEnumerable<tb_packages> Tb_Packages { get; set; }
        public IEnumerable<tb_hotels> Tb_Hotels { get; set; }
       

        public HttpPostedFileBase ImageFile { get; set; }

    }

    public class FeaturedViewModel
    {
        public IEnumerable<tb_tour> Tb_Tours { get; set; }
        public IEnumerable<tb_transportation> Tb_Transportations { get; set; }
        public IEnumerable<tb_packages> Tb_Packages { get; set; }
        

        public HttpPostedFileBase ImageFile { get; set; }

    }
}