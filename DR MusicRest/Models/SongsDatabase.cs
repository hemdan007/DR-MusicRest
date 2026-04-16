using Microsoft.EntityFrameworkCore;

namespace DR_MusicRest.Models
{
    public class SongsDatabase : ISongsRepo
    {
        private readonly SongsDbContext _songsRepoList;
        public SongsDatabase(SongsDbContext songsDbContext)
        {
            _songsRepoList = songsDbContext;
        }

        public IEnumerable<Song> GetAll(string? search = null)
        {
            return _songsRepoList.Songs.ToList();
        }

        public Song Add(Song song)
        {
            var addedSong = _songsRepoList.Songs.Add(song).Entity;
            _songsRepoList.SaveChanges();
            return addedSong;
        }
    }
}
