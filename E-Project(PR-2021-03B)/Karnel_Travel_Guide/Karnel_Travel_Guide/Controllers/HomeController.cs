using Karnel_Travel_Guide.Models;
using Karnel_Travel_Guide.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace Karnel_Travel_Guide.Controllers
{
   
    public class HomeController : Controller
    {


        //Making Object of Model Entities
        Karnel_Travel_GuideEntities db = new Karnel_Travel_GuideEntities();

        
            
        //making action method of index
        public ActionResult Index()
        {
            //making list of products that have discounts
            List<tb_transportation> SomeList1 = db.tb_transportation.Where(model => model.tr_discount == "yes").ToList();
            List<tb_tour> SomeList2 = db.tb_tour.Where(model => model.to_discount == "yes").ToList();
            List<tb_packages> SomeList3 = db.tb_packages.Where(model => model.pac_discount == "yes").ToList();

            //putting them in tempdata
            TempData["MyList1"] = SomeList1;
            TempData["MyList2"] = SomeList2;
            TempData["MyList3"] = SomeList3;

            //making list of all products
            List<tb_transportation> ProductList1 = db.tb_transportation.ToList();
            List<tb_tour> ProductList2 = db.tb_tour.ToList();
            List<tb_restaurant> ProductList3 = db.tb_restaurant.ToList();
            List<tb_resorts> ProductList4 = db.tb_resorts.ToList();
            List<tb_packages> ProductList5 = db.tb_packages.ToList();
            List<tb_hotels> ProductList6 = db.tb_hotels.ToList();

            //putting them in tempdata
            TempData["ProductList1"] = ProductList1;
            TempData["ProductList2"] = ProductList2;
            TempData["ProductList3"] = ProductList3;
            TempData["ProductList4"] = ProductList4;
            TempData["ProductList5"] = ProductList5;
            TempData["ProductList6"] = ProductList6;
           


            return View();
        }

        //making method of all products
        public ActionResult All_Products(int? Page_no, int? minPrice, int? maxPrice, string featured, string low_to_high, string high_to_low, string search, string Category)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //useing pro class
                Pro pag = new Pro();
                
                //converting pro tables into list
                pag.Transportations = db.tb_transportation.ToList();
                pag.Tours= db.tb_tour.ToList();
                pag.Restaurants= db.tb_restaurant.ToList();
                pag.Resorts = db.tb_resorts.ToList();
                pag.Packages = db.tb_packages.ToList();
                pag.Hotels = db.tb_hotels.ToList();
                
                
                //making pages for view
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;

                //checking the conditions that user want in the view
                //if low to high button is selected then
                if (!string.IsNullOrEmpty(low_to_high))
                {
                    //convert pro tables into order list
                    pag.Transportations = pag.Transportations.OrderBy(model => model.tr_price).ToList();
                    pag.Tours = pag.Tours.OrderBy(model => model.to_price).ToList();
                    pag.Restaurants = pag.Restaurants.OrderBy(model => model.rest_price).ToList();
                    pag.Resorts = pag.Resorts.OrderBy(model => model.reso_price).ToList();
                    pag.Packages = pag.Packages.OrderBy(model => model.pac_price).ToList();
                    pag.Hotels = pag.Hotels.OrderBy(model => model.h_price).ToList();
                }
                //if high to low button is selected then
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    //convert pro tables into descending order list
                    pag.Transportations = pag.Transportations.OrderByDescending(model => model.tr_price).ToList();
                    pag.Tours = pag.Tours.OrderByDescending(model => model.to_price).ToList();
                    pag.Restaurants = pag.Restaurants.OrderByDescending(model => model.rest_price).ToList();
                    pag.Resorts = pag.Resorts.OrderByDescending(model => model.reso_price).ToList();
                    pag.Packages = pag.Packages.OrderByDescending(model => model.pac_price).ToList();
                    pag.Hotels = pag.Hotels.OrderByDescending(model => model.h_price).ToList();
                }
                //if minimum price to maximum price slider is selected then
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    //convert pro tables into selected minimum and selected maximum price list
                    pag.Transportations = pag.Transportations.Where(p => p.tr_price >= minPrice && p.tr_price <= maxPrice).ToList();
                    pag.Tours = pag.Tours.Where(p => p.to_price >= minPrice && p.to_price <= maxPrice).ToList();
                    pag.Restaurants = pag.Restaurants.Where(p => p.rest_price >= minPrice && p.rest_price <= maxPrice).ToList();
                    pag.Resorts = pag.Resorts.Where(p => p.reso_price >= minPrice && p.reso_price <= maxPrice).ToList();
                    pag.Packages = pag.Packages.Where(p => p.pac_price >= minPrice && p.pac_price <= maxPrice).ToList();
                    pag.Hotels = pag.Hotels.Where(p => p.h_price >= minPrice && p.h_price <= maxPrice).ToList();

                }
                //if search button is used then
                if (!string.IsNullOrEmpty(search))
                {
                    //if searching word is in the table then it will convert into list
                    search = search.ToLower();
                    pag.Transportations = pag.Transportations.Where(p => p.tr_name.ToLower().Contains(search)).ToList();
                    pag.Tours = pag.Tours.Where(p => p.to_name.ToLower().Contains(search)).ToList();
                    pag.Restaurants = pag.Restaurants.Where(p => p.rest_name.ToLower().Contains(search)).ToList();
                    pag.Resorts = pag.Resorts.Where(p => p.reso_name.ToLower().Contains(search)).ToList();
                    pag.Packages = pag.Packages.Where(p => p.pac_name.ToLower().Contains(search)).ToList();
                    pag.Hotels = pag.Hotels.Where(p => p.h_name.ToLower().Contains(search)).ToList();

                }
                //if categories button is selected then
                if (!string.IsNullOrEmpty(Category))
                {
                    //showing categories
                    var c = db.tb_category.Where(m => m.c_name == Category).SingleOrDefault();
                    
                }

                //counting total products od every table
                var tc = db.tb_transportation.Count() + db.tb_tour.Count() + db.tb_restaurant.Count() + db.tb_resorts.Count() + db.tb_packages.Count() + db.tb_hotels.Count();
                //putting the count into tempdata
                TempData["ProductCount"] = tc;
                //maximumproducts in a page to show
                pag.Pager = new Pager(tc, Page_no, 9);

                
                //showing tables in the pager
                pag.Transportations = (pag.Transportations).Skip((Page_no.Value - 1) * 9).Take(2).ToList();
                pag.Tours = (pag.Tours).Skip((Page_no.Value - 1) * 9).Take(2).ToList();
                pag.Restaurants = (pag.Restaurants).Skip((Page_no.Value - 1) * 9).Take(1).ToList();
                pag.Resorts = (pag.Resorts).Skip((Page_no.Value - 1) * 9).Take(1).ToList();
                pag.Packages = (pag.Packages).Skip((Page_no.Value - 1) * 9).Take(1).ToList();
                pag.Hotels = (pag.Hotels).Skip((Page_no.Value - 1) * 9).Take(1).ToList();
                return View(pag);
            }

        }


        
        //making method of transport view
        public ActionResult P_V_tr(int id)
        {


            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //showing product to user when user clicks on it and putting rating of that product
                var data = db.tb_transportation.Where(m => m.tr_id == id).SingleOrDefault();
                
                TempData["Rating"] = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
                Rating_Percentage(id);
                return View(data);

            }

        }

       


        public ActionResult P_V_transportation(int? Page_no, int? minPrice, int? maxPrice, string low_to_high, string high_to_low, string search)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {

                //useing pro class converting pro tables into list making pages for view
                //if low to high button is selected then convert pro tables into order list
                //if high to low button is selected then convert pro tables into descending order list
                //if minimum price to maximum price slider is selected then convert pro tables into selected minimum and selected maximum price list
                //if search button is used then searching word is in the table then it will convert into list

                Pro pag = new Pro();
                pag.Transportations = db.tb_transportation.ToList();
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;
                

                    if (!string.IsNullOrEmpty(low_to_high))
                {
                    pag.Transportations = pag.Transportations.OrderBy(model => model.tr_price).ToList();
                }
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    pag.Transportations = pag.Transportations.OrderByDescending(model => model.tr_price).ToList();  
                }
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    pag.Transportations = pag.Transportations.Where(p => p.tr_price >= minPrice && p.tr_price <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    pag.Transportations = pag.Transportations.Where(p => p.tr_name.ToLower().Contains(search)).ToList();
                }
                var tc = db.tb_transportation.Count() ;
                TempData["ProductCount1"] = tc;
                pag.Pager = new Pager(tc, Page_no, 9);
                pag.Transportations = (pag.Transportations).Skip((Page_no.Value - 1) * 9).Take(9).ToList();
                
                return View(pag);

            }

        }


        //making method of tour view
        public ActionResult P_V_to(int id)
        {


            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //showing product to user when user clicks on it and putting rating of that product
                var data = db.tb_tour.Where(m => m.to_id == id).SingleOrDefault();

                TempData["Rating"] = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
                Rating_Percentage(id);
                return View(data);

            }

        }

        public ActionResult P_V_tours(int? Page_no, int? minPrice, int? maxPrice, string low_to_high, string high_to_low, string search)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //useing pro class converting pro tables into list making pages for view
                //if low to high button is selected then convert pro tables into order list
                //if high to low button is selected then convert pro tables into descending order list
                //if minimum price to maximum price slider is selected then convert pro tables into selected minimum and selected maximum price list
                //if search button is used then searching word is in the table then it will convert into list

                Pro pag = new Pro();
                pag.Tours = db.tb_tour.ToList();
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;


                if (!string.IsNullOrEmpty(low_to_high))
                {
                    pag.Tours = pag.Tours.OrderBy(model => model.to_price).ToList();
                }
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    pag.Tours = pag.Tours.OrderByDescending(model => model.to_price).ToList();
                }
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    pag.Tours = pag.Tours.Where(p => p.to_price >= minPrice && p.to_price <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    pag.Tours = pag.Tours.Where(p => p.to_name.ToLower().Contains(search)).ToList();
                }
                var tc = db.tb_tour.Count();
                TempData["ProductCount2"] = tc;
                pag.Pager = new Pager(tc, Page_no, 9);
                pag.Tours = (pag.Tours).Skip((Page_no.Value - 1) * 9).Take(9).ToList();

                return View(pag);

            }

        }


        //making method of restaurant view
        public ActionResult P_V_rest(int id)
        {

            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //showing product to user when user clicks on it and putting rating of that product
                var data = db.tb_restaurant.Where(m => m.rest_id == id).SingleOrDefault();

                TempData["Rating"] = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
                Rating_Percentage(id);
                return View(data);

            }

        }

        public ActionResult P_V_restaurant(int? Page_no, int? minPrice, int? maxPrice, string low_to_high, string high_to_low, string search)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //useing pro class converting pro tables into list making pages for view
                //if low to high button is selected then convert pro tables into order list
                //if high to low button is selected then convert pro tables into descending order list
                //if minimum price to maximum price slider is selected then convert pro tables into selected minimum and selected maximum price list
                //if search button is used then searching word is in the table then it will convert into list

                Pro pag = new Pro();
                pag.Restaurants = db.tb_restaurant.ToList();
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;


                if (!string.IsNullOrEmpty(low_to_high))
                {
                    pag.Restaurants = pag.Restaurants.OrderBy(model => model.rest_price).ToList();
                }
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    pag.Restaurants = pag.Restaurants.OrderByDescending(model => model.rest_price).ToList();
                }
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    pag.Restaurants = pag.Restaurants.Where(p => p.rest_price >= minPrice && p.rest_price <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    pag.Restaurants = pag.Restaurants.Where(p => p.rest_name.ToLower().Contains(search)).ToList();
                }
                var tc = db.tb_restaurant.Count();
                TempData["ProductCount3"] = tc;
                pag.Pager = new Pager(tc, Page_no, 9);
                pag.Restaurants = (pag.Restaurants).Skip((Page_no.Value - 1) * 9).Take(9).ToList();

                return View(pag);

            }

        }

        //making method of resort view
        public ActionResult P_V_reso(int id)
        {

            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //showing product to user when user clicks on it and putting rating of that product
                var data = db.tb_resorts.Where(m => m.reso_id == id).SingleOrDefault();

                TempData["Rating"] = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
                Rating_Percentage(id);
                return View(data);

            }

        }

        public ActionResult P_V_resorts(int? Page_no, int? minPrice, int? maxPrice, string low_to_high, string high_to_low, string search)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //useing pro class converting pro tables into list making pages for view
                //if low to high button is selected then convert pro tables into order list
                //if high to low button is selected then convert pro tables into descending order list
                //if minimum price to maximum price slider is selected then convert pro tables into selected minimum and selected maximum price list
                //if search button is used then searching word is in the table then it will convert into list

                Pro pag = new Pro();
                pag.Resorts = db.tb_resorts.ToList();
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;


                if (!string.IsNullOrEmpty(low_to_high))
                {
                    pag.Resorts = pag.Resorts.OrderBy(model => model.reso_price).ToList();
                }
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    pag.Resorts = pag.Resorts.OrderByDescending(model => model.reso_price).ToList();
                }
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    pag.Resorts = pag.Resorts.Where(p => p.reso_price >= minPrice && p.reso_price <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    pag.Resorts = pag.Resorts.Where(p => p.reso_name.ToLower().Contains(search)).ToList();
                }
                var tc = db.tb_resorts.Count();
                TempData["ProductCount4"] = tc;
                pag.Pager = new Pager(tc, Page_no, 9);
                pag.Resorts = (pag.Resorts).Skip((Page_no.Value - 1) * 9).Take(9).ToList();

                return View(pag);

            }

        }

        //making method of packages view
        public ActionResult P_V_pac(int id)
        {

            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //showing product to user when user clicks on it and putting rating of that product
                var data = db.tb_packages.Where(m => m.pac_id == id).SingleOrDefault();

                TempData["Rating"] = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
                Rating_Percentage(id);
                return View(data);

            }

        }

        public ActionResult P_V_packages(int? Page_no, int? minPrice, int? maxPrice, string low_to_high, string high_to_low, string search)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //useing pro class converting pro tables into list making pages for view
                //if low to high button is selected then convert pro tables into order list
                //if high to low button is selected then convert pro tables into descending order list
                //if minimum price to maximum price slider is selected then convert pro tables into selected minimum and selected maximum price list
                //if search button is used then searching word is in the table then it will convert into list

                Pro pag = new Pro();
                pag.Packages = db.tb_packages.ToList();
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;


                if (!string.IsNullOrEmpty(low_to_high))
                {
                    pag.Packages = pag.Packages.OrderBy(model => model.pac_price).ToList();
                }
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    pag.Packages = pag.Packages.OrderByDescending(model => model.pac_price).ToList();
                }
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    pag.Packages = pag.Packages.Where(p => p.pac_price >= minPrice && p.pac_price <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    pag.Packages = pag.Packages.Where(p => p.pac_name.ToLower().Contains(search)).ToList();
                }
                var tc = db.tb_packages.Count();
                TempData["ProductCount5"] = tc;
                pag.Pager = new Pager(tc, Page_no, 9);
                pag.Packages = (pag.Packages).Skip((Page_no.Value - 1) * 9).Take(9).ToList();

                return View(pag);

            }

        }

        //making method of hotels view
        public ActionResult P_V_h(int id)
        {

            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //showing product to user when user clicks on it and putting rating of that product
                var data = db.tb_hotels.Where(m => m.h_id == id).SingleOrDefault();

                TempData["Rating"] = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
                Rating_Percentage(id);
                return View(data);

            }

        }

        public ActionResult P_V_hotels(int? Page_no, int? minPrice, int? maxPrice, string low_to_high, string high_to_low, string search)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //useing pro class converting pro tables into list making pages for view
                //if low to high button is selected then convert pro tables into order list
                //if high to low button is selected then convert pro tables into descending order list
                //if minimum price to maximum price slider is selected then convert pro tables into selected minimum and selected maximum price list
                //if search button is used then searching word is in the table then it will convert into list

                Pro pag = new Pro();
                pag.Hotels = db.tb_hotels.ToList();
                Page_no = Page_no.HasValue ? Page_no.Value > 0 ? Page_no.Value : 1 : 1;
                int Page = Page_no.Value;


                if (!string.IsNullOrEmpty(low_to_high))
                {
                    pag.Hotels = pag.Hotels.OrderBy(model => model.h_price).ToList();
                }
                if (!string.IsNullOrEmpty(high_to_low))
                {
                    pag.Hotels = pag.Hotels.OrderByDescending(model => model.h_price).ToList();
                }
                if (minPrice.HasValue && maxPrice.HasValue)
                {
                    pag.Hotels = pag.Hotels.Where(p => p.h_price >= minPrice && p.h_price <= maxPrice).ToList();
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.ToLower();
                    pag.Hotels = pag.Hotels.Where(p => p.h_name.ToLower().Contains(search)).ToList();
                }
                var tc = db.tb_hotels.Count();
                TempData["ProductCount6"] = tc;
                pag.Pager = new Pager(tc, Page_no, 9);
                pag.Hotels = (pag.Hotels).Skip((Page_no.Value - 1) * 9).Take(9).ToList();

                return View(pag);

            }

        }

       

       
        //making httppost method of buy now
        [HttpPost]
        public ActionResult Buy_Now(int id, string qty)
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {

                if (id != 0)
                {
                    //when clicked select the id of the product and put it in cart
                    
                    var data = db.tb_transportation.Where(m => m.tr_id == id).SingleOrDefault();
                    var data1 = db.tb_tour.Where(m => m.to_id == id).SingleOrDefault();
                    var data2 = db.tb_restaurant.Where(m => m.rest_id == id).SingleOrDefault();
                    var data3 = db.tb_resorts.Where(m => m.reso_id == id).SingleOrDefault();
                    var data4 = db.tb_packages.Where(m => m.pac_id == id).SingleOrDefault();
                    var data5 = db.tb_hotels.Where(m => m.h_id == id).SingleOrDefault();
                    

                    var dat = (data,data1,data2,data3,data4,data5);

                    var pr = data.tr_price * Convert.ToInt32(qty);
                    var pr1 = data1.to_price * Convert.ToInt32(qty);
                    var pr2 = data2.rest_price * Convert.ToInt32(qty);
                    var pr3 = data3.reso_price * Convert.ToInt32(qty);
                    var pr4 = data4.pac_price * Convert.ToInt32(qty);
                    var pr5 = data5.h_price * Convert.ToInt32(qty);

                    ViewBag.price1 = pr;
                    ViewBag.price2 = pr1;
                    ViewBag.price3 = pr2;
                    ViewBag.price4 = pr3;
                    ViewBag.price5 = pr4;
                    ViewBag.price6 = pr5;

                    var price = (pr, pr1, pr2, pr3, pr4, pr5);

                    ViewBag.price = price;
                    return View(dat);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

        }

        //making method of add to cart
        public ActionResult Add_To_Cart()
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //else make object of cart, request cookies of user cart products
                Cart p = new Cart();

                var Cart = Request.Cookies["CartProducts"];

                //if cart is not null and cart value is empty then
                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    //split the values of cookies into their specifc table
                    p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    p.Transportations = db.tb_transportation.Where(x => p.ProductId.Contains(x.tr_id)).ToList();
                    p.Tours = db.tb_tour.Where(x => p.ProductId.Contains(x.to_id)).ToList();
                    p.Restaurants = db.tb_restaurant.Where(x => p.ProductId.Contains(x.rest_id)).ToList();
                    p.Resorts = db.tb_resorts.Where(x => p.ProductId.Contains(x.reso_id)).ToList();
                    p.Packages = db.tb_packages.Where(x => p.ProductId.Contains(x.pac_id)).ToList();
                    p.Hotels = db.tb_hotels.Where(x => p.ProductId.Contains(x.h_id)).ToList();
                    return View(p);
                }
                else
                {
                    return View();
                }
            }

        }

        //making method of deleting cookies
        [HttpPost]
        public ActionResult DeleteCookie()
        {
            //Check if Cookie exists.
            if (Request.Cookies["CartProducts"] != null)
            {
                //Fetch the Cookie using its Key.
                HttpCookie nameCookie = Request.Cookies["CartProducts"];

                //Set the Expiry date to past date.
                nameCookie.Expires = DateTime.Now.AddDays(-1);

                //Update the Cookie in Browser.
                Response.Cookies.Add(nameCookie);

                //Set Message in TempData.
                TempData["Message"] = "Cookie deleted.";
            }
            else
            {
                //Set Message in TempData.
                TempData["Message"] = "Cookie not found.";
            }

            return RedirectToAction("Add_To_Cart");
        }

        //making partialview of transportation
        public PartialViewResult Transportation()
        {
            return PartialView("_Add_To_Cart_tr");
        }


        //making partialview of tour
        public PartialViewResult Tour()
        {
            return PartialView("_Add_To_Cart_to");
        }

        //making partialview of restaurant
        public PartialViewResult Restaurant()
        {
            return PartialView("_Add_To_Cart_rest");
        }

        //making partialview of packages
        public PartialViewResult Packages()
        {
            return PartialView("_Add_To_Cart_pac");
        }

        //making partialview of resorts
        public PartialViewResult Resorts()
        {
            return PartialView("_Add_To_Cart_reso");
        }

        //making partialview of hotels
        public PartialViewResult Hotels()
        {
            return PartialView("_Add_To_Cart_h");
        }




        //making httppost method of order
        [HttpPost]
        public ActionResult Order()
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //else fetch cookies, make object of order table
                var Cart = Request.Cookies["CartProducts"];
                tb_orders order = new tb_orders();

                List<string> files = new List<string>();
                List<string> qty = new List<string>();

                //if cart is not null then
                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    //split cookies products
                    var ProductIds = Cart.Value.Split('-').Select(x => int.Parse(x)).Distinct().ToList();
                    var Ids = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();

                    //put them in list
                    foreach (var item in ProductIds.ToList())
                    {
                        files.Add(item.ToString());
                        qty.Add(Ids.Where(x => x == item).Count().ToString());

                        ViewBag.pro = string.Join(",", files);
                        ViewBag.qty = string.Join(",", qty);
                    }

                    //fetch all details for order table and save
                    order.o_items = ViewBag.pro;
                    order.o_quantity = ViewBag.qty;
                    order.o_billing_city = Session["u_address"].ToString();
                    order.o_billing_name = Session["u_name"].ToString();
                    order.o_date = DateTime.Now;
                    order.o_payment = "COD";
                    order.o_status = "Processing";
                    order.o_total = Convert.ToInt32(TempData["Total"]);
                    order.o_foreign_key = Convert.ToInt32(Session["u_id"]);

                    db.tb_orders.Add(order);
                    db.SaveChanges();
                    //Send_Email(Session["u_email"].ToString(), Session["u_name"].ToString());


                }

                return RedirectToAction("Index", "Home");

            }
        }


        //making method of advance search
        public ActionResult Advance_Search()
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                
                    return View();
                

            }

        }

        //method of your orders
        public ActionResult Your_Orders()
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                //else check your foreign key and make it into list if its the same as your id
                int id = Convert.ToInt32(Session["u_id"]);
                var data = db.tb_orders.Where(x => x.o_foreign_key == id).ToList();
                return View(data);
            }

        }


        //method of contact
        public ActionResult Contact()
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                return View();

            }
        }

        
        //method of about
        public ActionResult About()
        {
            //checking session
            if (Session["u_id"] == null)
            {
                //if null then ask user to login
                return RedirectToAction("User_Login", "User");
            }
            else
            {
                return View();

            }
        }



       

        //httppost method of rating
        [HttpPost]
        public int Rating(string star, string msg, int? id)
        {
            //object of rating table
            tb_rating r = new tb_rating();

            //if atleast 1 of these is not null then
            if (star != null || msg != null || id != null)
            {
                //add that rating to the table and save
                r.r_comment = msg.ToString();
                r.r_rating = Convert.ToInt32(star);
                r.r_user_id_foreign_key = Convert.ToInt32(Session["u_id"]);
                r.r_pro_id_foreign_key = Convert.ToInt32(id);

                db.tb_rating.Add(r);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }


        }

        //method of rating percentage
        public void Rating_Percentage(int id)
        {
            //sum up all the rating of that product
            var data = db.tb_rating.Where(x => x.r_pro_id_foreign_key == id).ToList();
            double i = Convert.ToDouble(data.Sum(x => x.r_rating));
            double c = data.Count();

            double total_rating = i / c;
            double per = total_rating / 5 * 100;
            TempData["Percentage"] = per;
        }

        //method of add to cart of transport
        public ActionResult Add_To_Cart_tr()
        {


            Cart p = new Cart();

            var Cart = Request.Cookies["CartProducts"];

            if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
            {
                p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                p.Transportations = db.tb_transportation.Where(x => p.ProductId.Contains(x.tr_id)).ToList();
                return View(p);
            }
            else
                {
                    return View();
                }

            

        }

        //method of add to cart of tour
        public ActionResult Add_To_Cart_to()
        {
            
                Cart p = new Cart();

                var Cart = Request.Cookies["CartProducts"];

                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    p.Tours = db.tb_tour.Where(x => p.ProductId.Contains(x.to_id)).ToList();
                    return View(p);
                }
                else
                {
                    return View();
                }

            

        }

        //method of add to cart of restaurant
        public ActionResult Add_To_Cart_rest()
        {

                Cart p = new Cart();

                var Cart = Request.Cookies["CartProducts"];

                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    p.Restaurants = db.tb_restaurant.Where(x => p.ProductId.Contains(x.rest_id)).ToList();
                    return View(p);
                }
                else
                {
                    return View();
                }

            

        }

        //method of add to cart of packages
        public ActionResult Add_To_Cart_reso()
        {

                Cart p = new Cart();

                var Cart = Request.Cookies["CartProducts"];

                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    p.Resorts = db.tb_resorts.Where(x => p.ProductId.Contains(x.reso_id)).ToList();
                    return View(p);
                }
                else
                {
                    return View();
                }

            

        }

        //method of add to cart of packages
        public ActionResult Add_To_Cart_pac()
        {

            
                Cart p = new Cart();

                var Cart = Request.Cookies["CartProducts"];

                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    p.Packages = db.tb_packages.Where(x => p.ProductId.Contains(x.pac_id)).ToList();
                    return View(p);
                }
                else
                {
                    return View();
                }

            

        }

        //method of add to cart of hotel
        public ActionResult Add_To_Cart_h()
        {

           
                Cart p = new Cart();

                var Cart = Request.Cookies["CartProducts"];

                if (Cart != null && !string.IsNullOrEmpty(Cart.Value))
                {
                    p.ProductId = Cart.Value.Split('-').Select(x => int.Parse(x)).ToList();
                    p.Hotels = db.tb_hotels.Where(x => p.ProductId.Contains(x.h_id)).ToList();
                    return View(p);
                }
                else
                {
                    return View();
                }

        }

    }
}



























