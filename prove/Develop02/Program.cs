using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
}

class JournalProgram
{
    private List<JournalEntry> journalEntries;

    public JournalProgram()
    {
        journalEntries = new List<JournalEntry>();
    }

    private string GetRandomPrompt()
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",

            // Added my own prompts below
            "Tell me progress about the 5 newly born puppies",
            "How do you feel about your preparation in moving out to your new apartment?",
            "How did your kids receive it when you broke the news you're moving out?",
            "What challenges you've encountered today?",
            "How did you face your family drama when you visited them last Sunday?",
        };

        Random random = new Random();
        int index = random.Next(prompts.Length);

        return prompts[index];
    }

    public void WriteNewEntry()
    {
        Console.WriteLine("----- Write New Journal Entry -----");

        JournalEntry entry = new JournalEntry();
        entry.Prompt = GetRandomPrompt();

        Console.WriteLine("Prompt: " + entry.Prompt);
        Console.Write("Response: ");
        entry.Response = Console.ReadLine();
        entry.Date = DateTime.Now;

        journalEntries.Add(entry);

        Console.WriteLine("Entry saved successfully!");
    }

    public void DisplayJournal()
    {
        Console.WriteLine("----- Journal Entries -----");

        if (journalEntries.Count == 0)
        {
            Console.WriteLine("No entries found.");
        }
        else
        {
            foreach (JournalEntry entry in journalEntries)
            {
                // I added a time stamp. It will show what time exactly did you input your entry
                Console.WriteLine("Date: " + entry.Date.ToString("MM/dd/yyyy HH:mm:ss"));
                Console.WriteLine("Prompt: " + entry.Prompt);
                Console.WriteLine("Response: " + entry.Response);
                Console.WriteLine();
            }
        }
    }

    public void SaveJournalToFile()
    {
        Console.WriteLine("----- Save Journal to File -----");
        Console.Write("Enter a filename: ");
        string filename = Console.ReadLine();
        string filePath = filename + "";

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (JournalEntry entry in journalEntries)
                {
                    // I added a time stamp apart from the date
                    writer.WriteLine("Date: " + entry.Date.ToString("MM/dd/yyyy HH:mm:ss"));
                    writer.WriteLine("Prompt: " + entry.Prompt);
                    writer.WriteLine("Response: " + entry.Response);
                    writer.WriteLine();
                }
            }

            Console.WriteLine("Journal saved to file successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while saving journal to file: " + ex.Message);
        }
    }

    public void LoadJournalFromFile()
    {
        Console.WriteLine("----- Load Journal from File -----");
        Console.Write("Enter a filename: ");
        string filename = Console.ReadLine();
        string filePath = filename + "";

        if (File.Exists(filePath))
        {
            try
            {
                journalEntries.Clear();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    JournalEntry entry = null;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Date: "))
                        {
                            entry = new JournalEntry();
                            entry.Date = DateTime.Parse(line.Substring(6));
                        }
                        else if (line.StartsWith("Prompt: "))
                        {
                            entry.Prompt = line.Substring(8);
                        }
                        else if (line.StartsWith("Response: "))
                        {
                            entry.Response = line.Substring(10);
                            journalEntries.Add(entry);
                        }
                    }
                }

                Console.WriteLine("Journal entry/ies loaded from file successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while loading journal entry/ies from file!:" + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("File not found! Please make sure the file exists and try again.");
        }
    }

    public void Run()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("----- Journal Entry Program Menu -----");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display entry/ies");
            Console.WriteLine("3. Save entry/ies to a file (ex. .txt, .csv)");
            Console.WriteLine("4. Load entry/ies from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Pick an option (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Nah! Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        JournalProgram journalProgram = new JournalProgram();
        journalProgram.Run();
    }
}