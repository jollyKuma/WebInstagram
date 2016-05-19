using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebInstagram.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User U)
        {
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    dc.Users.Add(U);
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Successfully Registration Done";

                }
            }
            return View(U);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            Debug.WriteLine("---------------------My login state");

            // if (ModelState.IsValid)
            //{
            Debug.WriteLine("---------------------My model state");

            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                Debug.WriteLine("---------------------Inside database entities");

                var v = dc.Users.Where(model => model.Username.Equals(u.Username) && model.Password.Equals(u.Password)).FirstOrDefault();
                Debug.WriteLine("---------------------Condition");
                if (v != null)
                {
                    
                    Session["LogedUserID"] = v.UserID.ToString();
                    Session["LogedUserFullname"] = v.Fullname.ToString();
                    Session["LogedUserEmail"] = v.EmailID.ToString();
                    Debug.WriteLine(Session["LogedUserID"]);
                    return RedirectToAction("Index");
                }
            }
            //}

            ViewBag.Message = "Login Error";
            return View(u);
        }
        public ActionResult Index()
        {

            if (Session["LogedUserID"] == null)
            {
                
                return RedirectToAction("Login");
            }
           
            dynamic mymodel = new ExpandoObject();
            
            
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Login");
        }

      
    }
}
