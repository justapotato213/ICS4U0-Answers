namespace catObject{
    
    /// <summary>
    /// A object that holds the characteristics of a cat, and some functions of a cat
    /// </summary>
    public class Cat {
        /// <summary>
        /// Stores the cats name as a string
        /// </summary> 
        private String _name;

        /// <summary>
        /// Getter and setter for cats name
        /// </summary>
        public String name{
            get => _name;
            set{
                _name = value;
            }
        }
        
        /// <summary> 
        /// Stores the cats colour as a string
        /// </summary> 
        private String _colour;

        /// <summary>
        /// Getter and setter for cats colour
        /// </summary>
        public String colour{
            get => _colour;
            set{
                _colour = value;
            }
        }

        /// <summary>
        /// Stores the cats weight (kg) as a double
        /// </summary> 
        private double _weight;

        /// <summary>
        /// Getter and setter for cats weight
        /// </summary>
        public double weight{
            get => _weight;
            set{
                _weight = value;
            }
        }

        /// <summary>
        /// Stores the cats height (cm) as a double
        /// </summary>
        private double _height;

        /// <summary>
        /// Getter and setter for cats height
        /// </summary>
        public double height{
            get => _height;
            set{
                _height = value;
            }
        }
        
        /// <summary>
        /// Stores the cats speed (km/h) as a double
        /// </summary>
        private double _speed;

        /// <summary>
        /// Getter and setter for cats speed
        /// </summary>
        public double speed{
            get => _speed;
            set{
                _speed = value;
            }
        }

        /// <summary>
        /// Stores the cats length (cm) as a double
        /// </summary>
        private double _length;

        /// <summary>
        /// Getter and setter for cats length
        /// </summary>
        public double length{
            get => _length;
            set{
                _length = value;
            }
        }

        /// <summary>
        /// Stores the cats age as an integer
        /// </summary>
        private int _age;

        /// <summary>
        /// Getter and setter for cats age
        /// </summary>
        public int age{
            get => _age;
            set{
                _age = value;
            }
        }

        /// <summary> 
        /// The class contructor 
        /// </summary>
        /// <param name="name"> Initial name of the cat </param>
        /// <param name="colour"> Initial colour of the cat </param>
        public Cat (string name, string colour){
            this._name = name;
            this._colour = colour;
            this._weight = 4.5;
            this._height = 24.00;
            this._speed = 48.00;
            this._length = 46;
            this._age = 3;
        } 

        /// <summary>
        /// The class constructor
        /// </summary>
        /// <param name="name"> Initial name of the cat </param>
        /// <param name="colour"> Initial colour of the cat </param>
        /// <param name="weight"> Initial weight of the cat </param>
        /// <param name="height"> Initial height of the cat </param>
        /// <param name="speed"> Initial speed of the cat </param>
        /// <param name="length"> Initial length of the cat </param>
        /// <param name="age"> Initial age of the cat </param>
        public Cat (string name, string colour, double weight, double height, double speed, double length, int age){
            this._name = name;
            this._colour = colour;
            this._weight = weight;
            this._height = height;
            this._speed = speed;
            this._length = length;
            this._age = age;
        }

        /// <summary>
        /// Increase the age by 1
        /// </summary>
        public void ageUp(){
            this._age += 1;
        }

    }
}