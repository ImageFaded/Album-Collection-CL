using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_Organise___CL
{
    class Artist
    {
        public string ArtistName;
        public List<Album> albums;

        public Artist(string ArtistName, List<Album> albums)
        {
            this.ArtistName = ArtistName;
            this.albums = albums;
        }

        public void SortAlbums()
        {
            albums.Sort(delegate (Album a1, Album a2) { return a1.AlbumName.CompareTo(a2.AlbumName); });
        }       

        public void AddArtist(Album albumEntry)
        {
            albums.Add(albumEntry);
        }

        public void RemoveAlbum(Album alb)
        {
            albums.Remove(alb);
        }

        public List<Album> ReturnAlbums()
        {
            SortAlbums();
            return albums;
        }
    }
}
