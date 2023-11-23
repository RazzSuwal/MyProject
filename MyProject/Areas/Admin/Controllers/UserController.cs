using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.Models;
using MyProject.MyCommonHelper;
using System.Linq.Expressions;

namespace MyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebsiteRole.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ApiiCALL
        public IActionResult AllUser()
        {
            var users = _unitOfWork.ApplicationUser.GetAll();
            return Json(new { data = users }); //data transfer through JASON into web page  
        }
        #endregion

        public IActionResult Index() { 
            return View();
        }

        #region DeleteAPICall
        [HttpDelete]
        public IActionResult Delete(string? id)
        {
            try
            {
                var user = _unitOfWork.ApplicationUser.GetT(x => x.Id == id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Error in Fetching Data" });
                }
                else
                {
                    _unitOfWork.ApplicationUser.Delete(user);
                    _unitOfWork.Save();
                    return Json(new { success = true, message = "Course Deleted" });
                }
            }
            catch (DbUpdateException ex)
            {
                // ... your existing code ...

                // Constraint violation (related records exist)
                TempData["error"] = "Cannot delete the user because it has related data.";
                return RedirectToAction("Index"); // Or redirect to an error view
            }
            catch (Exception ex)
            {
                // Handle other exceptions that might occur during the deletion process
                TempData["error"] = "An unexpected error occurred: " + ex.Message;
                return RedirectToAction("Index"); // Or redirect to an error view
            }
        }
        #endregion
    }
}
