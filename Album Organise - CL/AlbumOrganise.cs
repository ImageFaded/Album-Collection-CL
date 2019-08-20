using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Album_Organise___CL
{
    class AlbumOrganise
    {
        static List<Album> albumList = new List<Album>();

        static void Main(string[] args)
        {
            LoadAlbums();
            SortAlbums();
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
            Console.WriteLine("5: Quit");
            Console.WriteLine("6: Show Albums (Debug)\n");
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
                    case 6:
                        DisplayAllAlbums();
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
                Initialise();
            }
        }

        static void SearchArtist()
        {
            Console.Clear();
            int albumCount = 0;
            Console.WriteLine("Type In The Name Of An Artist:");
            string input = Console.ReadLine();
            foreach(Album alb in albumList)
            {
                if(alb.ArtistName == input)
                {
                    Console.WriteLine("Artist: " + alb.ArtistName);
                    Console.WriteLine("Album: " + alb.AlbumName + "\n");
                    albumCount++;
                }
            }
            Console.WriteLine("Album Count: " + albumCount);
            Console.WriteLine("Click Any Key To Return To Menu");
            Console.ReadKey();
            Initialise();
        }

        static void SearchAlbum()
        {

        }

        static void AddAlbum()
        {
            Console.Clear();
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();
            albumList.Add(new Album(ArtistName, AlbumName));
            SortAlbums();
            SaveJSON();
            Initialise();
        }
        
        static string InputArtist()
        {
            Console.Clear();
            string inputArtist;
            Console.WriteLine("Type In The Name Of The Artist: ");
            inputArtist = Console.ReadLine();
            while(inputArtist == "" || inputArtist == null)
            {
                Console.Clear();
                Console.WriteLine("Type In The Name Of The Artist: \n");
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
                inputArtist = Console.ReadLine();
            }
            return inputArtist;
            
        }

        static string InputAlbum()
        {
            Console.Clear();
            string inputAlbum;
            Console.WriteLine("Type In The Name Of The Album: ");
            inputAlbum = Console.ReadLine();
            while(inputAlbum == "" || inputAlbum == null)
            {
                Console.Clear();
                Console.WriteLine("Type In The Name Of The Album: \n");
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
                inputAlbum = Console.ReadLine();
            }
            return inputAlbum;
        }

        static void DeleteAlbum()
        {
            Console.Clear();
            bool ArtistRemoved = false;
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();
            foreach(Album album in albumList)
            {
                if(album.ArtistName == ArtistName && album.AlbumName == AlbumName)
                {
                    albumList.Remove(album);
                    ArtistRemoved = true;
                } 
            }

            if (!ArtistRemoved)
            {
                Console.WriteLine("\nNot Found!");
            } 
            else
            {
                Console.WriteLine("\nRemoved!");
            }
            Console.ReadKey();
            Initialise();

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
                    SaveJSON();
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

        static void SaveJSON()
        {
            string jsonSave = JsonConvert.SerializeObject(albumList);
            //var path = "Test";
            File.WriteAllText(@"Albums.json", jsonSave);
        }

        static void LoadAlbums()
        {
            //Add in JSON
            string jsonLoad = File.ReadAllText(@"Albums.Json");
            albumList = JsonConvert.DeserializeObject<List<Album>>(jsonLoad);
        }

        static void DisplayAllAlbums()
        {
            Console.Write("\n");
            foreach(Album alb in albumList)
            {
                if(alb.AlbumName != null)
                {
                    Console.WriteLine("Artist Name: " + alb.ArtistName);
                    Console.WriteLine("Album Name: " + alb.AlbumName + "\n");
                }
                 
            }
            Console.ReadKey();
            Initialise();
        }

        static void SortAlbums()
        {
            albumList.Sort(delegate (Album a1, Album a2) { return a1.ArtistName.CompareTo(a2.ArtistName); });
        }
    }
}
