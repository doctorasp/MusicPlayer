namespace MusicStream.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string SongName { get; set; }
        public string FilePath { get; set; }
        public int? PlayListId { get; set; }
        //public virtual Playlist Playlist { get; set; }
    }
}
