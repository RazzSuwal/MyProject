using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DataAccessLayer.Data;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.DataAccessLayer.Infrastructure.Repository;
using MyProject.Models;
using MyProject.Models.ViewModels;
using MyProject.MyCommonHelper;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =WebsiteRole.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories = _unitOfWork.Category.GetAll();
            return View(categoryVM);
        }

        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm = new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
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
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                var categories = _unitOfWork.Category.GetAll().ToList();

                // Sort categories by name using Bubble Sort algorithm
                SortingHelper.BubbleSortByName(categories);

                // Check if a category with the same name already exists
                if (!SortingHelper.BinarySearchByName(categories, vm.Category.Name))
                {
                    if (vm.Category.Id == 0)
                    {
                        _unitOfWork.Category.Add(vm.Category);
                    }
                    else
                    {
                        _unitOfWork.Category.Update(vm.Category);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Category Updated Successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Category with the same name already exists!";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            try
            {
                var category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                _unitOfWork.Category.Delete(category);
                _unitOfWork.Save();
                TempData["success"] = "Product Deleted Done!";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                // ... your existing code ...

                // Constraint violation (related records exist)
                TempData["error"] = "Cannot delete the category because it has related data.";
                return RedirectToAction("Index"); // Or redirect to an error view
            }
            catch (Exception ex)
            {
                // Handle other exceptions that might occur during the deletion process
                TempData["error"] = "An unexpected error occurred: " + ex.Message;
                return RedirectToAction("Index"); // Or redirect to an error view
            }
        }

    }
}
