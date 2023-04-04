/*
Author: Tony L
Date Created: March 3rd 2023
Last Updated: April 4th 2023    
*/

using System.Text.Json;
using Data_Structures.classes;

namespace Data_Structures;


internal class MainClass
{
    private static void Main()
    {
        // setup variables
        var days = 0;
        var name = "";
        var selection = 0;
        var helper = new Helper();
        Person player;

        var colours = new List<Colour>();
        var cats = new List<Cat>();
        var foods = new List<Food>();

        // all available options
        var dailyOptions = new List<string>
        {
            "Go to work.", "Play with your cat(s).", "Feed your cats.", "Go on a run with your cats.",
            "List all your cats.", "Quit the game."
        };

        // load all default colours
        var directory = @".\data\colours";
        // loop through the files
        foreach (var file in Directory.GetFiles(directory))
        {
            // load file info as a string
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert json to a class, and add to the list of colours
            colours.Add(JsonSerializer.Deserialize<Colour>(jsonString)!);
        }

        // load all default cats
        directory = @".\data\cats";
        // loop through the files
        foreach (var file in Directory.GetFiles(directory))
        {
            // load file info as a string
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert json to a class, and add to the list of colours
            cats.Add(JsonSerializer.Deserialize<Cat>(jsonString)!);
        }


        // load all default foods
        directory = @".\data\food";
        foreach (var file in Directory.GetFiles(directory))
        {
            // load file info as a string
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert json to a class, and add to the list of foods
            foods.Add(JsonSerializer.Deserialize<Food>(jsonString)!);
        }


        // Intro to the program
        Console.WriteLine(
            "Hi, and welcome to cat owner simulator! In this program, you'll be able to adopt a cat, and live with it");
        Console.WriteLine("Do you already have a save?");
        var saveDecisionList = new List<string> { "Yes", "No" };
        var saveDecision = helper.UserOptionsList(saveDecisionList);
        switch (saveDecision)
        {
            case 1:
            {
                try
                {
                    // load the player data
                    player = JsonSerializer.Deserialize<Person>(File.ReadAllText(@".\data\people\player.json"))!;
                }
                catch (FileNotFoundException)
                {
                    // file doesn't exist, switch to the default case
                    Console.WriteLine("Looks like you don't have a save already, making a new save!");
                    goto default;
                }
                break;
            }
            default:
                Console.WriteLine("Lets get started with your name:");

                // Loops until they have a valid input
                var validInput = false;
                while (!validInput)
                {
                    name = Console.ReadLine();
                    if (name is not null) validInput = true;
                }

                // User chooses a cat
                Console.WriteLine("===================================");
                Console.WriteLine($"Now lets get you a cat, {name}");
                // get user to choose a cat
                Console.WriteLine("Which cat do you want?");

                // get description of cats and add to an array
                var descriptions = new List<string>();
                foreach (var cat in cats) descriptions.Add(cat.Description());
                
                // get them to choose a cat
                selection = helper.UserOptionsList(descriptions);

                // Create the player object
                player = new Person("player", new List<Cat> { cats[selection - 1] }, 100);
                break;
        }

        // Game loop
        var exit = false;

        while (!exit)
        {
            // main menu
            Console.WriteLine("===================================");
            Console.WriteLine($"It is day {days}.");
            Console.WriteLine("What would you like to do?");
            var option = helper.UserOptionsList(dailyOptions);
            Console.WriteLine("===================================");
            switch (option)
            {
                case 1:
                    // adds money
                    player.money += 100;
                    Console.WriteLine("You earned a hundred dollars from working!");
                    // add 1 day 
                    days++;
                    break;
                case 2:
                    Console.WriteLine(
                        "You played with your cats! They all enjoyed it but as you left, they went back to lounging in the sun.");
                    // add 1 day
                    days++;
                    break;
                case 3:
                    // list of all food names
                    var foodNames = new List<string>();
                    foreach (var food in foods)
                        // add name of food
                        foodNames.Add(food.name);
                    Console.WriteLine("What food do you want to feed your cats?");
                    // ask for what they want to feed
                    selection = helper.UserOptionsList(foodNames);
                    // feed each cat
                    Console.WriteLine($"You fed your cats {foodNames[selection - 1]}");
                    foreach (var cat in player.cats)
                        // feed each cat the food
                        cat.Eat(foods[selection - 1]);
                    // add 1 day
                    days++;
                    break;
                case 4:
                    foreach (var cat in player.cats) cat.Run();
                    Console.WriteLine("Your cats get a good exercise.");
                    Console.WriteLine(
                        "On your run with your cats, you come across a stray cat. Do you want to keep it?");
                    var keepDec = new List<string>
                    {
                        // ask yes or no
                        "Yes",
                        "No"
                    };
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
                    foreach (var cat in player.cats) Console.WriteLine(cat.Description());
                    break;
                case 6:
                    // exit the while loop
                    exit = true;
                    // save all cats 
                    foreach (var cat in player.cats) cat.SaveCat();
                    player.SavePerson();
                    break;
            }
        }
    }
}

// Citations: 
// Microsoft, Redmond, WA, USA. C# documentation. (2023). Accessed: Mar. 10, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/csharp/ 