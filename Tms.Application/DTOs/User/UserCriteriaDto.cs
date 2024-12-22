using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Application.Common.Pagination;

namespace Tms.Application.DTOs.User
{
    public class UserCriteriaDto
    {
        public UserCriteriaDto()
        {
            PageParams = new PageParams();
        }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? BrandId { get; set; }

        public PageParams PageParams
        {
            get; set;
        }
    }
}
