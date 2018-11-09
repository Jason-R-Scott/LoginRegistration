using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LoginRegistration.ViewModels;
using LoginRegistration.Data;
using Microsoft.AspNetCore.Identity;
using LoginRegistration.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;


namespace LoginRegistration.Controllers
{
    public class AuthController : Controller
    {
        public DataContext _context;
        public AuthController(DataContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Registration()
        {
            
            return View();
        }

        [HttpPost("register")]
        public IActionResult Registration(RegistrationViewModel reg)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if(_context.Users.Any(u=>u.Email == reg.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View();
                }
                PasswordHasher<RegistrationViewModel> Hasher = new PasswordHasher<RegistrationViewModel> ();
                string hashedPassword = Hasher.HashPassword (reg, reg.Password);
                User newUser = new User {
                    FirstName = reg.FirstName,
                    LastName = reg.LastName,
                    Email = reg.Email,
                    Password = hashedPassword,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Add(newUser);
                _context.SaveChanges();

                HttpContext.Session.SetString("LoggedIn", "true");

                return View("Success");
            }
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        
        [HttpPost("login")]
        public IActionResult Login(LoginViewModel user)
        {
            if(ModelState.IsValid)
            {
                var userDb = _context.Users.FirstOrDefault(u=> u.Email == user.Email);

                if(userDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View();
                }

                var hasher = new PasswordHasher<LoginViewModel>();
                var result = hasher.VerifyHashedPassword(user, userDb.Password,user.Password);

                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View();
                }

                HttpContext.Session.SetString("LoggedIn", "true");
            
                return RedirectToAction("Success");
            }
            return View();
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            string loginCheck = HttpContext.Session.GetString("LoggedIn");
            System.Console.WriteLine("####################");
            System.Console.WriteLine(loginCheck);

            if(loginCheck != "true")
            {
                ModelState.AddModelError("Email", "Must be logged in to reach success page");
                return View("login");
            }

            return View("success");
        }

        [HttpGet("logout")]
        public IActionResult Logout ()
        {
            HttpContext.Session.Clear();
            return View ("Login");
        }
    }
}
