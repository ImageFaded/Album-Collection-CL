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
        static List<Artist> artistList = new List<Artist>();

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
            Console.WriteLine("5: Show Artists");
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
                        DisplayAllArtists();
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
            //int albumCount = 0;
            Console.WriteLine("Type In The Name Of An Artist: ");
            string input = Console.ReadLine();
            foreach(Artist art in artistList)
            {
                if(art.ArtistName == input)
                {
                    DisplayInformation(art,null);
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
            foreach(Artist art in artistList)
            {
                if(art.ArtistName != null)
                {
                    foreach (Album alb in art.ReturnAlbums())
                    {
                        if (alb.AlbumName == input)
                        {
                            DisplayInformation(art, alb);
                        }
                    }
                }
            }
            MenuPrompt();
            Initialise();
        }

        static void AddAlbum()
        {
            Console.Clear();
            Artist artistStore = null;
            bool inList = false;
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();
            foreach(Artist art in artistList)
            {
                if(art.ArtistName == ArtistName)
                {
                    artistStore = art;
                    inList = true;
                    break;
                }
            }

            if (inList)
            {
                artistStore.AddArtist(new Album(AlbumName));
            }
            else
            {
                artistList.Add(new Artist(ArtistName, new List<Album> { new Album(AlbumName) }));
            }

            SortArtists();
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
            Artist tempArt = null;
            Album tempAlb = null;
            bool ArtistRemoved = false;
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();

            foreach(Artist art in artistList)
            {
                if(art.ArtistName != null)
                {
                    foreach (Album alb in art.ReturnAlbums())
                    {
                        if (art.ArtistName == ArtistName && alb.AlbumName == AlbumName)
                        {
                            tempArt = art;
                            tempAlb = alb;
                            ArtistRemoved = true;
                        }
                    }
                }
            }

            if (!ArtistRemoved)
            {
                Console.WriteLine("\nNot Found!");
            } 
            else
            {
                tempArt.RemoveAlbum(tempAlb);
                if(tempArt.ReturnAlbums().Count() == 0)
                {
                    artistList.Remove(tempArt);
                }
                Console.WriteLine("\nRemoved!");
            }
            SaveJSON();
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
            string jsonSave = JsonConvert.SerializeObject(artistList);
            File.WriteAllText(ReturnLocation(), jsonSave);
        }

        static void LoadAlbums()
        {
            if (!File.Exists(ReturnLocation()))
            {
                File.Create(ReturnLocation());
                string jsonSave = JsonConvert.SerializeObject(artistList);
                File.WriteAllText(ReturnLocation(), jsonSave);
            }
            string jsonLoad = File.ReadAllText(ReturnLocation());
            artistList = JsonConvert.DeserializeObject<List<Artist>>(jsonLoad);
            SortArtists();
        }

        static string ReturnLocation()
        {
            return @"Artists.Json";
        }

        static void DisplayAllArtists()
        {
            Console.Clear();
            foreach(Artist art in artistList)
            {
                if (art.ArtistName != null)
                {
                    Console.WriteLine("Artist Name: " + art.ArtistName);
                    foreach(Album album in art.ReturnAlbums())
                    {
                        Console.WriteLine(" Album Name: " + album.AlbumName);
                    }
                }
            }
            MenuPrompt();
            Initialise();
        }

        static void SortArtists()
        {
            if(artistList.Count >= 5)
            {
                artistList.Sort(delegate (Artist a1, Artist a2) { return a1.ArtistName.CompareTo(a2.ArtistName); });
            }
        }

        static void DisplayInformation(Artist art, Album album)
        {
            Console.WriteLine("\nArtist: " + art.ArtistName);
            if(album == null)
            {
                foreach (Album alb in art.ReturnAlbums())
                {
                    Console.WriteLine(" Album: " + alb.AlbumName);
                }
            }
            else
            {
                Console.WriteLine(" Album: " + album.AlbumName);
            }

        }

        static void MenuPrompt()
        {
            Console.WriteLine("\nClick Any Key To Return To Menu");
            Console.ReadKey();
            Initialise();
        }

        static void FixAnomalies()
        {
            foreach(Artist art in artistList)
            {
                if(art.ReturnAlbums().Count() == 0)
                {
                    artistList.Remove(art);
                }
            }
        }
    }
}
