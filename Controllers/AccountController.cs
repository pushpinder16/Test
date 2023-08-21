using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Test3.Data;
using Test3.Models;


namespace Test3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var a = new Register()
                {
                    Name= model.Name,
                    Email= model.Email,
                    Password = model.Password,
                    ConfirmPassword= model.ConfirmPassword

                };
                _context.Registers.Add(a);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Registration Successfull";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errormessage"] = "can't submit Empty Fields !!!";
                return View(model);
            }

            
            }
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Login(Login model, bool IsRemember)
        {
            var data = _context.Registers.Where(e => e.Email == model.Email).SingleOrDefault();
            if (data != null)
            {


                bool isvalid = (data.Email == model.Email && data.Password == model.Password);
                if (isvalid)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email)
                    };


                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var authenticationProperties = new AuthenticationProperties();
                    if (IsRemember)
                    {
                        authenticationProperties.IsPersistent = true;
                        authenticationProperties.ExpiresUtc = DateTime.UtcNow.AddMonths(1);
                    }



                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                    HttpContext.Session.SetString("Email", model.Email);
                    TempData["Email"] = model.Email;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    TempData["Message"] = "Invalid Email!";
                    return View(model);
                }
            }
            else
            {
                TempData["Message"] = "Invalid Password!";
                return View(model);

            }



        }

    }
}



