using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyProject.DataAccessLayer.Data;
using MyProject.Models;

namespace MyProject.Areas.Users.Controllers
{
    [Area("Users")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            TempData["success"] = "Form Has Submitted!";
            return RedirectToAction("Index");
            }
            return View(teacher);
        }
    }
}
