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
            Console.WriteLine("5: Show Albums");
            Console.WriteLine("6: Quit\n");
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
                        DisplayAllAlbums();
                        break;
                    case 6:
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
                    DisplayInformation(alb);
                    albumCount++;
                }
            }
            MenuPrompt();
            Initialise();
        }

        static void SearchAlbum()
        {
            Console.Clear();
            Console.WriteLine("Type In The Name Of An Album: ");
            string input = Console.ReadLine();
            foreach(Album alb in albumList)
            {
                if(alb.AlbumName == input)
                {
                    DisplayInformation(alb);
                }
            }
        }

        static void AddAlbum()
        {
            //
            Console.Clear();
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();
            albumList.Add(new Album(ArtistName, AlbumName));
            SortAlbums();
            SaveJSON();
            Initialise();
            //
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
            File.WriteAllText(ReturnLocation(), jsonSave);
        }

        static void LoadAlbums()
        {
            //Add in JSON
            if (!File.Exists(ReturnLocation()))
            {
                File.Create(ReturnLocation());
                string jsonSave = JsonConvert.SerializeObject(albumList);
                File.WriteAllText(ReturnLocation(), jsonSave);
            }
            string jsonLoad = File.ReadAllText(ReturnLocation());
            albumList = JsonConvert.DeserializeObject<List<Album>>(jsonLoad);
            SortAlbums();
        }

        static string ReturnLocation()
        {
            return @"Albums.Json";
        }

        static void DisplayAllAlbums()
        {
            Console.Clear();
            foreach(Album alb in albumList)
            {
                if(alb.AlbumName != null)
                {
                    Console.WriteLine("Artist Name: " + alb.ArtistName);
                    Console.WriteLine("Album Name: " + alb.AlbumName + "\n");
                }
                 
            }
            MenuPrompt();
            Initialise();
        }

        static void SortAlbums()
        {
            if(albumList.Count >= 4)
            {
                albumList.Sort(delegate (Album a1, Album a2) { return a1.ArtistName.CompareTo(a2.ArtistName); });
            }
        }

        static void DisplayInformation(Album alb)
        {
            Console.WriteLine("Artist: " + alb.ArtistName);
            Console.WriteLine("Album: " + alb.AlbumName + "\n");
        }

        static void MenuPrompt()
        {
            Console.WriteLine("\nClick Any Key To Return To Menu");
            Console.ReadKey();
            Initialise();
        }
    }
}
