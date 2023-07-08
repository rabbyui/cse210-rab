
using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
abstract class Goal
{
    public string Name { get; set; }
    public int Value { get; set; }
    public bool IsCompleted { get; set; }

    public abstract void DisplayStatus();

    public virtual void Complete()
    {
        IsCompleted = true;
    }

    public virtual void Reset()
    {
        IsCompleted = false;
    }

    public virtual string GetStringRepresentation()
    {
        return $"{Name}:{Value}:{IsCompleted}";
    }
}

// Simple goal
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"Goal: {Name}");
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"Completed: {(IsCompleted ? "Yes" : "No")}");
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{base.GetStringRepresentation()}";
    }
}

// Eternal goal
class EternalGoal : Goal
{
    public EternalGoal(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"Goal: {Name}");
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"Completed: {(IsCompleted ? "Yes" : "No")}");
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{base.GetStringRepresentation()}";
    }
}

// Checklist goal
class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    public int CompletionCount { get; set; }

    public ChecklistGoal(string name, int value, int targetCount)
    {
        Name = name;
        Value = value;
        TargetCount = targetCount;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"Goal: {Name}");
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"Completed: {(IsCompleted ? "Yes" : "No")}");
        Console.WriteLine($"Completion Count: {CompletionCount}/{TargetCount}");
    }

    public override void Complete()
    {
        CompletionCount++;
        if (CompletionCount >= TargetCount)
        {
            IsCompleted = true;
            Value += 500; // Bonus points for completing the checklist goal
        }
    }

    public override void Reset()
    {
        CompletionCount = 0;
        IsCompleted = false;
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{base.GetStringRepresentation()}:{TargetCount}:{CompletionCount}";
    }
}

// Negative goal
class NegativeGoal : Goal
{
    public NegativeGoal(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"Goal: {Name}");
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"Completed: {(IsCompleted ? "Yes" : "No")}");
    }

    public override void Complete()
    {
        IsCompleted = true;
        Value = Math.Min(Value, 0);
        // Ensure the value is negative or zero
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{base.GetStringRepresentation()}";
    }
}

// Main program
class Program
{
    static List<Goal> goals = new List<Goal>();

    static void Main(string[] args)
    {
        LoadGoals();

        while (true)
        {
            Console.WriteLine("Eternal Quest Program");
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Mark a goal as complete");
            Console.WriteLine("3. Display goals");
            Console.WriteLine("4. Save goals");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    MarkGoalAsComplete();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    SaveGoals();
                    break;
                case "5":
                    SaveGoals();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("Create a New Goal");
        Console.WriteLine("-----------------");

        Console.Write("Enter the goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter the goal value: ");
        int value = int.Parse(Console.ReadLine());

        Console.WriteLine("Select the goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.WriteLine("4. Negative Goal");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                goals.Add(new SimpleGoal(name, value));
                break;
            case "2":
                goals.Add(new EternalGoal(name, value));
                break;
            case "3":
                Console.Write("Enter the target count for the checklist goal: ");
                int targetCount = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, value, targetCount));
                break;
            case "4":
                goals.Add(new NegativeGoal(name, value));
                break;
            default:
                Console.WriteLine("Invalid choice. Goal creation failed.");
                return;
        }

        Console.WriteLine("Goal created successfully!");
    }

    static void MarkGoalAsComplete()
    {
        Console.WriteLine("Mark Goal as Complete");
        Console.WriteLine("---------------------");

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
            return;
        }

        Console.WriteLine("Select a goal to mark as complete:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }

        Console.Write("Enter the goal number: ");
        int goalNumber = int.Parse(Console.ReadLine());

        if (goalNumber < 1 || goalNumber > goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal goal = goals[goalNumber - 1];
        if (goal.IsCompleted)
        {
            Console.WriteLine("Goal is already marked as complete.");
            return;
        }

        goal.Complete();
        Console.WriteLine("Goal marked as complete!");
    }

    static void DisplayGoals()
    {
        Console.WriteLine("Goals");
        Console.WriteLine("-----");

        if (goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"Goal {i + 1}:");
            goals[i].DisplayStatus();
            Console.WriteLine();
        }
    }

    static void SaveGoals()
    {
        Console.WriteLine("Saving goals to file...");

        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.txt"))
        {
            Console.WriteLine("Loading goals from file...");

            using (StreamReader reader = new StreamReader("goals.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    string goalType = parts[0];
                    string goalData = parts[1];
                    int value = int.Parse(parts[2]);
                    bool isCompleted = bool.Parse(parts[3]);

                    Goal goal;

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            goal = new SimpleGoal(goalData, value);
                            break;
                        case "EternalGoal":
                            goal = new EternalGoal(goalData, value);
                            break;
                        case "ChecklistGoal":
                            int targetCount = int.Parse(parts[4]);
                            int completionCount = int.Parse(parts[5]);
                            goal = new ChecklistGoal(goalData, value, targetCount);
                            ((ChecklistGoal)goal).CompletionCount = completionCount;
                            break;
                        case "NegativeGoal":
                            goal = new NegativeGoal(goalData, value);
                            break;
                        default:
                            Console.WriteLine($"Invalid goal type: {goalType}. Skipping goal.");
                            continue;
                    }

                    goal.IsCompleted = isCompleted;
                    goals.Add(goal);
                }
            }

            Console.WriteLine("Goals loaded successfully!");
        }
        else
        {
            Console.WriteLine("No goals file found. Starting with an empty goal list.");
        }
    }
}

// In this code, to exceed requirements, the second idea was implemented where I added
// negative goals which ensures the value is negative or zero when completing it. In short, it gain you nothing.
// Also, after inputting goals, and you accidentally exit, it will automatically save data entered.