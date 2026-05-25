using System;

namespace TaskHub.Models
{
    public enum TaskCategory { Personal, Work, Scoala }
    public enum TaskPriority { Low, Medium, High }

    public class TodoTask
    {
        public string Title { get; set; }
        public TaskCategory Category { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public bool IsUrgent { get; set; }
        public bool IsCompleted { get; set; } // Proprietatea care lipsea
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public TodoTask() { }

        public TodoTask(string linieFisier)
        {
            var date = linieFisier.Split(';');
            if (date.Length >= 7)
            {
                Title = date[0];
                Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), date[1]);
                DueDate = DateTime.Parse(date[2]);
                Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), date[3]);
                IsUrgent = bool.Parse(date[4]);
                IsCompleted = bool.Parse(date[5]);
                CreatedAt = DateTime.Parse(date[6]);
            }
        }

        public string ConversieLaSirPentruFisier() =>
            $"{Title};{Category};{DueDate};{Priority};{IsUrgent};{IsCompleted};{CreatedAt}";
    }
}