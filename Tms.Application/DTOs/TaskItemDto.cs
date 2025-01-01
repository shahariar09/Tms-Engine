using System;
using System.Collections.Generic;

namespace Tms.Application.DTOs
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<UserTaskDto> AssignedUsers { get; set; }
    }

    public class CreateTaskItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }
        public List<int> AssignedUserIds { get; set; }
    }

    public class UserTaskDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}