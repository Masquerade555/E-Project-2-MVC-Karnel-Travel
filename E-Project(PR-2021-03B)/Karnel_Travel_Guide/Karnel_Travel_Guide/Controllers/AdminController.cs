using Karnel_Travel_Guide.Models;
using Karnel_Travel_Guide.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karnel_Travel_Guide.Controllers
{
    public class AdminController : Controller
    {

        //Making Object of Model Entities
        Karnel_Travel_GuideEntities db = new Karnel_Travel_GuideEntities();
        
        
        public ActionResult Index()
        {
            return View();
        }


        //making action method of dashboard
        public ActionResult Dashboard()
        {
            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                //else count the total products, users, orders and money earned from it
                var tc = db.tb_transportation.Count() + db.tb_tour.Count() + db.tb_restaurant.Count() + db.tb_resorts.Count() + db.tb_packages.Count() + db.tb_hotels.Count() ;

                TempData["Product_Count"] = tc; 
                TempData["User_Count"] = db.tb_user.Count();
                TempData["Order_Count"] = db.tb_orders.Count();
                TempData["Order_Total"] = db.tb_orders.Sum(p => p.o_total);

                return View();
            }
        }

        //making admin login method
        public ActionResult Admin_Login()
        {
            return View();
        }

        //making admin login httppost method
        [HttpPost]
        public ActionResult Admin_Login(tb_admin a)
        {
            //check the email and password of view column and match it with admin table column
            var data = db.tb_admin.Where(model => model.ad_email == a.ad_email && model.ad_password == a.ad_password).FirstOrDefault();
            //if it is not null then
            if (data != null)
            {
                //make sessions
                Session["Admin_id"] = data.ad_id.ToString();
                Session["Admin_name"] = data.ad_name.ToString();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return View();
            }
        }

        
        //making all category action method
        public ActionResult All_Category()
        {

            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                //else convert category table into list
                var data = db.tb_category.ToList();
                return PartialView(data);
            }

        }

        
        //making category delete method
        public ActionResult Category_delete(int id)
        {
            //checking to see selected column id matches with table column id
            var data = db.tb_category.Where(m => m.c_id == id).SingleOrDefault();
            //delete the entity and save database
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("All_Category", "Admin");
        }

        //making category action method
        public ActionResult Category()
        {

            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                return View();
            }

        }

        
        //making httppost method of  category name
        [HttpPost]
        public int Cat(string cat_name)
        {
            //if category name is not null
            if (cat_name != null)
            {
                //making object of category table and naming its name column
                tb_category c = new tb_category();
                c.c_name = cat_name;

                //saving it in database
                db.tb_category.Add(c);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //making all product method
        public ActionResult All_Product()
        {

            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                //else converting every table into list and showing it
                var tables = new ProductViewModel
                {
                    Tb_Transportations = db.tb_transportation.ToList(),
                    Tb_Tours = db.tb_tour.ToList(),
                    Tb_Restaurants = db.tb_restaurant.ToList(),
                    Tb_Resorts = db.tb_resorts.ToList(),
                    Tb_Packages = db.tb_packages.ToList(),
                    Tb_Hotels = db.tb_hotels.ToList()
                    
                };
                return PartialView(tables);
            }

        }

        
        //making httppost method of product delete
        [HttpPost]
        public ActionResult Product_delete(int id)
        {
            //checking to match selected id with database tables id
            var data = db.tb_transportation.Where(m => m.tr_id == id).SingleOrDefault();
            var data1 = db.tb_tour.Where(m => m.to_id == id).SingleOrDefault();
            var data2 = db.tb_restaurant.Where(m => m.rest_id == id).SingleOrDefault();
            var data3 = db.tb_resorts.Where(m => m.reso_id == id).SingleOrDefault();
            var data4 = db.tb_packages.Where(m => m.pac_id == id).SingleOrDefault();
            var data5 = db.tb_hotels.Where(m => m.h_id == id).SingleOrDefault();
            
            //requesting image path of the selected product
            string image = Request.MapPath(data.tr_images.ToString());
            string image1 = Request.MapPath(data1.to_images.ToString());
            string image2 = Request.MapPath(data2.rest_images.ToString());
            string image3 = Request.MapPath(data3.reso_images.ToString());
            string image4 = Request.MapPath(data4.pac_images.ToString());
            string image5 = Request.MapPath(data5.h_images.ToString());
           
            //if image exists then delete it
            if (System.IO.File.Exists(image))
            {
                System.IO.File.Delete(image);
            }
            if (System.IO.File.Exists(image1))
            {
                System.IO.File.Delete(image1);
            }
            if (System.IO.File.Exists(image2))
            {
                System.IO.File.Delete(image2);
            }
            if (System.IO.File.Exists(image3))
            {
                System.IO.File.Delete(image3);
            }
            if (System.IO.File.Exists(image4))
            {
                System.IO.File.Delete(image4);
            }
            if (System.IO.File.Exists(image5))
            {
                System.IO.File.Delete(image5);
            }
           
            //deleting entity and saving database
            db.Entry(data).State = EntityState.Deleted;
            db.Entry(data1).State = EntityState.Deleted;
            db.Entry(data2).State = EntityState.Deleted;
            db.Entry(data3).State = EntityState.Deleted;
            db.Entry(data4).State = EntityState.Deleted;
            db.Entry(data5).State = EntityState.Deleted;
            

            db.SaveChanges();
            return RedirectToAction("All_Product", "Admin");
        }

      

        //making product method 
        public ActionResult Product()
        {

            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                
                
                return PartialView();
            }

        }

        //making partialview of transportation
        public PartialViewResult Transportation()
        {
            return PartialView("_Producttr");
        }

        //making partialview of tour
        public PartialViewResult Tour()
        {
            return PartialView("_Productto");
        }

        //making partialview of restaurant
        public PartialViewResult Restaurant()
        {
            return PartialView("_Productrest");
        }

        //making partialview of packages
        public PartialViewResult Packages()
        {
            return PartialView("_Productpac");
        }

        //making partialview of resorts
        public PartialViewResult Resorts()
        {
            return PartialView("_Productreso");
        }

        //making partialview of hotels
        public PartialViewResult Hotels()
        {
            return PartialView("_Producth");
        }

        //making partialview of accommodation
        public PartialViewResult Accommodation()
        {
            return PartialView("_Productac");
        }


        //making logout method of admin
        public ActionResult Logout()
        {
            //clearing all sessions
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Admin_login", "Admin");
        }



        //making method of all customers
        public ActionResult All_Customers()
        {

            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                //else convert user table into list
                var data = db.tb_user.ToList();
                return PartialView(data);
            }

        }

        //making httppost method of customer delete
        [HttpPost]
        public ActionResult Customer_delete(int id)
        {
            //matching selected id to match database customer table id
            var data = db.tb_user.Where(m => m.u_tb == id).SingleOrDefault();
            //entity state deleted and database saved
            db.Entry(data).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("All_Customers", "Admin");
        }

    
        //making order method
        public ActionResult Order()
        {

            //check to see if session is null or not
            if (Session["Admin_id"] == null)
            {
                //if condition is false than redirect to admin login
                return RedirectToAction("Admin_login", "Admin");
            }
            else
            {
                //else convert order table into descending to list
                var data = db.tb_orders.OrderByDescending(x => x.o_id).ToList();
                return PartialView(data);
            }

        }


        //making httppost method of order update
        [HttpPost]
        public ActionResult Order_Update(string status, int? id)
        {
            //if order status is not null and id is not null then
            if (status != null && id != null)
            {
                //match table order id with selected id
                var data = db.tb_orders.Where(x => x.o_id == id).SingleOrDefault();

                //updating order table
                data.o_id = Convert.ToInt32(id);
                data.o_billing_city = data.o_billing_city;
                data.o_billing_name = data.o_billing_name;
                data.o_date = data.o_date;
                data.o_items = data.o_items;
                data.o_payment = data.o_payment;
                data.o_status = status;
                data.o_total = data.o_total;
                data.o_quantity = data.o_quantity;
                data.o_foreign_key = data.o_foreign_key;

                //and saving database
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Order", "Admin");

            }
            else
            {
                //else redirect to admin order
                return RedirectToAction("Order", "Admin");
            }
        }



        //making method of product transportation
        public ActionResult Producttr()
        {
            

            return PartialView();
          

        }

        //making httppost method of product transportation
        [HttpPost]
        public ActionResult Producttr(tb_transportation p)
        {
            //making strings and saving extension and name in it
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);

            //checking if the image extensions are the same
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
            {
                //if same then save image in the folder and in database
                filename = filename + extension;
                p.tr_images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                p.ImageFile.SaveAs(filename);
                //making default discount to NO
                p.tr_discount = "no";

                db.tb_transportation.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");
            }
        }

        //making method of product tour
        public ActionResult Productto()
        {
            
            return PartialView();


        }

        //making httppost method of product tour
        [HttpPost]
        public ActionResult Productto(tb_tour p)
        {
            //making strings and saving extension and name in it
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);

            //checking if the image extensions are the same
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
            {
                //if same then save image in the folder and in database
                filename = filename + extension;
                p.to_images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                p.ImageFile.SaveAs(filename);
                //making default discount to NO
                p.to_discount = "no";

                db.tb_tour.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");
            }
        }

        //making method of product restaurant
        public ActionResult Productrest()
        {


            return PartialView();


        }

        //making httppost method of product restaurant
        [HttpPost]
        public ActionResult Productrest(tb_restaurant p)
        {
            //making strings and saving extension and name in it
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);
            //checking if the image extensions are the same
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
            {
                //if same then save image in the folder and in database
                filename = filename + extension;
                p.rest_images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                p.ImageFile.SaveAs(filename);

                

                db.tb_restaurant.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");
            }
        }

        //making method of product resort
        public ActionResult Productreso()
        {


            return PartialView();


        }

        //making httppost method of product resort
        [HttpPost]
        public ActionResult Productreso(tb_resorts p)
        {
            //making strings and saving extension and name in it
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);
            //checking if the image extensions are the same
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
            {
                //if same then save image in the folder and in database
                filename = filename + extension;
                p.reso_images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                p.ImageFile.SaveAs(filename);

                
                db.tb_resorts.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");
            }
        }

        //making method of product packages
        public ActionResult Productpac()
        {


            return PartialView();


        }

        //making httppost method of product packages
        [HttpPost]
        public ActionResult Productpac(tb_packages p)
        {
            //making strings and saving extension and name in it
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);
            //checking if the image extensions are the same
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
            {
                //if same then save image in the folder and in database
                filename = filename + extension;
                p.pac_images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                p.ImageFile.SaveAs(filename);
                //making default discount to NO
                p.pac_discount = "no";

                db.tb_packages.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");
            }
        }

        //making method of product hotels
        public ActionResult Producth()
        {


            return PartialView();


        }

        //making httppost method of product hotels
        [HttpPost]
        public ActionResult Producth(tb_hotels p)
        {
            //making strings and saving extension and name in it
            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);
            //checking if the image extensions are the same
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
            {
                //if same then save image in the folder and in database
                filename = filename + extension;
                p.h_images = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                p.ImageFile.SaveAs(filename);

                
                db.tb_hotels.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                //else clear model state
                ModelState.Clear();
                return Content("<script language='javascript' type='text/javascript'>alert('Error');</script>");
            }
        }



        //making method of edit transportation
        public ActionResult Edittr(int id)
        {
            //matching selected id to database table id
            var data = db.tb_transportation.Where(m => m.tr_id == id).FirstOrDefault();
            
            //saving that image in temporary data to show a preview of it
            TempData["Images"] = data.tr_images.ToString();
            TempData.Keep();
            return PartialView(data);

            
        }

        //making httppost method of edit transportation
        [HttpPost]
        public ActionResult Edittr(tb_transportation v)
        {
            //if view image is not null then
            if (v.ImageFile != null)
            {
                //making strings and saving extension and name in it
                string filename = Path.GetFileNameWithoutExtension(v.ImageFile.FileName);
                string extension = Path.GetExtension(v.ImageFile.FileName);
                //checking if the image extensions are the same
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    //if same then save the new image in the folder and in database
                    filename = filename + extension;
                    v.tr_images = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    v.ImageFile.SaveAs(filename);

                    db.Entry(v).State = EntityState.Modified;
                    db.SaveChanges();

                    string image = Request.MapPath(TempData["Images"].ToString());
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    //else clear model state
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            else
            {
                //else fetch tempdata and put it into string and save it 
                v.tr_images = TempData["Images"].ToString();
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        //making method of edit tour
        public ActionResult Editto(int id)
        {
            //matching selected id to database table id
            var data = db.tb_tour.Where(m => m.to_id == id).FirstOrDefault();

            //saving that image in temporary data to show a preview of it
            TempData["Images"] = data.to_images.ToString();
            TempData.Keep();
            return PartialView(data);


        }

        //making httppost method of edit tour
        [HttpPost]
        public ActionResult Editto(tb_tour v)
        {

            //if view image is not null then
            if (v.ImageFile != null)
            {
                //making strings and saving extension and name in it
                string filename = Path.GetFileNameWithoutExtension(v.ImageFile.FileName);
                string extension = Path.GetExtension(v.ImageFile.FileName);
                //checking if the image extensions are the same
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    //if same then save the new image in the folder and in database
                    filename = filename + extension;
                    v.to_images = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    v.ImageFile.SaveAs(filename);

                    db.Entry(v).State = EntityState.Modified;
                    db.SaveChanges();

                    string image = Request.MapPath(TempData["Images"].ToString());
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    //else clear model state
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            else
            {
                //else fetch tempdata and put it into string and save it 
                v.to_images = TempData["Images"].ToString();
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        //making method of edit restaurant
        public ActionResult Editrest(int id)
        {
            //matching selected id to database table id
            var data = db.tb_restaurant.Where(m => m.rest_id == id).FirstOrDefault();

            //saving that image in temporary data to show a preview of it
            TempData["Images"] = data.rest_images.ToString();
            TempData.Keep();
            return PartialView(data);


        }

        //making httppost method of edit restaurant
        [HttpPost]
        public ActionResult Editrest(tb_restaurant v)
        {

            //if view image is not null then
            if (v.ImageFile != null)
            {
                //making strings and saving extension and name in it
                string filename = Path.GetFileNameWithoutExtension(v.ImageFile.FileName);
                string extension = Path.GetExtension(v.ImageFile.FileName);
                //checking if the image extensions are the same
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    //if same then save the new image in the folder and in database
                    filename = filename + extension;
                    v.rest_images = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    v.ImageFile.SaveAs(filename);

                    db.Entry(v).State = EntityState.Modified;
                    db.SaveChanges();

                    string image = Request.MapPath(TempData["Images"].ToString());
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    //else clear model state
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            else
            {
                //else fetch tempdata and put it into string and save it 
                v.rest_images = TempData["Images"].ToString();
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        //making method of edit resort
        public ActionResult Editreso(int id)
        {
            //matching selected id to database table id
            var data = db.tb_resorts.Where(m => m.reso_id == id).FirstOrDefault();

            //saving that image in temporary data to show a preview of it
            TempData["Images"] = data.reso_images.ToString();
            TempData.Keep();
            return PartialView(data);


        }

        //making httppost method of edit resort
        [HttpPost]
        public ActionResult Editreso(tb_resorts v)
        {

            //if view image is not null then
            if (v.ImageFile != null)
            {
                //making strings and saving extension and name in it
                string filename = Path.GetFileNameWithoutExtension(v.ImageFile.FileName);
                string extension = Path.GetExtension(v.ImageFile.FileName);

                //checking if the image extensions are the same
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    //if same then save the new image in the folder and in database
                    filename = filename + extension;
                    v.reso_images = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    v.ImageFile.SaveAs(filename);

                    db.Entry(v).State = EntityState.Modified;
                    db.SaveChanges();

                    string image = Request.MapPath(TempData["Images"].ToString());
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    //else clear model state
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            else
            {
                //else fetch tempdata and put it into string and save it 
                v.reso_images = TempData["Images"].ToString();
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        //making method of edit packages
        public ActionResult Editpac(int id)
        {
            //matching selected id to database table id
            var data = db.tb_packages.Where(m => m.pac_id == id).FirstOrDefault();

            //saving that image in temporary data to show a preview of it
            TempData["Images"] = data.pac_images.ToString();
            TempData.Keep();
            return PartialView(data);


        }

        //making httppost method of edit package
        [HttpPost]
        public ActionResult Editpac(tb_packages v)
        {

            //if view image is not null then
            if (v.ImageFile != null)
            {
                //making strings and saving extension and name in it
                string filename = Path.GetFileNameWithoutExtension(v.ImageFile.FileName);
                string extension = Path.GetExtension(v.ImageFile.FileName);

                //checking if the image extensions are the same
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    //if same then save the new image in the folder and in database
                    filename = filename + extension;
                    v.pac_images = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    v.ImageFile.SaveAs(filename);

                    db.Entry(v).State = EntityState.Modified;
                    db.SaveChanges();

                    string image = Request.MapPath(TempData["Images"].ToString());
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    //else clear model state
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            else
            {
                //else fetch tempdata and put it into string and save it 
                v.pac_images = TempData["Images"].ToString();
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        //making method of edit hotels
        public ActionResult Edith(int id)
        {
            //matching selected id to database table id
            var data = db.tb_hotels.Where(m => m.h_id == id).FirstOrDefault();

            //saving that image in temporary data to show a preview of it
            TempData["Images"] = data.h_images.ToString();
            TempData.Keep();
            return PartialView(data);


        }

        //making httppost method of edit hotels
        [HttpPost]
        public ActionResult Edith(tb_hotels v)
        {

            //if view image is not null then
            if (v.ImageFile != null)
            {
                //making strings and saving extension and name in it
                string filename = Path.GetFileNameWithoutExtension(v.ImageFile.FileName);
                string extension = Path.GetExtension(v.ImageFile.FileName);

                //checking if the image extensions are the same
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    //if same then save the new image in the folder and in database
                    filename = filename + extension;
                    v.h_images = "~/Images/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                    v.ImageFile.SaveAs(filename);

                    db.Entry(v).State = EntityState.Modified;
                    db.SaveChanges();

                    string image = Request.MapPath(TempData["Images"].ToString());
                    if (System.IO.File.Exists(image))
                    {
                        System.IO.File.Delete(image);
                    }

                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    //else clear model state
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            else
            {
                //else fetch tempdata and put it into string and save it 
                v.h_images = TempData["Images"].ToString();
                db.Entry(v).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Dashboard", "Admin");
            }
        }

       
    }
}
 