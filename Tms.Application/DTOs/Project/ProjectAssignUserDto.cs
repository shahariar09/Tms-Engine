using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Application.DTOs.Project
{
    public class ProjectAssignUserDto
    {
        public ICollection<int> UserIds { get; set; }
        public int ProjectId { get; set; }

       
    }

    
}
