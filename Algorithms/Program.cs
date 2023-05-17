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
        
        // number of objects to put into list
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
        
        // number of times to repeat
        int trials = 10;
        
        // list that stores the tasks for the threadpool to complete
        var tasks = new List<Task<string[]>>();
        // list that stores the results from the threadpool
        var results = new List<string[]>();
        
        foreach (var numOfObject in numsOfObjects)
        {
            // repeat benchmarks x times
            for (int i = 0; i < trials; i++)
            {
                // add task to threadpool to complete
                var task = Task.Run(() => Benchmarks(numOfObject, ref names, ref colours));
                tasks.Add(task);
            }
        }
        // wait for all of them to complete
        Task.WaitAll(tasks.ToArray());

        // add the results to results array
        foreach (var task in tasks)
        {
            results.Add(task.Result);
        }

        // write all results into file
        foreach (var result in results)
        {
            // result is stored like this
            // [numOfObjects, [results]]
            using (StreamWriter outputFile = File.AppendText($@".\{result[0]}.txt"))
            {
                outputFile.WriteLine(result[1]);
            }
        }
    }

    /// <summary>
    /// Benchmarks algorithms against an array, and returns the results. 
    /// </summary>
    /// <param name="numOfCats">How many objects will be generated into a list.</param>
    /// <param name="names">A list of names as a string.</param>
    /// <param name="colours">A list of colours, as the Colour class.</param>
    /// <returns></returns>
    static string[] Benchmarks(int numOfCats, ref List<string> names, ref List<Colour> colours)
    {
        // random
        var rnd = new Random();

        // time
        List<decimal> milliTimes = new List<decimal>();

        // generate a number cats with random stats
        List<Cat> cats = new List<Cat>();

        for (int i = 0; i < numOfCats; i++)
        {
            // choose a random name
            var name = names[rnd.Next(0, names.Count)];
            // choose a random colour
            var colour = colours[rnd.Next(0, colours.Count)];
            // add to main array
            cats.Add(new Cat(name, colour));
        }

        // UNSORTED KNOWN LINEAR SEARCH 
        // choose a random volume to look for 
        int rndIndex = rnd.Next(0, cats.Count);
        // timing
        var watch = new Stopwatch();
        watch.Start();
        LinearSearch(cats[rndIndex].volume, cats);
        watch.Stop();
        // find the time in milliseconds
        decimal nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // UNSORTED UNKNOWN LINEAR SEARCH 
        // choose a random volume to look for 
        watch.Start();
        LinearSearch(999999999999999999999999999999999999999999d, cats);
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // INSERTION SORT
        watch.Start();
        InsertionSort(cats);
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();
        
        // DEFAULT SORT
        watch.Start();
        cats.Sort((x, y) => x.volume.CompareTo(y.volume));
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // KNOWN BINARY SEARCH
        watch.Start();
        BinarySearch(cats[rndIndex].volume, cats);
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // UNKNOWN BINARY SEARCH
        watch.Start();
        BinarySearch(20000000000000000000000000d, cats);
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // KNOWN LINEAR SEARCH 
        watch.Start();
        LinearSearch(cats[rndIndex].volume, cats);
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();

        // UNKNOWN LINEAR SEARCH
        watch.Start();
        LinearSearch(9999999999999999999999999999999999d, cats);
        watch.Stop();
        // add the time in milliseconds to the list
        milliTimes.Add((watch.ElapsedTicks * nanosecPerTick) / 1000000m);
        watch.Reset();
        
        // seperate all times in milliTimes by a comma
        string splitTimes = String.Join(",", milliTimes);
        
        // generates the array to return
        string[] returnValue = { numOfCats.ToString(), splitTimes };

        return returnValue;
    }

    /// <summary>
    /// Sorts a list of cats by volume using insertion sort.
    /// </summary>
    /// <param name="cats">List of cats to sort.</param>
    /// <returns>Sorted list of cats.</returns>
    private static List<Cat> InsertionSort(List<Cat> cats)
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
    /// Searches for a specific volume within a list of Cat objects, using binary search.
    /// </summary>
    /// <param name="vol">Volume to look for.</param>
    /// <param name="cats">List of cats to look in.</param>
    /// <returns>Index of the cat with the specific volume. Returns -1, if not found</returns>
    private static int BinarySearch(double vol, List<Cat> cats)
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
    /// Searches for a specific volume within a list of cats, using linear search
    /// </summary>
    /// <param name="vol">Volume to look for. </param>
    /// <param name="cats">List of cats to look in.</param>
    /// <returns>Index of the cat with the specific volume. Returns -1, if not found</returns>
    private static int LinearSearch(double vol, List<Cat> cats)
    {
        // linear search
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
