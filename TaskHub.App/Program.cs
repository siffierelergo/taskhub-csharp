using System;
using TaskHub.Models;
using TaskHub.Logic;

namespace TaskHub.App;

class Program
{
    static void Main(string[] args)
    {
        IStocareData stocare = new AdministrareTaskuriFisierText("taskuri.txt");
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== TASKHUB (FILE STORAGE) ===");
            Console.WriteLine("1. Adauga Task");
            Console.WriteLine("2. Afiseaza Toate");
            Console.WriteLine("3. Cauta Task");
            Console.WriteLine("4. Iesire");
            Console.Write("\nOptiune: ");

            switch (Console.ReadLine())
            {
                case "1": AdaugaTask(stocare); break;
                case "2": AfiseazaTaskuri(stocare); break;
                case "3": CautaTask(stocare); break;
                case "4": running = false; break;
            }
        }
    }

    static void AdaugaTask(IStocareData stocare)
    {
        Console.Clear();
        var t = new TodoTask();
        Console.Write("Titlu: "); t.Title = Console.ReadLine();
        Console.Write("Categorie (0-Personal, 1-Work, 2-Scoala): ");
        t.Category = (TaskCategory)int.Parse(Console.ReadLine());
        Console.Write("Prioritate (0-Low, 1-Urgent): ");
        t.Priority = (TaskPriority)int.Parse(Console.ReadLine());
        Console.Write("Termen (zile): ");
        t.DueDate = DateTime.Now.AddDays(int.Parse(Console.ReadLine()));

        stocare.AddTask(t);
        Console.WriteLine("\nTask salvat!");
        Console.ReadKey();
    }

    static void AfiseazaTaskuri(IStocareData stocare)
    {
        Console.Clear();
        var lista = stocare.GetTasks();
        foreach (var t in lista)
        {
            Console.WriteLine($"[{t.Category}] {t.Title} - {t.Priority} (Deadline: {t.DueDate:dd.MM.yyyy})");
        }
        Console.ReadKey();
    }

    static void CautaTask(IStocareData stocare)
    {
        Console.Clear();
        Console.Write("Cauta titlu: ");
        string keyword = Console.ReadLine();
        var rezultate = stocare.SearchTasks(keyword);

        foreach (var t in rezultate)
        {
            Console.WriteLine($"- {t.Title} ({t.Category})");
        }
        Console.ReadKey();
    }
}