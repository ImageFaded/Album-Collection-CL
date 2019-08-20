using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_Organise___CL
{
    /*
     * Artist - Musician/Band
     */
    class Artist
    {
        //Initialise artist name variable and create list to contain an artists albums
        public string ArtistName;
        public List<Album> albums;

        //On creation, set input variables to object variables
        public Artist(string ArtistName, List<Album> albums)
        {
            this.ArtistName = ArtistName;
            this.albums = albums;
        }

        //Sorts albums into an alphabetical order
        public void SortAlbums()
        {
            albums.Sort(delegate (Album a1, Album a2) { return a1.AlbumName.CompareTo(a2.AlbumName); });
        }       

        //Adds new album to list of albums
        public void AddAlbum(Album albumEntry)
        {
            albums.Add(albumEntry);
        }

        //Removes album from musician/bands discography
        public void RemoveAlbum(Album alb)
        {
            albums.Remove(alb);
        }

        //Returns sorted list of albums when called
        public List<Album> ReturnAlbums()
        {
            SortAlbums();
            return albums;
        }
    }
}
