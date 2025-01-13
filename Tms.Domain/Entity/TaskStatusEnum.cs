namespace Tms.Domain.Entity
{
    public static class TaskStatusEnum
    {
        public const string Open = "Open";
        public const string Pending = "Pending";
        public const string InProgress = "InProgress";
        public const string Done = "Done";
        public const string Closed = "Closed";

        public static readonly string[] ValidStatuses = new[]
        {
            Open, Pending, InProgress, Done, Closed
        };
        public static bool IsValidStatus(string status)
        {
            return ValidStatuses.Any(s => s.Equals(status, StringComparison.OrdinalIgnoreCase));
        }
    }
}