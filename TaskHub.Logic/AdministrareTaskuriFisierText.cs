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

        public AdministrareTaskuriFisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;
            using (Stream s = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
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
            if (!File.Exists(numeFisier)) return taskuri;

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linie))
                        taskuri.Add(new TodoTask(linie));
                }
            }
            return taskuri;
        }

        public List<TodoTask> SearchTasks(string keyword)
        {
            return GetTasks().Where(t => t.Title.ToLower().Contains(keyword.ToLower())).ToList();
        }

        public void DeleteTask(TodoTask taskDeSters)
        {
            var toate = GetTasks();
            toate.RemoveAll(t => t.Title == taskDeSters.Title && t.DueDate.Date == taskDeSters.DueDate.Date);

            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (var t in toate)
                    sw.WriteLine(t.ConversieLaSirPentruFisier());
            }
        }
    }
}