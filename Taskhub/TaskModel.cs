[Flags]
public enum TaskOptions
{
    None = 0,
    RemindMe = 1,
    HighPriority = 2,
    EmailNotification = 4,
    Recurring = 8
}
public enum TaskPriority
{
    Low,
    Medium,
    High,
    Urgent
}

public class TodoTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public TaskOptions Options { get; set; }

    // Proprietate calculată pentru a ajuta UI-ul (ex: task-uri expirate)
    public bool IsOverdue => !IsCompleted && DueDate < DateTime.Now;
}