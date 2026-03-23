using System;
using TaskHub.Models; // Necesar pentru TodoTask, TaskCategory, TaskSettings
using TaskHub.Logic;  // Necesar pentru TaskManager
namespace TaskHub.App
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== TASKHUB DASHBOARD ===");
                Console.WriteLine("1. Adauga Task");
                Console.WriteLine("2. Afiseaza Toate");
                Console.WriteLine("3. Cauta Task");
                Console.WriteLine("4. Iesire");
                Console.Write("\nOptiune: ");

                switch (Console.ReadLine())
                {
                    case "1": AdaugaTask(taskManager); break;
                    case "2": AfiseazaTaskuri(taskManager); break;
                    case "3": CautaTask(taskManager); break;
                    case "4": running = false; break;
                }
            }
        }

        static void AdaugaTask(TaskManager tm)
        {
            Console.Clear();
            var t = new TodoTask();

            Console.Write("Titlu: ");
            t.Title = Console.ReadLine();

            Console.Write("Categorie (0-Personal, 1-Work, 2-Scoala): ");
            t.Category = (TaskCategory)int.Parse(Console.ReadLine());

            Console.Write("Prioritate (0-Low, 1-Med, 2-High, 3-Urgent): ");
            t.Priority = (TaskPriority)int.Parse(Console.ReadLine());

            Console.Write("Termen (peste cate zile): ");
            t.DueDate = DateTime.Now.AddDays(int.Parse(Console.ReadLine()));

            // Exemplu utilizare [Flags]: Setam automat niste optiuni
            t.Settings = TaskSettings.RemindMe | TaskSettings.HighPriority;

            tm.AddTask(t);
            Console.WriteLine("\nTask salvat! Opțiuni activate automat: " + t.Settings);
            Console.ReadKey();
        }

        static void AfiseazaTaskuri(TaskManager tm)
        {
            Console.Clear();
            var lista = tm.GetSortedTasks();
            foreach (var t in lista)
            {
                Console.WriteLine($"[{t.Category}] {t.Title} - {t.Priority}");
                Console.WriteLine($"   Creat: {t.CreatedAt:dd.MM} | Ramase: {t.DaysRemaining} zile");
                Console.WriteLine($"   Extra: {t.Settings}");
                Console.WriteLine("------------------------------------------");
            }
            Console.ReadKey();
        }

        static void CautaTask(TaskManager tm)
        {
            Console.Clear();
            Console.Write("Cauta titlu: ");
            string query = Console.ReadLine();
            var rezultate = tm.SearchTasks(query);

            foreach (var t in rezultate)
                Console.WriteLine($"- {t.Title} ({t.Category})");

            Console.ReadKey();
        }
    }
}