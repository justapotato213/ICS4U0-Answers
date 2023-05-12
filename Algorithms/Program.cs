using System.Diagnostics;

namespace Algorithms;

using Algorithms.classes;
using System.Text.Json;
using System.Threading;

class MainClass
{
    static void Main()
    {
        Thread five = new Thread(() => SortAndSearch(5));
        five.Start();

        Thread ten = new Thread(() => SortAndSearch(10));
        ten.Start();
        
        Thread hundred = new Thread(()=> SortAndSearch(100));
        hundred.Start();

        Thread thousand = new Thread(() => SortAndSearch(1000));
        thousand.Start();

        Thread tenThousand = new Thread(() => SortAndSearch(10000));
        tenThousand.Start();

        Thread thirtyThousand = new Thread(() => SortAndSearch(30000));
        thirtyThousand.Start();

        Thread fiftyThousand = new Thread(() => SortAndSearch(50000));
        fiftyThousand.Start();

        Thread seventyThousand = new Thread(() => SortAndSearch(75000));
        seventyThousand.Start();
    }

    static void SortAndSearch(int numOfCats)
    {
        for (int x = 0; x < 10; x++)
        {
            // timing
            var watch = new Stopwatch();
            // random
            var rnd = new Random();
            // load name info
            var namesJson = File.ReadAllText($@".\data\names.json");
            // load name info from file
            var names = JsonSerializer.Deserialize<List<String>>(namesJson);
            // time
            List<long> nanoTimes = new List<long>();

            var colours = new List<Colour>();
            // load colour information from file
            foreach (var file in Directory.GetFiles(@".\data\colours"))
            {
                // load json from file
                var jsonString = File.ReadAllText(@$".\{file}");
                // convert to class, and then save to colours list
                colours.Add(JsonSerializer.Deserialize<Colour>(jsonString)!);
            }

            // generate a number cats with random stats
            List<Cat> cats = new List<Cat>();

            for (int i = 0; i < numOfCats; i++)
            {
                var name = names[rnd.Next(0, names.Count)];
                var colour = colours[rnd.Next(0, colours.Count)];
                cats.Add(new Cat(name, colour));
            }

            // INSERTION SORT
            watch.Start();
            cats = InsertionSort(cats);
            watch.Stop();
            // find the time in nanoseconds
            long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            nanoTimes.Add(watch.ElapsedTicks * nanosecPerTick);
            watch.Reset();
            
            
            // KNOWN BINARY SEARCH
            // choose a random volume to look for 
            int rndIndex = rnd.Next(0, cats.Count);
            watch.Start();
            BinarySearch(cats[rndIndex].volume, cats);
            watch.Stop();

            // find the time in nanoseconds
            nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            nanoTimes.Add(watch.ElapsedTicks * nanosecPerTick);
            watch.Reset();
            
            
            // UNKNOWN BINARY SEARCH
            watch.Start();
            BinarySearch(20000000000000000000000000d, cats);
            watch.Stop();
            
            // find the time in nanoseconds
            nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            nanoTimes.Add(watch.ElapsedTicks * nanosecPerTick);
            watch.Reset();
            
            
            // KNOWN LINEAR SEARCH 
            watch.Start();
            LinearSearch(cats[rndIndex].volume, cats);
            watch.Stop();
            
            // find the time in nanoseconds
            nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            nanoTimes.Add(watch.ElapsedTicks * nanosecPerTick);
            watch.Reset();
            

            // UNKNOWN LINEAR SEARCH
            watch.Start();
            LinearSearch(9999999999999999999999999999999999d, cats);
            watch.Stop();
            
            // find the time in nanoseconds
            nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            nanoTimes.Add(watch.ElapsedTicks * nanosecPerTick);
            watch.Reset();



            using (StreamWriter outputFile = File.AppendText($@".\{numOfCats}.txt"))
            {
                outputFile.WriteLine(string.Join(",", nanoTimes));
            }
        }
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
