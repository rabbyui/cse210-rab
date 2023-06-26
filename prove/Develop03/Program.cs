using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Create a library of scriptures
        List<Scripture> scriptureLibrary = new List<Scripture>
        {
            new Scripture("John 3:16", "“For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
            
            // Included 5 Book of Mormon Scripture Mastery
            new Scripture("1 Nephi 3:7", "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them."),
            new Scripture("Nephi 2:25", "Adam fell that men might be; and men are, that they might have joy."),
            new Scripture("Nephi 2:27", "Wherefore, men are free according to the flesh; and all things are given them which are expedient unto man. And they are free to choose liberty and eternal life, through the great Mediator of all men, or to choose captivity and death, according to the captivity and power of the devil; for he seeketh that all men might be hmiserable like unto himself."),
            new Scripture("Nephi 9:28", "O that cunning aplan of the evil one! O the vainness, and the frailties, and the foolishness of men! When they are learned they think they are wise, and they hearken not unto the counsel of God, for they set it aside, supposing they know of themselves, wherefore, their wisdom is foolishness and it profiteth them not. And they shall perish."),
            new Scripture("Nephi 25:23", "For we labor diligently to write, to persuade our children, and also our brethren, to believe in Christ, and to be reconciled to God; for we know that it is by grace that we are saved, after all we can do."),
            // Add more scriptures to the library including scripture mastery
        };

        Random random = new Random();

        // Choose a random scripture from the library
        Scripture scripture = scriptureLibrary[random.Next(scriptureLibrary.Count)];

        // Display the initial scripture
        scripture.Display();

        // Continue prompting the user until all words are hidden
        while (!scripture.AllWordsHidden)
        {
            Console.WriteLine("Press enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            // Hide a few random words and display the updated scripture
            scripture.HideRandomWords();
            scripture.Display();
        }
    }
}

class Scripture
{
    private string reference;
    private List<Word> words;
    private List<int> hiddenWordIndices;

    public bool AllWordsHidden { get; private set; }

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.words = ParseTextToWords(text);
        this.hiddenWordIndices = new List<int>();
        this.AllWordsHidden = false;
    }

    private List<Word> ParseTextToWords(string text)
    {
        string[] wordArray = text.Split(' ');
        List<Word> wordList = new List<Word>();

        foreach (string wordText in wordArray)
        {
            Word word = new Word(wordText);
            wordList.Add(word);
        }

        return wordList;
    }

    public void HideRandomWords()
    {
        Random random = new Random();

        // Find the indices of words that are not already hidden
        List<int> visibleWordIndices = new List<int>();
        for (int i = 0; i < words.Count; i++)
        {
            if (!words[i].IsHidden)
                visibleWordIndices.Add(i);
        }

        // If all words are already hidden, set the flag and return
        if (visibleWordIndices.Count == 0)
        {
            AllWordsHidden = true;
            return;
        }

        // Select a random word index from the visible word indices
        int randomIndex = random.Next(visibleWordIndices.Count);
        int wordIndex = visibleWordIndices[randomIndex];

        // Hide the selected word
        words[wordIndex].Hide();
        hiddenWordIndices.Add(wordIndex);
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(reference);

        for (int i = 0; i < words.Count; i++)
        {
            if (hiddenWordIndices.Contains(i))
                Console.Write("_ ");
            else
                Console.Write(words[i].Text + " ");
        }

        Console.WriteLine("\n");
    }
}

class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        this.Text = text;
        this.IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}