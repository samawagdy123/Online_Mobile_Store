using Microsoft.AspNetCore.Mvc;
using Online_mobile_store.Models;

namespace Online_mobile_store.Controllers
{
    public class AdminController : Controller
    {
        private readonly mobilestore_dbContext db;
        public AdminController(mobilestore_dbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var model = db.Users.Where(a => a.Role == "supplier").ToList();
            var deletionNotAllowed = TempData["DeletionNotAllowed"];
            ViewBag.DeletionNotAllowed = deletionNotAllowed != null ? true : false;

            return View(model);
        }

        public IActionResult delete(int id)
        {
            if (id == 0) return BadRequest();
            var prodlist = db.Products.Where(a => a.sellerid == id).ToList();
            if (prodlist != null)
            {
                foreach (var prod in prodlist)
                {
                    var sellerprod = db.Users_Products.FirstOrDefault(a => a.prod_id == prod.prod_id);
                    if (sellerprod != null)
                    {
						TempData["DeletionNotAllowed"] = true;
						return RedirectToAction("Index");
                    }
                }
            }

           

            var model = db.Users_Products.FirstOrDefault(a => a.user_id == id);
            if (model == null)
            {
                var user = db.Users.FirstOrDefault(a => a.ID == id);
                db.Users.Remove(user);
                db.SaveChanges();
                //TempData["DeletionNotAllowed"] = false;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public IActionResult showclient()
        {
            var model=db.Users.Where(a=>a.Role=="client").ToList();
            var deletionNotAllowed = TempData["DeletionNotAllowed"];
            ViewBag.DeletionNotAllowed = deletionNotAllowed != null ? true : false;

            return View(model);
        }
        public IActionResult deleteclient( int id)
        {
            if (id == 0) return BadRequest();
            var userproduct=db.Users_Products.FirstOrDefault(a=>a.user_id==id);
            if(userproduct != null)
            {

                TempData["DeletionNotAllowed"] = true;
                return RedirectToAction("showclient");

            }
            var model = db.Users.FirstOrDefault(a=>a.ID==id);
            if (model != null)
            {
                db.Users.Remove(model);
                db.SaveChanges();
                return RedirectToAction("showclient");

            }
            return RedirectToAction("showclient");
        }
    }
}
