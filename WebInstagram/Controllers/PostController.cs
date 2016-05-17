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

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            List<Post> all = new List<Post>();

            // Here MyDatabaseEntities is our datacontext
            using (MyDatabaseEntities1 dc = new MyDatabaseEntities1())
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
             }*/
            IG.FileName = IG.File.FileName;

            byte[] data = new byte[IG.File.ContentLength];
            IG.File.InputStream.Read(data, 0, IG.File.ContentLength);
          
            IG.ImageData = data;
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities1 dc = new MyDatabaseEntities1())
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
            }
            return RedirectToAction("Gallery");
        }

    }
}
