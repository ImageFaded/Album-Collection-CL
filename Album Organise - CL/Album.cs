using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_Organise___CL
{
    class Album
    {
        public string ArtistName;
        public string AlbumName;
        
        public Album(string ArtistInput, string AlbumInput)
        {
            ArtistName = ArtistInput;
            AlbumName = AlbumInput;
        }        
    }
}
