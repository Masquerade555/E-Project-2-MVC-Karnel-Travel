using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karnel_Travel_Guide.Models
{
    public class Cart
    {
        public (List<tb_transportation>, List<tb_tour>, List<tb_restaurant>, List<tb_resorts>, List<tb_packages>, List<tb_hotels>) MyCart { get; set; }
        public List<int> ProductId { get; set; }

        //public List<int> ProductId1 { get; set; }
        //public List<int> ProductId2 { get; set; }
        //public List<int> ProductId3 { get; set; }
        //public List<int> ProductId4 { get; set; }
        //public List<int> ProductId5 { get; set; }
        //public List<int> ProductId6 { get; set; }
        

        public List<tb_tour> Tours { get; set; }
        public List<tb_transportation> Transportations { get; set; }
        public List<tb_restaurant> Restaurants { get; set; }
        public List<tb_resorts> Resorts { get; set; }
        public List<tb_packages> Packages { get; set; }
        public List<tb_hotels> Hotels { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }

    public class Favourite
    {
        public (List<tb_transportation>, List<tb_tour>, List<tb_restaurant>, List<tb_resorts>, List<tb_packages>, List<tb_hotels>) MyFav { get; set; }
        public List<int> ProductId { get; set; }

        public List<tb_tour> Tours { get; set; }
        public List<tb_transportation> Transportations { get; set; }
        public List<tb_restaurant> Restaurants { get; set; }
        public List<tb_resorts> Resorts { get; set; }
        public List<tb_packages> Packages { get; set; }
        public List<tb_hotels> Hotels { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}