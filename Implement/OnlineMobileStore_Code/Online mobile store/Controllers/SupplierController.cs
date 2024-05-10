using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_mobile_store.Models;
using System.Security.Claims;

namespace Online_mobile_store.Controllers
{
    public class SupplierController : Controller
    {
        private readonly mobilestore_dbContext db;
        public SupplierController(mobilestore_dbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var SellerId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var products = db.Products.Where(a => a.sellerid == SellerId);
            var deletionNotAllowed = TempData["DeletionNotAllowed"];
            ViewBag.DeletionNotAllowed = deletionNotAllowed != null ? true : false;
            return View(products);

        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Add(Product product, IFormFile Image)
        {
            if (IsNameExist(product.prod_name))
            {
                ViewBag.ErrorMsg = "this name is already exist";
                 return  View(product);

            }
            
            var SellerId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            product.sellerid = SellerId;

            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    string ImgeName = product.prod_id + product.prod_name + "." + Image.FileName.Split(".").Last();

                    using (var fs = new FileStream("wwwroot/Images/products/" + ImgeName, FileMode.Create))
                    {
                        await Image.CopyToAsync(fs);
                    }

                    product.image = ImgeName;
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }
            return View(product);
        }
        public IActionResult Edit( int id)
        {
            var product = db.Products.FirstOrDefault(a => a.prod_id == id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product,int id, IFormFile Image)
        {
            product.prod_id = id;
            var SellerId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            product.sellerid = SellerId;


            if (Image != null)
            {
                string ImgeName = product.prod_id + product.prod_name + "." + Image.FileName.Split(".").Last();

                using (var fs = new FileStream("wwwroot/Images/products/" + ImgeName, FileMode.Create))
                {
                    await Image.CopyToAsync(fs);
                }

                product.image = ImgeName;

            }
            else
            {
                product.image = getImage(id);
            }
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
               
               
                    db.Products.Update(product);
                    db.SaveChanges();

                    return RedirectToAction("Index");
               
                
            }
            return View(product);
        }
        public bool IsNameExist(string name) {
            return db.Products.Any(a => a.prod_name == name);
        }

        public IActionResult Delete(int id)
        {

            if (id == 0) return BadRequest();

            if (checkUsersProducts(id))
            {
               TempData["DeletionNotAllowed"] = true;
                var SellerId = Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                var products = db.Products.Where(a => a.sellerid == SellerId);
                return View("Index",products);
            }
            var product = db.Products.FirstOrDefault(a=>a.prod_id==id);
            db.Products.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        private string getImage(int id)
        {
            var product = db.Products.FirstOrDefault(a => a.prod_id == id);
            db.Entry(product).State = EntityState.Detached;
            return product.image;
        }

        private bool checkUsersProducts(int id)
        {
            return db.Users_Products.Any(a => a.prod_id == id);

        }


    }
}
