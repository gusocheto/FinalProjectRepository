using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Website.Common.ApplicationConstants;

namespace E_commerceSite.Web.Application.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class UserManagmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
