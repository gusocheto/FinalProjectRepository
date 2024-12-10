
using Website.Data.Models;

namespace Website.Services.Data.Interfaces
{
    public interface IBaseService
    {
        bool IsGuidValid(string? id, ref Guid parsedGuid);
        List<Category> GetCategories();
        List<ProductType> GetProductTypes();
        List<Status> GetStatusTypes();
    }
}
