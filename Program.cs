using Sem2_Dz2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;



internal class Program
{
    const int s = 5;

    static Shelf shelfA = new Shelf(s);
    static Shelf shelfB = new Shelf(s);

    static Journal<PlacedEvent> placedJournal = new Journal<PlacedEvent>();
    static Journal<TakenEvent> takenJournal = new Journal<TakenEvent>();
    static Journal<MovedEvent> movedJournal = new Journal<MovedEvent>();
    static Journal<FailedAttemptEvent> failedJournal = new Journal<FailedAttemptEvent>();

    public static int IntInput()
    {
        while (true)
        {
            string str = Console.ReadLine();
            if (int.TryParse(str, out int Int))
            {
                return Int;
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
            }
        }
    }

    static void Main()
    {

        

        LoadLogs();

        

        while (true)
        {

            Console.WriteLine("=== Склад ===");
            shelfA.Print("A");
            shelfB.Print("B");

            Console.WriteLine();
            Console.WriteLine("1 - Положить товар");
            Console.WriteLine("2 - Забрать товар");
            Console.WriteLine("3 - Перенести товар");
            Console.WriteLine("4 - Показать журналы");
            Console.WriteLine("5 - Выход");

            Console.Write("Ваш выбор: ");
            string userChoice = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(userChoice, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Place();
                        break;

                    case 2:
                        Take();
                        break;

                    case 3:
                        Move();
                        break;

                    case 4:
                        ShowLogs();
                        break;

                    case 5:
                        SaveLogs();
                        return;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод");
            }
            Console.WriteLine();
        }

    }

    static Shelf GetShelf(string name)
    {
        if (name == "A")
        {
            return shelfA;
        }
        else
        {
            return shelfB;
        }
    }

    static string ReadShelf()
    {
        while (true)
        {
            Console.Write("Полка (A или B): ");
            string s = Console.ReadLine();

            if (s == "A" || s == "B")
                return s;

            Console.WriteLine("Ошибка. Введите A или B");
        }
    }
    static int ReadSlot()
    {
        while (true)
        {
            Console.Write("Слот 1-5: ");

            if (int.TryParse(Console.ReadLine(), out int x))
            {
                if (x >= 1 && x <= 5)
                    return x - 1;
            }

            Console.WriteLine("Ошибка ввода");
        }
    }

    static int ReadInt(string msg, int min, int max)
    {
        while (true)
        {
            Console.Write(msg);

            if (int.TryParse(Console.ReadLine(), out int x)&& x >= min && x <= max)
                return x;

            Console.WriteLine("Ошибка ввода.");
        }
    }

    static void Place()
    {
        string shelfName = ReadShelf();
        int slot = ReadSlot();

        var shelf = GetShelf(shelfName);

        if (shelf.Get(slot) != null)
        {
            Console.WriteLine("Слот занят.");
            failedJournal.Add(new FailedAttemptEvent("Положить", shelfName, slot + 1, "слот занят"));
            return;
        }

        Console.Write("Название товара: ");
        string item = Console.ReadLine();

        shelf.Put(slot, item);

        placedJournal.Add(new PlacedEvent(shelfName, slot + 1, item));

        Console.WriteLine("OK");
    }

    static void Take()
    {
        string S = ReadShelf(); 

        int slot = ReadSlot(); ;
        Shelf shelf = GetShelf(S);

        if (shelf.Get(slot) == null)
        {
            Console.WriteLine("Слот пуст.");
            failedJournal.Add(new FailedAttemptEvent("Забрать", S, slot + 1, "слот пуст"));
            return;
        }

        string item = shelf.Take(slot);

        takenJournal.Add(new TakenEvent(S, slot + 1, item));

        Console.WriteLine($"Забрано: {item}");
    }

    static void Move()
    {
        Console.WriteLine("Источник:");
        string s1 = ReadShelf();
        int slot1 = ReadInt("Слот: ", 1, s) - 1;

        Console.WriteLine("Назначение:");
        string s2 = ReadShelf();
        int slot2 = ReadInt("Слот: ", 1, s) - 1;

        var shelf1 = GetShelf(s1);
        var shelf2 = GetShelf(s2);

        if (shelf1.Get(slot1) == null)
        {
            Console.WriteLine("Источник пуст.");
            failedJournal.Add(new FailedAttemptEvent("Перенести", s1, slot1 + 1, "слот пуст"));
            return;
        }

        if (shelf2.Get(slot2) != null)
        {
            Console.WriteLine("Назначение занято.");
            failedJournal.Add(new FailedAttemptEvent("Перенести", s2, slot2 + 1, "слот занят"));
            return;
        }

        string item = shelf1.Take(slot1);
        shelf2.Put(slot2, item);

        movedJournal.Add(new MovedEvent(s1, slot1 + 1, s2, slot2 + 1, item));

        Console.WriteLine("OK");
    }

    static void ShowLogs()
    {
        Console.WriteLine("\n--- Размещения ---");
        foreach (var e in placedJournal.GetAll())
            Console.WriteLine(e.ToScreenLine());

        Console.WriteLine("\n--- Изъятия ---");
        foreach (var e in takenJournal.GetAll())
            Console.WriteLine(e.ToScreenLine());

        Console.WriteLine("\n--- Переносы ---");
        foreach (var e in movedJournal.GetAll())
            Console.WriteLine(e.ToScreenLine());

        Console.WriteLine("\n--- Ошибки ---");
        foreach (var e in failedJournal.GetAll())
            Console.WriteLine(e.ToScreenLine());

        Console.WriteLine();
    }

    static void LoadLogs()
    {
        if (File.Exists("placed.log"))
        {
            foreach (var line in File.ReadAllLines("placed.log"))
            {
                var e = PlacedEvent.FromLogLine(line);
                placedJournal.Add(e);

                GetShelf(e.Shelf).Put(e.Slot - 1, e.Item);
            }
        }

        if (File.Exists("taken.log"))
        {
            foreach (var line in File.ReadAllLines("taken.log"))
                takenJournal.Add(TakenEvent.FromLogLine(line));
        }

        if (File.Exists("moved.log"))
        {
            foreach (var line in File.ReadAllLines("moved.log"))
                movedJournal.Add(MovedEvent.FromLogLine(line));
        }

        if (File.Exists("failed.log"))
        {
            foreach (var line in File.ReadAllLines("failed.log"))
                failedJournal.Add(FailedAttemptEvent.FromLogLine(line));
        }
    }

    static void SaveLogs()
    {
        placedJournal.SaveToFile("placed.log");
        takenJournal.SaveToFile("taken.log");
        movedJournal.SaveToFile("moved.log");
        failedJournal.SaveToFile("failed.log");

        Console.WriteLine("Журналы сохранены.");
    }
}
