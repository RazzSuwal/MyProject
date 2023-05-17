using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.DataAccessLayer.Data;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.DataAccessLayer.Infrastructure.Repository;
using MyProject.Models;
using MyProject.Models.ViewModels;
using System.Security.Claims;

namespace MyProject.Areas.Users.Controllers
{
    [Area("Users, Admin")]
    public class MyCourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IEnumerable<SelectListItem> Categories { get; private set; }
        public IEnumerable<SelectListItem> Teachers { get; private set; }


        public MyCourseController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        #region ApiiCALL
        public IActionResult MyCourses(Course course)
        {
            var courses = _unitOfWork.Course.GetAll(x => x.TeacherId == course.TeacherId, includeProperties: "Category");
            return Json(new { data = courses }); //data transfer through JASON into web page
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CourseVM vm = new CourseVM()
            {
                Course = new(),
                Categories = _unitOfWork.Category.GetAll().Select(x =>
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })

            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Course = _unitOfWork.Course.GetT(x => x.Id == id);
                if (vm.Course == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CourseVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                //var claimsIdentity = (ClaimsIdentity)User.Identity;
                //var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                //vm.UserId = claims.Value;
                //for file upload
                string fileName = String.Empty;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "CourseImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    if (vm.Course.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Course.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Course.ImageUrl = @"\CourseImage\" + fileName;
                }
                if (vm.Course.Id == 0)
                {
                    _unitOfWork.Course.Add(vm.Course);
                    TempData["success"] = "Course Added!";
                }
                else
                {
                    _unitOfWork.Course.Update(vm.Course);
                    TempData["success"] = "Course Update Done!";
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #region DeleteAPICall
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var course = _unitOfWork.Course.GetT(x => x.Id == id);
            if (course == null)
            {
                return Json(new { success = false, message = "Error in Fetching Data" });
            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, course.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitOfWork.Course.Delete(course);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Course Deleted" });
            }
        }
        #endregion
    }
}
