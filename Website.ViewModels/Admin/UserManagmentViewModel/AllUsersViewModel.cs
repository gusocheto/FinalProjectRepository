using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Website.Data.Models;
using Website.Services.Mapping;

namespace Website.ViewModels.Admin.UserManagmentViewModel
{
    public class AllUsersViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        public string? Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<ApplicationUser, AllUsersViewModel>()
                .ForMember(d => d.Roles, cfg => cfg.Ignore());
        }
    }
}
