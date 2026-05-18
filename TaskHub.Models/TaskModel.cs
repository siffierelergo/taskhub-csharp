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

        // PROPRIETĂȚILE LIPSA:
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; } = false;

        public TodoTask() { }

        public TodoTask(string linieFisier)
        {
            var date = linieFisier.Split(';');
            if (date.Length >= 7) // Am crescut la 7 câmpuri
            {
                Title = date[0];
                Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), date[1]);
                DueDate = DateTime.Parse(date[2]);
                Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), date[3]);
                IsUrgent = bool.Parse(date[4]);
                CreatedAt = DateTime.Parse(date[5]); // Citim data creării
                IsCompleted = bool.Parse(date[6]);   // Citim starea finalizării
            }
        }

        public string ConversieLaSirPentruFisier()
        {
            // Salvăm toate cele 7 câmpuri
            return $"{Title};{Category};{DueDate};{Priority};{IsUrgent};{CreatedAt};{IsCompleted}";
        }
    }
}