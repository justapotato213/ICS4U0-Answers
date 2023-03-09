using Data_Structures.classes;
using System.Text.Json;


namespace Data_Structures
{
    class MainClass
    {
        static void Main(string[] args)
        {
            // setup variables
            List<Colour> colours = new List<Colour>();
            List<string> catNames = new List<string>
                { "Luna", "Nala", "Oliver", "Leo", "Simba", "Milo", "Tigger", "Max", "Lola" };
            List<string> peopleNames = new List<string>
                { "John", "William", "James", "Charles", "Robert", "Henry", "Karely", "Catalina", "Devin", "Sydney" };
            List<Cat> cats = new List<Cat>();

            var name = "";

            // load all default colours
            string directory = @".\data\colours";
            // loop through the files
            foreach (string file in Directory.GetFiles(directory))
            {
                // load file info as a string
                string jsonString = File.ReadAllText(@$".\{file}");
                // convert json to a class, and add to the list of colours
                colours.Add(JsonSerializer.Deserialize<Colour>(jsonString)!);
            }

            // load all default cats
            directory = @".\data\cats";
            // loop through the files
            foreach (string file in Directory.GetFiles(directory))
            {
                // load file info as a string
                string jsonString = File.ReadAllText(@$".\{file}");
                // convert json to a class, and add to the list of colours
                cats.Add(JsonSerializer.Deserialize<Cat>(jsonString)!);
            }

            // Intro to the program
            Console.WriteLine(
                "Hi, and welcome to cat owner simulator! In this program, you'll be able to adopt a cat, and live with it");
            Console.WriteLine("But enough of the basics, lets get started with your name:");

            // Loops until they have a valid input
            bool validInput = false;
            while (!validInput)
            {
                name = Console.ReadLine();
                if (name is not null)
                {
                    validInput = true;
                }
            }

            // User chooses a cat
            Console.WriteLine($"Now lets get you a cat, {name}");
            Console.WriteLine("Here are your choices:");
            // Print all descriptions
            for (int i = 0; i < cats.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cats[i].Description()}");
            }

            // get user to choose a cat
            

            
        }
    }
}