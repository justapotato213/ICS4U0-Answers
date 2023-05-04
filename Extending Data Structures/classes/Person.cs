using System.Text.Json;
using System.Text.Json.Serialization;

namespace Data_Structures.classes;

public class Person
{
    /// <summary>
    ///     Class Constructor
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="cats">List of the cats they own</param>
    /// <param name="dogs">Lists of the dogs they own </param>
    /// <param name="money">Persons money</param>
    [JsonConstructor]
    public Person(string name, List<Cat> cats, List<Dog> dogs, decimal money)
    {
        this.name = name;
        this.cats = cats;
        this.dogs = dogs;

        pets = new List<Animal>();
        pets.AddRange(cats);
        pets.AddRange(dogs);

        this.money = money;
        foods = new List<Food>();
    }

    /// <summary>
    ///     Name of the person as a string (first, middle, last)
    /// </summary>
    public string name { get; }

    /// <summary>
    ///     Stores all owned pets as a list
    /// </summary>
    [JsonIgnore]
    public List<Animal> pets { get; set; }

    /// <summary>
    ///     Stores all cats as a list
    /// </summary>
    public List<Cat> cats { get; set; }

    /// <summary>
    ///     Stores all pets as a list
    /// </summary>
    public List<Dog> dogs { get; set; }

    /// <summary>
    ///     Stores a persons money, as a decimal
    /// </summary>
    public decimal money { get; set; }

    public List<Food> foods { get; set; }

    /// <summary>
    ///     Returns person information in a JSON
    /// </summary>
    /// <returns>Person information in JSON, as a string</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    ///     Saves the person information in json, to the "/data/people" folder
    /// </summary>
    public void SavePerson()
    {
        var fileName = @".\data\people\player.json";
        var jsonString = ToJson();
        File.WriteAllText(fileName, jsonString);
    }
}