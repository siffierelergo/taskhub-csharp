namespace TaskHub.Logic;

using System;
using System.Collections.Generic;
using System.Linq;
using TaskHub.Models; // <--- ACEASTA LINIE LIPSEA

public class TaskManager
{
    private List<TodoTask> _tasks = new List<TodoTask>();

    public void AddTask(TodoTask task) => _tasks.Add(task);

    public List<TodoTask> GetSortedTasks()
    {
        return _tasks
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToList();
    }

    public double GetDailyProgress()
    {
        var todayTasks = _tasks.Where(t => t.CreatedAt.Date == DateTime.Today).ToList();
        if (!todayTasks.Any()) return 0;

        int completed = todayTasks.Count(t => t.IsCompleted);
        return (double)completed / todayTasks.Count * 100;
    }

    public List<TodoTask> SearchTasks(string keyword)
    {
        // Verificăm dacă keyword nu este null pentru a evita erori
        if (string.IsNullOrEmpty(keyword)) return new List<TodoTask>();

        return _tasks
            .Where(t => t.Title != null && t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}