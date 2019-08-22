using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Album_Organise___CL
{
    //Helps keep track of album collection
    class AlbumOrganise
    {
        //List of artists which stores information such as name of band/artist as well as their albums
        static List<Artist> artistList = new List<Artist>();

        //Initialise program
        static void Main(string[] args)
        {
            LoadArtists();
            Initialise();
        }

        //Displays main menu and prompts input
        static void Initialise()
        {
            MenuDisplay();
            UserInput();
        }

        //Clears the display and shows users which options they have to input to the program
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

        //Takes in user input and calls appropriate function
        static void UserInput()
        {
            //Depending on which number is inputted to program, take user to appropriate function
            try
            {
                //Read user input and convert to integer
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
                        //If an out of range input is put in, prompt user to try again
                        Console.WriteLine("Input Not Recognised: Please Enter Again");
                        Initialise();
                        break;
                }
            }
            catch (Exception)
            {
                //If the user inputs incorrect formating such as a string, prompt user to try again
                Console.WriteLine("\nMalformed Input: Please Enter Again");
                Initialise();
            }
        }

        //Searches list for artist
        static void SearchArtist()
        {
            //Clear console and prompt user for the name of an Artist
            Console.Clear();
            Console.WriteLine("Type In The Name Of An Artist: ");
            string input = FormatString(Console.ReadLine());
            //For every artist in the artist list, if there is one that matches the input
            foreach(Artist art in artistList)
            {
                if(art.ArtistName == input)
                {
                    //Display information relating to Artist and album Count
                    DisplayInformation(art,null);
                    Console.WriteLine("\nAlbum Count: " + AlbumCount(art));
                }
            }
            //Takes user to Main Menu
            MenuPrompt();
            Initialise();
        }

        //Searches list for specific album
        static void SearchAlbum()
        {
            //Clear console and prompt user for the name of an Album
            Console.Clear();
            Console.WriteLine("Type In The Name Of An Album: ");
            string input = FormatString(Console.ReadLine());            
            //For every artist in the artlist list, if the input is not null
            foreach(Artist art in artistList)
            {
                if(art.ArtistName != null)
                {
                    //For each album an artist has
                    foreach (Album alb in art.ReturnAlbums())
                    {
                        //If the album matches the input
                        if (alb.AlbumName == input)
                        {
                            //Display information relating to the album
                            DisplayInformation(art, alb);
                        }
                    }
                }
            }
            //Takes user to Main Menu
            MenuPrompt();
            Initialise();
        }

        //Adds album to the artistList
        static void AddAlbum()
        {
            //Clears console and creates temporary variables
            Console.Clear();
            Artist artistStore = null;
            //Album albumType;
            bool inList = false;
            bool reduntantEntry = false;
            //Prompt user for input of an artist name and album name
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();
            string AlbumType = InputType();
            //If there is an artist already with the same name in the list
            foreach(Artist art in artistList)
            {
                if(art.ArtistName == ArtistName)
                {
                    //For each album by an artist
                    foreach(Album alb in art.ReturnAlbums())
                    {
                        //If there is already an album with the same name set as a redundant entry
                        if(alb.AlbumName == AlbumName)
                        {
                            reduntantEntry = true;
                        }
                    }
                    if (!reduntantEntry)
                    {
                        //Set artist to temporary variable and escape loop
                        artistStore = art;
                        inList = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(ArtistName + " - "  + AlbumName + " is already present in list\n");
                        MenuPrompt();
                    }

                }
            }
            //If already in the list
            if (inList)
            {
                switch (AlbumType)
                {
                    case "Cd":
                    case "Full Length":
                    case "Full-Length":
                        artistStore.AddAlbum(new CD(AlbumName));
                        break;
                    case "Ep":
                        artistStore.AddAlbum(new EP(AlbumName));
                        break;
                    case "Vinyl":
                        artistStore.AddAlbum(new Vinyl(AlbumName));
                        break;
                    case "Digital":
                        artistStore.AddAlbum(new Digital(AlbumName));
                        break;
                    case "Cassette":
                        artistStore.AddAlbum(new Cassette(AlbumName));
                        break;
                }
            }
            else
            {
                switch (AlbumType)
                {
                    case "Cd":
                    case "Full Length":
                    case "Full-Length":
                        artistList.Add(new Artist(ArtistName, new List<Album> { new CD(AlbumName) }));
                        break;
                    case "Ep":
                        artistList.Add(new Artist(ArtistName, new List<Album> { new EP(AlbumName) }));
                        break;
                    case "Vinyl":
                        artistList.Add(new Artist(ArtistName, new List<Album> { new Vinyl(AlbumName) }));
                        break;
                    case "Digital":
                        artistList.Add(new Artist(ArtistName, new List<Album> { new Digital(AlbumName) }));
                        break;
                    case "Cassette":
                        artistList.Add(new Artist(ArtistName, new List<Album> { new Cassette(AlbumName) }));
                        break;
                }
            }
            //Sorts artist list where needed and saves information to JSON, taking user to menu again
            SortArtists();
            SaveJSON();
            Initialise();
        }
        
        //Prompts user to input the name of an artist
        static string InputArtist()
        {
            //Clears console and prompts for name of an artist
            Console.Clear();
            string inputArtist;
            Console.WriteLine("Type In The Name Of The Artist: ");
            inputArtist = FormatString(Console.ReadLine());
            //While the input is blank or null
            while(inputArtist == "" || inputArtist == null)
            {
                //Reprompt user to input information correctly
                Console.Clear();
                Console.WriteLine("Type In The Name Of The Artist: \n");
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
                inputArtist = Console.ReadLine();
            }
            //Return name of artist
            return inputArtist;         
        }

        //Prompts user to input the name of an album
        static string InputAlbum()
        {
            //Clears console and prompts for name of an album
            Console.Clear();
            string inputAlbum;
            Console.WriteLine("Type In The Name Of The Album: ");
            inputAlbum = FormatString(Console.ReadLine());
            //While input is blank or null
            while(inputAlbum == "" || inputAlbum == null)
            {
                //Reprompt user to input information correctly
                Console.Clear();
                Console.WriteLine("Type In The Name Of The Album: \n");
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
                inputAlbum = Console.ReadLine();
            }
            //Return name of album
            return inputAlbum;
        }

        //Prompts user to input the type of album
        static string InputType()
        {
            //Clears console and prompts for type of album
            Console.Clear();
            string inputType;
            Console.WriteLine("Type In The Type Of Album: ");
            inputType = FormatString(Console.ReadLine().ToLower());
            //While input is blank or null
            while (inputType == "" || inputType == null || WordCheck(FormatString(inputType)))
            {
                //Reprompt user to input information correctly
                Console.Clear();
                Console.WriteLine("Type In The Type Of Album: \n");
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
                Console.WriteLine(inputType);
                inputType = Console.ReadLine();
            }
            //Return name of album
            return inputType;
        }

        //Removes album from list
        static void DeleteAlbum()
        {
            //Clear console and create temporary variables
            Console.Clear();
            Artist tempArt = null;
            Album tempAlb = null;
            bool AlbumToRemove = false;
            //Prompt user for input of an artist name and album name
            string ArtistName = InputArtist();
            string AlbumName = InputAlbum();
            //For every artist in the list that isn't null
            foreach(Artist art in artistList)
            {
                if(art.ArtistName != null)
                {
                    //For every album in the artists list
                    foreach (Album alb in art.ReturnAlbums())
                    {
                        //If the artist name and album name match the input
                        if (art.ArtistName == ArtistName && alb.AlbumName == AlbumName)
                        {
                            //Set information to temporary variables
                            tempArt = art;
                            tempAlb = alb;
                            AlbumToRemove = true;
                        }
                    }
                }
            }

            //If there is no album to remove
            if (!AlbumToRemove)
            {
                Console.WriteLine("\nNot Found!");
            } 
            //If there is an album to remove
            else
            {
                //Call function inside artist to remove album
                tempArt.RemoveAlbum(tempAlb);
                //If the artist has no more albums
                if(AlbumCount(tempArt) == 0)
                {
                    //Remove the artist from the list
                    artistList.Remove(tempArt);
                }
                Console.WriteLine("\nRemoved!");
            }
            //Save information to JSON and take user back to menu
            SaveJSON();
            Console.ReadKey();
            Initialise();

        }

        //Exits program
        static void ExitProgram(bool InputError)
        {
            //Clears console and prompts user if they would like to exit
            Console.Clear();
            Console.WriteLine("Are You Sure You Would Like To Exit?");
            Console.WriteLine("(Yes/No)");
            //If an input error is detected
            if (InputError)
            {
                //Tell user previous input was erroneous and they should try again
                Console.WriteLine("Previous Input Erroneous: Please Enter Again");
            }
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                //If Yes
                case "YES":
                case "Yes":
                case "yes":
                case "Y":
                case "y":
                    //Save list to JSON and exit the program
                    SaveJSON();
                    Environment.Exit(0);
                    break;
                //If No
                case "NO":
                case "No":
                case "no":
                case "N":
                case "n":
                    //Return to Main Menu
                    Initialise();
                    break;
                default:
                    //Any Other Input shall throw an error and recall the function
                    ExitProgram(true);
                    break;
            }

        }

        //Saves information to JSON file
        static void SaveJSON()
        {
            //Serialises JSON and adds to a string, then writes this to the JSON
            string jsonSave = JsonConvert.SerializeObject(artistList);
            File.WriteAllText(ReturnLocation(), jsonSave);
        }

        //Loads artists into program
        static void LoadArtists()
        {
            //If no file exists with the filename expected
            if (!File.Exists(ReturnLocation()))
            {
                //Create a file in the location and add in information to prevent file from crashing when being added to
                File.Create(ReturnLocation());
                string jsonSave = JsonConvert.SerializeObject(artistList);
                File.WriteAllText(ReturnLocation(), jsonSave);
            }
            //Sets all text in the JSON file to a string and deserialises this, setting to the ArtistList Object, then sorted
            string jsonLoad = File.ReadAllText(ReturnLocation());
            artistList = JsonConvert.DeserializeObject<List<Artist>>(jsonLoad);
            SortArtists();
        }

        //Returns location of JSON file
        static string ReturnLocation()
        {
            return @"Artists.Json";
        }

        //Displays all Artists within the ArtistList
        static void DisplayAllArtists()
        {
            //Clear the console
            Console.Clear();
            int CDCount = 0;
            int EPCount = 0;
            int VinylCount = 0;
            int DigitalCount = 0;
            int CassetteCount = 0;
            //For every artist that isnt null in artist list
            foreach(Artist art in artistList)
            {
                if (art.ArtistName != null)
                {
                    //Write the Artist Name to the console
                    Console.WriteLine("Artist: " + art.ArtistName);
                    //For every album in the album list of each artist
                    foreach(Album album in art.ReturnAlbums())
                    {
                        //Write all the albums to the console
                        //NOTE - Space at start of album is to make the Artist/Album Text Distinct
                        Console.WriteLine(" Album: " + album.AlbumName + " Format: " + album.AlbumFormat);

                        switch (album.AlbumFormat)
                        {
                            case "CD":
                                CDCount++;
                                break;
                            case "EP":
                                EPCount++;
                                break;
                            case "Vinyl":
                                VinylCount++;
                                break;
                            case "Digital":
                                DigitalCount++;
                                break;
                            case "Cassette":
                                CassetteCount++;
                                break;
                        }
                    }
                    Console.Write("\n");
                }
                
            }
            Console.WriteLine("CD Count: {0}\nEP Count:{1}\nVinyl Count:{2}\nDigital Count:{3}\nCassette Count:{4}",CDCount,EPCount,VinylCount,DigitalCount,CassetteCount);
            
            //Return To Main Menu
            MenuPrompt();
            Initialise();
        }

        //Sorts Artist lists alphabetically
        static void SortArtists()
        {
            //If the Artist List count is 5 or more
            if(artistList.Count >= 5)
            {
                //Sort artist list alphabetically
                artistList.Sort(delegate (Artist a1, Artist a2) { return a1.ArtistName.CompareTo(a2.ArtistName); });
            }
        }

        //Displays information relating to Artists/Albums
        static void DisplayInformation(Artist art, Album album)
        {
            //Write the name of the artist
            Console.WriteLine("\nArtist: " + art.ArtistName);
            //If no specific album is needed to be inputted
            if(album == null)
            {
                //Display all albums by an artist
                foreach (Album alb in art.ReturnAlbums())
                {
                    Console.WriteLine(" Album: " + alb.AlbumName);
                }
            }
            else
            {
                //Write the name of the specifc album inputted to the console
                Console.WriteLine(" Album: " + album.AlbumName);
            }

        }

        //Prompts user to return to main menu
        static void MenuPrompt()
        {
            //Prompt user to click a key to return to main menu
            Console.WriteLine("\nClick Any Key To Return To Menu");
            Console.ReadKey();
            Initialise();
        }

        //Return the Album Count of the Artist inputted
        static int AlbumCount(Artist art)
        {
            //Return amount of albums Artist has made
            return art.ReturnAlbums().Count();
        }

        //Sets input string to be in Title Case for storage and searching
        static string FormatString(string InputString)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(InputString);
        }

        //Checks if Album Type is valid
        static bool WordCheck(string input)
        {
            switch (input)
            {
                case "Cd":
                case "Ep":
                case "Full-Length":
                case "Full Length":
                case "Vinyl":
                case "Cassette":
                case "Digital":
                    return false;
                default:
                    return true;              
            }
        }
    }
}
