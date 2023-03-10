using System.Text.Json;

namespace Data_Structures.classes
{
    /// <summary>
    /// Object that contains values about food and its stats
    /// </summary>
    public class Food
    {
        /// <summary>
        /// The name of the food as a string
        /// </summary>
        public required string name { get; set; }

        /// <summary>
        /// Caloric value of the food as a double
        /// </summary>
        public double caloricValue { get; set; }

        /// <summary>
        /// Nutritional value of the food as a double
        /// </summary>
        public double nutrients { get; set; }

        /// <summary>
        /// Fat value of the food as a double
        /// </summary>
        public double fat { get; set; }

        /*
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="caloricValue">Caloric value of food</param>
        /// <param name="nutrients">Nutritional  value of food</param>
        /// <param name="fat">Fat value of the food</param>
        /// <param name="name">Name of the food</param>
        public Food(string name, int caloricValue, int nutrients, int fat)
        {
            this.name = name;
            this.caloricValue = caloricValue;
            this.nutrients = nutrients;
            this.fat = fat;
        }
        */

        /// <summary>
        /// Increases the weight based on the food values
        /// </summary>
        /// <returns>Weight increase in kilograms</returns>
        public double WeightIncrease()
        {
            return (nutrients / caloricValue) * fat;
        }

        /// <summary>
        /// Increases the length based on food value
        /// </summary>
        /// <returns>Length increase in cm</returns>
        public double LengthIncrease()
        {
            return (nutrients / caloricValue);
        }

        /// <summary>
        /// Increases the height of the cat based on food value
        /// </summary>
        /// <returns>Height increase in cm</returns>
        public double HeightIncrease()
        {
            return (nutrients / caloricValue);
        }

        /// <summary>
        /// Returns the food information stored as a JSON.
        /// </summary>
        /// <returns>The JSON in a string format</returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Saves the food information in json, to the "/data/food" folder
        ///</summary>
        public void SaveFood()
        {
            string fileName = @$".\data\food\{name}.json";
            string jsonString = ToJson();
            File.WriteAllText(fileName, jsonString);
        }
    }
}
