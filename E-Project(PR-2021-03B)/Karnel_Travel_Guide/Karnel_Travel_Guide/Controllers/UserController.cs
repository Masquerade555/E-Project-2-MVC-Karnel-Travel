using Karnel_Travel_Guide.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Karnel_Travel_Guide.Controllers
{
    public class UserController : Controller
    {

        //Making Object of Model Entities
        Karnel_Travel_GuideEntities db = new Karnel_Travel_GuideEntities();

        
        public ActionResult Index()
        {
            return View();
        }


        //Making User Sign Up ActionMethod
        public ActionResult User_Signup()
        {
            return View();
        }



        //Making User Sign Up HttpPost Method
        [HttpPost]
        public ActionResult User_Signup(tb_user u)
        {
            //Using if condition to see if User Sign Up View is empty or not
            if (u != null)
            {
                //if User Sign Up is not empty than save data in tb_user table and redirect to index of home controller
                db.tb_user.Add(u);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //if condition is false than clear the model state 
                ModelState.Clear();
                return View();
            }
        }

        //Making User Sign In ActionMethod
        public ActionResult User_Login()
        {
            return View();
        }

        //Making User Sign In HttpPost Method
        [HttpPost]
        public ActionResult User_Login(tb_user l)
        {
            //if User Sign In is not empty than check data in tb_user table where data enterd is same as table data
            if (l != null)
            {
                var data = db.tb_user.Where(m => m.u_email == l.u_email && m.u_password == l.u_password).FirstOrDefault();
                
                if (data != null)
                {
                    //make sessions and redirect to index of home controller
                    Session["u_id"] = data.u_tb.ToString();
                    Session["u_name"] = data.u_name.ToString();
                    Session["u_address"] = data.u_address.ToString();
                    Session["u_contact"] = data.u_contact.ToString();
                    Session["u_email"] = data.u_email.ToString();
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        // logging out the user and clearing all sessions
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //making action method to show user profile
        public ActionResult User_Profile(int id)
        {
            //checking session to see if its null or not
            if (Session["u_id"] == null)
            {
                //if null then redirect to index of home controller
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //if not null then show the user its profile 
                var data = db.tb_user.Where(m => m.u_tb == id).SingleOrDefault();

                return View(data);
            }

        }

        //making httppost method of user profile
        [HttpPost]
        public int User_Profile(tb_user u)
        {
            //checking the data of view to see if its null or not
            if (u != null)
            {

                //if not null then changes made are saved in user table
                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }

        }

        //making action method of user updating its password
        [HttpPost]
        public int Update_Password(string old_p, string new_p, int id)
        {
            //checking to see if old password and new password columns are null or not
            if (old_p != null && new_p != null)
            {
                //if it is not null then find user id on table column
                var find = db.tb_user.Where(u => u.u_tb == id).SingleOrDefault();

                //check if old password is same as user table password if true then
                if (find.u_password == old_p)
                {
                    //save the new info in user column in database table
                    find.u_address = find.u_address;
                    find.u_city = find.u_city;
                    find.u_contact = find.u_contact;
                    find.u_email = find.u_email;
                    find.u_tb = id;
                    find.u_name = find.u_name;
                    find.u_password = new_p.ToString();
                    db.Entry(find).State = EntityState.Modified;
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }

        //making httppost method of forget password
        [HttpPost]
        public int Forget_Password(string email, string password)
        {
            //if email is not null then
            if (email != null)
            {
                //if it is not null then find user id on table column
                var find = db.tb_user.Where(x => x.u_email == email).SingleOrDefault();
                
                //check to see if entered email is same as database email
                if (find.u_email == email)
                {
                    //if password is null then
                    if (password != null)
                    {
                        //save all info then save it in database
                        find.u_address = find.u_address;
                        find.u_city = find.u_city;
                        find.u_contact = find.u_contact;
                        find.u_email = find.u_email;
                        find.u_tb = find.u_tb;
                        find.u_name = find.u_name;
                        find.u_password = password.ToString();
                        db.Entry(find).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }


                return 1;
            }
            else
            {
                return 0;
            }

        }

    }
}