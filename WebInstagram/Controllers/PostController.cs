using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebInstagram.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

      /*  public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            List<Post> all = new List<Post>();

            // Here MyDatabaseEntities is our datacontext
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                all = dc.Posts.ToList();
            }
            return View(all);
        }
        public ActionResult Upload()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Upload(Post IG)
        {

           
            Debug.WriteLine("---------------------------------------");

            /* if(IG.File.ContentLength > (2*1024*1024))
             {
                 ModelState.AddModelError("CustomerError", "File size must be less than 2 MB");
                 return View();
             }
             if (!(IG.File.ContentType != "image/jpeg" || IG.File.ContentType == "image/gif")) 
             {
                 ModelState.AddModelError("Customer", "File type allowed : jpeg and gif");
                 return View();
             }
           IG.FileName = IG.File.FileName;

            byte[] data = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(data, 0, IG.File.ContentLength);
          
            IG.ImageData = data;
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    try
                    {
                        Debug.WriteLine(IG.FileName);
                        Debug.WriteLine(IG.File.ContentType);
                        Debug.WriteLine(IG.File.ContentLength);
                        Debug.WriteLine(IG.Created);
                        dc.Posts.Add(IG);
                        dc.SaveChanges();
                        ModelState.Clear();
                        IG = null;
                        ViewBag.Message = "Successfully Uploaded";
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Gallery");
        }*/
        public ActionResult Gallery()
        {
            List<Post> all = new List<Post>();

            // Here MyDatabaseEntities is our datacontext
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                all = dc.Posts.ToList();
            }
            return View(all);
        }
        [HttpPost]
        public ActionResult Upload(Post IG)
        {
            // Apply Validation Here


            if (IG.File.ContentLength > (2 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
                return View();
            }
            if (!(IG.File.ContentType == "image/jpeg" || IG.File.ContentType == "image/gif"))
            {
                ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
                return View();
            }
             
            IG.FileName = IG.File.FileName;
            IG.Created = DateTime.Now.ToString();
            IG.Image_Path = "~/Uploads/" + IG.FileName;
            byte[] data = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(data, 0, IG.File.ContentLength);

            IG.ImageData = data;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                try{
                    Debug.WriteLine(IG.Title);
                    Debug.WriteLine(IG.UserID);
                    Debug.WriteLine(IG.Created);
                    Debug.WriteLine(IG.Image_Path);
                dc.Posts.Add(IG);
                Debug.WriteLine("Walay sure kung ma add ------------------------");
                dc.SaveChanges();
                Debug.WriteLine("-------------------------------Nasave Na");
                  }
                catch (DbEntityValidationException dbEx)
                {
    foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
        foreach (var validationError in validationErrors.ValidationErrors)
        {
            Trace.TraceInformation("Property: {0} Error: {1}", 
                                    validationError.PropertyName, 
                                    validationError.ErrorMessage);
        }
    }
}
            }
            return RedirectToAction("Index","User");
        }
        public ActionResult Upload()
        {
            return View();
        }

    }
}
