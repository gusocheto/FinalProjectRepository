using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models.Enums;
using Website.Data.Models;
using Website.Services.Data.Interfaces;

namespace Website.Services.Data
{
    public class BaseService : IBaseService
    {
        public bool IsGuidValid(string? id, ref Guid parsedGuid)
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

        public List<Category> GetCategories()
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

        public List<ProductType> GetProductTypes()
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

        public List<Status> GetStatusTypes()
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
    }
}
