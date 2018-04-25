using System.Collections.Generic;

namespace MusicStream.Models
{
    public class Playlist
    {
        public Playlist()
        {
            Songs = new List<Song>();
        }
        public int PlaylistID { get; set; }
        public string PlaylistName { get; set; }
        public byte[] PlaylistCover { get; set; }
        public string CoverType { get; set; }
        public List<Song> Songs { get; set; }
    }
}
