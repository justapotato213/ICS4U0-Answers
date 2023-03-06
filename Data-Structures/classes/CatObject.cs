using System.Text.Json;

namespace catObject
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
        public string colour { get; set; }

        /// <summary>
        /// Stores the cats breed as a string
        /// </summary>
        public string breed { get;  private set; }

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
        /// The class contructor 
        /// </summary>
        /// <param name="name"> Initial name of the cat </param>
        /// <param name="colour"> Initial colour of the cat </param>
        /// <param name="breed"> Breed of the cat </param>
        /// <param name="sex"> Sex of the cat </param>
        public Cat(string name, string colour, string breed, string sex)
        {
            this.name = name;
            this.colour = colour;
            this.breed = breed;
            this.sex = sex;
            this.weight = 4.5;
            this.height = 24.00;
            this.speed = 48.00;
            this.length = 46;
            this.age = 3;
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
        public Cat(string name, string colour, string breed, string sex, double weight, double height, double speed, double length, int age)
        {
            this.name = name;
            this.colour = colour;
            this.breed = breed;
            this.sex = sex;
            this.weight = weight;
            this.height = height;
            this.speed = speed;
            this.length = length;
            this.age = age;
        }

        /// <summary>
        /// Returns a description of a cat in a string format.
        /// </summary>
        /// <returns>The description of the cat.</returns>
        public string description()
        {
            return $"{name} is a {breed} cat, who is {age} years old. They can run at {speed}km/h, weights {weight}kg, is {length}cm long and is {height}cm tall";
        }

        /// <summary>
        /// Simulates a cat eating, increasing weight, length and height, but decreasing speed
        /// </summary>
        public void eat(){
            this.weight += 0.05;
            this.speed -= 0.05;
            this.length += 0.05;
            this.height += 0.05;
        }
        
        /// <summary>
        /// Simulates a cat running, decreasing its weight, but increasing speed, length, and height
        /// </summary>
        public void run(){
            this.weight -= 0.05;
            this.speed += 0.05;
            this.length += 0.05;
            this.height += 0.05;
        }
        
        /// <summary>
        /// Increase the age by 1
        /// </summary>
        public void ageUp()
        {
            this.age += 1;
        }

        /// <summary>
        /// Returns the cats information stored as a JSON. 
        /// </summary>
        /// <returns>The JSON in a string format</returns>
        public string toJson(){
            return JsonSerializer.Serialize(this);
        }
    }
}