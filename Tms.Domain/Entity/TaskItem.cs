using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entity
{
    [Table("TMS_TASK_ITEM")]
    public class TaskItem
    {
        public int Id { get; set; }

        // Task Attributes
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; } // Low, Medium, High
        public string Status { get; set; } // To-Do, In Progress, Completed
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }  
        public Project Project { get; set; }
        // public ICollection<UserTask> AssignedUsers { get; set; } // Many-to-Many with Users
        public virtual ICollection<UserTask> AssignedUsers { get; set; } = new List<UserTask>();
    }
}