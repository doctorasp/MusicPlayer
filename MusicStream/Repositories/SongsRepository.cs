using Dapper;
using MusicStream.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MusicStream.Repositories
{
    public class SongsRepository : ISongRepository
    {
        string connectionString { get; set; }
        public SongsRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public Song Add(Song song)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Songs (FilePath, SongName) VALUES(@FilePath, @SongName); SELECT CAST(SCOPE_IDENTITY() as int)";
                int songId = db.Query<int>(sqlQuery, song).FirstOrDefault();
                song.ID = songId;
            }
            return song;
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Songs WHERE ID = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public Song Get(int id)
        {
            Song song = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                song = db.Query<Song>("SELECT * FROM Songs WHERE ID = @id", new { id }).FirstOrDefault();
            }
            return song;
        }

        public List<Song> GetSongs()
        {
            List<Song> songs = new List<Song>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                songs = db.Query<Song>("SELECT * FROM Songs").ToList();
            }
            return songs;
        }
    }
}
