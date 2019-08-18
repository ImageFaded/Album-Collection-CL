using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_Organise___CL
{
    class AlbumOrganise
    {
        List<Album> albumList = new List<Album>();

        static void Main(string[] args)
        {
            LoadAlbums();
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
            Console.WriteLine("3: Add Album");
            Console.WriteLine("4: Delete Album");
            Console.WriteLine("5: Quit\n");
            Console.WriteLine("Select An Option: ");
        }

        static void UserInput()
        {
            try
            {
                int userInput = Convert.ToInt32(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        SearchArtist();
                        break;
                    case 2:
                        SearchAlbum();
                        break;
                    case 3:
                        AddAlbum();
                        break;
                    case 4:
                        DeleteAlbum();
                        break;
                    case 5:
                        ExitProgram(false);
                        break;
                    default:
                        Console.WriteLine("Input Not Recognised: Please Enter Again");
                        Initialise();
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nMalformed Input: Please Enter Again");
            }
        }

        static void SearchArtist()
        {
            Console.Clear();
            Console.WriteLine("Type In The Name Of An Artist:");
            string input = Console.ReadLine();
            Console.WriteLine("Click Any Key To Return To Menu");
            Console.ReadKey();
            Initialise();
        }

        static void SearchAlbum()
        {

        }

        static void AddAlbum()
        {

        }
        
        static void DeleteAlbum()
        {

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

        static void LoadAlbums()
        {
            //Add in JSON
        }
    }
}
