using System.Diagnostics;

namespace Algorithms;

using Algorithms.classes;
using System.Collections;
using System.Text.Json;
using System.Threading;

class MainClass
{
    static void Main()
    {
        // overall setup
        
        // load name info
        var namesJson = File.ReadAllText($@".\data\names.json");
        
        // load name info from file
        var names = JsonSerializer.Deserialize<List<String>>(namesJson)!;
        
        int[] numsOfObjects = { 5, 10, 100, 1000, 10000, 30000, 50000, 75000};
        
        var colours = new List<Colour>();
        // load colour information from file
        foreach (var file in Directory.GetFiles(@".\data\colours"))
        {
            // load json from file
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert to class, and then save to colours list
            colours.Add(JsonSerializer.Deserialize<Colour>(jsonString)!);
        }

        int trials = 10;

        var tasks = new List<Task<string[]>>();
        var results = new List<string[]>();

        foreach (var numOfObject in numsOfObjects)
        {
            for (int i = 0; i < trials; i++)
            {
                var task = Task.Run(() => SortAndSearch(numOfObject, ref names, ref colours));

                tasks.Add(task);
            }
        }
        Task.WaitAll(tasks.ToArray());


        foreach (var task in tasks)
        {
            results.Add(task.Result);
        }

        // write all results into file
        
        foreach (var result in results)
        {
            using (StreamWriter outputFile = File.AppendText($@".\{result[0]}.txt"))
            {
                outputFile.WriteLine(result[1]);
            }
        }


    }

    static string[] SortAndSearch(int numOfCats, ref List<string> names, ref List<Colour> colours)
    {

        // random
        var rnd = new Random();

        // time
        List<decimal> nanoTimes = new List<decimal>();

        // generate a number cats with random stats
        List<Cat> cats = new List<Cat>();

        for (int i = 0; i < numOfCats; i++)
        {
            var name = names[rnd.Next(0, names.Count)];
            var colour = colours[rnd.Next(0, colours.Count)];
            cats.Add(new Cat(name, colour));
        }

        // LINEAR SEARCH UNSORTED IN ARRAY
        // choose a random volume to look for 
        int rndIndex = rnd.Next(0, cats.Count);
        // timing
        var watch = new Stopwatch();
        watch.Start();
        LinearSearch(cats[rndIndex].volume, cats);
        watch.Stop();
        // find the time in milliseconds
        decimal nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // LINEAR SEARCH
        // choose a random volume to look for 
        watch.Start();
        LinearSearch(999999999999999999999999999999999999999999d, cats);
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // INSERTION SORT
        watch.Start();
        InsertionSort(cats);
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();
        
        // DEFAULT SORT
        watch.Start();
        cats.Sort((x, y) => x.volume.CompareTo(y.volume));
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // KNOWN BINARY SEARCH
        watch.Start();
        BinarySearch(cats[rndIndex].volume, cats);
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // UNKNOWN BINARY SEARCH
        watch.Start();
        BinarySearch(20000000000000000000000000d, cats);
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // KNOWN LINEAR SEARCH 
        watch.Start();
        LinearSearch(cats[rndIndex].volume, cats);
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // UNKNOWN LINEAR SEARCH
        watch.Start();
        LinearSearch(9999999999999999999999999999999999d, cats);
        watch.Stop();
        // find the time in milliseconds
        nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        nanoTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        string splitTimes = String.Join(",", nanoTimes);

        string[] returnValue = { numOfCats.ToString(), splitTimes };

        return returnValue;
        
    }

    static List<Cat> InsertionSort(List<Cat> cats)
    {
        for (int i = 0; i < cats.Count; i++)
        {

            Cat cat = cats[i];

            // starting index to start comparing
            // compares from the current index to the start of the array
            // finds the correct position by checking if it under j, and over j-1
            int j = i - 1;
            while (j >= 0 && cats[j].volume > cat.volume)
            {
                cats[j + 1] = cats[j];
                j--;
            }

            cats[j + 1] = cat;
        }
        
        return cats;
    }

    /// <summary>
    /// Binary search algorithm
    /// </summary>
    /// <param name="vol">Item to look for</param>
    /// <param name="cats">Array to look in</param>
    /// <returns>Index of the item</returns>
    static int BinarySearch(double vol, List<Cat> cats)
    {
        int start = 0;
        int end = cats.Count - 1;
        int middleOfArray = 0;
            
        // binary search
        while (start <= end)
        {
            middleOfArray = (start + end) / 2;
        
            if (cats[middleOfArray].volume == vol)
            {
                return middleOfArray;
            }
            
            if (cats[middleOfArray].volume < vol)
            {
                start = middleOfArray + 1;
            }
            else
            {
                end = middleOfArray - 1;
            }
        }
        return -1;
    }

    /// <summary>
    /// Searches for a volume within a list of cats, linearly
    /// </summary>
    /// <param name="vol"></param>
    /// <param name="cats"></param>
    /// <returns>The index of the cat that has the volume</returns>
    static int LinearSearch(double vol, List<Cat> cats)
    {
        // LINEAR SEARCH
        for (int i = 0; i < cats.Count; i++)
        {
            if (cats[i].volume == vol)
            {
                return i;
            }
        }

        return -1;
    }
}
