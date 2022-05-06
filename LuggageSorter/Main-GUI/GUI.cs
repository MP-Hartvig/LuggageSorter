using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorter
{
    class GUI
    {
        public void StartMenu()
        {
            Manager mana = new Manager();

            // Bool to control the menu
            bool startMenu = true;
            while (startMenu)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("               Luggage sorting system");
                Console.WriteLine("==================================================\n");
                Console.WriteLine("1. Start the system");
                Console.WriteLine("2. Open/Close terminals");
                Console.WriteLine("3. Open/Close terminals");
                Console.WriteLine("4. Change destinations");
                Console.WriteLine("5. Exit");
                Console.Write("\r\nEnter a number: ");
                

                // Switch case for each menu point
                switch (Console.ReadLine())
                {
                    // Creates a journal
                    case "1":
                        Console.WriteLine("==================================================\n");
                        mana.Initializer();
                        break;
                    // Loads a journal
                    case "2":
                        Console.WriteLine("==================================================\n");

                        break;
                    // Loads a journal
                    case "3":
                        Console.WriteLine("==================================================\n");

                        break;
                    // Loads a journal
                    case "4":
                        Console.WriteLine("==================================================\n");

                        break;
                    // Exits the program
                    case "5":
                        mana.Terminator();
                        startMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
