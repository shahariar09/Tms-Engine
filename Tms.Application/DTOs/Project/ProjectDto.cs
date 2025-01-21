using System;

namespace Tms.Application.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        // Add the AssignedUsers field to the DTO
        public List<ProjectUserDto> ProjectUsers { get; set; } = null;
    }

    public class CreateProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class ProjectUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
