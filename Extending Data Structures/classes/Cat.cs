using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data_Structures.classes;

/// <summary>
///     A object that holds the characteristics of a cat, and some functions of a cat
/// </summary>
public class Cat : Animal
{
    /// <summary>
    ///     The class constructor
    /// </summary>
    public Cat()
    {
        guid = Guid.NewGuid();
        furballsCoughed = 0;
        skinType = "fur";
        species = "Cat";
        // random number generator
        var rnd = new Random();

        string[] breeds =
        {
            "Siamese", "British Shorthair", "Maine Coon", "Persian", "Ragdoll", "American Shorthair",
            "Scottish Fold"
        };

        string[] sexs = { "Male", "Female" };

        var colours = new List<Colour>();
        // load colour information from file
        foreach (var file in Directory.GetFiles(@".\data\colours"))
        {
            // load json from file
            var jsonString = File.ReadAllText(@$".\{file}");
            // convert to class, and then save to colours list
            colours.Add(JsonSerializer.Deserialize<Colour>(jsonString)!);
        }

        // randomly generate stats for cat
        // get a random name from the user
        Console.WriteLine("What name would you like your cat?");
        name = Console.ReadLine()!;

        // choose random breed from list
        breed = breeds[rnd.Next(breeds.Length)];
        // choose random colour from list
        colour = colours[rnd.Next(colours.Count)];
        // choose random sex
        sex = sexs[rnd.Next(sexs.Length)];

        // choose random age
        age = rnd.Next(0, 10);
        // check if its a kitten
        if (age == 0)
        {
            // custom sts
            weight = rnd.NextDouble() * 1 + 1;
            height = rnd.NextDouble() * 5 + 2;
            length = rnd.NextDouble() * 6 + 3;
            speed = height * length * 0.05 - weight;
            // check if its a negative number, if it is set it to 1km/h
            if (speed <= 0) speed = 1;
        }
        else
        {
            // custom stats
            weight = rnd.NextDouble() * 3 + 2;
            height = rnd.NextDouble() * 6 + 23;
            length = rnd.NextDouble() * 10 + 40;
            speed = height * length * 0.05 - weight;
            // check if its a negative number, if it is set it to 1km/h
            if (speed <= 0) speed = 1;
        }
    }

    /// <summary>
    ///     The class constructor
    /// </summary>
    /// <param name="guid">Guid of the cat</param>
    /// <param name="name">Name of the cat</param>
    /// <param name="species">Species of the cat</param>
    /// <param name="colour">Colour of the cat</param>
    /// <param name="breed">Breed of the cat</param>
    /// <param name="sex">Sex of the cat</param>
    /// <param name="weight">Weight of the cat</param>
    /// <param name="height">Height of the cat</param>
    /// <param name="speed">Speed of the cat</param>
    /// <param name="length">Length of the cat</param>
    /// <param name="age">Age of the cat</param>
    /// <param name="skinType">Type of skin the cat has</param>
    /// <param name="furballsCoughed">Number of fur balls the cat has coughed over its life</param>
    [JsonConstructor]
    public Cat(string name, string species, Colour colour, string breed, string sex, double weight, double height,
        double speed, double length, int age, string skinType, int furballsCoughed, Guid guid)
    {
        this.guid = guid;
        this.name = name;
        this.species = species;
        this.colour = colour;
        this.breed = breed;
        this.sex = sex;
        this.weight = weight;
        this.height = height;
        this.speed = speed;
        this.length = length;
        this.age = age;
        this.skinType = skinType;
        this.furballsCoughed = furballsCoughed;
    }

    /// <summary>
    ///     Number of furballs the cat has coughed up over its lifetime
    /// </summary>
    public int furballsCoughed { get; set; }

    /// <summary>
    ///     Prints the animal sound (meow).
    /// </summary>
    public override void AnimalSound()
    {
        Console.WriteLine($@"{name} meows at you, and then runs away!");
    }

    public override void SaveAnimal()
    {
        {
            var fileName = @$".\data\{species}\{guid.ToString()}.json";
            var jsonString = ToJson();
            File.WriteAllText(fileName, jsonString);
        }
    }
}