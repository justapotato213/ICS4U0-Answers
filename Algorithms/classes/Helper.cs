namespace Algorithms.classes;

/// <summary>
///     Contains various helper functions.
/// </summary>
public class Helper
{
    /// <summary>
    ///     Given a list of options, the user will choose an option.
    /// </summary>
    /// <returns>Which option the user returned, as an int.</returns>
    public int UserOptionsList(List<string> optionsList)
    {
        // display the options
        for (var i = 0; i < optionsList.Count; i++) Console.WriteLine($@"{i + 1}. {optionsList[i]}");

        // ask the user for which option they want, validating and ensuring input works
        var selection = -1;
        var validInput = false;
        while (!validInput)
            // check if its a num
            try
            {
                selection = Convert.ToInt32(Console.ReadLine());

                // check if its a valid num
                if (selection >= 1 && selection <= optionsList.Count)
                    validInput = true;
                else
                    // tell user its wrong
                    Console.WriteLine($"{selection} was not a valid option!");
            }
            catch (FormatException)
            {
                // tell user its wrong
                Console.WriteLine("Please enter a valid option.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Please enter a valid option.");
            }

        return selection;
    }
}