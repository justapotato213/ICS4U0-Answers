using System.Text.Json;
using System.Text.Json.Serialization;

namespace Algorithms.classes;

public class Person
{
    /// <summary>
    ///     Class Constructor
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="cats">List of the cats they own</param>
    public Person(string name, List<Cat> cats)
    {
        this.name = name;
        this.cats = cats;
        foods = new List<Food>();
    }

    /// <summary>
    ///     Class Constructor
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="cats">List of the cats they own</param>
    /// <param name="money">Persons money</param>
    [JsonConstructor]
    public Person(string name, List<Cat> cats, decimal money)
    {
        this.name = name;
        this.cats = cats;
        this.money = money;
        foods = new List<Food>();
    }

    /// <summary>
    ///     Name of the person as a string (first, middle, last)
    /// </summary>
    public string name { get; private set; }

    /// <summary>
    ///     Stores all owned cats as a list
    /// </summary>
    public List<Cat> cats { get; set; }

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
        var fileName = @$".\data\people\{name}.json";
        var jsonString = ToJson();
        File.WriteAllText(fileName, jsonString);
    }
}