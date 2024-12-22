using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entity
{
    [Table("TMS_ROLE")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
