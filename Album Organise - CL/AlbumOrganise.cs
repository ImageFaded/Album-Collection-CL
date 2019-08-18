using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_Organise___CL
{
    class AlbumOrganise
    {
        static void Main(string[] args)
        {
            Initialise();
        }

        static void Initialise()
        {
            MenuDisplay();
            UserInput();
        }

        static void MenuDisplay()
        {
            Console.Clear();
            Console.WriteLine("Album Organisation Tool\n");
            Console.WriteLine("1: Search Artist");
            Console.WriteLine("2: Search Album");
            Console.WriteLine("3: Add Artist");
            Console.WriteLine("4: Delete Artist");
            Console.WriteLine("5: Quit\n");
            Console.WriteLine("Select An Option");
        }

        static void UserInput()
        {
            try
            {
                int userInput = Convert.ToInt32(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        ExitProgram(false);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nMalformed Input: Please Enter Again");
            }
        }

        static void ExitProgram(bool InputError)
        {
            Console.Clear();
            Console.WriteLine("Are You Sure You Would Like To Exit?");
            Console.WriteLine("(Yes/No)");
            if (InputError)
            {
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
            }
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "YES":
                case "Yes":
                case "yes":
                case "Y":
                case "y":
                    Environment.Exit(0);
                    break;
                case "NO":
                case "No":
                case "no":
                case "N":
                case "n":
                    Initialise();
                    break;
                default:
                    ExitProgram(true);
                    break;
            }

        }
    }
}
