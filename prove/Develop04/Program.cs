using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    private static Dictionary<string, int> activityLog = new Dictionary<string, int>();

    static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        Console.WriteLine("Welcome to the Mindfulness Program!");
        Console.WriteLine("Menu Options:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Exit");
        Console.WriteLine("Select an item from the menu:");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
        {
            Console.WriteLine("Invalid input. Please enter a number from 1 to 4.");
        }

        switch (choice)
        {
            case 1:
                BreathingActivity breathingActivity = new BreathingActivity();
                breathingActivity.Start();
                LogActivity(breathingActivity.Name);
                break;
            case 2:
                ReflectionActivity reflectionActivity = new ReflectionActivity();
                reflectionActivity.Start();
                LogActivity(reflectionActivity.Name);
                break;
            case 3:
                ListingActivity listingActivity = new ListingActivity();
                listingActivity.Start();
                LogActivity(listingActivity.Name);
                break;
            case 4:
                Console.WriteLine("Very well! Enjoy the rest of the day!");
                Console.WriteLine();
                PrintActivityLog();
                Environment.Exit(0);
                break;
        }

        Console.WriteLine();
        Menu();
    }

    static void LogActivity(string activityName)
    {
        if (activityLog.ContainsKey(activityName))
        {
            activityLog[activityName]++;
        }
        else
        {
            activityLog[activityName] = 1;
        }
    }

    static void PrintActivityLog()
    {
        Console.WriteLine("Activity Log:");
        foreach (var activity in activityLog)
        {
            Console.WriteLine($"{activity.Key}: {activity.Value} times");
        }
        Console.WriteLine();
    }
}

class MindfulnessActivity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }

    protected virtual void DisplayStartingMessage()
    {
        Console.WriteLine($"--- {Name} ---");
        Console.WriteLine(Description);
        Console.Write("Enter the duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        AnimateCountdown(3);
    }

    protected virtual void DisplayEndingMessage()
    {
        Console.WriteLine();
        Console.WriteLine("Good job! You have completed the activity.");
        Console.WriteLine($"Activity: {Name}");
        Console.WriteLine($"Duration: {Duration} seconds");
        Thread.Sleep(3000);
    }

    protected void AnimateCountdown(int duration)
    {
        const int countdownDelay = 1000; // Delay between countdown numbers in milliseconds

        for (int seconds = duration; seconds > 0; seconds--)
        {
            Console.Write($"Starting in {seconds}...");
            Thread.Sleep(countdownDelay);
            Console.SetCursorPosition(Console.CursorLeft - $"Starting in {seconds}...".Length, Console.CursorTop);
        }

        Console.WriteLine();
    }

    protected void AnimateText(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(50);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    protected override void DisplayStartingMessage()
    {
        Name = "Breathing Activity";
        Description = "This activity will help you relax by guiding you through deep breathing exercises. Clear your mind and focus on your breath.";

        Console.WriteLine($"--- {Name} ---");
        AnimateText(Description);
        Console.Write("Enter the duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        AnimateCountdown(3);
    }

    public void Start()
    {
        DisplayStartingMessage();

        AnimateText("Start breathing in...");
        Thread.Sleep(1000);

        for (int i = 1; i <= Duration; i++)
        {
            AnimateText(i % 2 == 0 ? "Breathe out..." : "Breathe in...");
            Thread.Sleep(1000);
        }

        DisplayEndingMessage();
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private List<string> prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "What did you learn from this experience?",
        "How did this experience make you feel?",
        "What positive impact did this experience have on others?"
    };

    protected override void DisplayStartingMessage()
    {
        Name = "Reflection Activity";
        Description = "This activity will encourage you to reflect on positive experiences and gain insights from them.";

        Console.WriteLine($"--- {Name} ---");
        AnimateText(Description);
        Console.Write("Enter the duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        AnimateCountdown(3);
    }

    public void Start()
    {
        DisplayStartingMessage();

        AnimateText("Here is a prompt to reflect upon:");
        AnimateText(prompts[new Random().Next(0, prompts.Count)]);
        Thread.Sleep(3000);

        AnimateText("Now, answer the following question:");
        AnimateText(questions[new Random().Next(0, questions.Count)]);
        Thread.Sleep(Duration * 1000);

        DisplayEndingMessage();
    }
}

class ListingActivity : MindfulnessActivity
{
    protected override void DisplayStartingMessage()
    {
        Name = "Listing Activity";
        Description = "This activity will help you practice mindfulness by listing objects or items in your surroundings.";

        Console.WriteLine($"--- {Name} ---");
        AnimateText(Description);
        Console.Write("Enter the duration in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        AnimateCountdown(3);
    }

    public void Start()
    {
        DisplayStartingMessage();

        AnimateText("Start listing items around you...");
        AnimateText("Press Enter after each item. Enter 'done' to finish.");

        List<string> items = new List<string>();
        string item;
        do
        {
            item = Console.ReadLine();
            if (item != "done")
                items.Add(item);
        } while (item != "done");

        AnimateText($"You listed {items.Count} items:");
        foreach (var listItem in items)
        {
            AnimateText(listItem);
            Thread.Sleep(500);
        }

        DisplayEndingMessage();
    }
}

// // Exceed the requirements in comments in the Program.cs file:
// I made different animation "AnimateText" and "AnimateCountdown".
// I added a log of how many times activities were performed. 