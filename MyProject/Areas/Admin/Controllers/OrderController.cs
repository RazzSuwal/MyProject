using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.CommonHelper;
using MyProject.DataAccessLayer.Infrastructure.IRepository;
using MyProject.Models;
using MyProject.MyCommonHelper;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #region ApiiCALL
        public IActionResult AllOrders(string status)
        {
            IEnumerable<OrderHeader> orderHeader;
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                orderHeader = _unitOfWork.OrderHeader.GetAlls(includeProperties: new Expression<Func<OrderHeader, object>>[]
                    {
                        t => t.ApplicationUser,
                        u => u.Course
                    });
         
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeader = _unitOfWork.OrderHeader.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Course");

            }
            switch (status)
            {
                case "pending":
                    orderHeader = orderHeader.Where(x => x.PaymentStatus == PaymentStatus.StatusPending);
                    break;
                case "approved":
                    orderHeader = orderHeader.Where(x => x.PaymentStatus == PaymentStatus.StatusApproved);
                    break;

                default:
                    break;
            }
        return Json(new { data = orderHeader }); //data transfer through JASON into web page
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
    }
}
