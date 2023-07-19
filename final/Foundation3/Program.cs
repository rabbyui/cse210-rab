using System;

class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Event
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public Address EventAddress { get; set; }

    public string GetStandardDetails()
    {
        string details = $"Title: {Title}\nDescription: {Description}\nDate: {Date.ToShortDateString()}\nTime: {Time.ToString()}\nAddress: {EventAddress.GetFullAddress()}";
        return details;
    }

    public virtual string GetFullDetails()
    {
        string details = GetStandardDetails();
        return details;
    }

    public string GetShortDescription()
    {
        string description = $"Type: {GetType().Name}\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
        return description;
    }
}

class Lecture : Event
{
    public string Speaker { get; set; }
    public int Capacity { get; set; }

    public override string GetFullDetails()
    {
        string details = base.GetFullDetails();
        details += $"\nSpeaker: {Speaker}\nCapacity: {Capacity}";
        return details;
    }
}

class Reception : Event
{
    public string RSVPEmail { get; set; }

    public override string GetFullDetails()
    {
        string details = base.GetFullDetails();
        details += $"\nRSVP Email: {RSVPEmail}";
        return details;
    }
}

class OutdoorGathering : Event
{
    public string WeatherStatement { get; set; }

    public override string GetFullDetails()
    {
        string details = base.GetFullDetails();
        details += $"\nWeather: {WeatherStatement}";
        return details;
    }
}

class InheritancewithEventPlanning
{
    static void Main(string[] args)
    {
        Event[] events = new Event[3];

        // Create Lecture event
        Lecture lecture = new Lecture
        {
            Title = "Artificial Intelligence in Healthcare",
            Description = "Explore the role of AI in revolutionizing healthcare",
            Date = new DateTime(2023, 7, 15),
            Time = new TimeSpan(14, 0, 0),
            EventAddress = new Address
            {
                Street = "123 Main St",
                City = "New York",
                State = "NY",
                Country = "USA"
            },
            Speaker = "Dr. Jane Smith",
            Capacity = 100
        };

        // Create Reception event
        Reception reception = new Reception
        {
            Title = "Networking Mixer",
            Description = "An evening of networking and socializing",
            Date = new DateTime(2023, 8, 5),
            Time = new TimeSpan(18, 30, 0),
            EventAddress = new Address
            {
                Street = "456 Elm St",
                City = "London",
                State = "",
                Country = "United Kingdom"
            },
            RSVPEmail = "rsvp@example.com"
        };

        // Create Outdoor Gathering event
        OutdoorGathering gathering = new OutdoorGathering
        {
            Title = "Summer Music Festival",
            Description = "An outdoor music festival featuring various artists",
            Date = new DateTime(2023, 7, 30),
            Time = new TimeSpan(16, 0, 0),
            EventAddress = new Address
            {
                Street = "789 Oak St",
                City = "Los Angeles",
                State = "CA",
                Country = "USA"
            },
            WeatherStatement = "Sunny and warm"
        };

        events[0] = lecture;
        events[1] = reception;
        events[2] = gathering;

        // Display event details
        foreach (Event evnt in events)
        {
            Console.WriteLine("--- Event Details ---");
            Console.WriteLine(evnt.GetFullDetails());
            Console.WriteLine();
        }
    }
}

// This program is designed to track events for a fitness center. It includes classes to represent  
// different types of events such as lectures, receptions, and outdoor gatherings. 