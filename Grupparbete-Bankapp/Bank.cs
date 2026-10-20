
using System;

namespace Grupparbete_Bankapp
{
    public class Bank
    {
        public string Name { get; private set; }

        public Bank(string name)
        {
            Name = name;
        }

        public void ShowInfo()
        {
            Console.WriteLine("========================");
            Console.WriteLine($"Välkommen till {Name}!");
            Console.WriteLine("========================");
        }
    }
}
