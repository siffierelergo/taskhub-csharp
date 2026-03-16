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

    // Proprietate calculată pentru a ajuta UI-ul (ex: task-uri expirate)
    public bool IsOverdue => !IsCompleted && DueDate < DateTime.Now;
}