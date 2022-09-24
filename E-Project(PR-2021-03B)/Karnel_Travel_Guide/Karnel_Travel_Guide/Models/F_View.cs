using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karnel_Travel_Guide.Models
{
    

    public class F_View
    {
          Karnel_Travel_GuideEntities db = new Karnel_Travel_GuideEntities();
        public IEnumerable<tb_tour> Tb_Tours { get; set; }
        public IEnumerable<tb_transportation> Tb_Transportations { get; set; }
        public IEnumerable<tb_packages> Tb_Packages { get; set; }

        public int to_id { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int GarmentJacketId { get; set; }
        public int GarmentShirtId { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }

    public class CompleteView
    {
        public IEnumerable<tb_tour> Tb_Tours { get; set; }
        public IEnumerable<tb_transportation> Tb_Transportations { get; set; }
        public IEnumerable<tb_restaurant> Tb_Restaurants { get; set; }
        public IEnumerable<tb_resorts> Tb_Resorts { get; set; }
        public IEnumerable<tb_packages> Tb_Packages { get; set; }
        public IEnumerable<tb_hotels> Tb_Hotels { get; set; }
        public IEnumerable<tb_accommodation> Tb_Accommodations { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}