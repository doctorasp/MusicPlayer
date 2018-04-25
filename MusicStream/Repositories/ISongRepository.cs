using MusicStream.Models;
using System.Collections.Generic;

namespace MusicStream.Repositories
{
    interface ISongRepository
    {
        Song Get(int id);
        List<Song> GetSongs();
        void Delete(int id);
        Song Add(Song song);
    }
}
