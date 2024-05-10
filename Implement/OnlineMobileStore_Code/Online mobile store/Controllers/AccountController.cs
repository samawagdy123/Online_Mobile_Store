using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Online_mobile_store.Models;
using System.Data;
using System.Security.Claims;

namespace Online_mobile_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly mobilestore_dbContext db;
        public AccountController(mobilestore_dbContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Show()
        {
            await HttpContext.SignOutAsync();
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginView user)
        {
			ModelState.Remove("user.UserName");
			ModelState.Remove("user.Email");
			ModelState.Remove("user.Password");
			ModelState.Remove("user.Gender");
			ModelState.Remove("user.Role");


			if (ModelState.IsValid)
            {
                try
                {
                    var Role = "";
                    var userId = "";


                    var admin = db.Users.SingleOrDefault(a => a.Role == "admin" && a.Email == user.Email && a.Password == user.Password);
                    if (admin != null)
                    {
                        Role = "Admin";
                        userId = admin.ID.ToString();

                    }

                    var Supplier = db.Users.SingleOrDefault(a => a.Role == "supplier" && a.Email == user.Email && a.Password == user.Password);
                    if (Supplier != null)
                    {
                        Role = "supplier";
                        userId = Supplier.ID.ToString();

                    }

                    var Client = db.Users.SingleOrDefault(a => a.Role == "client" && a.Email == user.Email && a.Password == user.Password);
                    if (Client != null)
                    {
                        Role = "client";
                        userId = Client.ID.ToString();

                    }
                    //if(Client==null && admin==null && Supplier == null)
                    //{
                    //    ModelState.AddModelError("","Invalid Email or Password");
                    //}

                    if (!string.IsNullOrEmpty(Role))
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, Role),
                        new Claim(ClaimTypes.NameIdentifier, userId),
                    };
                     

                        var userIdentity = new ClaimsIdentity(claims, "login");

                        var userPrincipal = new ClaimsPrincipal(userIdentity);

                        HttpContext.SignInAsync(userPrincipal);

                        switch (Role)
                        {
                            case "Admin":
                                return RedirectToAction("Index", "Admin");
                            case "supplier":
                                return RedirectToAction("Index", "supplier");
                            case "client":
                                return RedirectToAction("Index", "Client");
                        }
                    }

                    ModelState.AddModelError("", "Invalid email or password.");
                    return View("index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                    return View("Show");
                }
            }
            return View("index");
           
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("index" ,"client");
        }

        public IActionResult AccessError()
        {
            return View("AccessError");
        }
        [HttpPost]
        public IActionResult registarview(User user)
        {
            if(IsEmailExist(user.Email))
            {
                ViewBag.Error = "The Email Is Already Exist";

                return BadRequest(new { Message = "The Email Is Already Exist" });
            }
            

            
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Json(new { success = true, user = user });
                //UserLoginView userLoginView = new UserLoginView();
                // userLoginView.Email = user.Email;
                // userLoginView.Password = user.Password;
                // //return RedirectToAction("Login", userLoginView);
                // if (!string.IsNullOrEmpty(user.Role))
                // {
                //     var claims = new List<Claim>
                //     {
                //         new Claim(ClaimTypes.Name, user.Email),
                //         new Claim(ClaimTypes.Role, user.Role),
                //         new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                //     };


                    //     var userIdentity = new ClaimsIdentity(claims, "login");

                    //     var userPrincipal = new ClaimsPrincipal(userIdentity);

                    //     HttpContext.SignInAsync(userPrincipal);

                    //     switch (user.Role)
                    //     {
                    //         case "Admin":
                    //             return RedirectToAction("Index", "Admin");
                    //         case "supplier":
                    //             return RedirectToAction("Index", "supplier");
                    //         case "client":
                    //             return RedirectToAction("Index", "Client");
                    //     }
               
            }
            else
            {
                return BadRequest(new {Message="Enter Valid Data"});
            }
          

            //}
            return View("Index");
        }
        public bool IsEmailExist(string email)
        {
            return db.Users.Any(a => a.Email == email);
        }

    }
}
