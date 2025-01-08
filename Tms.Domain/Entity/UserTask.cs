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
        //[Key]
        //public int Id { get; set; }
        //[ForeignKey(nameof(User))]
        //public int UserId { get; set; }
        //public User User { get; set; }

        //[ForeignKey(nameof(TaskItem))]
        //public int TaskItemId { get; set; }
        //public virtual TaskItem TaskItem { get; set; }
        
        
    [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TaskItemId { get; set; }
        public virtual TaskItem TaskItem { get; set; }
    }

}

