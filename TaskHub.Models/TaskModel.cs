namespace TaskHub.Models;

public enum TaskPriority { Low, Medium, High, Urgent }
public enum TaskCategory { Personal, Work, Scoala, Diverse }

[Flags]
public enum TaskSettings { None = 0, RemindMe = 1, HighPriority = 2, Recurring = 4 }

public class TodoTask
{
    private const char SEPARATOR = ';';

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskCategory Category { get; set; }
    public TaskSettings Settings { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int DaysRemaining => (DueDate.Date - DateTime.Now.Date).Days;

    public TodoTask() { }

    public TodoTask(string linieFisier)
    {
        var date = linieFisier.Split(SEPARATOR);
        Id = Guid.Parse(date[0]);
        Title = date[1];
        Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), date[2]);
        Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), date[3]);
        Settings = (TaskSettings)Enum.Parse(typeof(TaskSettings), date[4]);
        CreatedAt = DateTime.Parse(date[5]);
        DueDate = DateTime.Parse(date[6]);
        IsCompleted = bool.Parse(date[7]);
    }

    public string ConversieLaSirPentruFisier()
    {
        return string.Join(SEPARATOR.ToString(),
            Id, Title, Priority, Category, Settings, CreatedAt, DueDate, IsCompleted);
    }
}