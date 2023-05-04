using System.Text.Json;

namespace Data_Structures.classes;

/// <summary>
///     Stores colour information (NON RGB)
/// </summary>
public class Colour
{
    /// <summary>
    ///     Class constructor
    /// </summary>
    /// <param name="pattern">Pattern of the colours (ex. spotted)</param>
    /// <param name="primary">Primary colour </param>
    /// <param name="secondary">Secondary colour</param>
    public Colour(string pattern, string primary, string secondary)
    {
        this.pattern = pattern;
        this.primary = primary;
        this.secondary = secondary;
    }

    /// <summary>
    ///     Stores the pattern of the colour as a string (ex. spotted)
    /// </summary>
    public string pattern { get; set; }

    /// <summary>
    ///     Stores the primary colour as a string
    /// </summary>
    public string primary { get; set; }

    /// <summary>
    ///     Stores the secondary colour as a string
    /// </summary>
    public string secondary { get; set; }


    /// <summary>
    ///     Gives a description of the colour in a string format
    /// </summary>
    /// <returns>A string containing the description</returns>
    public string Description()
    {
        if (primary != secondary)
            return $"{primary}-{secondary} {pattern}";
        return $"{primary}";
    }

    /// <summary>
    ///     Returns the cats information stored as a JSON.
    /// </summary>
    /// <returns>The cats information stored as a JSON, in a string.</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    /// <summary>
    ///     Saves the colour information as a JSON
    /// </summary>
    public void SaveColour()
    {
        var fileName = @$".\data\colours\{primary}-{secondary}-{pattern}.json";
        var jsonString = ToJson();
        File.WriteAllText(fileName, jsonString);
    }
}