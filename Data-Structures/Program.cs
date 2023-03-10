
using Data_Structures.classes;
using System.Text.Json;


namespace Data_Structures
{
    class MainClass
    {
        static void Main(string[] args)
        {
            // setup variables
            int days = 0;
            var name = "";
            var selection = 0;
            Helper helper = new Helper();
            Person player;

            List<Colour> colours = new List<Colour>();
            List<Cat> cats = new List<Cat>();
            List<Food> foods = new List<Food>();

            // all available options
            List<string> dailyOptions = new List<string>
            {
                "Go to work.", "Play with your cat(s).", "Feed your cats.", "Go on a run with your cats.",
                "List all your cats.", "Quit the game."
            };

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


            // load all default foods
            directory = @".\data\food";
            foreach (string file in Directory.GetFiles(directory))
            {
                // load file info as a string
                string jsonString = File.ReadAllText(@$".\{file}");
                // convert json to a class, and add to the list of foods
                foods.Add(JsonSerializer.Deserialize<Food>(jsonString)!);
            }


            // Intro to the program
            Console.WriteLine("Hi, and welcome to cat owner simulator! In this program, you'll be able to adopt a cat, and live with it");
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
            Console.WriteLine("===================================");
            Console.WriteLine($"Now lets get you a cat, {name}");
            // get user to choose a cat
            Console.WriteLine("Which cat do you want?");

            // get description of cats and add to an array
            List<string> descriptions = new List<string>();
            foreach (Cat cat in cats)
            {
                descriptions.Add(cat.Description());
            }

            selection = helper.UserOptionsList(descriptions);

            // Create the player object
            player = new Person(name: name!, cats: new List<Cat> { cats[selection - 1] }, money: 100);

            // Game loop
            bool exit = false;

            while (!exit)
            {
                // main menu
                Console.WriteLine($"It is day {days}.");
                Console.WriteLine("What would you like to do?");
                int option = helper.UserOptionsList(dailyOptions);
                switch (option)
                {
                    case 1:
                        // adds money
                        player.money += 100;
                        Console.WriteLine("You earnt a hundred dollars from working!");
                        // add 1 day 
                        days++;
                        break;
                    case 2:
                        Console.WriteLine("You played with your cats! They all enjoyed it but as you left, they went back to lounging in the sun.");
                        // add 1 day
                        days++;
                        break;
                    case 3:
                        // list of all food names
                        List<string> foodNames = new List<string>();
                        foreach (Food food in foods)
                        {
                            // add name of food
                            foodNames.Add(food.name);
                        }
                        Console.WriteLine("What food do you want to feed your cats?");
                        // ask for what they want to feed
                        selection = helper.UserOptionsList(foodNames);
                        // feed each cat
                        Console.WriteLine($"You fed your cats {foodNames[selection - 1]}");
                        foreach (Cat cat in player.cats)
                        {
                            // feed each cat the food
                            cat.Eat(foods[selection - 1]);
                        }
                        // add 1 day
                        days++;
                        break;
                    case 4:
                        Console.WriteLine("On your run with your cats, you come across a stray cat. Do you want to keep it?");
                        List<string> keepDec = new List<string>();
                        // ask yes or no
                        keepDec.Add("Yes");
                        keepDec.Add("No");
                        selection = helper.UserOptionsList(keepDec);
                        // add to player cats
                        if (selection == 1)
                        {
                            player.cats.Add(new Cat());
                            Console.WriteLine("You adopt the cat successfully!");
                        }
                        else
                        {
                            Console.WriteLine("The cat runs away.");
                        }
                        // add 1 day
                        days++;
                        break;
                    case 5:
                        Console.WriteLine("Here are all your cats:");
                        // loop through players cats, and get them to print the description
                        foreach (Cat cat in player.cats)
                        {
                            Console.WriteLine(cat.Description());
                        }
                        break;
                    case 6:
                        // exit the while loop
                        exit = true;
                        // save all cats 
                        directory = @".\data\cats";
                        foreach (Cat cat in player.cats)
                        {
                            cat.SaveCat();
                        }
                        break;
                }
            }
        }
    }
}