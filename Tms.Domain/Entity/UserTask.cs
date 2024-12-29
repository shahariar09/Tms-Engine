using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entity
{
    [Table("TMS_USER_TASK")]
    public class UserTask
    {
        
        public int UserId { get; set; }
        public User User { get; set; }
        [Key]
        public int TaskId { get; set; }
        public TaskItem Task { get; set; }
    }
}
