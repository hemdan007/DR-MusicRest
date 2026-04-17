using Microsoft.EntityFrameworkCore;

namespace DR_MusicRest.Models
{
    public class SongsDatabase : ISongsRepo
    {
        private readonly SongsDbContext _context;
        public SongsDatabase(SongsDbContext songsDbContext)
        {
            _context = songsDbContext;
        }

        public IEnumerable<Song> GetAll(string? search = null)
        {
            return _context.Songs.ToList();
        }


        public Song? GetSongById(int id)
        {
            return _context.Songs.Find(id);
        }


        public Song Add(Song song)
        {
            var addedSong = _context.Songs.Add(song).Entity;
            _context.SaveChanges();
            return addedSong;
        }


        public Song? UpdateSong(int id, Song updatedSong)
        {
            var existingSong = GetSongById(id);
            if (existingSong != null)
            {
                existingSong.Title = updatedSong.Title;
                existingSong.Artist = updatedSong.Artist;
                existingSong.Duration = updatedSong.Duration;
                existingSong.PublicationYear = updatedSong.PublicationYear;
                _context.SaveChanges();
                return existingSong;
            }
            return null;
        }


    }
}
