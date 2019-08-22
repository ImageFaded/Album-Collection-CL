namespace Album_Organise___CL
{
    //Creates album with variable name
    class Album
    {
        //Variable for album name
        public string AlbumName;
        public string AlbumFormat;

        //On creation, set input variables to object variables
        public Album(string AlbumName)
        {
            this.AlbumName = AlbumName;
            AlbumFormat = null;
        }
    }

    class EP : Album
    {
        public EP(string AlbumName) : base(AlbumName)
        {
            this.AlbumName = AlbumName;
            AlbumFormat = "Ep";
        }
    }

    class CD : Album
    {
        public CD(string AlbumName) : base(AlbumName)
        {
            this.AlbumName = AlbumName;
            AlbumFormat = "CD";
        }
    }

    class Vinyl : Album
    {
        public Vinyl(string AlbumName) : base(AlbumName)
        {
            this.AlbumName = AlbumName;
            AlbumFormat = "Vinyl";
        }
    }

    class Digital : Album
    {
        public Digital(string AlbumName) : base(AlbumName)
        {
            this.AlbumName = AlbumName;
            AlbumFormat = "Digital";
        }
    }

    class Cassette : Album
    {
        public Cassette(string AlbumName) : base(AlbumName)
        {
            this.AlbumName = AlbumName;
            AlbumFormat = "Cassette";
        }
    }

}
