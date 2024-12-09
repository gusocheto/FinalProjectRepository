using Microsoft.AspNetCore.Mvc;
using Website.Data.Models.Enums;
using Website.Data.Models;
using System.Security.Claims;

namespace E_commerceSite.Web.Application.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            
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

        protected List<Category> GetCategories()
        {
            return Enum.GetValues(typeof(CategoryEnumaration))
                       .Cast<CategoryEnumaration>()
                       .Select(e => new Category
                       {
                           CategoryId = (int)e,
                           CategoryType = e
                       })
                       .ToList();
        }

        protected List<ProductType> GetProductTypes()
        {
            return Enum.GetValues(typeof(ProductCategorizationEnumaration))
                       .Cast<ProductCategorizationEnumaration>()
                       .Select(e => new ProductType
                       {
                           ProductTypeId = (int)e,
                           ProductTypeName = e
                       })
                       .ToList();
        }

        protected List<Status> GetStatusTypes()
        {
            return Enum.GetValues(typeof(StatusEnumaration))
                       .Cast<StatusEnumaration>()
                       .Select(e => new Status
                       {
                           StatusId = (int)e,
                           StatusType = e
                       })
                       .ToList();
        }

        protected string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }


    }
}
