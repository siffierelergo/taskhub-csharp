namespace TaskHub.Models
{
    public enum TaskPriority { Low, Medium, High, Urgent }
    public enum TaskCategory { Personal, Work, Scoala, Diverse }

    [Flags]
    public enum TaskSettings { None = 0, RemindMe = 1, HighPriority = 2, Recurring = 4 }

    public class TodoTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskCategory Category { get; set; }
        public TaskSettings Settings { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int DaysRemaining => (DueDate.Date - DateTime.Now.Date).Days;
    }
}