/*
Author: Tony L
Date Created: March 3rd 2023
Last Updated: May 4th 2023    
Purpose: To learn more about extending data structures with inheritance, by using an example of a pet owner simulator, featuring cats and dogs, and various activities each the user can do.
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
        var animals = new List<Animal>();
        var foods = new List<Food>();
        string[] species = { "cat", "dog" };

        // all available options
        var dailyOptions = new List<string>
        {
            "Go to work.", "Play with your pet(s).", "Feed your pet.", "Go on a run with your pet(s).",
            "List all your pet(s).", "Quit the game."
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

        // load all cats
        directory = @".\data\Cat";
        foreach (var file in Directory.GetFiles(directory))
        {
            // load file info as a string
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert json to a class, and add to the list of foods
            animals.Add(JsonSerializer.Deserialize<Cat>(jsonString)!);
        }

        // load all dogs
        directory = @".\data\Dog";
        foreach (var file in Directory.GetFiles(directory))
        {
            // load file info as a string
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert json to a class, and add to the list of foods
            animals.Add(JsonSerializer.Deserialize<Dog>(jsonString)!);
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
            "Hi, and welcome to pet owner simulator! In this program, you'll be able to adopt pets, and live with them");
        Console.WriteLine("Do you already have a save?");
        var saveDecisionList = new List<string> { "Yes", "No" };
        var saveDecision = helper.UserOptionsList(saveDecisionList);
        switch (saveDecision)
        {
            // already have a save
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
            // dont have a save
            default:
                Console.WriteLine("Lets get started with your name:");

                // Loops until they have a valid input
                var validInput = false;
                while (!validInput)
                {
                    name = Console.ReadLine();
                    if (name is not null) validInput = true;
                }


                // User chooses a pet
                Console.WriteLine("===================================");
                Console.WriteLine($"Now lets get you a pet, {name}");
                // get user to choose a pet
                Console.WriteLine("Which pet do you want?");

                // get description of pets and add to an array
                var descriptions = new List<string>();
                foreach (var pet in animals) descriptions.Add(pet.Description());

                // get them to choose a pet
                selection = helper.UserOptionsList(descriptions);

                var cat = animals[selection - 1] as Cat;

                if (cat is not null)
                {
                    player = new Person(name!, new List<Cat> { cat }, new List<Dog>(), 0);
                }
                else
                {
                    var dog = animals[selection - 1] as Dog;
                    player = new Person(name!, new List<Cat>(), new List<Dog> { dog }, 0);
                }

                break;
        }

        // Game loop
        var exit = false;

        while (!exit)
        {
            days++;
            // add furballs and squirrels chased
            foreach (var pet in player.pets)
                // once a week
                if (days % 7 == 0)
                {
                    var cat = pet as Cat;
                    if (cat is not null) cat.furballsCoughed++;
                }
                else
                {
                    var dog = pet as Dog;
                    if (dog is not null) dog.squirrelsChased += 2;
                }

            // main menu
            Console.WriteLine("===================================");
            Console.WriteLine($"It is day {days}.");
            Console.WriteLine("What would you like to do?");
            var option = helper.UserOptionsList(dailyOptions);
            Console.WriteLine("===================================");
            switch (option)
            {
                // work
                case 1:
                    // adds money
                    player.money += 100;
                    Console.WriteLine("You earned a hundred dollars from working!");

                    break;
                // play
                case 2:
                    foreach (var pet in player.pets) pet.AnimalSound();

                    break;
                // feed
                case 3:
                    // list of all food names
                    var foodNames = new List<string>();
                    foreach (var food in foods)
                        // add name of food
                        foodNames.Add(food.name);

                    Console.WriteLine("What food do you want to feed your pets?");
                    // ask for what they want to feed
                    selection = helper.UserOptionsList(foodNames);

                    // feed each cat
                    Console.WriteLine($"You fed your pets {foodNames[selection - 1]}");
                    foreach (var pet in player.pets)
                        // feed each pet the food
                        pet.Eat(foods[selection - 1]);

                    break;
                // exercise
                case 4:
                    foreach (var pet in player.pets) pet.Run();
                    Console.WriteLine("Your pets get a good exercise.");

                    // randomly choose an animal 
                    var rnd = new Random();

                    var animal = species[rnd.Next(species.Length)];

                    Console.WriteLine(
                        $@"On your run with your pets, you come across a stray {animal}. Do you want to keep it?");
                    var keepDec = new List<string>
                    {
                        // ask yes or no
                        "Yes",
                        "No"
                    };
                    selection = helper.UserOptionsList(keepDec);
                    // want to adopt
                    if (selection == 1)
                    {
                        // add the animal to the players pets
                        if (animal == "cat")
                        {
                            var cat = new Cat();
                            player.pets.Add(cat);
                            player.cats.Add(cat);
                        }
                        else if (animal == "dog")
                        {
                            var dog = new Dog();
                            player.pets.Add(dog);
                            player.dogs.Add(dog);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"The {animal} runs away.");
                    }

                    break;
                // list all pets
                case 5:
                    Console.WriteLine("Here are all your pets:");
                    // loop through players cats, and get them to print the description
                    foreach (var pet in player.pets)
                    {
                        var cat = pet as Cat;

                        if (cat is not null)
                            Console.WriteLine(pet.Description() +
                                              $" They have coughed up {cat.furballsCoughed} fur balls over their life.");

                        var dog = pet as Dog;
                        if (dog is not null)
                            Console.WriteLine(pet.Description() +
                                              $" They have chased {dog.squirrelsChased} squirrels over their life.");
                    }

                    break;
                // quit
                case 6:
                    // exit the while loop
                    exit = true;
                    // save all cats 
                    foreach (var pet in player.pets) pet.SaveAnimal();
                    player.SavePerson();

                    break;
            }
        }
    }
}

// Citations: 
// Microsoft, Redmond, WA, USA. C# documentation. (2023). Accessed: Mar. 10, 2023. [Online]. Available: https://learn.microsoft.com/en-us/dotnet/csharp/ 