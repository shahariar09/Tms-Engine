using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tms.Domain.Entity
{
    [Table("TMS_PROJECT")]
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public ICollection<ProjectUser> ProjectAssignUsers { get; set; }
    }
}
