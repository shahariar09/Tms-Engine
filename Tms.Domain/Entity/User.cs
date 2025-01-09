using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tms.Domain.Entity;

[Table("TMS_USER")]
public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Email { get; set; }
    public Role Role { get; set; }
    public int? RoleId { get; set; }

    // public ICollection<ProjectUser> ProjectAssignUsers { get; set; } = new List<ProjectUser>();
    public ICollection<ProjectUser> ProjectAssignUsers { get; set; }

}
