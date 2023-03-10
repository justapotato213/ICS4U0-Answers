using System.Text.Json;
using System.Text.Json.Serialization;


namespace Data_Structures.classes
{
    /// <summary>
    /// A object that holds the characteristics of a cat, and some functions of a cat
    /// </summary>
    public class Cat
    {
        /// <summary>
        /// Stores the cats name as a string
        /// </summary> 
        public string name { get; set; }

        /// <summary> 
        /// Stores the cats colour as a string
        /// </summary> 
        public Colour colour { get; set; }

        /// <summary>
        /// Stores the cats breed as a string
        /// </summary>
        public string breed { get; private set; }

        /// <summary>
        /// Stores the cats sex as a string
        /// </summary>
        public string sex { get; private set; }

        /// <summary>
        /// Stores the cats weight (kg) as a double
        /// </summary> 
        public double weight { get; set; }

        /// <summary>
        /// Stores the cats height (cm) as a double
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// Stores the cats speed (km/h) as a double
        /// </summary>
        public double speed { get; set; }

        /// <summary>
        /// Stores the cats length (cm) as a double
        /// </summary>
        public double length { get; set; }

        /// <summary>
        /// Stores the cats age as an integer
        /// </summary>
        public int age { get; set; }

        /// <summary>
        /// The class constructor
        /// </summary>
        public Cat()
        {
            // random number generator
            Random rnd = new Random();

            string[] breeds =
            {
                "Siamese", "British Shorthair", "Maine Coon", "Persian", "Ragdoll", "American Shorthair",
                "Scottish Fold"
            };
            string[] sexs = {"Male", "Female"};

            List<Colour> colours = new List<Colour>();
            // load colour information from file
            foreach (string file in Directory.GetFiles(@".\data\colours"))
            {
                // load json from file
                string jsonString = File.ReadAllText(@$".\{file}");
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
                // custom stats
                weight = rnd.NextDouble() * 1 + 1;
                height = rnd.NextDouble() * 5 + 2;
                length = rnd.NextDouble() * 6 + 3;
                speed = Math.Abs((height * length * 0.05) - weight);
            }
            else
            {
                // custom stats
                weight = rnd.NextDouble() * 3 + 2;
                height = rnd.NextDouble() * 6 + 23;
                length = rnd.NextDouble() * 10 + 40;
                speed = (height * length * 0.05) - weight;
            }
        }

        /// <summary> 
        /// The class contructor 
        /// </summary>
        /// <param name="name"> Initial name of the cat </param>
        /// <param name="colour"> Initial colour of the cat </param>
        /// <param name="breed"> Breed of the cat </param>
        /// <param name="sex"> Sex of the cat </param>
        public Cat(string name, Colour colour, string breed, string sex)
        {
            this.name = name;
            this.colour = colour;
            this.breed = breed;
            this.sex = sex;
            weight = 4.5;
            height = 24.00;
            speed = (height * length * 0.05) - weight;
            length = 46;
            age = 3;
        }

        /// <summary>
        /// The class constructor
        /// </summary>
        /// <param name="name"> Initial name of the cat </param>
        /// <param name="colour"> Initial colour of the cat </param>
        /// <param name="breed"> Breed of the cat </param>
        /// <param name="sex"> Sex of the cat </param>
        /// <param name="weight"> Initial weight of the cat </param>
        /// <param name="height"> Initial height of the cat </param>
        /// <param name="speed"> Initial speed of the cat </param>
        /// <param name="length"> Initial length of the cat </param>
        /// <param name="age"> Initial age of the cat </param>
        [JsonConstructor]
        public Cat(string name, Colour colour, string breed, string sex, double weight, double height, double speed,
            double length, int age)
        {
            this.name = name;
            this.colour = colour;
            this.breed = breed;
            this.sex = sex;
            this.weight = weight;
            this.height = height;
            this.speed = (height * length * 0.05) - weight;
            this.length = length;
            this.age = age;
        }

        /// <summary>
        /// Returns a description of a cat in a string format.
        /// </summary>
        /// <returns>The description of the cat.</returns>
        public string Description()
        {
            return
                $"{name} is a {breed} cat, who is {age} years old. They can run at {Math.Round(speed)}km/h, weighs {Math.Round(weight)}kg, is {Math.Round(length)}cm long and is {Math.Round(height)}cm tall. They have {colour.Description()} fur.";
        }

        /// <summary>
        /// Simulates a cat eating, increasing weight, length and height, but decreasing speed
        /// </summary>
        /// <param name="food">Food to feed to the cat</param>
        public void Eat(Food food)
        {
            weight += food.WeightIncrease();
            length += food.LengthIncrease();
            height += food.HeightIncrease();

            // recalculate speed based on new weight, length and height
            speed = (height * length * 0.05) - weight;
        }

        /// <summary>
        /// Simulates a cat running, decreasing its weight, but increasing speed, length, and height
        /// </summary>
        public void Run()
        {
            weight -= 0.05;
            speed += 0.05;
            length += 0.05;
            height += 0.05;
        }

        /// <summary>
        /// Increase the age by 1
        /// </summary>
        public void AgeUp()
        {
            age += 1;
        }

        /// <summary>
        /// Returns the cats information stored as a JSON. 
        /// </summary>
        /// <returns>The JSON in a string format</returns>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Saves the cats information in json, to the "/data/cats" folder
        ///</summary>
        public void SaveCat()
        {
            string fileName = @$".\data\cats\{name}.json";
            string jsonString = ToJson();
            File.WriteAllText(fileName, jsonString);
        }
    }
}