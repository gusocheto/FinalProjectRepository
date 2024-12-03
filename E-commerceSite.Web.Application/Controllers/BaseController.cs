using Microsoft.AspNetCore.Mvc;

namespace E_commerceSite.Web.Application.Controllers
{
    public class BaseController : Controller
    {
        //protected readonly IManagerService managerService;

        public BaseController(/*imanagerservice managerservice*/)
        {
            //this.managerservice = managerservice;
        }

        protected bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }

        //protected async Task<bool> IsUserManagerAsync()
        //{
        //    string? userId = this.User.GetUserId();
        //    bool isManager = await this.managerService
        //        .IsUserManagerAsync(userId);

        //    return isManager;
        //}

        //protected async Task AppendUserCookieAsync()
        //{
        //    bool isManager = await this.IsUserManagerAsync();

        //    this.HttpContext.Response.Cookies.Append(IsManagerCookieName, isManager.ToString());
        //}
    }
}
