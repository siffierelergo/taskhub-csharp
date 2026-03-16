using System;
using System.Collections.Generic;

namespace TaskHub
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
                Console.WriteLine("=== TaskHub Dashboard ===");
                Console.WriteLine("1. Adaugă un task nou");
                Console.WriteLine("2. Arată toate task-urile");
                Console.WriteLine("3. Caută un task");
                Console.WriteLine("4. Iesire");
                Console.Write("\nAlege o opțiune: ");

                string optiune = Console.ReadLine();

                switch (optiune)
                {
                    case "1":
                        AdaugaTask(taskManager);
                        break;
                    case "2":
                        AfiseazaToateTaskurile(taskManager);
                        break;
                    case "3":
                        CautaTask(taskManager);
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Opțiune invalidă! Apasă orice tastă...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Metodă pentru adăugarea unui task
        static void AdaugaTask(TaskManager tm)
        {
            Console.Clear();
            Console.Write("Titlu task: ");
            string titlu = Console.ReadLine();

            Console.Write("Prioritate (0-Low, 1-Med, 2-High, 3-Urgent): ");
            if (int.TryParse(Console.ReadLine(), out int p))
            {
                tm.AddTask(new TodoTask { Title = titlu, Priority = (TaskPriority)p });
                Console.WriteLine("Task adăugat! Apasă orice tastă...");
            }
            else
            {
                Console.WriteLine("Prioritate invalidă.");
            }
            Console.ReadKey();
        }

        // Metodă pentru afișarea tuturor task-urilor
        static void AfiseazaToateTaskurile(TaskManager tm)
        {
            Console.Clear();
            Console.WriteLine("--- Toate Task-urile ---");
            var taskuri = tm.GetSortedTasks();

            if (taskuri.Count == 0) Console.WriteLine("Lista este goală.");

            foreach (var t in taskuri)
            {
                Console.WriteLine($"[{t.Priority}] {t.Title}");
            }

            Console.WriteLine("\nApasă orice tastă pentru a reveni la meniu...");
            Console.ReadKey();
        }

        // Metodă pentru căutare
        static void CautaTask(TaskManager tm)
        {
            Console.Clear();
            Console.Write("Introdu cuvântul de căutare: ");
            string keyword = Console.ReadLine();
            var rezultate = tm.SearchTasks(keyword);

            Console.WriteLine($"\nRezultate pentru '{keyword}':");
            foreach (var t in rezultate)
            {
                Console.WriteLine($"- {t.Title} ({t.Priority})");
            }

            Console.WriteLine("\nApasă orice tastă...");
            Console.ReadKey();
        }
    }
}