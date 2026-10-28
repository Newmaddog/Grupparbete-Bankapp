using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    public static class Constants
    {
        // Here I show the user that 0 means cancel.
        public static void WriteExitText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("0. Cancel");
            Console.ResetColor();
        }
    }
}
