using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data_Structures.classes;

/// <summary>
///     A object that holds the characteristics of a dog, and some functions of a dog
/// </summary>
public class Dog : Animal
{
    /// <summary>
    ///     The class constructor
    /// </summary>
    public Dog()
    {
        guid = Guid.NewGuid();
        squirrelsChased = 0;
        skinType = "fur";
        species = "dog";
        // random number generator
        var rnd = new Random();

        string[] breeds =
        {
            "German Shepherd", "Bulldog", "Labrador Retriever", "Golden Retriever", "French Bulldog", "Shiba Inu",
            "Poodle", "Pitbull"
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

        // randomly generate stats for dog
        // get a random name from the user
        Console.WriteLine("What name would you like your dog?");
        name = Console.ReadLine()!;

        // choose random breed from list
        breed = breeds[rnd.Next(breeds.Length)];
        // choose random colour from list
        colour = colours[rnd.Next(colours.Count)];
        // choose random sex
        sex = sexs[rnd.Next(sexs.Length)];

        // choose random age
        age = rnd.Next(0, 10);
        // check if its a puppy
        if (age == 0)
        {
            // custom stats
            weight = rnd.NextDouble() * 1 + 2;
            height = rnd.NextDouble() * 5 + 3;
            length = rnd.NextDouble() * 6 + 4;
            speed = height * length * 0.05 - weight;
            // check if its a negative number, if it is set it to 1km/h
            if (speed <= 0) speed = 1;
        }
        else
        {
            // custom stats
            weight = rnd.NextDouble() * 3 + 3;
            height = rnd.NextDouble() * 10 + 23;
            length = rnd.NextDouble() * 10 + 50;
            speed = height * length * 0.05 - weight;
            // check if its a negative number, if it is set it to 1km/h
            if (speed <= 0) speed = 1;
        }
    }

    /// <summary>
    ///     The class constructor
    /// </summary>
    /// <param name="guid">Guid of the dog</param>
    /// <param name="name">Name of the dog</param>
    /// <param name="species">Species of the dog</param>
    /// <param name="colour">Colour of the dog</param>
    /// <param name="breed">Breed of the dog</param>
    /// <param name="sex">Sex of the dog</param>
    /// <param name="weight">Weight of the dog</param>
    /// <param name="height">Height of the dog</param>
    /// <param name="speed">Speed of the dog</param>
    /// <param name="length">Length of the dog</param>
    /// <param name="age">Age of the dog</param>
    /// <param name="skinType">Skin type of the dog</param>
    /// <param name="squirrelsChased">How many squirrels the dog has chased over its life</param>
    [JsonConstructor]
    public Dog(string name, string species, Colour colour, string breed, string sex, double weight, double height,
        double speed, double length, int age, string skinType, int squirrelsChased, Guid guid)
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
        this.squirrelsChased = squirrelsChased;
    }


    /// <summary>
    ///     How mny squirrels the dog has chased over its lifetime
    /// </summary>
    public int squirrelsChased { get; set; }

    /// <summary>
    ///     Prints the animals sound (bark)
    /// </summary>
    public override void AnimalSound()
    {
        Console.WriteLine($@"{name} barks at you, and then gives you their toy.");
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