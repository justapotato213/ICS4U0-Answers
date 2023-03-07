using CatNS;

namespace PersonNS
{
    class Person
    {
        /// <summary>
        /// Name of the person as a string (first, middle, last)
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Stores all owned cats as a list
        /// </summary>
        public List<Cat> cats { get; set; }

        public decimal money { get; set; }

        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="name">Name of the person</param>
        /// <param name="cats">List of the cats they own</param>
        public Person(string name, List<Cat> cats)
        {
            this.name = name;
            this.cats = cats;
        }
        
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="name">Name of the person</param>
        /// <param name="cats">List of the cats they own</param>
        /// <param name="money">Persons money</param>
        public Person(string name, List<Cat> cats, decimal money){
            this.name = name;
            this.cats = cats;
            this.money = money;
        }

    }

}