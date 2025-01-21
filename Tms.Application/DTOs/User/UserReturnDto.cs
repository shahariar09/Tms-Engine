using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Application.DTOs.User
{
    public class UserReturnDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }

        //public ICollection<ProjectAssignUser> ProjectAssignUsers { get; set; }
    }
}
