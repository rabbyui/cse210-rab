using System;
using System.Collections.Generic;

abstract class Activity
{
    public DateTime Date { get; set; }
    public int Length { get; set; }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    public abstract string GetSummary();
}

class Running : Activity
{
    public double Distance { get; set; }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return (Distance / Length) * 60;
    }

    public override double GetPace()
    {
        return Length / Distance;
    }

    public override string GetSummary()
    {
        string summary = $"{Date.ToShortDateString()} Running ({Length} min) - Distance: {Distance} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
        return summary;
    }
}

class StationaryBicycles : Activity
{
    public double Speed { get; set; }

    public override double GetDistance()
    {
        return Speed * Length / 60;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60 / Speed;
    }

    public override string GetSummary()
    {
        string summary = $"{Date.ToShortDateString()} Stationary Bicycles ({Length} min) - Distance: {GetDistance()} miles, Speed: {Speed} mph, Pace: {GetPace()} min per mile";
        return summary;
    }
}

class Swimming : Activity
{
    public int Laps { get; set; }

    public override double GetDistance()
    {
        return Laps * 50 / 1000.0;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Length) * 60;
    }

    public override double GetPace()
    {
        return Length / GetDistance();
    }

    public override string GetSummary()
    {
        string summary = $"{Date.ToShortDateString()} Swimming ({Length} min) - Distance: {GetDistance()} km, Speed: {GetSpeed()} kph, Pace: {GetPace()} min per km";
        return summary;
    }
}

class PolymorphismwithExerciseTracking
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        // Create Running activity
        Console.WriteLine();
        Running running = new Running
        {
            Date = new DateTime(2022, 11, 3),
            Length = 30,
            Distance = 3.0
        };
        activities.Add(running);

        // Create Stationary Bicycles activity
        StationaryBicycles stationaryBicycles = new StationaryBicycles
        {
            Date = new DateTime(2022, 11, 3),
            Length = 30,
            Speed = 6.0
        };
        activities.Add(stationaryBicycles);

        // Create Swimming activity
        Swimming swimming = new Swimming
        {
            Date = new DateTime(2022, 11, 3),
            Length = 30,
            Laps = 30
        };
        activities.Add(swimming);

        // Display activity summaries
        foreach (Activity activity in activities)

        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

// This program is an exercise tracking application that allows users to record and track their activities, 
// such as running, stationary bicycling, and swimming. It utilizes polymorphism to calculate and display 
// activity-specific information, including distance, speed, and pace, for each activity.