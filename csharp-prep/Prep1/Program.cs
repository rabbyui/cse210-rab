using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string first_n = Console.ReadLine();

        Console.Write("What is your last name? ");
        string last_n = Console.ReadLine();

        Console.WriteLine($"Your name is {last_n}, {first_n} {last_n}");
    }
}