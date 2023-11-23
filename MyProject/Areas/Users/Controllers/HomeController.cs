using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.DataAccessLayer.Infrastructure.Repository;
using MyProject.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MyProject.Controllers
{
    [Area("Users")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork = null)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Course> courses = _unitOfWork.Course.GetAlls(includeProperties: new Expression<Func<Course, object>>[]
           {
                t => t.Category
           });
            ViewBag.ShowFooter = true;
            return View(courses);
        }
        [HttpGet]
        public IActionResult Course()
        {
            IEnumerable<Course> courses = _unitOfWork.Course.GetAlls(includeProperties: new Expression<Func<Course, object>>[]
           {
                t => t.Category
           });
            ViewBag.ShowFooter = true;
            return View(courses);
        }
        [HttpGet]
        public IActionResult Details(int? CourseId)
        {
            Cart cart = new Cart()
            {
                Course = _unitOfWork.Course.GetT(x => x.Id == CourseId, includeProperties: "Category"),
                CourseId = (int)CourseId
            };
            ViewBag.ShowFooter = true;
            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(Cart cart)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cart.ApplicationUserId = claims.Value;

                var cartItem = _unitOfWork.Cart.GetT(x => x.CourseId == cart.CourseId && x.ApplicationUserId == claims.Value);
                if (cartItem == null)
                {

                    _unitOfWork.Cart.Add(cart);
                    TempData["success"] = "Course Has Added to Cart!";
                    _unitOfWork.Save();
                    HttpContext.Session.SetInt32("SessionCart", _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value).ToList().Count);

                }
                else
                {
                    TempData["success"] = "Course Has Alreday Added to Cart!";
                    
                }

            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}