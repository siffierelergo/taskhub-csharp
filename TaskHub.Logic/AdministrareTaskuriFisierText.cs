using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using TaskHub.Models;

namespace TaskHub.Logic;

public class AdministrareTaskuriFisierText : IStocareData
{
    private string numeFisier;

    public AdministrareTaskuriFisierText(string numeFisier)
    {
        this.numeFisier = numeFisier;
        File.Open(numeFisier, FileMode.OpenOrCreate).Close();
    }

    public void AddTask(TodoTask task)
    {
        using (StreamWriter sw = new StreamWriter(numeFisier, true))
        {
            sw.WriteLine(task.ConversieLaSirPentruFisier());
        }
    }

    public List<TodoTask> GetTasks()
    {
        List<TodoTask> taskuri = new List<TodoTask>();
        using (StreamReader sr = new StreamReader(numeFisier))
        {
            string linie;
            while ((linie = sr.ReadLine()) != null)
            {
                taskuri.Add(new TodoTask(linie));
            }
        }
        return taskuri;
    }

    public List<TodoTask> SearchTasks(string keyword)
    {
        var toate = GetTasks();
        return toate
            .Where(t => t.Title != null && t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}