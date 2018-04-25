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
using Microsoft.EntityFrameworkCore;

namespace BeltExamCSharp2.Controllers
{
    public class WallController : Controller
    {
        //Connect to the Database
        private BeltExamCSharp2Context _context;
        public WallController(BeltExamCSharp2Context context){
            _context = context;
        }
        
        [HttpGet]
        [Route("bright_ideas")]
        public IActionResult Index (){
            // Check if the user is logged into the system if not redirect them to the login page.
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Home");
            }
            //List of all users
            List<User> dashboardList = _context.Users.ToList();
            ViewBag.dashboard = dashboardList;
            //Pull Logged in user
            User loggedUser = _context.Users.SingleOrDefault(u => u.UserId == uId);
            ViewBag.LoggedUser = loggedUser;

            List<Post> ideaList = _context.Posts.Include(p => p.Writer).Include(p => p.Likes).OrderByDescending(p => p.Likes.Count).ToList();
            ViewBag.Ideas = ideaList;

            return View("Index");
        }
        [HttpPost]
        [Route("CreatePost")]
        public IActionResult CreatePost(string Content){
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Home");
            }

            Post newPost = new Post{
                Content = Content,
                WriterId = (int) uId,
            };

            _context.Add(newPost);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("CreateLike/{PostId}")]
        public IActionResult CreateLike (int PostId){
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Home");
            }

            Like newLike = new Like{
                UserId = (int) uId,
                PostId = PostId,
                };
            _context.Add(newLike);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("bright_ideas/{PostId}")]
        public IActionResult IdeaPage(int PostId){
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Home");
            }
            
            Post thisPost = _context.Posts.Include(p => p.Writer).Include(p => p.Likes).ThenInclude(like => like.User).SingleOrDefault(p => p.PostId == PostId);
            ViewBag.ThisPost = thisPost;

            var listofLikers = _context.Likes.Where(l => l.PostId == PostId).Include(l => l.User).GroupBy(l => l.UserId);
            ViewBag.ListOfLikers = listofLikers;

            

            

            return View("IdeaPage");
        }
        [HttpGet]
        [Route("RemovePost/{PostId}")]
        public IActionResult RemovePost(int PostId){
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Home");
            }

            Post removeThis = _context.Posts.SingleOrDefault(p => p.PostId == PostId);

            if((int)uId == removeThis.WriterId){
                _context.Posts.Remove(removeThis);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
        [Route("users/{UserId}")]
        public IActionResult UserPage(int UserId){
            int? uId = HttpContext.Session.GetInt32("UserId");
            if(uId == null){
                return RedirectToAction("Index", "Home");
            }
            //Include(u => u.Likes).Include(u => u.PostsWritten)
            User thisUser = _context.Users.Include(u => u.Likes).Include(u => u.PostsWritten).SingleOrDefault(u => u.UserId == UserId);
            ViewBag.ThisUser = thisUser;


            return View("UserPage");

        }
    }
}