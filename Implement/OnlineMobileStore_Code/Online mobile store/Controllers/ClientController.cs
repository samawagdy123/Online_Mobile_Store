using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using NuGet.Versioning;
using Online_mobile_store.Models;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Online_mobile_store.Controllers
{
    public class ClientController : Controller
    {
        private readonly mobilestore_dbContext db;

        public ClientController(mobilestore_dbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var model = db.Products.Where(a=>a.quantity>0).ToList();
            int UserId;
            //Get LoggedInUser
            bool result = Int32.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out UserId);
            if (!result)
            {
                ViewBag.cartorders = 0;

            }
            else
            {
                var todaay = DateOnly.FromDateTime(DateTime.Today);
                var cartordersList = db.Users_Products.Where(p => p.status == "selected" && p.user_id == UserId).ToList();
                ViewBag.cartorders = cartordersList.Count();
            }
            return View(model);
        }

        public IActionResult CheckOut()
        {
            var userId = 0;
            var res = Int32.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out userId);
            if (!res) return RedirectToAction("index");

            var userProducts = db.Users_Products
                        .Include(up => up.prod) // Assuming you're using Entity Framework and have navigation property Product
                        .Where(up => up.user_id == userId)
                        .ToList();
            var cartordersList = db.Users_Products.Where(p => p.status == "selected" && p.user_id == userId ).ToList();
            ViewBag.cartorders = cartordersList.Count();

            decimal  Total = 0;
            foreach (var item in userProducts)
            {
                Total += item?.selected_quantity * item?.prod?.price?? 0;
            }


            ViewBag.TotalPrice = Total;
            return View(userProducts);
        }
        [HttpPost]
        public IActionResult AddToCart(int Productid)
        {
            int UserId;
            //Get LoggedInUser
            bool result = Int32.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out UserId);
            if (!result)
            {
                return Json(new { LoginRequired = true });
            }
            else
            {
                DateOnly selectdate = DateOnly.FromDateTime(DateTime.Now);
                var existorder = db.Users_Products.FirstOrDefault(p => p.user_id == UserId && p.prod_id == Productid && p.status == "selected");
                if (existorder == null)
                {
                    Users_Product product = new Users_Product();
                    product.prod_id = Productid;
                    product.user_id = UserId;
                    product.status = "selected";
                    product.date = selectdate;
                    product.selected_quantity = GetQuantity(selectdate, product.user_id, product.prod_id) + 1;
                    db.Users_Products.Add(product);
                    db.SaveChanges();

                }
                else
                {
                    existorder.selected_quantity += 1;
                    db.SaveChanges();
                }
                var model = db.Products.ToList();
                var cartordersList = db.Users_Products.Where(p => p.status == "selected" && p.user_id == UserId).ToList();
                var cartorders = cartordersList.Count();
                return Json(cartorders);
            }
        }
        private int GetQuantity(DateOnly selectdate ,int userid,int productid)
        {
           return db.Users_Products.Count(p => p.date == selectdate && p.user_id == userid && p.prod_id == productid);

        }
    }
}
