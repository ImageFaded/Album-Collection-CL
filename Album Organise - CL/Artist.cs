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
        List<Album> albums;

        public Artist(string ArtistName, List<Album> albums)
        {
            this.ArtistName = ArtistName;
            this.albums = albums.ToList();
        }

        public void AddArtist(Album albumEntry)
        {
            albums.Add(albumEntry);
        }
    }
}
