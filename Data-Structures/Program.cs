using System;
using System.Collections.Generic;
using CatNS;
using FoodNS;
using ColourNS;
using PersonNS;


namespace CatSim
{
    class MainClass
    {
        static void Main(string[] args)
        {
            List<Cat> cats = new List<Cat>();

            Colour colour = new Colour("spotted", "yellow", "black");
            Cat cat = new Cat("Bagles", colour, "Calico", "Male", 5, 20, 14, 20, 5);

            cats.Add(cat);

            Person person = new Person("John Smith", cats, 900);

            Console.WriteLine(person.cats[0].Description());

            Food bagel = new Food(150, 20, 9);
            person.cats[0].Eat(bagel);
        }
    }
}