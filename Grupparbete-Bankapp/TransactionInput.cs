using System;
using System.Collections.Generic;
using System.Text;

namespace Grupparbete_Bankapp
{
    // Here I read text and numbers from the user and check if waiting transfers are ready to run.
    internal class TransactionInput
    {
        private TransactionService service;

        public bool EndOfInput { get; set; }

        // Here I save the transaction service so waiting transfers can run while the user types.
        public TransactionInput(TransactionService service)
        {
            this.service = service;
            EndOfInput = false;
        }

        // Here I read one key at a time so transfers can run without waiting for the user to press Enter.
        public string ReadText(string prompt)
        {
            // Check the real clock and finish payments that are due.
            service.ProcessTransactions(DateTime.Now);

            Console.Write(prompt);

            // ReadLine handles input from a file or another program, including the end of input.
            if (Console.IsInputRedirected == true)
            {
                // The question mark means that ReadLine can also return null.
                string? line = Console.ReadLine();

                service.ProcessTransactions(DateTime.Now);

                if (line == null)
                {
                    EndOfInput = true;
                    return "0";
                }

                return line;
            }

            string text = "";
            bool finished = false;

            while (finished == false)
            {
                service.ProcessTransactions(DateTime.Now);

                // Here I only read a key if one has been pressed, so the program can keep checking waiting transfers.
                if (Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    // Enter finishes this line of text.
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        finished = true;
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (text.Length > 0)
                        {
                            // Here I remove the last character from the text when the user presses Backspace.
                            string shorterText = "";

                            for (int i = 0; i < text.Length - 1; i++)
                            {
                                shorterText = shorterText + text[i];
                            }

                            text = shorterText;
                            // Here I move back, cover the last character with a space and move back again.

                            Console.Write("\b \b");
                        }
                    }
                    // Here I add the character the user typed to the text and show it on the screen.
                    else if (key.KeyChar >= ' ')
                    {
                        text = text + key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    // Here I wait 50 milliseconds before checking again so the loop does not run too fast.
                    System.Threading.Thread.Sleep(50);
                }
            }

            return text;
        }

        // Keep asking until the user enters a whole number.
        public int ReadNumber(string prompt)
        {
            int number = 0;
            bool valid = false;

            while (valid == false && EndOfInput == false)
            {
                string text = ReadText(prompt);

                if (EndOfInput == true)
                {
                    return 0;
                }

                try
                {
                    number = int.Parse(text);
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Enter a whole number.");
                }
            }

            return number;
        }

        // Read a number using either a comma or a dot.
        public decimal ReadDecimal(string prompt)
        {
            decimal number = 0;
            bool valid = false;

            while (valid == false && EndOfInput == false)
            {
                string text = ReadText(prompt);

                if (EndOfInput == true)
                {
                    return 0;
                }

                number = 0;

                decimal place = 1;

                bool hasDecimal = false;
                bool hasDigit = false;
                bool negative = false;

                valid = true;

                try
                {
                    for (int i = 0; i < text.Length; i++)
                    {
                        if (text[i] == '-' && i == 0)
                        {
                            negative = true;
                        }
                        else if (text[i] == ',' || text[i] == '.')
                        {
                            if (hasDecimal == true)
                            {
                                valid = false;
                            }

                            hasDecimal = true;
                        }
                        else if (text[i] >= '0' && text[i] <= '9')
                        {
                            // For example, '5' becomes the number 5.
                            int digit = text[i] - '0';

                            hasDigit = true;

                            if (hasDecimal == false)
                            {
                                number = number * 10 + digit;
                            }
                            else
                            {
                                place = place * 10;
                                number = number + digit / place;
                            }
                        }
                        else
                        {
                            valid = false;
                        }
                    }

                    if (hasDigit == false)
                    {
                        valid = false;
                    }

                    if (negative == true)
                    {
                        number = 0 - number;
                    }
                }
                catch
                {
                    valid = false;
                }

                if (valid == false)
                {
                    Console.WriteLine("Enter a number such as 100.50 or 100,50. Write one thousand as 1000.");
                }
            }

            return number;
        }
    }
}

