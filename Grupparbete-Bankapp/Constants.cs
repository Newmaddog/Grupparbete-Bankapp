using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    public static class Constants
    {
        public static readonly string[] NotUsableWords = ["Exit", "Cancel"];
        public static bool IsNotUsableWordsUtilize(string text)
        {
            if (Constants.NotUsableWords.Contains(text, StringComparer.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        public static void WriteExitText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("To cancel, type Exit or Cancel.");
            Console.ResetColor();
        }
    }
}
