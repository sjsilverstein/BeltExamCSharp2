using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExamCSharp2.Models;
using BeltExamCSharp2.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BeltExamCSharp2.Controllers
{
    public class HomeController : Controller
    {
        // Connection to Database
        private BeltExamCSharp2Context _context;
        public HomeController(BeltExamCSharp2Context context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            return View("Index");
        }
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(RegistrationViewModel newUser){
            if(ModelState.IsValid){

                User emailChecker = _context.Users.SingleOrDefault(u => u.Email == newUser.Email);

                if(emailChecker != null){
                    //Error Here
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("Index");
                }
                
                User aliasChecker = _context.Users.SingleOrDefault(u => u.Alias == newUser.Alias);

                if(aliasChecker!= null){
                    //Error Here
                    ModelState.AddModelError("Alias", "User Name is already in use.");
                    return View("Index");
                }
                
                User addUser = new User{
                    Email = newUser.Email,
                    Alias = newUser.Alias,
                    Name = newUser.Name,
                    Password = newUser.Password
                };
                //Hash that Password
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                addUser.Password = Hasher.HashPassword(addUser, newUser.Password);
                
                // add User to Database
                _context.Add(addUser);
                _context.SaveChanges();

                // Save the new user id to session and move them to the logged in page.
                List<User> thisUser = _context.Users.Where(u => u.Email == addUser.Email && u.Alias == addUser.Alias).ToList();
                HttpContext.Session.SetInt32("UserId", (int)thisUser[0].UserId);

                return RedirectToAction("Index", "Wall");
            }
            return View("Index");
        }
        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index"); 
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login (LoginViewModel userToCheck){
            if (ModelState.IsValid){

                User IsUser = _context.Users.SingleOrDefault(u => u.Email == userToCheck.Email);
                if(IsUser != null){

                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(IsUser, IsUser.Password, userToCheck.Password)){
                        HttpContext.Session.SetInt32("UserId", (int)IsUser.UserId);
                        return RedirectToAction("Index", "Wall");
                    }
                }
            }
            return View("Index");
        }

       
    }
}
