using System;
using System.Collections.Generic;

namespace catSimulator
{
    class MainClass
    {
        static void Main(string[] args)
        {
            List<catObject.Cat> cats = new List<catObject.Cat>();

            catObject.Cat cat = new catObject.Cat("Cat Name", "Orange", "Calico", "Male", 5, 20, 30, 10, 1);
            Console.WriteLine(cat.description());

            cats.Add(cat);

            

        }
    }
    
}