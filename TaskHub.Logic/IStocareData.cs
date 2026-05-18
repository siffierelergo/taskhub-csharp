using System.Collections.Generic;
using TaskHub.Models;

namespace TaskHub.Logic
{
    public interface IStocareData
    {
        void AddTask(TodoTask task);
        List<TodoTask> GetTasks();
        List<TodoTask> SearchTasks(string keyword);
        void DeleteTask(TodoTask task);
    }
}