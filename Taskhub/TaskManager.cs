using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TaskManager
{
	private List<TodoTask> _tasks = new List<TodoTask>();

	public void AddTask(TodoTask task) => _tasks.Add(task);

	// Sortare automată: Urgență (Priority) apoi Dată
	public List<TodoTask> GetSortedTasks()
	{
		return _tasks
			.OrderByDescending(t => t.Priority)
			.ThenBy(t => t.DueDate)
			.ToList();
	}

	// Calcul progres zilnic (pentru Dashboard)
	public double GetDailyProgress()
	{
		var todayTasks = _tasks.Where(t => t.CreatedAt.Date == DateTime.Today).ToList();
		if (!todayTasks.Any()) return 0;

		int completed = todayTasks.Count(t => t.IsCompleted);
		return (double)completed / todayTasks.Count * 100;
	}
	// Căutare după un cuvânt cheie în titlu
	public List<TodoTask> SearchTasks(string keyword)
	{
		return _tasks
			.Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
			.ToList();
	}
}