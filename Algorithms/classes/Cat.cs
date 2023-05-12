using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Algorithms.classes;

/// <summary>
///     A object that holds the characteristics of a cat, and some functions of a cat
/// </summary>
public class Cat
{
    /// <summary>
    ///     The class constructor
    /// </summary>
    public Cat(string name, Colour colour)
    {
        // random number generator
        var rnd = new Random();

        string[] breeds =
        {
            "Siamese", "British Shorthair", "Maine Coon", "Persian", "Ragdoll", "American Shorthair",
            "Scottish Fold"
        };
        string[] sexs = { "Male", "Female" };

        

        this.name = name;

        // choose random breed from list
        breed = breeds[rnd.Next(breeds.Length)];
        // choose random colour from list
        this.colour = colour;
        // choose random sex
        sex = sexs[rnd.Next(sexs.Length)];

        // choose random age
        age = rnd.Next(0, 10);
        // check if its a kitten
        if (age == 0)
        {
            // custom stats
            weight = rnd.NextDouble() * 1 + 1;
            height = rnd.NextDouble() * 5 + 2;
            length = rnd.NextDouble() * 6 + 3;
            width = rnd.NextDouble() * 3 + 4;
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
            width = rnd.NextDouble() * 5 + 20;
            speed = height * length * 0.05 - weight;
            // check if its a negative number, if it is set it to 1km/h
            if (speed <= 0) speed = 1;
        }
        
        // calculate the volume
        volume = length * width * height;
    }

    /// <summary>
    ///     Stores the cats name as a string
    /// </summary>
    public string name { get; set; }

    /// <summary>
    ///     Stores the cats colour as a string
    /// </summary>
    public Colour colour { get; set; }

    /// <summary>
    ///     Stores the cats breed as a string
    /// </summary>
    public string breed { get; }

    /// <summary>
    ///     Stores the cats sex as a string
    /// </summary>
    public string sex { get; }

    /// <summary>
    ///     Stores the cats weight (kg) as a double
    /// </summary>
    public double weight { get; set; }

    /// <summary>
    ///     Stores the cats height (cm) as a double
    /// </summary>
    public double height { get; set; }

    /// <summary>
    ///     Stores the cats speed (km/h) as a double
    /// </summary>
    public double speed { get; set; }

    /// <summary>
    ///     Stores the cats length (cm) as a double
    /// </summary>
    public double length { get; set; }

    /// <summary>
    ///     Stores the cats age as an integer
    /// </summary>
    
    public int age { get; set; }
    
    /// <summary>
    ///     Stores the cats volume (cm^3) as an double 
    /// </summary>
    public double volume { get; set; }
    
    /// <summary>
    ///     Width of the cat in cm
    /// </summary>
    public double width { get; set; }

    /// <summary>
    ///     Returns a description of a cat in a string format.
    /// </summary>
    /// <returns>The description of the cat.</returns>
    public string Description()
    {
        return
            $"{name} is a {breed} cat, who is {age} years old. They can run at {Math.Round(speed)}km/h, weighs {Math.Round(weight)}kg, is {Math.Round(length)}cm long and is {Math.Round(height)}cm tall. They have {colour.Description()} fur.";
    }

    /// <summary>
    ///     Simulates a cat eating, increasing weight, length and height, but decreasing speed
    /// </summary>
    /// <param name="food">Food to feed to the cat</param>
    public void Eat(Food food)
    {
        weight += food.WeightIncrease();
        length += food.LengthIncrease();
        height += food.HeightIncrease();

        // recalculate speed based on new weight, length and height
        speed = height * length * 0.05 - weight;
        // check if its a negative number, if it is set it to 1km/h
        if (speed <= 0) speed = 1;
    }

    /// <summary>
    ///     Simulates a cat running, decreasing its weight, but increasing speed, length, and height
    /// </summary>
    public void Run()
    {
        weight -= 0.05;
        speed += 0.05;
        length += 0.05;
        height += 0.05;
    }

    /// <summary>
    ///     Increase the age by 1
    /// </summary>
    public void AgeUp()
    {
        age += 1;
    }

    /// <summary>
    ///     Returns the cats information stored as a JSON.
    /// </summary>
    /// <returns>The JSON in a string format</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    ///     Saves the cats information in json, to the "/data/cats" folder
    /// </summary>
    public void SaveCat()
    {
        var fileName = @$".\data\cats\{name}.json";
        var jsonString = ToJson();
        File.WriteAllText(fileName, jsonString);
    }
}