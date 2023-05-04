using System.Text.Json;

namespace Data_Structures.classes;

/// <summary>
///     An object which holds all the common functions and characteristics of an animal.
/// </summary>
public abstract class Animal
{
    /// <summary>
    ///     Guid of the animal
    /// </summary>
    public Guid guid { get; set; }

    /// <summary>
    ///     Name of the animal
    /// </summary>
    public string name { get; set; }

    /// <summary>
    ///     Species of the animal
    /// </summary>
    public string species { get; set; }

    /// <summary>
    ///     Colour of the animal
    /// </summary>
    public Colour colour { get; set; }

    /// <summary>
    ///     Breed of animal
    /// </summary>
    public string breed { get; set; }

    /// <summary>
    ///     Sex of the animal
    /// </summary>
    public string sex { get; set; }

    /// <summary>
    ///     Stores the animals weight (kg) as a double
    /// </summary>
    public double weight { get; set; }

    /// <summary>
    ///     Stores the animals height (cm) as a double
    /// </summary>
    public double height { get; set; }

    /// <summary>
    ///     Stores the animals speed (km/h) as a double
    /// </summary>
    public double speed { get; set; }

    /// <summary>
    ///     Stores the animals length (cm) as a double
    /// </summary>
    public double length { get; set; }

    /// <summary>
    ///     Stores the animals age as an integer
    /// </summary>
    public int age { get; set; }

    /// <summary>
    ///     Stores the animals skin type, such as fur or scales
    /// </summary>
    public string skinType { get; set; }

    /// <summary>
    ///     Returns the a description of the animal in a string format.
    /// </summary>
    /// <returns>A description of the animal in a string format.</returns>
    public string Description()
    {
        return
            $"{name} is a {breed} {species}, who is {age} years old. They can run at {Math.Round(speed)}km/h, weighs {Math.Round(weight)}kg, is {Math.Round(length)}cm long and is {Math.Round(height)}cm tall. They have {colour.Description()} {skinType}.";
    }

    /// <summary>
    ///     Simulates an animal eating, increasing weight, length and height, but decreasing speed
    /// </summary>
    /// <param name="food">Food to feed to the animal</param>
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
    ///     Simulates a animal running, decreasing its weight and increasing speed, length, and height
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
    ///     Returns the animal information stored as a JSON.
    /// </summary>
    /// <returns>The JSON in a string format</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    ///     Saves the animals information in json, to the "/data/{species}" folder
    /// </summary>
    public abstract void SaveAnimal();


    /// <summary>
    ///     Prints the sound the animal makes. (E.G. meow, woof, etc.)
    /// </summary>
    public abstract void AnimalSound();
}