using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaskHub.Models;

namespace TaskHub.Logic
{
    public class AdministrareTaskuriFisierText : IStocareData
    {
        private string numeFisier;
        public AdministrareTaskuriFisierText(string numeFisier) { this.numeFisier = numeFisier; if (!File.Exists(numeFisier)) File.Create(numeFisier).Close(); }

        public void AddTask(TodoTask task)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true)) sw.WriteLine(task.ConversieLaSirPentruFisier());
        }

        public List<TodoTask> GetTasks()
        {
            List<TodoTask> taskuri = new List<TodoTask>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                    if (!string.IsNullOrWhiteSpace(linie)) taskuri.Add(new TodoTask(linie));
            }
            return taskuri;
        }

        public List<TodoTask> SearchTasks(string kw) => GetTasks().Where(t => t.Title.ToLower().Contains(kw.ToLower())).ToList();

        public void DeleteTask(TodoTask taskDeSters)
        {
            var toate = GetTasks();
            // Ștergem task-ul care are EXACT aceeași dată de creare
            toate.RemoveAll(t => t.CreatedAt.ToString() == taskDeSters.CreatedAt.ToString());
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
                foreach (var t in toate) sw.WriteLine(t.ConversieLaSirPentruFisier());
        }
    }
}