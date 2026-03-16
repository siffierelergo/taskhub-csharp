using System;
using System.Collections.Generic;

namespace TaskHub
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Inițializăm managerii
            TaskManager taskManager = new TaskManager();
            ThemeSettings themeManager = new ThemeSettings();

            Console.WriteLine("--- Testare TaskHub ---");

            // 2. Testăm Managerul de Teme
            themeManager.ApplyTheme(VisualTheme.Dark);

            // 3. Adăugăm câteva task-uri de probă
            taskManager.AddTask(new TodoTask
            {
                Title = "Proiect UI",
                Priority = TaskPriority.Urgent,
                DueDate = DateTime.Now.AddDays(1)
            });

            taskManager.AddTask(new TodoTask
            {
                Title = "Cumpărături",
                Priority = TaskPriority.Low,
                DueDate = DateTime.Now.AddDays(3)
            });

            taskManager.AddTask(new TodoTask
            {
                Title = "Curs C#",
                Priority = TaskPriority.High,
                IsCompleted = true, // Marcăm unul ca terminat pentru progres
                DueDate = DateTime.Now
            });

            // 4. Afișăm task-urile sortate după urgență
            Console.WriteLine("\nListă Task-uri (Sortate):");
            var sortedTasks = taskManager.GetSortedTasks();

            foreach (var task in sortedTasks)
            {
                string status = task.IsCompleted ? "[V]" : "[ ]";
                Console.WriteLine($"{status} {task.Priority} | {task.Title} (Deadline: {task.DueDate.ToShortDateString()})");
                Console.WriteLine($"   ID unic: {task.Id}");
            }

            // 5. Afișăm progresul zilei
            double progress = taskManager.GetDailyProgress();
            Console.WriteLine($"\n--- Progres Zilnic: {progress}% ---");

            Console.WriteLine("\nApăsați orice tastă pentru a închide...");
            Console.ReadKey();
        }
    }
}