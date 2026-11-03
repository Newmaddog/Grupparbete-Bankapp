
using System;

namespace Grupparbete_Bankapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
           StartBank startBank = new StartBank();

            startBank.Start();
        }
    }
}
